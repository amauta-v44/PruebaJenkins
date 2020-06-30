using System.Threading.Tasks;
using Devsoft.Api.Dtos.Authentication;

namespace Devsoft.Api.Services
{
    public interface IAuthService
    {
        Task<JwtResponse> LoginAsync(string username, string password);
        Task<JwtResponse> RegisterAsync(string username, string password);
        
    }
}