using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageOnline.Models;

namespace ManageOnline.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private MProductEntities _dbContext = new MProductEntities();

        // GET: Category
        public ActionResult Index()
        {
            return View(_dbContext.Categories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rs = _dbContext.Categories.Find(id);
            if (rs == null)
            {
                return HttpNotFound();
            }
            return View(rs);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category_Id,Category_Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rs = _dbContext.Categories.Find(id);
            if (rs == null)
            {
                return HttpNotFound();
            }
            return View(rs);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_Id,Category_Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(category).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        // GET: Product/Create
        public ActionResult Delete(int? id)
        {
            return View(_dbContext.Categories.FirstOrDefault(x=>x.Category_Id == id));
        }
        // GET: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rs = _dbContext.Categories.FirstOrDefault(x=>x.Category_Id == id);
            if (rs == null)
            {
                return HttpNotFound();
            }
            _dbContext.Categories.Remove(rs);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string name)
        {
            ViewBag.keyword = name;
            var rs = _dbContext.Categories.Where(x => x.Category_Name.Contains(name)).ToList();
            return View(rs);
        }

    }
}
