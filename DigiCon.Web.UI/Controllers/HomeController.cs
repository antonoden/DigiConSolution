using DigiCon.Domain.Entities;
using DigiCon.Service.Repositories;
using DigiCon.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace DigiCon.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private IViewclientRepository viewclientService;
        private IPlaylistRepository playlistService;
        private ISlideRepository slideService;
        private ITemplateRepository templateService;

        public HomeController()
        {
            viewclientService = new ViewclientRepository();
            playlistService = new PlaylistRepository();
            slideService = new SlideRepository();
            templateService = new TemplateRepository();
        }

        [HttpGet]
        // GET: Browse
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListViewclients()
        {
            return PartialView("~/Views/Viewclients/_ListViewclientsPartial.cshtml", viewclientService.GetAllViewclients());
        }

        [HttpGet]
        public ActionResult Viewclient(int viewclientId)
        {
            List<Playlist> playlists = new List<Playlist>();
            foreach (ViewclientPlaylist vp in viewclientService.GetPlaylistConnections(viewclientId))
            {
                playlists.Add(vp.Playlist);
            }
            Viewclient viewclient = viewclientService.GetViewclientByID(viewclientId);
            ViewModel_ListViewclient model = new ViewModel_ListViewclient()
            {
                Name = viewclient.Name,
                ID = viewclient.ViewclientID,
                Playlists = playlists
            };
            return PartialView("~/Views/Viewclients/_ViewclientPartial.cshtml", model);
        }

        [HttpGet]
        public ActionResult Playlist(int playlistId)
        {
            List<Slide> slides = new List<Slide>();
            foreach (PlaylistSlide ps in playlistService.GetSlideConnections(playlistId))
            {
                slides.Add(ps.Slide);
            }
            Playlist playlist = playlistService.GetPlaylistByID(playlistId);
            ViewModel_ListPlaylist model = new ViewModel_ListPlaylist()
            {
                Name = playlist.Name,
                ID = playlist.PlaylistID,
                Slides = slides
            };
            return PartialView("~/Views/Playlists/_PlaylistPartial.cshtml", model);
        }

        public ActionResult Slide(int slideId, int playlistId)
        {
            PlaylistSlide ps = playlistService.GetSlideConnection(playlistId, slideId);
            Slide slide = slideService.GetSlideById(slideId);
            ViewModel_ListSlide model = new ViewModel_ListSlide()
            {
                Contenttype = slide.ContentType,
                Name = slide.Name,
                ID = slide.SlideID,
                Path = slide.Path,
                Playorder = ps.Playorder,
                Duration = ps.Duration,
                Animation = ps.Animation.Name
            };
            return PartialView("~/Views/Files/_SlidePartial.cshtml", model);
        }

        /* Add playlist(s) to a viewclient */
        [HttpGet]
        public ActionResult AddPlaylist(int viewclientId)
        {
            ViewModel_AddPlaylistToViewclient model = new ViewModel_AddPlaylistToViewclient();
            model.playlists = playlistService.GetAllPlaylists().ToList();
            model.viewclientID = viewclientId;
            return PartialView("~/Views/Viewclients/_AddPlaylistToViewclientPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddPlaylist(int viewclientId, ViewModel_AddPlaylistToViewclient model)
        {
            List<Playlist> playlists = playlistService.GetAllPlaylists().ToList();
            for (int i = 0; i < playlists.Count(); i++)
            {
                if (model.isSelected[i])
                {
                    viewclientService.ConnectPlaylist(viewclientId, playlists[i].PlaylistID);
                }
            }
            return RedirectToAction("Viewclient", new { viewclientId = viewclientId });
        }

        [HttpGet]
        public ActionResult CreateAndAddPlaylist(int viewclientId)
        {
            ViewModel_AddCreatePlaylistToViewclient model = new ViewModel_AddCreatePlaylistToViewclient();
            model.viewclientId = viewclientId;
            return PartialView("~/Views/Viewclients/_AddCreatePlaylistToViewclientPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult CreateAndAddPlaylist(int viewclientId, ViewModel_AddCreatePlaylistToViewclient model)
        {
            Playlist playlist = new Domain.Entities.Playlist() { Name = model.playlistName };
            playlistService.InsertPlaylist(playlist);
            viewclientService.ConnectPlaylist(viewclientId, playlist.PlaylistID);
            return RedirectToAction("Viewclient", new { viewclientId = viewclientId });
        }

        /* Add slide(s) to a playlist */
        [HttpGet]
        public ActionResult AddSlide(int playlistId)
        {
            ViewModel_AddSlideToPlaylist model = new ViewModel_AddSlideToPlaylist();
            model.Slides = slideService.GetAllSlides().ToList();
            model.playlistID = playlistId;
            return PartialView("~/Views/Playlists/_AddSlideToPlaylistPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddSlide(int playlistId, ViewModel_AddSlideToPlaylist model)
        {
            List<Slide> Slides = slideService.GetAllSlides().ToList();
            for (int i = 0; i < Slides.Count(); i++)
            {
                if (model.isSelected[i])
                {
                    playlistService.ConnectSlide(playlistId, Slides[i].SlideID);
                }
            }
            return RedirectToAction("Playlist", new { playlistId = playlistId });
        }

        [HttpGet]
        public ActionResult AddYoutube(int playlistId)
        {
            ViewModel_AddYoutubeToPlaylist model = new ViewModel_AddYoutubeToPlaylist();
            model.playlistId = playlistId;
            return PartialView("~/Views/Playlists/_AddYoutubeToPlaylistPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddYoutube(int playlistId, ViewModel_AddYoutubeToPlaylist model)
        {
            string embedUrl = "";
            string[] urlparts = model.url.Split('/');
            foreach (string part in urlparts)
            {
                if (part == "https:" || part == "http:") { embedUrl += part + "//"; }
                else if (part == "www.youtube.com") { embedUrl += part + "/embed/"; }
                else if (part == "") { }
                else { embedUrl += part.Split('=')[1]; }
            }
            Slide slide = new Slide() { Name = model.name, ContentType = "youtube", Path = embedUrl };
            slideService.InsertSlide(slide);
            playlistService.ConnectSlide(playlistId, slide.SlideID);
            return RedirectToAction("Playlist", new { playlistId = playlistId });
        }

        [HttpGet]
        public ActionResult DisplayView(int viewclientId)
        {
            List<SlideView> Slides = new List<SlideView>();
            foreach (ViewclientPlaylist vp in viewclientService.GetPlaylistConnections(viewclientId))
            {
                int numberOfSlidesBefore = Slides.Count(); // if we got more than one playlist this is a handy variable
                foreach (var playlistslide in playlistService.GetSlideConnections(vp.PlaylistID))
                {
                    Slides.Add(new SlideView
                    {
                        Slide = playlistslide.Slide,
                        Duration = playlistslide.Duration,
                        Transition = playlistslide.Animation.Name,
                        Playorder = playlistslide.Playorder + numberOfSlidesBefore
                    });
                }
            }
            return View("~/Views/Viewclients/DisplayView.cshtml", Slides);
        }

        [HttpGet]
        public ActionResult UploadSlide(int playlistId)
        {
            ViewModel_UploadSlideToPlaylist model = new ViewModel_UploadSlideToPlaylist();
            model.playlistId = playlistId;
            return PartialView("~/Views/Playlists/_UploadSlideToPlaylistPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult UploadSlide(int playlistId, ViewModel_UploadSlideToPlaylist model)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                try
                {
                    switch (file.ContentType)
                    {
                        case "image/png":
                        case "image/jpg":
                        case "image/jpeg":
                            var path = Path.Combine(
                                        Server.MapPath("/Content/Uploaded_Files/Slides/"),
                                        Path.GetFileName(file.FileName));
                            file.SaveAs(path); //File will be saved in application root
                            Slide slide = new Slide
                            {
                                Name = file.FileName,
                                Path = "/Content/Uploaded_Files/Slides/" + file.FileName,
                                ContentType = file.ContentType
                            };
                            slideService.InsertSlide(slide);
                            playlistService.ConnectSlide(playlistId, slide.SlideID);
                            model.statusList.Add(new UploadPostInfo()
                            {
                                filename = file.FileName,
                                imgsrc = "/content/images/ok.png",
                                message = "File added to database"
                            });
                            break;
                        default:
                            model.statusList.Add(new UploadPostInfo()
                            {
                                filename = file.FileName,
                                imgsrc = "/content/images/notok.png",
                                message = "File type is not supported"
                            });
                            break;
                    }
                } catch (Exception ex)
                {
                    model.statusList.Add(new UploadPostInfo()
                    {
                        filename = file.FileName,
                        imgsrc = "/content/images/notok.png",
                        message = "Could not add to database, error:" + ex
                    });
                }
            }
            return PartialView("~/Views/Playlists/_UploadSlideToPlaylistPartial.cshtml", model);
        }
    

        /* Edit slide(s) in a playlist */
        [HttpGet]
        public ActionResult EditPlaylist(int playlistId)
        {
            ViewModel_EditPlaylist model = new ViewModel_EditPlaylist();       

               Playlist playlist = playlistService.GetPlaylistByID(playlistId);
               if (playlist == null)
               {
                   return HttpNotFound();
               }
               foreach (PlaylistSlide connection in playlistService.GetSlideConnections(playlistId))
               {
                   model.Slides.Add(new SlideView()
                   {
                       Slide = connection.Slide,
                       Duration = connection.Duration,
                       Playorder = connection.Playorder
                   });
               }
            model.Slides = model.Slides.OrderBy(l => l.Playorder).ToList();
            model.playlistID = playlist.PlaylistID;
            model.playlistName = playlist.Name;
            return PartialView("~/Views/Playlists/_EditPlaylistPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditPlaylist(int playlistId, ViewModel_EditPlaylist model)
        {
            if(ModelState.IsValid)
            {
                playlistService.UpdatePlaylist(playlistId, new Playlist() { Name = model.playlistName });
                foreach (var slide in model.Slides)
                {
                    PlaylistSlide connection = playlistService.GetSlideConnection(playlistId, slide.Slide.SlideID);
                    if(connection == null)
                    {
                        playlistService.ConnectSlide(playlistId, slide.Slide.SlideID);
                    } else
                    {
                        playlistService.UpdateSlideConnection(connection,
                            new PlaylistSlide()
                            {
                                Duration = slide.Duration,
                                AnimationID = 1,
                                Playorder = slide.Playorder
                            });
                    }
                }
                model.Slides.OrderBy(l => l.Playorder);
                model.playlistName = playlistService.GetPlaylistByID(playlistId).Name;
            }
            return PartialView("~/Views/Playlists/_EditPlaylistPartial.cshtml", model);
        }

        /* deletes a connection (slide + playlist), returns the partial view with given connection deleted */ 
        [HttpGet]
        public ActionResult DeleteSlide(int playlistId, int slideId, ViewModel_EditPlaylist model)
        {
            playlistService.DisconnectSlide(playlistId, slideId);
            foreach (PlaylistSlide connection in playlistService.GetSlideConnections(playlistId))
            {
                model.Slides.Add(new SlideView()
                {
                    Slide = connection.Slide,
                    Duration = connection.Duration,
                    Playorder = connection.Playorder
                });
            }
            model.playlistName = playlistService.GetPlaylistByID(playlistId).Name;
            return PartialView("~/Views/Playlists/_EditPlaylistPartial.cshtml", model);
        }

        [HttpGet]
        public ActionResult DeletePlaylistFromViewclient(int playlistId, int viewclientId, ViewModel_EditPlaylistsInViewclient model)
        {
            viewclientService.DisconnectPlaylist(viewclientId, playlistId);
            foreach (ViewclientPlaylist connection in viewclientService.GetPlaylistConnections(viewclientId))
            {
                model.playlists.Add(new PlaylistView()
                {
                    playlist = connection.Playlist,
                    Playorder = connection.Playorder
                });
            }
            model.viewclientName = viewclientService.GetViewclientByID(viewclientId).Name;
            return PartialView("~/Views/Viewclients/_EditViewclientPartial.cshtml", model);
        }

        [HttpGet]
        public ActionResult EditViewclient(int viewclientId)
        {
            Viewclient viewclient = viewclientService.GetViewclientByID(viewclientId);
            if (viewclient == null)
            {
                return HttpNotFound();
            }
            ViewModel_EditPlaylistsInViewclient model = new ViewModel_EditPlaylistsInViewclient()
            {
                viewclientID = viewclient.ViewclientID,
                viewclientName = viewclient.Name
            };
            foreach (var connection in viewclientService.GetPlaylistConnections(viewclient.ViewclientID))
            {
                model.playlists.Add(new PlaylistView()
                {
                    playlist = connection.Playlist,
                    Playorder = connection.Playorder
                });
            }
            model.playlists.OrderBy(l => l.Playorder);
            return PartialView("~/Views/Viewclients/_EditViewclientPartial.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditViewclient(int viewclientId, ViewModel_EditPlaylistsInViewclient model)
        {
            if(ModelState.IsValid)
            {
                viewclientService.UpdateViewclient(
                    viewclientId, 
                    new Viewclient() { Name = model.viewclientName }
                    );
                foreach (var playlist in model.playlists)
                {
                    ViewclientPlaylist connection = viewclientService.GetPlaylistConnection(viewclientId, playlist.playlist.PlaylistID);
                    if(connection == null)
                    {
                        viewclientService.ConnectPlaylist(viewclientId, playlist.playlist.PlaylistID);
                    } else
                    {
                        viewclientService.UpdatePlaylistConnection(connection,
                            new ViewclientPlaylist()
                            {
                                Playorder = connection.Playorder
                            });
                    }
                }
                model.playlists.OrderBy(l => l.Playorder);
            }
            return PartialView("~/Views/Viewclients/_EditViewclientPartial.cshtml", model);
        }
    }
}