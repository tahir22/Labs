using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
using Microsoft.EntityFrameworkCore;
using TahirMvc123.Models;

namespace TahirMvc123.Controllers
{
    public class VlilageController : Controller
    {
        // GET: Vlilage
        private readonly MvcDBContext vDb;

        public VlilageController(MvcDBContext _db)
        {


            vDb = _db;
            
        }
        public ActionResult Index()
        {

            return View(vDb.Vlilage.ToList());
        }
 
        // GET: Vlilage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vlilage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vlilage vlilag)
        {
            try
            {
                // TODO: Add insert logic here
                vlilag.Date = DateTime.Now;
                vDb.Vlilage.Add(vlilag);
                vDb.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Vlilage/Edit/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {

            }
            Vlilage villags = vDb.Vlilage.Where(x => x.VlilageId == id).FirstOrDefault();
            if (villags == null)
            {

            }
            return View(villags);
        }

        public ActionResult Edit(int? id)
        {

            Vlilage cast = vDb.Vlilage.Find(id);
            if (cast == null)
            {




            }
           
            return View(cast);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vlilage employee)
        {
            if (ModelState.IsValid)
            {
                vDb.Entry(employee).State = EntityState.Modified;
                vDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {

            }


            Vlilage village = vDb.Vlilage.Where(x => x.VlilageId == id).FirstOrDefault();
            if (village == null)
            {
            }
            return View(village);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vlilage cast = vDb.Vlilage.Find(id);
            vDb.Vlilage.Remove(cast);
            vDb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}