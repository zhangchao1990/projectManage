using ProjectManage.DAL;
using ProjectManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProjectManage.Controllers
{
    public class HomeController : Controller
    {
        private ProjectContext db = new ProjectContext();

        public ActionResult Index()
        {

            if (Session["user"] == null)
            {
                return Redirect("/Employer/Login");
            }
            var model = Session["user"] as Employer;
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerateMenus()
        {
            if (Session["user"] == null)
            {
                return Redirect("/Employer/Login");
            }
            var model = Session["user"] as Employer;
            var roleMenus = db.RoleMenus.Include("Menu").Where(a => a.RoleId == model.Role.Id && a.Menu.PId != 0).ToList();
            var Menus = db.Menus.Where(a => a.PId == 0).OrderBy(a=>a.OrderNum).ToList();

            StringBuilder str = new StringBuilder();
            foreach (var menu in Menus)
            {
                if (roleMenus.FindAll(a => a.Menu.PId == menu.Id).Count > 0)
                {
                    str.Append("<div title=\"" + menu.Title + "\" style=\"overflow: auto;\">");
                    str.Append(" <table border=\"0\" cellspacing=\"1\" cellpadding=\"1\" align=\"center\">");
                    foreach (var roleMenu in roleMenus.Where(a=>a.Menu.PId == menu.Id))
                    {
                        str.Append("<tr>");
                        str.Append("<td height=\"23\">");
                        str.Append(" <a href=\"#\" onclick='showTab(\"" + roleMenu.Menu.Url + "\",$(this).html());'>" + roleMenu.Menu.Title + "</a>");
                        str.Append("</td>");
                        str.Append("</tr>");
                    }
                    str.Append("</table>");
                    str.Append("</div>");
                }
            }
            return Content(str.ToString());

        }




        public ActionResult ChangedPassword(string password, string oldPassword)
        {
            try
            {
                if (Session["user"] == null)
                {
                    return Redirect("/Employer/Login");
                }
                var model = Session["user"] as Employer;
                var count = db.Employers.Where(a => a.UserCount == model.UserCount && a.Password == oldPassword).Count();
                if (count > 0)
                {
                    model.Password = password;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "密码不对" });
                }
                return Json(new { IsSuccess = true, Message = "修改成功" });
            }
            catch (Exception ex) 
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
                
        }

        //protected override void Dispose(bool disposing)
        //{
        //    //db.Dispose();
        //    //base.Dispose(disposing);
        //}
    }
}
