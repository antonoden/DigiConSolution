using DigiCon.Domain.Entities;
using System.Collections.Generic;

namespace DigiCon.Web.UI.Models
{
    public class ViewModel_Playlist
    {
        public Playlist Playlist { get; set; }

        public int Playorder { get; set; }
        
        public IEnumerable<Slide> Slides { get; set; }
    }
}