using DigiCon.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DigiCon.Web.UI.Models
{
    public class ViewModel_ListSlide
    {
        public string Contenttype { get; set; }

        [Display(Name = "Namn")]
        public string Name { get; set; }

        public int ID { get; set; }

        public string Path { get; set; }

        [Display(Name = "Visningstid")]
        public int Duration { get; set; }

        [Display(Name = "Spelording")]
        public int Playorder { get; set; }

        [Display(Name = "Animation")]
        public string Animation { get; set; }
    }

    public class ViewModel_UploadAndAddSlide
    {
        public List<PostInfo> statusList { get; set; }

        public IEnumerable<HttpPostedFileBase> File { get; set; }

        public class PostInfo
        {
            public string filename { get; set; }

            public string imgsrc { get; set; }

            public string message { get; set; }
        }
    }

    public class ViewModel_UploadedSlides
    {
    }


    public class ViewModel_AddYoutubeSlide
    {
        public string url { get; set; }

        public string name { get; set; }
    }

    public class SlideView
    {
        public Slide Slide { get; set; }

        public int Playorder { get; set; }

        public int Duration { get; set; }

        public string Transition { get; set; }
    }
}