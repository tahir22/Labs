using Labs.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TahirMvc123.Models;

namespace TahirMvc123.Api
{
    [ApiController, Route("api/users")]
    public class UserController : Controller
    {
        private readonly MvcDBContext _con;

        public UserController(MvcDBContext _db)
        {
            _con = _db;
        }

        // users/
        [HttpGet("")]
        public IActionResult Index()
        {
            var users = _con.User;

            return Ok(users);
        }

        // users/login
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] User user)
        {
            try
            {
                // TODO: Add insert logic here
                var userInDb = _con.User.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
                if (userInDb != null)
                {
                    // await CreateAuthenticationCookie(userInDb);

                    // create & return token
                    var token = await BuildToken(userInDb);
                    return Ok(token);
                }
                else
                {
                    return BadRequest(new { Error = "Error is found" });
                }

            }
            catch (Exception ex)
            {
                throw;
            }
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

                return Ok(user);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok();
        }

        [HttpGet("user/accessdenied")]
        public IActionResult AccessDenied()
        {
            return View(new { Error = "Access denied" });
        }

        async Task CreateAuthenticationCookie(User user)
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
                claims.Add(new Claim(Constants.ClaimType, rol.Value));
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


        // JWT ===================================== JWT ================

        private async Task<string> BuildToken(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.UserName),
                };

            // fetch user's roles
            // #. admin, employee, basic-user

            // fetch claims for each role
            // #. create, update, delete, grade-1, everything

            // add in claims collections.
            // #. calims << create, update, delete, grade-1, everything
            //claims.Add(new Claim(Constants.ClaimType, "create"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-name-is-khan-and-i-am-not-"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              "labs-i",
              "labs-a",
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }



        // ================ method ==========
        [HttpGet("hello"), Authorize]
        public string Hello()
        {
            return "heelooo";
        }
    }
}