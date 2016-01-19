using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DigiCon.Service.Repositories
{
    public interface IPlaylistRepository : IDisposable
    {
        Playlist GetPlaylistByID(int? id);
        IEnumerable<Playlist> GetAllPlaylists();
        void InsertPlaylist(Playlist playlist);
        void DeletePlaylist(int id);
        void UpdatePlaylist(int id, Playlist playlist);
        void ConnectSlide(int playlistId, int slideId);
        void DisconnectSlide(int playlistId, int slideId);
        void ConnectPlaylist(int playlistIdA, int playlistIdB);
        void DisconnectPlaylist(int playlistIdA, int playlistIdB);
        void UpdateSlideConnection(PlaylistSlide connection_to_update, PlaylistSlide new_connection);
        IEnumerable<PlaylistSlide> GetSlideConnections(int playlistId);
        PlaylistSlide GetSlideConnection(int playlistid, int slideid);
        void Save();
    }
}
