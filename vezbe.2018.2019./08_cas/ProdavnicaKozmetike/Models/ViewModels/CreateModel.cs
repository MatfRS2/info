using System.ComponentModel.DataAnnotations;

namespace ProdavnicaKozmetike.Models.ViewModels{
    public class CreateModel{

        [Required]
        public string Ime { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Sifra { get; set; }
        public string ReturnUrl { get; set; } = "/";

    }

}