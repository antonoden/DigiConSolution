using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DigiCon.Web.UI.Controllers;

namespace DigiCon.Web.UI.Models.Files
{
    public class Upload
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
}
