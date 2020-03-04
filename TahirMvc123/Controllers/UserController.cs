using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TahirMvc123.Models;

namespace TahirMvc123.Controllers
{
   [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly MvcDBContext _con;

        public UserController(MvcDBContext _db)
        {


            _con = _db;

        }

        public IActionResult Index()
        {
            return View();
        }





        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.msg = TempData["Msg"];
            ViewBag.type = TempData["type"];

            return View();
        }

        // POST: Vlilage/Create
        [HttpPost]

        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            try
            {
                // TODO: Add insert logic here

                var checkUser = _con.User.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
               if( checkUser !=null )
                { int userid = checkUser.Id;
                    await CreateAuthenticationCookie(user , userid);




                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] = "Email Or password invalid .....!!!!! please enter correct password";
                    TempData["type"] = 0;
             
                    return RedirectToAction("Login");
                }

               
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Registration()
        {
             
               
            return View();
        }

        // POST: Vlilage/Create
        [HttpPost]

        public ActionResult Registration(User user)
        {
            try
            {
                // TODO: Add insert logic here
                user.Date = DateTime.Now;
                _con.User.Add(user);
                _con.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }

        public async Task CreateAuthenticationCookie(User user ,int userid)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
               // new Claim(ClaimTypes.Role, "Admin"), 
            };

            //if (user.Email.ToLower().Contains("admin"))
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            //}

            var checkuserRole = (from ur in _con.UesrRole
                                 join r in _con.Role on ur.RoleId equals r.Id
                                 join u in _con.User on ur.UserId equals u.Id
                                 where ur.UserId==userid select r.Name).ToList();


       
            foreach (var item in checkuserRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }



           
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>, 

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //IsPersistent = true,
                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //  RedirectUri = "/home/index"
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        [HttpGet("user/logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction(nameof(Login));
        }

        [HttpGet("user/accessdenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}