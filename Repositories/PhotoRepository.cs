using FloorPlanApplication.Data;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanApplication.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDBContext _context;

        public PhotoRepository(AppDBContext context)
        {
            _context = context;
        }

        public bool AddPhoto(Photo Photo)
        {
            _context.Add(Photo);
            return Save();
        }

        public bool DeletePhotoReference(Photo Photo)
        {
            _context.Remove(Photo);
            return Save();
        }

        public async Task<Photo> GetPhotoByID(int ID)
        {
            return await _context.Photos
                        .FirstOrDefaultAsync(p => p.ID == ID);
        }

        public async Task<IEnumerable<Photo>> GetPhotosForPlan(int PlanID)
        {
            return await _context.Photos
                            .Where(p => p.PlanID == PlanID)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Photo>> GetPhotosSortedByTime(int index, int number)
        {
            return await _context.Photos
                            .Skip(index * number)
                            .Take(number)
                            .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePhoto(Photo Photo)
        {
            _context.Update(Photo);
            return Save();
        }
    }
}
