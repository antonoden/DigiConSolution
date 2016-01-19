using DigiCon.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DigiCon.Web.UI.Models
{
    public class ViewModel_ListPlaylist
    {
        [Display(Name = "Namn")]
        public string Name { get; set; }

        public int ID { get; set; }

        [Display(Name = "Spelordning")]
        public int Playorder { get; set; }

        public IEnumerable<Slide> Slides { get; set; }
    }

    public class ViewModel_AddSlideToPlaylist
    {
        public int playlistID { get; set; }
        
        public List<Slide> Slides { get; set; }

        public List<bool> isSelected { get; set; }
    }

    public class ViewModel_EditPlaylist
    {
        public ViewModel_EditPlaylist()
        {
            Slides = new List<SlideView>();
        }
        public int playlistID { get; set; }

        public string playlistName { get; set; }

        public List<SlideView> Slides { get; set; }
    }


    public class ViewModel_AddYoutubeToPlaylist
    {
        public int playlistId { get; set; }

        public string url { get; set; }

        public string name { get; set; }
    }

    public class ViewModel_UploadSlideToPlaylist
    {
        public ViewModel_UploadSlideToPlaylist()
        {
            statusList = new List<UploadPostInfo>();
        }

        public int playlistId { get; set; }

        public List<UploadPostInfo> statusList { get; set; }
    }

    public class PlaylistView
    {
        public Playlist playlist { get; set; }

        public int Playorder { get; set; }

    }

    public class UploadPostInfo
    {
        public string filename { get; set; }

        public string imgsrc { get; set; }

        public string message { get; set; }
    }
}