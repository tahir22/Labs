using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Labs.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TahirMvc123.Models;

namespace TahirMvc123.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcDBContext db;

        public HomeController(ILogger<HomeController> logger , MvcDBContext _db)
        {
            db = _db;
            _logger = logger;
        }

        public IActionResult Index()
        {

            var customerr = db.Customers.ToList();

            ////// Hash
            ////var hash = SecurePasswordHasher.Hash("mypassword");
            ////var hash2 = SecurePasswordHasher.Hash("sdjkasjdlkasjdlkjaskldjaskldaskldaskldlak");
             
            ////// Verify
            ////var passResult = SecurePasswordHasher.Verify("mypassword", hash); 
            ////var failResult = SecurePasswordHasher.Verify("mypassword3", hash);

            ////var passResult2 = SecurePasswordHasher.Verify("sdjkasjdlkasjdlkjaskldjaskldaskldaskldlak", hash2); 
            ////var failResult2 = SecurePasswordHasher.Verify("sdjkasjdlkasjdlkjaskldjaskldaskldaskldlakF", hash2);

            return View(customerr);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
