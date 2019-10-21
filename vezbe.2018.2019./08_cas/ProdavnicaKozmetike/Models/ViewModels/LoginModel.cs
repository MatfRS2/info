using System.ComponentModel.DataAnnotations;

namespace ProdavnicaKozmetike.Models.ViewModels{

    public class LoginModel {

        [Required]
        public string Ime { get; set; }

        [Required]
        [UIHint("sifra")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}