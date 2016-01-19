using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DigiCon.Web.UI.Models.Playlists
{
    public class EditPlaylist
    {
        public EditPlaylist()
        {
            Slides = new List<SlideView>();
        }
        public Playlist Playlist { get; set; }
        public List<SlideView> Slides { get; set; }
        public IEnumerable<SelectListItem> PlayOrder { get; set; }
    }
    
}