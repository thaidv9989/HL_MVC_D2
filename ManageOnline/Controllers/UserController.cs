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
    public class UserController : Controller
    {
        private MProductEntities _dbContext = new MProductEntities();

        // GET: User
        public ActionResult Index()
        {
            return View(_dbContext.Users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rs = _dbContext.Users.Find(id);
            if (rs == null)
            {
                return HttpNotFound();
            }
            return View(rs);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,Password,FirstName,LastName,DOB,State,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rs = _dbContext.Users.Find(id);
            if (rs == null)
            {
                return HttpNotFound();
            }
            return View(rs);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Password,FirstName,LastName,DOB,State,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Product/Delete
        public ActionResult Delete(int? id)
        {

            return View(_dbContext.Users.FirstOrDefault(x=>x.UserID == id));
        }
        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var rs = _dbContext.Users.Find(id);
            if(rs == null)
            {
                return HttpNotFound();
            }
            _dbContext.Users.Remove(rs);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Search(string name)
        {
            ViewBag.keyword = name;
            var rs = _dbContext.Users.Where(x => x.UserName.Contains(name)).ToList();
            return View(rs);
        }


    }
}
