using AutoMapper;
using Company.G05.DAL.Models;
using Company.G05.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G05.PL.Controllers
{
     [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> RoleManager, UserManager<ApplicationUser> userManager)
        {
            _RoleManager = RoleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchInput)
        {
            var user = Enumerable.Empty<RoleViewModel>();
            if (string.IsNullOrEmpty(searchInput))
            {
                user = await _RoleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }
                ).ToListAsync();
            }
            else
            {
                user = await _RoleManager.Roles.Where(R => R.Name
                .ToLower()
                .Contains(searchInput.ToLower()))
                .Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name

                }
                ).ToListAsync();
            }

            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var Role = new IdentityRole()
                {

                    Name = model.RoleName

                };

                var Result = await _RoleManager.CreateAsync(Role);
                if (Result.Succeeded)
                {
                    //AddOrRemoveUser(Role.Id);
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var userDB = await _RoleManager.FindByIdAsync(Id);
            if (userDB == null)
                return NotFound();

            var user = new RoleViewModel()
            {
                Id = userDB.Id,
                RoleName = userDB.Name,

            };


            return View(ViewName, user);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string Id, RoleViewModel model)
        {

            if (Id is null)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var userDB = await _RoleManager.FindByIdAsync(Id);
                if (userDB == null)
                    return NotFound();

                userDB.Name = model.RoleName;


                var result = await _RoleManager.UpdateAsync(userDB);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index");
                }

            }

            return View(model);

        }
        public async Task<IActionResult> Delete(string Id)
        {
            return await Details(Id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string Id, RoleViewModel model)
        {

            if (Id is null)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var userDB = await _RoleManager.FindByIdAsync(Id);
                if (userDB == null)
                    return NotFound();



                var result = await _RoleManager.DeleteAsync(userDB);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index");
                }

            }

            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            ViewData["roleID"]= roleId;
            if (roleId == null)
                return BadRequest();
            var role = await _RoleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();
            var UsersInRole = new List<UsersInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,

                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {

                    userInRole.IsSelected = true;

                }
                else
                {
                    userInRole.IsSelected = false;
                }
                UsersInRole.Add(userInRole);

            }

            return View(UsersInRole);


        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId, List<UsersInRoleViewModel> users)
        {
            if (roleId == null)
                return BadRequest();
            var role = await _RoleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if(appUser is not null)
                    {
                        if (user.IsSelected && ! await _userManager.IsInRoleAsync(appUser , role.Name))
                        {
                           await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                           await _userManager.RemoveFromRoleAsync(appUser, role.Name);

                        }
                    }
                }

                return RedirectToAction(nameof(Edit), new {id = role.Id});
            }
            return View(users);
        }
    }
}
