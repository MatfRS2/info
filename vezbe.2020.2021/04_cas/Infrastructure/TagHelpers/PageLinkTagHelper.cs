using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Prodavnica2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Infrastructure.TagHelpers
{
    [HtmlTargetElement ("div", Attributes = "model-strane")]
    public class PageLinkTagHelper : TagHelper
    {
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
        public string PageClassNeoznaceno { get; set; }
        public string PageClassSelektovano { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper url = urlHelperFactory.GetUrlHelper(ViewContext);

            for(int i = 1; i<= ModelStrane.BrojStrana; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = url.Action(Akcija, new { Strana = i });

                tag.AddCssClass(PageClass);

                tag.AddCssClass(i == ModelStrane.TrenutnaStrana ?
                    PageClassSelektovano : PageClassNeoznaceno);

                tag.InnerHtml.Append(i.ToString());
                output.Content.AppendHtml(tag);
            }
        }
    }
}
