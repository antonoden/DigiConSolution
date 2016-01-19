using System.Collections.Generic;

namespace DigiCon.Domain.Entities
{
    public class Animation
    {
        public int AnimationID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PlaylistSlide> PlaylistSlides { get; set; }
        
    }
}
