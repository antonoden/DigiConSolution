using DigiCon.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigiCon.Web.UI.Models
{
    public class ViewModel_ListViewclient
    {
        [Display(Name = "Namn")]
        public string Name { get; set; }

        public int ID { get; set; }

        public IEnumerable<Playlist> Playlists { get; set; }
    }

    public class ViewModel_AddPlaylistToViewclient
    {
        public int viewclientID { get; set; }

        public List<Playlist> playlists { get; set; }

        public List<bool> isSelected { get; set; }
    }

    public class ViewModel_AddCreatePlaylistToViewclient
    {
        public int viewclientId { get; set; }

        public string playlistName { get; set; }
    }

    public class ViewModel_EditPlaylistsInViewclient
    {
        public ViewModel_EditPlaylistsInViewclient()
        {
            playlists = new List<PlaylistView>();
        }
        public int viewclientID { get; set; }

        public string viewclientName { get; set; }

        public List<PlaylistView> playlists { get; set; }
    }


}