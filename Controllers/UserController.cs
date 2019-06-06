using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Account.Controllers
{
    public class UserController : Controller
    {
        private AccountContext context;

        public UserController(AccountContext dbcontext)
        {
            context = dbcontext;
        }

        private int? UserSession
        {
            get 
            { 
                return HttpContext.Session.GetInt32("UserId"); 
            }
            set 
            { 
                HttpContext.Session.SetInt32("UserId", (int)value); 
            }
        }


        [HttpGet("")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("userlogin")]
        public IActionResult UserLogin(UserLogin usersubmitted)
        {
            if(ModelState.IsValid)
            {
                var userInfoInDb = context.Users.FirstOrDefault(u => u.Email == usersubmitted.Email);

                if(userInfoInDb == null)
                {
                    ModelState.AddModelError("Email", "Enter a correct Email");
                    return View("Login");
                }
                
                var passhasher = new PasswordHasher<UserLogin>();
                
                var result = passhasher.VerifyHashedPassword(usersubmitted, userInfoInDb.Password, usersubmitted.Password);
                
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Enter a correct Password");
                    return View("Login");
                }

                else
                {
                    UserSession = userInfoInDb.UserId;
                    return Redirect("/success");
                }

            }

            else
            {
                return View("Login");
            }
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                if(context.Users.Any(u => u.Email == user.Email)) 
                {
                    ModelState.AddModelError("Email", "Email already exist!");
                    return View("Registration");
                }
                else
                {
                    UserSession = context.Create(user);
                    return Redirect("/success");
                }
            }
            else
            {
                return View("Registration");
            }
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            if(UserSession == null)
            {
                return Redirect("/login");
            }
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/login");
        }
    }
}
