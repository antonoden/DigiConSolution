using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiCon.Domain.Entities
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ViewclientPlaylist> ViewClientPlaylists { get; set; }
        public virtual ICollection<PlaylistSlide> PlaylistSlides { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
