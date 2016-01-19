using System.ComponentModel.DataAnnotations;

namespace DigiCon.Web.UI.Models.Files
{
    public class AddYoutube
    {
        [Required]
        public string Url { get; set; }
    
        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; } 
    }
}