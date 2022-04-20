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
    public class ProductController : Controller
    {
        private MProductEntities _dbContext = new MProductEntities();

        // GET: Product
        public ActionResult Index()
        {
           
            return View(_dbContext.Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rs = _dbContext.Products.FirstOrDefault(x=>x.Product_Id == id);
            if (rs == null)
            {
                return HttpNotFound();
            }
            return View(rs);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_Id,Product_Name,Product_Img,Description,Price,Category_Id,CreatedDate,UpdatedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Id = new SelectList(_dbContext.Categories, "Category_Id", "Category_Name", product.Category_Id);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rs = _dbContext.Products.FirstOrDefault(x => x.Product_Id == id);
            if (rs == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(_dbContext.Categories, "Category_Id", "Category_Name", rs.Category_Id);
            return View(rs);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_Id,Product_Name,Product_Img,Description,Price,Category_Id,CreatedDate,UpdatedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(_dbContext.Categories, "Category_Id", "Category_Name", product.Category_Id);
            return View(product);
        }
        // GET: Product/Delete
        public ActionResult Delete(int id)
        {
            var rs = _dbContext.Products.FirstOrDefault(x => x.Product_Id == id);
            return View(rs);
        }
        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var rs = _dbContext.Products.Find(id);
            if(rs == null)
            {
                return HttpNotFound();
            }
            _dbContext.Products.Remove(rs);
            return RedirectToAction("Index");
        }


        public ActionResult Search(string name)
        {
            ViewBag.keyword = name;
            var rs = _dbContext.Products.Where(x => x.Product_Name.Contains(name)).ToList(); 
            return View(rs);   
        }
    }
}
