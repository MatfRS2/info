using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProdavnicaKozmetike.Models.ViewModels;

namespace ProdavnicaKozmetike.Infrastructure{

    [HtmlTargetElement ("div", Attributes="model-strane")]
    public class PageLinkTagHelper : TagHelper{

        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory url)
        {
            urlHelperFactory = url;
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public PageInfo ModelStrane { get; set; }

        public string Akcija { get; set; }

        public string PageClass { get; set; }
        public string PageClassNormalno { get; set; }
        public string PageClassSelektovano { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            for(int i=1; i<= ModelStrane.BrojStrana; i++)
            {
                TagBuilder tag = new TagBuilder("a"); 
                tag.Attributes["href"] = urlHelper.Action(Akcija, new {brojStraneProizvoda = i});

                 tag.AddCssClass(PageClass);
                tag.AddCssClass(i == ModelStrane.TrenutnaStrana ? PageClassSelektovano : PageClassNormalno);

                tag.InnerHtml.Append(i.ToString());

                output.Content.AppendHtml(tag);
            }
        }


    }


}