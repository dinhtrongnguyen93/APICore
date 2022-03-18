
using ApiCore.Models.ActionModel;
using System.Threading.Tasks;

namespace ApiCore.Models.DAL.IService
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest model);
    }
}
