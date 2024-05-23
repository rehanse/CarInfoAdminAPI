using CarInfo.DataAccess.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<ResponseStatus> LoginAsync(LoginModel model);
        Task<ResponseStatus> LogoutAsync();
        Task<ResponseStatus> RegisterAsync(RegisterModel model);
    }
}
