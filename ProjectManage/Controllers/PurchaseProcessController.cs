using ProjectManage.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManage.Controllers
{
    public class PurchaseProcessController : Controller
    {


        private ProjectContext db = new ProjectContext();


        //
        // GET: /PurchaseProcess/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /PurchaseProcess/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PurchaseProcess/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PurchaseProcess/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Confirm(IList<int> idList)
        {
            try
            {


                foreach (var id in idList)
                {
                    var model = db.PurchaseDetail.Find(id);
                    model.Status = 2;
                    db.Entry(model).State = EntityState.Modified; ;
                    db.SaveChanges();
                }
                return Json(new { IsSuccess = true, Message = "保存成功" });
            }
            catch(Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
        }

        //
        // GET: /PurchaseProcess/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PurchaseProcess/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PurchaseProcess/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PurchaseProcess/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
