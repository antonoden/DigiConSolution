using DigiCon.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigiCon.Web.UI.Models.Viewclients
{
    public class EditViewclient
    {
        public EditViewclient()
        {
            Playlists = new List<ViewModel_ListPlaylist>();
        }
        [Display(Name = "Namn")]
        public string Name { get; set; }
        public int ViewclientID { get; set; }
        public Viewclient Viewclient { get; set; }
        public List<ViewModel_ListPlaylist> Playlists { get; set; }
    }
}