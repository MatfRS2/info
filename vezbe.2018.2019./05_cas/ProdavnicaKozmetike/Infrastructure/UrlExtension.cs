using Microsoft.AspNetCore.Http;

namespace ProdavnicaKozmetike.Infrastructure{

    public static class UrlExtension{

        public static string PathAndQuery(this HttpRequest request)
        {
            return request.QueryString.HasValue ? 
                      $"{request.Path}{request.QueryString}" : request.Path.ToString();
             
        }

    }


}