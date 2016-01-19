using System;
using System.Collections.Generic;
using DigiCon.Domain.Entities;
using DigiCon.Data.Sql;
using System.Data.Entity;
using System.Linq;

namespace DigiCon.Service.Repositories
{
    public class PlaylistRepository : IPlaylistRepository, IDisposable
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        public void ConnectPlaylist(int playlistIdA, int playlistIdB)
        {
            throw new NotImplementedException();
        }

        public void ConnectSlide(int playlistId, int slideId)
        {
            if(dbContext.Playlist_Slide.Find(playlistId, slideId) == null)
            {
                Playlist playlist = dbContext.Playlists.Find(playlistId);
                Slide slide = dbContext.Slides.Find(slideId);
                int connectedSlides = dbContext.Playlist_Slide.Where(ps => ps.PlaylistID == playlistId).Count();
                dbContext.Playlist_Slide.Add(new PlaylistSlide()
                {
                    PlaylistID = playlist.PlaylistID,
                    SlideID = slide.SlideID,
                    Playorder = connectedSlides + 1,
                    Animation = dbContext.Animations.First()
                });
                Save();
            }
        }

        public void DeletePlaylist(int id)
        {
            Playlist playlist;
            if((playlist = dbContext.Playlists.Find(id)) != null)
            {
                dbContext.Playlists.Remove(playlist);
                dbContext.SaveChanges();
            }
        }

        public void DisconnectPlaylist(int playlistIdA, int playlistIdB)
        {
            throw new NotImplementedException();
        }

        public void DisconnectSlide(int playlistId, int slideId)
        {
            PlaylistSlide connection = dbContext.Playlist_Slide.Find(playlistId, slideId);
            dbContext.Playlist_Slide.Remove(connection);
            // changes the playorders on the slidesconnections that had higher playorder than the removed slideconnection
            foreach (PlaylistSlide playlistslide in GetSlideConnections(playlistId).Where(c => c.Playorder > connection.Playorder))
            {
                playlistslide.Playorder--;
            }
            Save(); 
        }

        public Playlist GetPlaylistByID(int? id)
        {
            return dbContext.Playlists.Find(id);
        }

        public IEnumerable<Playlist> GetAllPlaylists()
        {
            return dbContext.Playlists;
        }

        public void InsertPlaylist(Playlist playlist)
        {
            dbContext.Playlists.Add(playlist);
            Save();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void UpdatePlaylist(int id, Playlist playlist)
        {
            Playlist playlist_to_update = dbContext.Playlists.Find(id);
            playlist_to_update.Name = playlist.Name;
            dbContext.Entry(playlist_to_update).State = EntityState.Modified;
            Save();
        }

        public IEnumerable<PlaylistSlide> GetSlideConnections(int playlistId)
        {
            List<PlaylistSlide> connections = new List<PlaylistSlide>();
            foreach (var playlistslide in dbContext.Playlist_Slide.Where(
                ps => ps.PlaylistID == playlistId))
            {
                connections.Add(playlistslide);
            }
            return connections;
        }

        public PlaylistSlide GetSlideConnection(int playlistid, int slideid)
        {
            return dbContext.Playlist_Slide.Find(playlistid, slideid);
        }

        public void UpdateSlideConnection(PlaylistSlide connection_to_update, PlaylistSlide updated_connection)
        {
            connection_to_update.AnimationID = updated_connection.AnimationID;
            connection_to_update.Duration = updated_connection.Duration;
            connection_to_update.Playorder = updated_connection.Playorder;
            dbContext.Entry(connection_to_update).State = EntityState.Modified;
            Save();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}