using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Labs.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TahirMvc123.Models;

namespace TahirMvc123.Controllers
{
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

        [HttpPost]
        public async Task<ActionResult> LoginAsync(User user)
        {
            try
            {
                // TODO: Add insert logic here

                var checkUser = _con.User.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                if (checkUser != null)
                {
                    await CreateAuthenticationCookie(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] = "Email Or password invalid .....!!!!! please enter correct password";
                    TempData["type"] = 0;

                    return RedirectToAction("Login");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Password) == false)
                {
                    user.HashedPassword = SecurePasswordHasher.Hash(user.Password);
                }

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

        public async Task CreateAuthenticationCookie(User user)
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

            var checkuserRole = (from ur in _con.UserRoles
                                 join r in _con.Roles on ur.RoleId equals r.Id
                                 join u in _con.User on ur.UserId equals u.Id

                                 where ur.UserId == user.Id
                                 select r).ToList();


            List<RoleClaim> RoleClaimss = new List<RoleClaim>();

            foreach (var item in checkuserRole)
            {
                var crRole2s = (from rr in _con.Roles
                                join cur in _con.RoleClaims on rr.Id equals cur.RoleId
                                where cur.RoleId == item.Id
                                select cur).ToList();

                RoleClaimss.AddRange(crRole2s);
                //var crRole = _con.Role.Include(x => x.Userclaims).Select(c=>c.Userclaims).ToList();

            }
            //RoleClaimss = RoleClaimss.Distinct().ToList();
            foreach (var rol in RoleClaimss)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol.UserclaimsValues));
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
               // new Claim(ClaimTypes.Role, "Admin"), 
            };

            if (user.Email.ToLower().Contains("admin"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
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