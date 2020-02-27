using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TahirMvc123.Models;

namespace TahirMvc123.Controllers
{
    public class CastController : Controller
    {

        private readonly MvcDBContext _con;

        public CastController(MvcDBContext _db)
        {


            _con = _db;

        }
        public IActionResult Index()
        {
            var Casts = _con.Cast
        .Include(c => c.Vlilage).AsNoTracking().ToList();


            return View(Casts);
        }



        public ActionResult Create()
        {
            List<SelectListItem> Villagelist = new  List<SelectListItem>();

            var villagelist = _con.Vlilage.ToList();
            Villagelist.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var item in villagelist)
            {
                Villagelist.Add(new SelectListItem() { Text = item.VlilageName.ToString(), Value =item.Id.ToString() });
            }
            ViewBag.villageList = Villagelist;
            return View();
        }

        // POST: Vlilage/Create
        [HttpPost]
    
        public ActionResult Create(Cast cast)
        {
            try
            {
                // TODO: Add insert logic here
                //cast.Date = DateTime.Now;
                _con.Cast.Add(cast);
                _con.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                 
            }
            Cast ccast = _con.Cast
                .Include(c=>c.Vlilage).Where(x=>x.Id==id).FirstOrDefault();
            if (ccast == null)
            {
                 
            }
            return View(ccast);
        }

        public ActionResult Edit(int? id)
        {
            List<SelectListItem> Villagelist2 = new List<SelectListItem>();

            var villagelist = _con.Vlilage.ToList();
            Villagelist2.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var item in villagelist)
            {
                Villagelist2.Add(new SelectListItem() { Text = item.VlilageName.ToString(), Value = item.Id.ToString() });
            }
            if (id == null)
            {
                
            }
            Cast cast = _con.Cast.Find(id);
            if (cast == null)
            {
                

               
               
            }
            ViewBag.villageList2 = Villagelist2;
            return View(cast);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cast employee)
        {
            if (ModelState.IsValid)
            {
                _con.Entry(employee).State = EntityState.Modified;
                _con.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {

            }


            Cast cast = _con.Cast.Include(c => c.Vlilage).Where(x => x.Id == id).FirstOrDefault();
            if (cast == null)
            { 
            }
            return View(cast);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cast cast = _con.Cast.Find(id);
            _con.Cast.Remove(cast);
            _con.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}