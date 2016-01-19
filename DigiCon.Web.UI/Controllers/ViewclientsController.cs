using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DigiCon.Domain.Entities;
using DigiCon.Web.UI.Models.Viewclients;
using DigiCon.Web.UI.Models;
using DigiCon.Service.Repositories;

namespace DigiCon.Web.UI.Controllers
{
    public class ViewclientsController : Controller
    {
        private IViewclientRepository viewclientService;
        private IPlaylistRepository playlistService;
        private ISlideRepository slideService;

        public ViewclientsController()
        {
            viewclientService = new ViewclientRepository();
            playlistService = new PlaylistRepository();
            slideService = new SlideRepository();
        }

        // GET: Viewclients
        [HttpGet]
        public ActionResult Index()
        {
            return View(viewclientService.GetAllViewclients());
        }

        // GET: Viewclients/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Create());
        }

        // POST: Viewclients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Create model)
        {
            if (ModelState.IsValid)
            {
                Viewclient viewclient = new Viewclient() { Name = model.Name };
                viewclientService.InsertViewclient(viewclient);
                return RedirectToAction("Edit", new { id = viewclient.ViewclientID });
            }
            ViewBag.StatusMsg = "Ok";
            return View(model);
        }

        // GET: Viewclients/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viewclient viewclient = viewclientService.GetViewclientByID(id);
            if (viewclient == null)
            {
                return HttpNotFound();
            }
            EditViewclient model = new EditViewclient()
            {
                Name = viewclient.Name,
                ViewclientID = viewclient.ViewclientID,
                Viewclient = viewclient
            };
            foreach(ViewclientPlaylist viewclientplaylist in viewclient.ViewclientPlaylists.Where(vp => vp.ViewclientID == viewclient.ViewclientID))
            {
                model.Playlists.Add(new ViewModel_ListPlaylist()
                {
                    Name = viewclientplaylist.Playlist.Name,
                    ID = viewclientplaylist.Playlist.PlaylistID,
                    Playorder = viewclientplaylist.Playorder
                });
            }
            return View(model);
        }

        // POST: Viewclients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, EditViewclient model)
        {
            if (ModelState.IsValid)
            {
                viewclientService.UpdateViewclient(id, new Viewclient() { Name = model.Name });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Viewclients/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viewclient viewclient = viewclientService.GetViewclientByID(id);
            if (viewclient == null)
            {
                return HttpNotFound();
            }
            return View(viewclient);
        }

        // POST: Viewclients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            viewclientService.DeleteViewclient(id);
            return RedirectToAction("Index");
        }

        // GET: Viewclients/AddPlaylist/5
        [HttpGet]
        public ActionResult AddPlaylist(int id)
        {
            AddPlaylist model = new AddPlaylist();
            model.Playlists = playlistService.GetAllPlaylists().ToList();
            return View(model);
        }

        // POST: Viewclients/AddPlaylist/5
        [HttpPost]
        public ActionResult AddPlaylist(int id, AddPlaylist model)
        {
            List<Playlist> playlists = playlistService.GetAllPlaylists().ToList();
            for (int i = 0; i < playlists.Count; i++)
            {
                if (model.IsSelected[i])
                {
                    viewclientService.ConnectPlaylist(id, playlists[i].PlaylistID);
                }
            }
            return RedirectToAction("Edit", new { id = id });
        }

        public ActionResult DeletePlaylist(int id, int playlistid)
        {
            viewclientService.DisconnectPlaylist(id, playlistid);
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: Viewclients/AddPlaylist/5
        [HttpGet]
        public ActionResult DisplayView(int id)
        {
            List<SlideView> Slides = new List<SlideView>(); 
            foreach (ViewclientPlaylist vp in viewclientService.GetPlaylistConnections(id))
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
            return View(Slides);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                viewclientService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
