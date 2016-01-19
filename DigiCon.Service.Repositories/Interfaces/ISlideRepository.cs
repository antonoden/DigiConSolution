using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCon.Service.Repositories
{
    public interface ISlideRepository : IDisposable
    {
        Slide GetSlideById(int? id);
        IEnumerable<Slide> GetAllSlides();
        void InsertSlide(Slide slide);
        void DeleteSlide(int id);
        void UpdateSlide(int id, Slide updated_slide);
        void ConnectTemplate(int slideId, int templateId);
        void DisconnectTemplate(int slideId, int templateId);
        void Save();
    }
}
