using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DigiCon.Domain.Entities;
using DigiCon.Web.UI.Models.Playlists;
using DigiCon.Web.UI.Models;
using DigiCon.Service.Repositories;

namespace DigiCon.Web.UI.Controllers
{
    public class PlaylistsController : Controller
    {
        private IPlaylistRepository playlistService;
        private IAnimationRepository animationService;
        private ISlideRepository slideService;

        public PlaylistsController()
        {
            playlistService = new PlaylistRepository();
            animationService = new AnimationRepository();
            slideService = new SlideRepository();
        }

        // GET: Playlists
        [HttpGet]
        public ActionResult Index()
        {
            return View(playlistService.GetAllPlaylists().ToList());
        }

        // GET: Playlists/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Create());
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Create model)
        {
            if (ModelState.IsValid)
            {
                Playlist playlist = new Playlist() { Name = model.Name };
                playlistService.InsertPlaylist(playlist);
                return RedirectToAction("Edit", new { id = playlist.PlaylistID });
            }
            return View(model);
        }

        // GET: Playlists/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewModel_EditPlaylist model = new ViewModel_EditPlaylist();

            Playlist playlist = playlistService.GetPlaylistByID(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            foreach(PlaylistSlide connection in playlistService.GetSlideConnections(id))
            {
                model.Slides.Add(new SlideView()
                {
                    Slide = connection.Slide,
                    Duration = connection.Duration,
                    Transition = connection.Animation.Name,
                    Playorder = connection.Playorder
                });
            }
            model.playlistName = playlist.Name;
            return View(model);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ViewModel_EditPlaylist model)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                playlistService.UpdatePlaylist(id, new Playlist() { Name = model.playlistName });
                foreach(var slide in model.Slides)
                {
                    PlaylistSlide connection;
                    if((connection = playlistService.GetSlideConnection(id, slide.Slide.SlideID)) == null)
                    {
                        playlistService.ConnectSlide(id, slide.Slide.SlideID);
                    } else
                    {
                        playlistService.UpdateSlideConnection(connection,
                            new PlaylistSlide()
                            {
                                Duration = slide.Duration,
                                AnimationID = animationService.GetAnimationByName(slide.Transition).AnimationID,
                                Playorder = slide.Playorder
                            });
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Playlists/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = playlistService.GetPlaylistByID(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            playlistService.DeletePlaylist(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddSlide(int id)
        {
            AddSlide model = new AddSlide();
            model.Slides = slideService.GetAllSlides().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddSlide(int id, AddSlide model)
        {
            Playlist playlist = playlistService.GetPlaylistByID(id);
            List<Slide> slides = slideService.GetAllSlides().ToList();
            for (int i = 0; i < slides.Count; i++)
            {
                if (model.IsSelected[i])
                {
                    playlistService.ConnectSlide(playlist.PlaylistID, slides[i].SlideID);
                }
            }
            return RedirectToAction("Edit", new { id = playlist.PlaylistID });
        }

        [HttpGet]
        public ActionResult DeleteSlide(int playlistid, int slideid)
        {
            playlistService.DisconnectSlide(playlistid, slideid);
            return RedirectToAction("Edit", new { id = playlistid });
        } 

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                playlistService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
