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
    public class ToolController : Controller
    {

        private ProjectContext db = new ProjectContext();

        //
        // GET: /Tool/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadAllByPage(int page, int rows) 
        {
            try
            {
                var toolName = Request.Params["ToolName"];
                var borrowPerson = Request.Params["BorrowPerson"];
                long total = 0;
                var list = db.Tools.OrderBy(a=>a.ToolName).ThenByDescending(a => a.BorrowDate).Skip((page - 1) * rows).Take(rows);

                if (!string.IsNullOrEmpty(toolName)) 
                {
                    list = list.Where(a=>a.ToolName.Contains(toolName));
                }
                if (!string.IsNullOrEmpty(borrowPerson))
                {
                    list = list.Where(a => a.ToolName.Contains(borrowPerson));
                }
                total = list.Count();

                var result = new { total = total, rows = list.ToList() };
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //
        // GET: /Tool/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Tool/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tool/Create

        [HttpPost]
        public ActionResult Create(Tool entity)
        {
            try
            {
                db.Tools.Add(entity);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Tool/Edit/5

        public ActionResult Edit(int id)
        {
            var model = db.Tools.Find(id);
            return View(model);
        }

        //
        // POST: /Tool/Edit/5

        [HttpPost]
        public ActionResult Edit(Tool model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Tool/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tool/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList)
        {
            foreach (var id in idList)
            {
                var tool = db.Tools.Find(id);
                db.Tools.Remove(tool);
                db.SaveChanges();
            }
            return Json(new { IsSuccess = true, Message = "保存成功" });
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
