using ProjectManage.DAL;
using ProjectManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManage.Controllers {
    public class EmployerController : Controller {


        private ProjectContext db = new ProjectContext();
        //
        // GET: /Employer/

        public ActionResult Index() {
            return View();
        }

        public ActionResult LoadAllByPage(int page, int rows) {
            try {

                long total = 0;
                var list = db.Employers.OrderBy(a => a.Id).Select(a => new {
                    a.Id,
                    a.Name,
                    a.Memo,
                    DeptName = a.Department.DeptName,
                    DeptId = a.Department.Id,
                    RoleId = a.Role.Id,
                    RoleName = a.Role.RoleName
                });
                total = list.Count();
                list = list.Skip((page - 1) * rows).Take(rows);
                var result = new { total = total, rows = list.ToList() };
                return Json(result);
            } catch (Exception ex) {

                throw ex;
            }
        }

        //
        // GET: /Employer/Details/5

        public ActionResult Details(int id) {
            return View();
        }

        //
        // GET: /Employer/Create

        public ActionResult Create() {
            return View();
        }

        //
        // POST: /Employer/Create

        [HttpPost]
        public ActionResult Create(Employer model, int DepartmentId, int RoleId) {
            try {
                // TODO: Add insert logic here
                var count = db.Employers.Where(a => a.UserCount == model.UserCount).Count();
                if (count > 0) {
                    return Json(new { IsSuccess = false, Message = "存在相同的账号！" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                model.Department = db.Departments.Find(DepartmentId);
                model.Role = db.Roles.Find(RoleId);
                db.Employers.Add(model);

                db.SaveChanges();
            } catch (Exception ex) {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Employer/Edit/5

        public ActionResult Edit(int id) {
            var model = db.Employers.Find(id);

            return View(model);
        }

        //
        // POST: /Employer/Edit/5

        [HttpPost]
        public ActionResult Edit(Employer model, int DepartmentId, int RoleId) {
            try {

                var oldmodel = db.Employers.Where(a => a.Id == model.Id).Include(a => a.Department).Include(a => a.Role).Single();
                oldmodel.Department = db.Departments.Find(DepartmentId);
                oldmodel.Role = db.Roles.Find(RoleId);
                oldmodel.Name = model.Name;
                oldmodel.Password = model.Password;
                oldmodel.UserCount = model.UserCount;
                db.Entry(oldmodel).State = EntityState.Modified;
                db.SaveChanges();

            } catch (Exception ex) {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Employer/Delete/5

        public ActionResult Delete(int id) {
            return View();
        }

        //
        // POST: /Employer/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList) {
            foreach (var id in idList) {
                var obj = db.Employers.Find(id);
                db.Employers.Remove(obj);
                db.SaveChanges();
            }
            return Json(new { IsSuccess = true, Message = "保存成功" });
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Employer model) {

            var obj = db.Employers.Include("Role").Where(a => a.UserCount == model.UserCount && a.Password == model.Password).FirstOrDefault();
            if (obj != null) {
                Session["user"] = obj;

                return Redirect("/Home/Index");
            }
            else {
                ViewBag.ErrorMsg = "登陆失败，用户名密码不存在！";
                return View();
            }

        }



        public ActionResult GetEmployersList() {

            var user = Session["user"] as Employer;
            if (user == null) {
                return Redirect("/Home/Index");
            }
            var list = from t in db.Employers select new ComboxItem { id = t.Name, text = t.Name, selected = false };
            List<ComboxItem> result;
            if (user.Role.RoleName == "总经理" || user.Role.RoleName=="系统管理员") {

                result = list.ToList();
            }
            else 
            {
                result = list.Where(t => t.id == user.Name).ToList();
            }
            result.ForEach(a => { if (a.text == user.Name) { a.selected = true; } });


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut() {
            Session["user"] = null;
            return Redirect("/Employer/Login");
        }


        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
