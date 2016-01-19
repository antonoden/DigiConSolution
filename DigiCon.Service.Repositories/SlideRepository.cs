using System;
using System.Collections.Generic;
using DigiCon.Domain.Entities;
using DigiCon.Data.Sql;
using System.Linq;

namespace DigiCon.Service.Repositories
{
    public class SlideRepository : ISlideRepository, IDisposable
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public void ConnectTemplate(int slideId, int templateId)
        {
            Slide slide = dbContext.Slides.Find(slideId);
            slide.TemplateID = templateId;
            dbContext.Entry(slide).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteSlide(int id)
        {
            Slide slide;
            if((slide = dbContext.Slides.Find(id)) != null)
            {
                dbContext.Slides.Remove(slide);
                dbContext.SaveChanges();
            }
        }

        public void DisconnectTemplate(int slideId, int templateId)
        {
            Slide slide = dbContext.Slides.Find(slideId);
            slide.TemplateID = 0; // setting templateID to 0 equals no template
            dbContext.Entry(slide).State = System.Data.Entity.EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public IEnumerable<Slide> GetAllSlides()
        {
            return dbContext.Slides;
        }

        public Slide GetSlideById(int? id)
        {
            return dbContext.Slides.Find(id);
        }

        public void InsertSlide(Slide slide)
        {
            dbContext.Slides.Add(slide);
            dbContext.SaveChanges();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void UpdateSlide(int id, Slide updated_slide)
        {
            Slide slide_to_update;
            if((slide_to_update = dbContext.Slides.Find(id)) != null)
            {
                slide_to_update.Name = updated_slide.Name;
                dbContext.Entry(slide_to_update).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}
