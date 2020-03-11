using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TahirMvc123.Models;

namespace TahirMvc123.Controllers
{
    public class RoleAndPermissionController : Controller
    {
        private readonly MvcDBContext _con;

        public RoleAndPermissionController(MvcDBContext _db)
        {
            _con = _db;
        }
        public IActionResult Index()
        {
            ViewBag.msg = TempData["Msg"];
            ViewBag.type = TempData["type"];
            var checkRle = _con.Roles.ToList();
            return View(checkRle);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRole(Role role )
        {
            try
            {
                Role r = new Role();

                _con.Roles.Add(role);
                _con.SaveChanges();
                TempData["msg"] = "Save successfully";
                TempData["type"] = 1;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                TempData["msg"] = "Not Save  successfully";
                TempData["type"] = 0;
                return RedirectToAction(nameof(Index));
                throw;
            }
           
        }




        [HttpGet]
        public IActionResult CreateR()
        {
            var rolee = _con.Roles.ToList();
            return View(rolee);
        }

        [HttpPost]
        public IActionResult CreateR(int[] roles)
        {
            var roleList  = roles;
            return View();
        }
        
        [HttpPost]
        public IActionResult AssignRoles(int[] roles)
        {
            var roleList  = roles;
            return View();
        }

    }
}