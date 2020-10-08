using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Prodavnica.Infrastructure
{
    public static class UrlExtension
    {
        public static string PutanjaIUpit(this HttpRequest request)
        {
            return request.QueryString.HasValue ?
                $"{request.Path}{request.QueryString}" :
                request.Path.ToString();
        }
    }
}
