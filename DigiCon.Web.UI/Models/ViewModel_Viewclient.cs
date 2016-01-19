using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiCon.Web.UI.Models
{
    public class ViewModel_Viewclient
    {
        public Viewclient Viewclient { get; set; }
        public IEnumerable<Playlist> Playlists { get; set; }
    }
}