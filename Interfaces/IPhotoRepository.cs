using FloorPlanApplication.Models;

namespace FloorPlanApplication.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotosForPlan(int PlanID);
        Task<IEnumerable<Photo>> GetPhotosSortedByTime(int index, int number);
        Task<Photo> GetPhotoByID(int ID);
        bool AddPhoto(Photo Photo);
        bool UpdatePhoto(Photo Photo);
        bool DeletePhotoReference(Photo Photo);
        bool Save();
    }
}
