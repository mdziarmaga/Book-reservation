using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IAuthService
    {
        Task Login(LoginModel model);
        Task Register(RegisterModel model);
        Task Logout();
    }
}
