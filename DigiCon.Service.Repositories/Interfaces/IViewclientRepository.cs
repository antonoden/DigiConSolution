using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCon.Service.Repositories
{
    public interface IViewclientRepository : IDisposable
    {
        Viewclient GetViewclientByID(int? id);
        IEnumerable<Viewclient> GetAllViewclients();
        void InsertViewclient(Viewclient viewclient);
        void DeleteViewclient(int id);
        void UpdateViewclient(int id, Viewclient viewclient);
        void ConnectPlaylist(int viewclientId, int playlistId);
        void DisconnectPlaylist(int viewclientId, int playlistId);
        void UpdatePlaylistConnection(ViewclientPlaylist connection_To_update, ViewclientPlaylist updated_connection);
        IEnumerable<ViewclientPlaylist> GetPlaylistConnections(int viewclientId);
        ViewclientPlaylist GetPlaylistConnection(int viewclientid, int playlistid);
        void Save();
    }
}
