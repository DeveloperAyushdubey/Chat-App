using System.ComponentModel.DataAnnotations;

namespace projectApi.Models
{
    public class UserPorfileModel
    {
        public long? sno { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        public string? MobileNo { get; set; }

        public string? About { get; set; }
        public string? StatusLine { get; set; }
        public string? Image { get; set; }

        [Required(ErrorMessage = "Flag is required")]
        public string? Flag { get; set; }

        public IFormFile? file { get; set; } // Optional file
    }

}
