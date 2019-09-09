using System.Threading.Tasks;
using dating_app.api.Models;

namespace dating_app.api.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);

         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username);
    }
}