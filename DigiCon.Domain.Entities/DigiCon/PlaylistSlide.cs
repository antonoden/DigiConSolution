using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiCon.Domain.Entities
{
    public class PlaylistSlide
    {
        public PlaylistSlide()
        {
            Duration = 5000;
        }
        
        public int PlaylistID { get; set; }
        
        public int SlideID { get; set; }
        
        
        public int AnimationID { get; set; }
        public int Duration { get; set; }
        public int Playorder { get; set; }
        
        public virtual Slide Slide { get; set; }
        public virtual Playlist Playlist { get; set; }
        public virtual Animation Animation { get; set; }
    }
}