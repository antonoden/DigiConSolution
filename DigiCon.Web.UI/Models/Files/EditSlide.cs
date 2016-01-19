using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigiCon.Web.UI.Models.Files
{
    public class EditSlide
    {
        [Display(Name = "Namn")]
        public string Name { get; set; }
        public string Path { get; set; }
    }
}