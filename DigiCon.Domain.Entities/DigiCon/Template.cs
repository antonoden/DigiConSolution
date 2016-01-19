using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiCon.Domain.Entities
{
    public class Template
    {
        
        public int TemplateID { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string Path { get; set; }

        public virtual ICollection<Slide> Slides { get; set; }
    }
}
