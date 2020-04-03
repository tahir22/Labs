using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        private static Random random = new Random();

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

        //public async Task Send2FACode(User user)
        //{
        //    // var mobilecode = GetRandomCode();
        //    //var code = Guid.NewGuid().ToString();
        //     var code =  RandomNumber(6);
        //    // get user from db.. user >
        //    user.EmailVerificationCode = code;
        //    var updatedUser = _con.User.FirstOrDefault();
        //     updatedUser.EmailVerificationCode = user.EmailVerificationCode;

        //    await _con.SaveChangesAsync();

        //    //var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
        //    //{
        //    //    Credentials = new System.Net.NetworkCredential("mhd.tahir0023@gmail.com", "@Silakhan#8421724#"),
        //    //    EnableSsl = true
        //    //};
        //    //client.Send(updatedUser.Email, "mhd.tahir0023@gmail.com", "VeryCode", code);
        //    //Console.WriteLine("Sent");
        //    //Console.ReadLine();
        //    //string codee= "<a href="Verify2FACode?code=  "> Verify </a>" ;



        //    string email = updatedUser.Email;
        //    //string filename = dlg.FileName;


        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        //    mail.From = new MailAddress("mhd.tahir0023@gmail.com");
        //    mail.To.Add(email);
        //    mail.Subject = "verification code";
        //    mail.Body = "your verification code : " + code;
        //    //Attachment attachment = new Attachment(filename);
        //    //mail.Attachments.Add(attachment);

        //    SmtpServer.Port = 25;
        //    SmtpServer.Credentials = new System.Net.NetworkCredential("mhd.tahir0023@gmail.com", "@Silakhan#8421724#");
        //     SmtpServer.EnableSsl = true;

        //     SmtpServer.Send(mail);


        //    // send code in email/mobile number..

        //}

        public static   string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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
                if (string.IsNullOrWhiteSpace(rol.Value) == false)
                {
                    claims.Add(new Claim("permission", rol.Value));
                }
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
            string s = "tahir";
            string m = "1,8,6,4,2,9,7";
            //string m2 = "123654";
            //string m3 = "0123456789";
            //string m4 = "ff55612365";

            //bool g = m.PhoneNoValidation();
         string g = m.TSortString();
            //bool g1 = m2.PhoneNoValidation();s
            //bool g2 = m3.PhoneNoValidation();
            //bool g3 = m4.PhoneNoValidation();

           var yyy= _con.Customers.ToList().Where(x=>x.Name.TSortString()==g).ToList();
            var ss =s.stringlenght();
            return View();
        }



      

    }


}