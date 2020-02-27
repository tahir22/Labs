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
    public class FamilyController : Controller
    {
        private readonly MvcDBContext _con;

        public FamilyController(MvcDBContext _db)
        {


            _con = _db;

        }
        public IActionResult Index()
        {
            var family = _con.Family
        .Include(c => c.Cast).AsNoTracking().ToList();


            return View(family);
        }



        public ActionResult Create()
        {
            List<SelectListItem> Castlist = new List<SelectListItem>();

            var Family = _con.Cast.ToList();
            Castlist.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var item in Family)
            {
                Castlist.Add(new SelectListItem() { Text = item.CastName.ToString(), Value = item.Id.ToString() });
            }
            ViewBag.Castlist = Castlist;
            return View();
        }

        // POST: Vlilage/Create
        [HttpPost]

        public ActionResult Create(Family fmlyy)
        {
            try
            {
                // TODO: Add insert logic here
                //cast.Date = DateTime.Now;
                _con.Family.Add(fmlyy);
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
            Family fmlyss = _con.Family
                .Include(c => c.Cast).Where(x => x.Id == id).FirstOrDefault();
            if (fmlyss == null)
            {

            }
            return View(fmlyss);
        }

        public ActionResult Edit(int? id)
        {
            List<SelectListItem> Familylist2 = new List<SelectListItem>();

            var family = _con.Cast.ToList();
            Familylist2.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var item in family)
            {
                Familylist2.Add(new SelectListItem() { Text = item.CastName.ToString(), Value = item.Id.ToString() });
            }
            if (id == null)
            {

            }
            Family fms= _con.Family.Find(id);
            if (fms == null)
            {




            }
            ViewBag.Familylist2 = Familylist2;
            return View(fms);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Family employee)
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


            Family family1 = _con.Family.Include(c => c.Cast).Where(x => x.Id == id).FirstOrDefault();
            if (family1 == null)
            {
            }
            return View(family1);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Family family2 = _con.Family.Find(id);
            _con.Family.Remove(family2);
            _con.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}