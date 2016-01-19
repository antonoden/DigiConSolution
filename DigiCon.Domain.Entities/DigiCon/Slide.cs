using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiCon.Domain.Entities
{
    public class Slide
    {
        public int SlideID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public int? TemplateID { get; set; }
        
        // foreign tables
        public virtual ICollection<PlaylistSlide> SlidePlaylists { get; set; }
        public virtual Template Template { get; set; }
    }
}
