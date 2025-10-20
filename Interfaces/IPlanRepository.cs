using FloorPlanApplication.Models;

namespace FloorPlanApplication.Interfaces
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetPlans(int index, int number);
        Task<IEnumerable<Plan>> GetPlansByNumberOfBedrooms(int min, int max, int index, int number);
        Task<IEnumerable<Plan>> GetPlansByNumberOfBathrooms(int min, int max, int index, int number);
        Task<IEnumerable<Plan>> GetPlansByPrice(double min, double max, int index, int number);
        Task<IEnumerable<Plan>> GetPlansByArea(double min, double max, int index, int number);
        Task<Plan> GetPlanByID(int ID);
        bool AddPlan(Plan Plan);
        bool UpdatePlan(Plan Plan);
        bool DeletePlan(Plan Plan);
        bool Save();
    }
}
