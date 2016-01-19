using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiCon.Domain.Entities;
using DigiCon.Data.Sql;

namespace DigiCon.Service.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public void DeleteTemplate(int id)
        {
            Template template;
            if ((template = dbContext.Templates.Find(id)) != null)
            {
                dbContext.Templates.Remove(template);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Template> GetAllTemplates()
        {
            return dbContext.Templates;
        }

        public Template GetTemplateByID(int? id)
        {
            return dbContext.Templates.Find(id);
        }

        public void InsertTemplate(Template template)
        {
            dbContext.Templates.Add(template);
            dbContext.SaveChanges();
        }

        public void UpdateTemplate(int id, Template template)
        {
            Template template_to_update = dbContext.Templates.Find(id);
            if(template_to_update != null)
            {
                template_to_update.Name = template.Name;
                dbContext.SaveChanges();
                dbContext.Entry(template_to_update).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}
