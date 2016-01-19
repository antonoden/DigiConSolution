using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCon.Domain.Entities
{
    public class ViewclientPlaylist
    {
        public int ViewclientID { get; set; }
        public int PlaylistID { get; set; }
        public DateTime Schedule { get; set; }
        public int Playorder { get; set; }
        
        public virtual Playlist Playlist { get; set; }
        public virtual Viewclient Viewclient { get; set; }
    }
}
