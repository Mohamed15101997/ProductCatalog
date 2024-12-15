using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProductCatalogTask.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		public IActionResult Register()
		{
			return View("Register");
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel user)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser appUser = new ApplicationUser
				{
					Name = user.Name,
					UserName = user.UserName
				};
				IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

				if (result.Succeeded)
				{
					var roleExists = await _roleManager.RoleExistsAsync("User");

					if (roleExists)
					{
						await _userManager.AddToRoleAsync(appUser, "User");
					}
					await _signInManager.SignInAsync(appUser, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}

				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}
			return View("Register", user);
		}

		public IActionResult Login()
		{
			return View("Login");
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginUser)
		{
			if (ModelState.IsValid) 
			{
				ApplicationUser appUser = await _userManager.FindByNameAsync(loginUser.UserName);
				if (appUser != null) 
				{
					bool isFound = await _userManager.CheckPasswordAsync(appUser, loginUser.Password);
					if (isFound) 
					{
						List<Claim> claims = new List<Claim>();
						claims.Add(new Claim("UserOfName" , appUser.Name));
						await _signInManager.SignInWithClaimsAsync(appUser, loginUser.RememberMe, claims);
						return RedirectToAction("Index", "Home");
					}
					ModelState.AddModelError("","User Name or Password is InValid");
				}
			}
			return View("Login" , loginUser);
		}
		public async Task<IActionResult> SignOut() 
		{ 
			await _signInManager.SignOutAsync();
			return View("Login"); 
		}
	}

}

