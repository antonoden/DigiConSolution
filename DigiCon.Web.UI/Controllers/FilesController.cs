using DigiCon.Domain.Entities;
using DigiCon.Service.Repositories;
using DigiCon.Web.UI.Models.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace DigiCon.Web.UI.Controllers
{
    public class FilesController : Controller
    {
        private ISlideRepository slideService;
        private ITemplateRepository templateService;

        public FilesController()
        {
            slideService = new SlideRepository();
            templateService = new TemplateRepository();
        }

        string path;
        static string uploadpath = "/Content/Uploaded_Files/";
        static string pathtemplate = uploadpath + "Templates/";
        static string pathslide = uploadpath + "Slides/";

        [HttpGet]
        // GET: Upload
        public ActionResult Upload()
        {
            return View(new Upload());
        }
        
        [HttpPost]
        public ActionResult Upload(Upload model)
        {
            if (model.File != null)
            {

                foreach (HttpPostedFileBase file in model.File)
                {
                    string postmsg = null;
                    string postimg = null;
                    string postname = null;
                    model.statusList = new List<Upload.PostInfo>();               
                    try
                    {
                        switch (file.ContentType)
                        {
                            
                            case "application/odp":
                            case "application/vnd.oasis.opendocument.presentation":
                            case "application/ppt":
                                //  case "application/octet-stream":
                                setPath(file, pathslide);
                                templateService.InsertTemplate(new Template
                                {
                                    Name = file.FileName,
                                    Path = "/Content/Uploaded_Files/Templates/" + file.FileName,
                                    ContentType = file.ContentType
                                });
                                break;
                            case "image/jpeg":
                            case "image/jpg":
                            case "image/png":
                                setPath(file, pathslide);
                                slideService.InsertSlide(new Slide
                                {
                                    Name = file.FileName,
                                    Path = "/Content/Uploaded_Files/Slides/" + file.FileName,
                                    ContentType = file.ContentType
                                });
                                break;
                            default:
                                path = "Media not supported";
                                postmsg = "File type is not supported";
                                break;
                }
                        file.SaveAs(path);
                        if (postmsg!="")
                        {
                            postmsg = "File added to database";
                        }

                        postname = file.FileName;
                        postimg = "/content/images/ok.png";
                    }         
                    catch (Exception ex)
                    {
                        postmsg = "Could not add to database, error:" + ex;
                        postname = file.FileName;
                        postimg = "/content/images/notok.png";
                    }
                    model.statusList.Add(new Upload.PostInfo
                    {
                        filename = postname,
                        imgsrc = postimg,
                        message = postmsg
                    });
                }


                    }
            return View(model);
        }

        [HttpGet]
        public ActionResult AddYoutube()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddYoutube(AddYoutube model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            string embedUrl = "";
            string[] urlparts = model.Url.Split('/');
            foreach(string part in urlparts)
            {
                if(part == "https:" || part == "http:") { embedUrl += part + "//"; }
                else if(part == "www.youtube.com") { embedUrl += part + "/embed/"; }
                else if(part == "") { }
                else { embedUrl += part.Split('=')[1]; }
            }
            slideService.InsertSlide(new Slide { Name = model.Name, ContentType = "youtube", Path = embedUrl });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(slideService.GetAllSlides());
        }

        // GET: Files/DetailsSlide/5
        [HttpGet]
        public ActionResult EditSlide(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = slideService.GetSlideById(id);
            if(slide == null)
            {
                return HttpNotFound();
            }
            EditSlide model = new EditSlide()
            { Name = slide.Name, Path = slide.Path };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSlide(int id, EditSlide model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            slideService.UpdateSlide(id, new Slide { Name = model.Name });
            return RedirectToAction("Index");
        } 

        // GET: Files/DetailsTemplate/5
        [HttpGet]
        public ActionResult EditTemplate(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = templateService.GetTemplateByID(id);
            if(template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        [HttpGet]
        public ActionResult DeleteSlide(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = slideService.GetSlideById(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSlideConfirmed(int id)
        {
            slideService.DeleteSlide(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteTemplate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = templateService.GetTemplateByID(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTemplate(int id)
        {
            templateService.DeleteTemplate(id);
            return RedirectToAction("List");
        }

        public void setPath(HttpPostedFileBase file, string savepath)
            {
            path = Path.Combine(Server.MapPath(savepath), Path.GetFileName(file.FileName));
        }
    }


}