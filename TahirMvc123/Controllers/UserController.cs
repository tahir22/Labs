using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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






        public ActionResult Login()
        {
            ViewBag.msg = TempData["Msg"];
            ViewBag.type = TempData["type"];

            return View();
        }

        // POST: Vlilage/Create
        [HttpPost]

        public ActionResult Login(User user)
        {
            try
            {
                // TODO: Add insert logic here

                var checkUser = _con.User.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
               if( checkUser !=null )
                {
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


    }
}