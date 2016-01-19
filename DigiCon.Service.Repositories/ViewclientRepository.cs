using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiCon.Domain.Entities;
using DigiCon.Data.Sql;

namespace DigiCon.Service.Repositories
{
    public class ViewclientRepository : IViewclientRepository
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public void ConnectPlaylist(int viewclientId, int playlistId)
        {
            if(dbContext.Viewclient_Playlist.Find(viewclientId, playlistId) == null)
            {
                Viewclient viewclient = dbContext.Viewclients.Find(viewclientId);
                Playlist playlist = dbContext.Playlists.Find(playlistId);
                viewclient.ViewclientPlaylists.Add(new ViewclientPlaylist
                {
                    Playlist = playlist,
                    PlaylistID = playlist.PlaylistID,
                    Viewclient = viewclient,
                    ViewclientID = viewclient.ViewclientID,
                    Playorder = viewclient.ViewclientPlaylists.Where(vp => vp.ViewclientID == viewclient.ViewclientID).Count() + 1,
                    Schedule = DateTime.Now
                });
                dbContext.SaveChanges();
            }
        }

        public void DeleteViewclient(int id)
        {
            Viewclient viewclient = GetViewclientByID(id);
            dbContext.Viewclients.Remove(viewclient);
            dbContext.SaveChanges();
        }

        public void DisconnectPlaylist(int viewclientId, int playlistId)
        {
            ViewclientPlaylist connection = dbContext.Viewclient_Playlist.Find(viewclientId, playlistId);
            if(connection != null)
            {
                foreach(var vp in dbContext.Viewclient_Playlist.Where(vp=> vp.ViewclientID == viewclientId))
                {
                    if(vp.Playorder > connection.Playorder)
                    {
                        vp.Playorder--;
                        dbContext.Entry(vp).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                dbContext.Viewclient_Playlist.Remove(connection);
            }
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public IEnumerable<Viewclient> GetAllViewclients()
        {
            return dbContext.Viewclients;
        }

        public ViewclientPlaylist GetPlaylistConnection(int viewclientid, int playlistid)
        {
            return dbContext.Viewclient_Playlist.Find(viewclientid, playlistid);
        }

        public IEnumerable<ViewclientPlaylist> GetPlaylistConnections(int viewclientId)
        {
            List<ViewclientPlaylist> connections = new List<ViewclientPlaylist>();
            foreach (ViewclientPlaylist vp in dbContext.Viewclient_Playlist.Where(vp => vp.ViewclientID == viewclientId))
            {
                connections.Add(vp);
            }
            return connections;
        }
        
        public Viewclient GetViewclientByID(int? id)
        {
            return dbContext.Viewclients.Find(id);
        }

        public void InsertViewclient(Viewclient viewclient)
        {
            dbContext.Viewclients.Add(viewclient);
            dbContext.SaveChanges();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void UpdatePlaylistConnection(ViewclientPlaylist connection_To_update, ViewclientPlaylist updated_connection)
        {
            connection_To_update.Playorder = updated_connection.Playorder;
            connection_To_update.Schedule = updated_connection.Schedule;
            dbContext.Entry(connection_To_update).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void UpdateViewclient(int id, Viewclient updated_viewclient)
        {
            Viewclient viewclient_to_update = dbContext.Viewclients.Find(id);
            viewclient_to_update.Name = updated_viewclient.Name;
            dbContext.Entry(viewclient_to_update).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
