using Edukate.Context;
using Edukate.Models;
using Edukate.ViewModels.AuthVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Edukate.Controllers
{
    public class AuthController : Controller
    {
        SignInManager<AppUser> _signInManager { get; }
        UserManager<AppUser> _userManager { get; }
        
        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            
            _signInManager=signInManager;
            _userManager=userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult>Login(LoginVM loginVM)
        {
            AppUser user;
            if (loginVM.UsernameOrEmail.Contains("@"))
            {
                 user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(loginVM);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong");
                return View(loginVM);
            }
            return RedirectToAction("Index", "Home");   
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult>Register(RegisterVM registerVM)
        {
            
            if (!ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Email = registerVM.Email,
                    UserName = registerVM.Username,
                    Name= registerVM.Name,
                    Surname = registerVM.Surname,
                };

                if (registerVM.Password == null)
                {
                    return View(registerVM);
                }
                var result = await _userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, "User");

                    return RedirectToAction("Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();

        }
    }
}
