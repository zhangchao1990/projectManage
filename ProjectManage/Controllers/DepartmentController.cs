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
    public class DepartmentController : Controller
    {

        private ProjectContext db = new ProjectContext();
        //
        // GET: /Department/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadAllByPage(int page, int rows)
        {
            try
            {

                long total = 0;
                var list = db.Departments.OrderBy(a => a.CreateTime).Skip((page - 1) * rows).Take(rows).Select(a => new { a.DeptName,a.Id,a.Memo });
                total = db.Departments.Count();

                var result = new { total = total, rows = list.ToList() };
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //
        // GET: /Department/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(Department department)
        {
            try
            {
                // TODO: Add insert logic here

                department.CreateTime = DateTime.Now;
                db.Departments.Add(department);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Department/Edit/5

        public ActionResult Edit(int id)
        {
            var dept = db.Departments.Find(id);
            return View(dept);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(Department dept)
        {
            db.Entry(dept).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Department/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Department/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList)
        {
            foreach (var id in idList)
            {
                var obj = db.Departments.Find(id);
                db.Departments.Remove(obj);
                db.SaveChanges();
            }
            return Json(new { IsSuccess = true, Message = "保存成功" });
        }

        public ActionResult GetDepartmentList() 
        {
            var list = from t in db.Departments orderby t.Id select new { id = t.Id, text = t.DeptName };
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
