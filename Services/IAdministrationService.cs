using Library.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IAdministrationService
    {
        Task<IdentityResult> AddRole(RoleModel roleModel);
    }
}
