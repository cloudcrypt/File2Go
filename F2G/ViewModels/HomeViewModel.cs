using System.ComponentModel.DataAnnotations;

namespace F2G.ViewModels
{
    public class HomeViewModel
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "File Name")]
        public string filename { get; set; }

    }
}
