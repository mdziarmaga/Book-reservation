using Library.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrationService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> AddRole(RoleModel roleModel)
        {
            var identityRole = new IdentityRole
            {
                Name = roleModel.Name,
                
            };

            var result = await roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
                return IdentityResult.Success;
  
            return IdentityResult.Failed();
        }
    }
}
