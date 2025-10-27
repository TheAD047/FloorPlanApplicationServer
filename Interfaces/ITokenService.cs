using FloorPlanApplication.Models;

namespace FloorPlanApplication.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
