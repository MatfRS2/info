using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodavnica2.Infrastructure
{
    public static class UrlExtension
    {
        public static string UzmiUrl(this HttpRequest request)
        {
            return request.QueryString.HasValue ?
                $"{request.Path}{request.QueryString}" :
                request.Path.ToString();
        }
    }
}

