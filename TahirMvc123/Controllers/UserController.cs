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
                    await CreateAuthenticationCookie(checkUser);

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
            };

            var userRoles = (from ur in _con.UserRoles
                                 join r in _con.Roles on ur.RoleId equals r.Id
                                 join u in _con.User on ur.UserId equals u.Id

                                 where ur.UserId == user.Id
                                 select r).ToList();

            List<RoleClaim> RoleClaims = new List<RoleClaim>();

            foreach (var item in userRoles)
            {
                var crRole2s = (from rr in _con.Roles
                                join cur in _con.RoleClaims on rr.Id equals cur.RoleId
                                where cur.RoleId == item.Id
                                select cur).ToList();

                RoleClaims.AddRange(crRole2s);
            }

            foreach (var rol in RoleClaims)
            {
                claims.Add(new Claim("permission", rol.Value));
            }
             
            foreach (var rol in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol.Name));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
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