using ProjectManage.DAL;
using ProjectManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManage.Controllers
{
    public class RoleController : Controller
    {

        private ProjectContext db = new ProjectContext();
        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadAllByPage(int page, int rows)
        {
            try
            {

                long total = 0;
                var list = db.Roles.OrderBy(a => a.CreateTime).Skip((page - 1) * rows).Take(rows).Select(a => new { a.RoleName, a.Id });
                total = db.Roles.Count();

                var result = new { total = total, rows = list.ToList() };
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //
        // GET: /Role/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(Role model)
        {
            try
            {
                // TODO: Add insert logic here

                model.CreateTime = DateTime.Now;
                db.Roles.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id)
        {
            var model = db.Roles.Find(id);
            return View(model);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(Role model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Role/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList)
        {
            foreach (var id in idList)
            {
                var obj = db.Roles.Find(id);
                db.Roles.Remove(obj);
                db.SaveChanges();
            }
            return Json(new { IsSuccess = true, Message = "保存成功" });
        }

        public ActionResult GetRoleList()
        {
            var list = from t in db.Roles orderby t.Id select new { id = t.Id, text = t.RoleName };
            var result = list.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
