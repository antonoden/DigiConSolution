using System;
using System.Collections.Generic;
using DigiCon.Domain.Entities;
using DigiCon.Data.Sql;
using System.Linq;

namespace DigiCon.Service.Repositories
{
    public class AnimationRepository : IAnimationRepository
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        public IEnumerable<Animation> getAllAnimations()
        {
            return dbContext.Animations;
        }

        public Animation GetAnimationByID(int id)
        {
            return dbContext.Animations.Find(id);
        }

        public Animation GetAnimationByName(string name)
        {
            return dbContext.Animations.ToList().Where(a => a.Name == name).First();
        }
    }
}
