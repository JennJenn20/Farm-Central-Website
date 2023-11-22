using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FarmingWebsite.Models;

namespace FarmingWebsite.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class farmersProductsController : Controller
    {
        
        private AuthAppDBEntities db = new AuthAppDBEntities();

        // GET: farmersProducts
        public ActionResult Index()
        {
            var farmersProducts = db.farmersProducts.Include(f => f.Farmer).Include(f => f.Product);
            return View(farmersProducts.ToList());
        }

        // GET: farmersProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farmersProduct farmersProduct = db.farmersProducts.Find(id);
            if (farmersProduct == null)
            {
                return HttpNotFound();
            }
            return View(farmersProduct);
        }

        // GET: farmersProducts/Create
        public ActionResult Create()
        {
            ViewBag.farmersUsername = new SelectList(db.Farmers, "FarmerUsername", "FarmerName");
            ViewBag.productID = new SelectList(db.Products, "productID", "productName");
            return View();
        }

        // POST: farmersProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "farmersProduct1,farmersUsername,productID")] farmersProduct farmersProduct)
        {
            if (ModelState.IsValid)
            {
                db.farmersProducts.Add(farmersProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.farmersUsername = new SelectList(db.Farmers, "FarmerUsername", "FarmerName", farmersProduct.farmersUsername);
            ViewBag.productID = new SelectList(db.Products, "productID", "productName", farmersProduct.productID);
            return View(farmersProduct);
        }

        // GET: farmersProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farmersProduct farmersProduct = db.farmersProducts.Find(id);
            if (farmersProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.farmersUsername = new SelectList(db.Farmers, "FarmerUsername", "FarmerName", farmersProduct.farmersUsername);
            ViewBag.productID = new SelectList(db.Products, "productID", "productName", farmersProduct.productID);
            return View(farmersProduct);
        }

        // POST: farmersProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "farmersProduct1,farmersUsername,productID")] farmersProduct farmersProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farmersProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.farmersUsername = new SelectList(db.Farmers, "FarmerUsername", "FarmerName", farmersProduct.farmersUsername);
            ViewBag.productID = new SelectList(db.Products, "productID", "productName", farmersProduct.productID);
            return View(farmersProduct);
        }

        // GET: farmersProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            farmersProduct farmersProduct = db.farmersProducts.Find(id);
            if (farmersProduct == null)
            {
                return HttpNotFound();
            }
            return View(farmersProduct);
        }

        // POST: farmersProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            farmersProduct farmersProduct = db.farmersProducts.Find(id);
            db.farmersProducts.Remove(farmersProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
