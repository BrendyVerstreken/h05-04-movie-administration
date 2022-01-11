using System.Threading.Tasks;
using Wba.Oefening.RateAMovie.Web.ViewModels;

namespace Wba.Oefening.RateAMovie.Web.Services.Interfaces
{
    public interface IAccountService
    {
        string HashPassword(string password);
        Task<bool> RegisterUser(AccountRegisterViewModel accountRegisterViewModel);

        Task<bool> Login(AccountLoginViewModel accountLoginViewModel);
    }
}