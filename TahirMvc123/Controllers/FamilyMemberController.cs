using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TahirMvc123.Models;

namespace TahirMvc123.Controllers
{

   [Authorize]

    public class FamilyMemberController : Controller
    {
       
        private readonly MvcDBContext _con;

        public FamilyMemberController(MvcDBContext _db)
        {


            _con = _db;

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            ViewBag.msg = TempData["Msg"];
            ViewBag.type = TempData["type"];
            var ChildrenName2 = "";
            var fmly = _con.FamilyMember
                .Include(c => c.Family)
                .Include(x => x.Parent)
                .Include(v => v.ChildrenList)
                .AsNoTracking().ToList();

            foreach (var item in fmly)
            {
                foreach (var item2 in item.ChildrenList)
                {
                    ChildrenName2 += item2.MemberName + ", ";
                }
                if (ChildrenName2 != "")
                {
                    item.ChildrenName = ChildrenName2.Remove(ChildrenName2.Length - 2);

                    ChildrenName2 = "";
                }
            }

            return View(fmly);
        }



        public ActionResult Create()
        {
            List<SelectListItem> Familylist = new List<SelectListItem>();
            List<SelectListItem> parentList = new List<SelectListItem>();

            var Family = _con.Family.ToList();
            var parentLists = _con.FamilyMember.ToList();

            Familylist.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var item in Family)
            {
                Familylist.Add(new SelectListItem() { Text = item.FamilyName.ToString(), Value = item.Id.ToString() });
            }
            parentList.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var itemm in parentLists)
            {
                parentList.Add(new SelectListItem() { Text = itemm.MemberName.ToString(), Value = itemm.Id.ToString() });
            }
            ViewBag.parentid = parentList;
            ViewBag.Familylist = Familylist;
            return View();
        }

        // POST: Vlilage/Create
        [HttpPost]

        public ActionResult Create(FamilyMember fmlyy)
        {
            try
            {
             // TODO: Add insert logic here
                  //cast.Date = DateTime.Now;
                    _con.FamilyMember.Add(fmlyy);
                    _con.SaveChanges();
                    TempData["msg"] = "Save successfully";
                    TempData["type"] = 1;
                    return RedirectToAction(nameof(Index));
                


        }
            catch
            {


            


            TempData["msg"] = "Not Save  successfully ";
                TempData["type"] = 0;
                return RedirectToAction(nameof(Index));
            }
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {

            }
            FamilyMember fmlyss = _con.FamilyMember
                .Include(c => c.Family).Where(x => x.Id == id).FirstOrDefault();
            if (fmlyss == null)
            {

            }
            return View(fmlyss);
        }

        public ActionResult Edit(int? id)
        {
            List<SelectListItem> Familylist2 = new List<SelectListItem>();

            var family = _con.Family.ToList();
            Familylist2.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var item in family)
            {
                Familylist2.Add(new SelectListItem() { Text = item.FamilyName.ToString(), Value = item.Id.ToString() });
            }
            if (id == null)
            {

            }
            FamilyMember familyMembers = _con.FamilyMember.Find(id);
            if (familyMembers == null)
            {




            }
            ViewBag.Familylist2 = Familylist2;
            return View(familyMembers);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FamilyMember employee)
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


            FamilyMember cast = _con.FamilyMember.Include(c => c.Family).Where(x => x.Id == id).FirstOrDefault();
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
            FamilyMember FamilyMemberss = _con.FamilyMember.Find(id);
            _con.FamilyMember.Remove(FamilyMemberss);
            _con.SaveChanges();
            return RedirectToAction("Index");
        }





        public ActionResult CreateAll()
        {
            List<SelectListItem> Familylist = new List<SelectListItem>();
            List<SelectListItem> parentList = new List<SelectListItem>();

            var Family = _con.Family.ToList();
            var parentLists = _con.FamilyMember.ToList();

            Familylist.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var item in Family)
            {
                Familylist.Add(new SelectListItem() { Text = item.FamilyName.ToString(), Value = item.Id.ToString() });
            }
            parentList.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var itemm in parentLists)
            {
                parentList.Add(new SelectListItem() { Text = itemm.MemberName.ToString(), Value = itemm.Id.ToString() });
            }
            ViewBag.parentid = parentList;
            ViewBag.Familylist = Familylist;
            return View();
        }

        // POST: Vlilage/Create
        [HttpPost]

        public ActionResult CreateAll(FamilyMember fmlyy)
        {
            try
            {
                // TODO: Add insert logic here
                //cast.Date = DateTime.Now;
                _con.FamilyMember.Add(fmlyy);
                _con.SaveChanges();
                TempData["msg"] = "Save successfully";
                TempData["type"] = 1;
                return RedirectToAction(nameof(Index));



            }
            catch
            {





                TempData["msg"] = "Not Save  successfully ";
                TempData["type"] = 0;
                return RedirectToAction(nameof(Index));
            }
        }







    }
}