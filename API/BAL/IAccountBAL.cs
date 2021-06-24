using System.Threading.Tasks;
using API.DTO;

namespace API.BAL
{
    public interface IAccountBAL
    {
        Task<UserDTO> Login(string username, string password);
    }
}