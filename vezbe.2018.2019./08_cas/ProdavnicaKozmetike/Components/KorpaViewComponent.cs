using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models;
using ProdavnicaKozmetike.Infrastructure;

namespace  ProdavnicaKozmetike.Components
{
    public class KorpaViewComponent : ViewComponent
    {
        private Korpa GetKorpa(){
            Korpa korpa = HttpContext.Session.GetJson<Korpa>("Korpa") ?? new Korpa();
            return korpa;
        }
    

    public IViewComponentResult Invoke()
    {
        Korpa korpa = GetKorpa();
        return View(korpa);
    }


    }    
}