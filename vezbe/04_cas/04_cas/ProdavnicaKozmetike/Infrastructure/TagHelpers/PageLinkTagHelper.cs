using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using ProdavnicaKozmetike.Models.ViewModels;

/* Pravi se novi TagHelper -- o svemu ovome bice vise reci kasnije */

namespace ProdavnicaKozmetike.Infrastructure{

    /* U [] se stavlja koji elementi ce biti pogodjeni dejstvom ovog tag-helpera. 
       U ovom primeru, to su svi "div" elementi koji imaju atribut "model-strane"
    */

    [HtmlTargetElement ("div", Attributes="model-strane")]
    public class PageLinkTagHelper : TagHelper{

        /* IUrlHelperFactory je servis koji omogucava kreiranje izlaznog URL-a (web adrese) na osnovu
        podataka (koji dolaze iz jednog skupa -- u ovom primeru na osnovu liste proizvoda se pravi vise strana sa tim proizvodima).
        Ipak da bi ovo radilo potrebno je koristiti ViewContext objekat.
        Kreira se IUrlHelper objekat koji se konfigurise koriscenjem ViewContext objekta, a onda sve to sluzi za izracunavanje url.
        */
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory url)
        {
            urlHelperFactory = url;
        }

        /* Atributom ViewContext se oznacava da ovaj parametar treba da dobije vrednost ViewContex objekta 
           kada se kreira nova instanca objekta PageLinkTagHelper. */
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public PageInfo ModelStrane { get; set; }

        public string Akcija { get; set; }

        //30. naredna tri reda se dodaju zbog stilizovanja
        public string PageClass { get; set; }
        public string PageClassNormalno { get; set; }
        public string PageClassSelektovano { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            for(int i=1; i<= ModelStrane.BrojStrana; i++)
            {
                TagBuilder tag = new TagBuilder("a"); /* Kreira se novi tag sa zadatim imenom. */
                /* Za metod Action koristi se sintaksa Action(String, Object).
                   Generise URL na osnovu zadatog imena akcije i vrednosti za tu rutu. */
                tag.Attributes["href"] = urlHelper.Action(Akcija, new {brojStraneProizvoda = i});

                /* 30. Stlizovanje, naredna 2 reda */
                tag.AddCssClass(PageClass);
                tag.AddCssClass(i == ModelStrane.TrenutnaStrana ? PageClassSelektovano : PageClassNormalno);

                /* Kada se napravi tag treba da stoji nesto ovako: <a href="">broj_strane</a>. 
                   Sa ovim  .InnerHtml.Append se upravo dodaje taj boj strane. */
                tag.InnerHtml.Append(i.ToString());

                /* U okviru div elementa dodaje se napravljeni tag, odnosno <a href="">broj_strane</a>. */
                output.Content.AppendHtml(tag);
            }
        }


    }


}