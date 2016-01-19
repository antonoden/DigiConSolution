using DigiCon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCon.Service.Repositories
{
    public interface ITemplateRepository
    {
        IEnumerable<Template> GetAllTemplates();
        Template GetTemplateByID(int? id);
        void InsertTemplate(Template template);
        void DeleteTemplate(int id);
        void UpdateTemplate(int id, Template template);
    }
}
