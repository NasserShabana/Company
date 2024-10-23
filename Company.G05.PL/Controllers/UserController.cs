using AutoMapper;
using Company.G05.DAL.Models;
using Company.G05.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Company.G05.PL.Controllers
{
	[Authorize (Roles= "Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;

		public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}
		public async Task<IActionResult> Index(string searchInput)
		{
			var user = Enumerable.Empty<UserViewModel>();
			if (string.IsNullOrEmpty(searchInput))
			{
				user = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}
				).ToListAsync();
			}
			else
			{
				user = await _userManager.Users.Where(U => U.Email
				.ToLower()
				.Contains(searchInput.ToLower()))
				.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}
				).ToListAsync();
			}

			return View(user);
		}

		public async Task<IActionResult> Details(string Id, string ViewName = "Details")
		{
			if (Id is null)
				return BadRequest();
			var userDB = await _userManager.FindByIdAsync(Id);
			if (userDB == null)
				return NotFound();

			var user = new UserViewModel()
			{
				Id=userDB.Id,
				Email = userDB.Email,
				FirstName = userDB.FirstName,
				LastName = userDB.LastName,
				Roles = _userManager.GetRolesAsync(userDB).Result

			};


            return View(ViewName, user);
		}

		public async Task<IActionResult> Edit(string Id)
		{
			return await Details(Id, "Edit");
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string Id, UserViewModel model)
		{

			if (Id is null)
				return BadRequest();
			if (ModelState.IsValid)
			{
				var userDB = await _userManager.FindByIdAsync(Id);
				if (userDB == null)
					return NotFound();

				userDB.FirstName = model.FirstName;
				userDB.LastName = model.LastName;
				userDB.Email = model.Email;

				var result = await _userManager.UpdateAsync(userDB);
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
        public async Task<IActionResult> Delete([FromRoute] string Id, UserViewModel model)
		{

			if (Id is null)
				return BadRequest();
			if (ModelState.IsValid)
			{
				var userDB = await _userManager.FindByIdAsync(Id);
				if (userDB == null)
					return NotFound();

				

				var result = await _userManager.DeleteAsync(userDB);
				if (result.Succeeded)
				{

					return RedirectToAction("Index");
				}

			}

			return View(model);

		}
	}
}