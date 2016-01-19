
using DigiCon.Domain.Entities;
using System.Collections.Generic;

namespace DigiCon.Service.Repositories
{
    public interface IAnimationRepository
    {
        Animation GetAnimationByID(int id);
        IEnumerable<Animation> getAllAnimations();
        Animation GetAnimationByName(string name);
    }
}
