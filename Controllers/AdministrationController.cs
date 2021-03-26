using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationController(IAdministrationService administrationService,
                                        RoleManager<IdentityRole> roleManager,
                                        UserManager<IdentityUser> userManager)
        {
            this.administrationService = administrationService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
                return View(roleModel);
            var identityRole = new IdentityRole
            {
                Name = roleModel.Name,
            };

            var result = await roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
                return View();

            //var result = administrationService.AddRole(roleModel);
            // if (result.IsCompletedSuccessfully)
            //    return View();
            return View(roleModel);
        }

        [HttpGet]
        public IActionResult ListRole()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var model = new EditUserBelongedToRoleViewModel
                {
                    Name = role.Name,
                    Id = role.Id,
                };

                foreach (var item in userManager.Users)
                {
                    if (await userManager.IsInRoleAsync(item, role.Name))
                    {
                        model.Users.Add(item.UserName);
                    }
                }

                return View(model);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditUserBelongedToRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                role.Name = model.Name;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("ListRole");
                return View();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
                return View();

            var model = new List<UserRoleViewModel>();

            foreach (var item in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    IdUser = item.Id,
                    UserName = item.UserName,
                };

                if (await userManager.IsInRoleAsync(item, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
                return View();
            foreach (var item in model)
            {
                var user = await userManager.FindByIdAsync(item.IdUser);

                if(item.IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    var result = await userManager.AddToRoleAsync(user, role.Name);
                    //if(result.Succeeded)
                    //{
                    //    return RedirectToAction("EditRole");
                    //}
                }
                else if (!item.IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    var result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }


            }

            return RedirectToAction("EditRole", new { id = roleId});
        }
    }
}
