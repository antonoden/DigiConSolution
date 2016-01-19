using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DigiCon.Web.UI.Models.Playlists
{
    public class AddSlide
    {
        public List<Slide> Slides { get; set; }
        public List<Boolean> IsSelected { get; set; }
    }
}