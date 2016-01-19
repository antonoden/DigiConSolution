using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiCon.Web.UI.Models
{
    public class ViewModel_Slide
    {
        public Slide Slide { get; set; }

        public int Duration { get; set; }

        public int Playorder { get; set; }

        public string Animation { get; set; }
    }
}