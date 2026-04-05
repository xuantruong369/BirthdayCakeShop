using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            UserDTO userDTO = new UserDTO()
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash
            };

            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = await _authService.Login(userDTO);
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    HttpContext.Session.SetInt32("Id", u.UserId);
                    if (u.Avatar != null)
                    {
                        TempData["Avatar"] = "~/Client/img/" + u.Avatar;
                    }
                    TempData["Avatar"] = "~/Admin/images/faces/face1.jpg";
                    if (u.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            var user = new AddUserDTO
            {
                Username = registerUser.Username,
                PasswordHash = registerUser.PasswordHash,
                CustomerName = registerUser.CustomerName,
                Phone = registerUser.Phone,
                Address = registerUser.Address,
                ConfirmPassword = registerUser.ConfirmPassword
            };


            if (!ModelState.IsValid)
            {
                return View(registerUser);
            }

            var result = await _userService.Register(user);

            if (!result.Success)
            {
                ModelState.AddModelError(result.Field, result.Message);
                return View(registerUser);
            }

            return RedirectToAction("Login");
        }
    }
}