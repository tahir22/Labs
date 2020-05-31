using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TahirMvc123.Controllers
{
    public class TestExmplController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}