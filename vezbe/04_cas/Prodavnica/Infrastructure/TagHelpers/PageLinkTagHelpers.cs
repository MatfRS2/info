using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers; //potrebno za kreiranje TagHelpera
using Microsoft.AspNetCore.Mvc.Routing;      //potrebno zbog IUrlHelperFactory
using Microsoft.AspNetCore.Mvc.ViewFeatures; //potrebno zbog [ViewContext]
using Microsoft.AspNetCore.Mvc.Rendering;    //potrebno zbog ViewContext
using Prodavnica.Models.ViewModels;

/* Tag-Helperi se smestaju u folder Infrastucture/TagHelpers. */
namespace Prodavnica.Infrastructure.TagHelpers
{
    /* U [] se stavljaju ogranicenja za tag-helper kojim se zapravo kaze
     * na koje elemente html strane deluju.
     * U ovom primeru ovde smo rekli da tag-helper deluje na div element koji
     * ima polje(atribut) model-strane.
     * Postoji jos polja, recimo ParentTag kojim moze da se zada da se
     * primenjuje samo nad elementima koji su u okviru odredjenog html elementa itd.
     * 
     * Iako su sva polja opciona preporuka je da se delovanje tag-helpera ogranici
     * tako da bude veoma specificno gde se koristi.
     */
    [HtmlTargetElement("div", Attributes="model-strane")]
    public class PageLinkTagHelpers : TagHelper
    {
        /* IUrlHelperFactory je servis koji omogucava kreiranje izlaznog URL-a
         * na osnovu podataka koji dolaze iz jednog skupa.
         * Kreira se IUrlHelper objekat koji se konfigurise koriscenjem ViewContex
         * objekta. Sve to zajedno pomaze da se izracuna url -- odnosno
         * kada kliknemo na neki link da ispravno odluci na koju stranu da ode.
         * Odnosno, na osnovu tekuce strane (tekuceg url-a) i kliknute strane
         * on pravi ispravan url.
         */
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelpers(IUrlHelperFactory url)
        {
            urlHelperFactory = url;
        }

        /* Kreiramo polje ViewContext koje je tipa ViewContext.
         * Ukratko, ova klasa sadrzi podatke o Http Context, Http Response,
         * Http Requests.
         * 
         * Sa atributom [ViewContext] se oznacava da se prilikom kreiranja 
         * instance objekta PageLinkTagHelpers
         * nasem parametru ViewContext se dodele podaci koji postoje za trenutnu 
         * stranu, tj. vrednost ViewContext koji se odnosi na stranu gde se 
         * nalazi tag-helper.
         * 
         * Atribut [HtmlAttributeNotBound] je preporuka da se stavi.
         * Objasnjenje: The HtmlAttributeNotBound attribute prevents MVC from assigning 
         * a value to this property if there is a view-context attribute on the 
         * input HTML element. This is good practice, especially if you are writing tag 
         * helpers for other developers to use.
         */
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        /* Ova dva podatka se prosledjuju sa razor stranice.
         * Na razor stranici tag helper je dat sa: <div model-strane="@Model.ModelStrane" akcija="Spisak"></div>
         * Kako je to div koji ima polje model-strane on ga unifikuje sa ovim tag helperom.
         * Pri tome, parametar ModelStrane odgovara html elementu model-strane i njegova vrednost je @Model.ModelStrane
         * Slicno, parametar Akcija odgovara html elementu akcija (i vrednost mu je "Spisak")
         * Obratiti paznju na imena: parametar NestoImaDugackoIme ce da odgovara html elementu nesto-ima-dugacko-ime
         */
        public PodaciZaPrikazStrane ModelStrane { get; set; }
        public string Akcija { get; set; }

        /* Kada se pravi Tag-Helper vrsi se menjanje metoda Process.
         * Postoji i asihroni Process, ali je preporuka da se tag-helperi
         * ne pisu asihrono. 
         */
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            for(int i =1; i<= ModelStrane.BrojStrana; i++)
            {
                /* Kreira se novi tag sa imenom. Zelimo da kreiramo
                 * <a href="adresa">broj</a>
                 */
                 //kreiramo tag <a>
                TagBuilder tag = new TagBuilder("a");

                //u tag dodajemo href
                /* Za metod Action koristi se sintaksa Action(String, Object).
                 * Generise se URL na osnovu zadatog imena akcije i vrednosti za tu
                 * rutu.
                 */
                tag.Attributes["href"] = urlHelper.Action(Akcija, new { tekucaStrana = i });

                //dodaje se deo u unutrasnjosti taga
                tag.InnerHtml.Append(i.ToString());

                /* U okviru div elementa dodaje se napravljeni tag, odnosno
                 * <a href="adresa">i</a>
                 */
                output.Content.AppendHtml(tag);
            }
        }
    }
}
