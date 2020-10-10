using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Prodavnica.Models;
using Prodavnica.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Prodavnica.Controllers
{
    /* Authorize uzima podatke iz HttpContext objekta 
     * (preciznije HttpContext.User) i proverava da li korisnik ima prava 
     * pristupa.
     */
    [Authorize (Roles = "Administrator")]
    public class AccountController : Controller
    {

        private UserManager<MojKorisnik> userManager;
        private SignInManager<MojKorisnik> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<MojKorisnik> usrManager,
            SignInManager<MojKorisnik> sgnInManager,
            RoleManager<IdentityRole> rlManager)
        {
            userManager = usrManager;
            signInManager = sgnInManager;
            roleManager = rlManager;
        }

        public ViewResult Korisnici() =>
            View(userManager.Users);

        [HttpGet]
        public ViewResult KreirajKorisnika() => View();

        [HttpPost]
        public async Task<IActionResult> KreirajKorisnika(KreirajKorisnikaModel korisnik)
        {
            if (ModelState.IsValid)
            {
                MojKorisnik mojKorisnik = new MojKorisnik
                {
                    UserName = korisnik.Ime,
                    Email = korisnik.Email
                };

                IdentityResult result = await userManager.CreateAsync(mojKorisnik, korisnik.Sifra);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(mojKorisnik, "ObicanKorisnik");
                    return RedirectToAction("Korisnici");
                }
                else
                {
                    foreach (IdentityError e in result.Errors)
                        ModelState.AddModelError("", e.Description);
                }
            }

            return View(korisnik);
        }

        [HttpPost]
        public async Task<IActionResult> Obrisi(string id)
        {
            MojKorisnik mojKorisnik = await userManager.FindByIdAsync(id);

            if (mojKorisnik != null)
            {
                IdentityResult rezultat = await userManager.DeleteAsync(mojKorisnik);

                if (rezultat.Succeeded)
                    return RedirectToAction("Korisnici");
                else
                {
                    foreach (IdentityError e in rezultat.Errors)
                        ModelState.AddModelError("", e.Description);
                }
                    
            }
            else
                ModelState.AddModelError("", "Ne postoji korisnik");

            return View("Korisnici");
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Prijavljivanje() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Prijavljivanje(PrijavljivanjeModel korisnik)
        {
            if (ModelState.IsValid)
            {
                MojKorisnik mojKorisnik =
                    await userManager.FindByEmailAsync(korisnik.Email);

                if (mojKorisnik != null)
                {
                    /* Ukidaju se sve postojece sesije za korisnika.
                     */
                    await signInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult rezultat = await signInManager
                        .PasswordSignInAsync(mojKorisnik, korisnik.Sifra, false, false);

                    /* Prilikom prijave ne bi bilo lose proslediti url 
                     * na koji je hteo korisnik da ode.
                     * Za sada to preskacemo.
                     */
                    if (rezultat.Succeeded)
                        return Redirect("/Admin/SpisakProizvoda");
                }

                ModelState.AddModelError("", "Neispravan email ili sifra");
            }

            return View();
        }

        public async Task<RedirectToActionResult> Odjava()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Spisak", "Proizvod");
        }
    }
}