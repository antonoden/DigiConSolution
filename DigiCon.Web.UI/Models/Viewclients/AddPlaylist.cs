using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DigiCon.Web.UI.Models.Viewclients
{
    public class AddPlaylist
    {
        public List<Playlist> Playlists { get; set; }
        public List<Boolean> IsSelected { get; set; }
    }
}