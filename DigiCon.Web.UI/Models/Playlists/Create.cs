using System.ComponentModel.DataAnnotations;

namespace DigiCon.Web.UI.Models.Playlists
{
    public class Create
    {
        [Required(ErrorMessage = "Inget namn valdes")]
        [Display(Name = "Namn")]
        public string Name { get; set; }
    }
}