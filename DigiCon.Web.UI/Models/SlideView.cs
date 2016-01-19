using DigiCon.Domain.Entities;

namespace DigiCon.Web.UI.Models
{    
    public class SlideView
    {
        public Slide Slide { get; set; }

        public int Playorder { get; set; }

        public int Duration { get; set; }

        public string Transition { get; set; }
    }
}