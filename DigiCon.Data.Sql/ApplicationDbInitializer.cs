using DigiCon.Domain.Entities;
using System.Collections.Generic;

namespace DigiCon.Data.Sql
{
    class ApplicationDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>

    {
        protected override void Seed(ApplicationDbContext context)
        {
            IList<Animation> defaultAnimations = new List<Animation>();
            defaultAnimations.Add(new Animation() { Name = "Default" });
            defaultAnimations.Add(new Animation() { Name = "Explosion" });
            defaultAnimations.Add(new Animation() { Name = "SlideFromRight" });
            defaultAnimations.Add(new Animation() { Name = "SlideFromLeft" });
            defaultAnimations.Add(new Animation() { Name = "SlideFromUp" });
            defaultAnimations.Add(new Animation() { Name = "SlideFromDown" });

            foreach(Animation animation in defaultAnimations)
            {
                context.Animations.Add(animation);
            }
            base.Seed(context);
        }
    }
}
