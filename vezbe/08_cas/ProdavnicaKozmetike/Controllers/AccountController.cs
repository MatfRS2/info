using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProdavnicaKozmetike.Models.ViewModels;
using ProdavnicaKozmetike.Models;


namespace ProdavnicaKozmetike.Controllers {

    [Authorize (Roles = "Admin")]
    public class AccountController : Controller {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private RoleManager<IdentityRole> roleManager;


        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr,
                                 RoleManager<IdentityRole> roleMgr) 
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMgr;
        }

        public ViewResult KreirajKorisnika() => View();

        [HttpPost]
        public async Task<IActionResult> KreirajKorisnika(CreateModel korisnik)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser{
                    UserName = korisnik.Ime,
                    Email = korisnik.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, korisnik.Sifra);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                    return RedirectToAction("Login", new { korisnik.ReturnUrl });
                }
                else
                {
                    foreach (IdentityError e in result.Errors)
                        ModelState.AddModelError("", e.Description);
                }

            }
            return View(korisnik);
        }


        [AllowAnonymous]
        public ViewResult Login(string returnUrl) 
        {
            return View(new LoginModel {
                ReturnUrl = returnUrl });
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel) 
        {
            if (ModelState.IsValid) 
            {
                AppUser user =
                    await userManager.FindByNameAsync(loginModel.Ime);
                if (user != null) 
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync
                            (user, loginModel.Password, false, false)).Succeeded) 
                                return Redirect(loginModel?.ReturnUrl ?? "/Admin/SpisakProizvoda");
                }
            }

            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        /* Ni ovo  nije idealno, u ozbiljnijim aplikacijama ... */
        [AllowAnonymous]
        public async Task<RedirectResult> Logout(string returnUrl = "/") 
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}