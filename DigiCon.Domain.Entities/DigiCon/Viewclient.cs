
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiCon.Domain.Entities
{
    public class Viewclient
    {
        public int ViewclientID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ViewclientPlaylist> ViewclientPlaylists { get; set; }
    }
}
