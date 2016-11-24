using Newtonsoft.Json;
using ProjectManage.DAL;
using ProjectManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProjectManage.Controllers
{
    public class RoleMenuController : Controller
    {


        private ProjectContext db = new ProjectContext();

        //
        // GET: /RoleMenu/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /RoleMenu/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RoleMenu/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RoleMenu/Create

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

        //
        // GET: /RoleMenu/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RoleMenu/Edit/5

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
        // GET: /RoleMenu/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RoleMenu/Delete/5

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

        /// <summary>
        /// 获取除跟角色节点外的所有角色节点
        /// </summary>
        /// <returns>返回角色树</returns>
        public ActionResult GetRoleTree()
        {

            StringBuilder strNode = new StringBuilder();
            strNode.Append("[{id:'0',pId:'-1' ,name:'角色类别',isParent:true,open:true}");
            var roleNodeList = db.Roles.ToList();
            foreach (var roleModel in roleNodeList)
            {
                strNode.Append(",");
                strNode.Append("{id:'" + roleModel.Id + "',pId:'0',name:'" + roleModel.RoleName + "' }");
            }
            strNode.Append("]");

            return Content(strNode.ToString());
        }


        /// <summary>
        /// 获取角色权限分配树
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsid">角色ID</param>
        /// <returns></returns>
        public ActionResult GetPermissionTree(string id, string roleId)
        {
            string permissionNodes = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        permissionNodes = GetModuleNode(int.Parse(roleId));
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Error("加载角色权限树失败：", ex);
            }
            return Content(permissionNodes);
        }

        public string GetModuleNode(int roleId)
        {
            StringBuilder strNode = new StringBuilder();
            var menus = db.Menus.ToList();
            var roleMenus = db.RoleMenus.Where(a => a.RoleId == roleId).ToList();
            strNode.Append("[");
            for (int i = 0; i < menus.Count; i++)
            {
                var menu = menus[i];
                if (roleMenus.FindAll(a => a.MenuId == menu.Id).Count > 0)
                {
                    strNode.Append("{id:" + menu.Id + ",pId:" + menu.PId + ",name:'" + menu.Title + "',checked:true,open:true }");

                }
                else
                {
                    strNode.Append("{id:" + menu.Id + ",pId:" + menu.PId + ",name:'" + menu.Title + "',open:true }");
                }
                if (i < menus.Count - 1)
                {
                    strNode.Append(",");
                }

            }
            strNode.Append("]");

            return strNode.ToString();
        }

        public ActionResult CreateRolePermission(int roleId, string menus) 
        {
            try
            {
                // TODO: Add insert logic here

                var listMenus = JsonConvert.DeserializeObject<List<Menu>>(menus);

                db.Database.ExecuteSqlCommand("delete from RoleMenus where RoleId={0}",roleId);

                foreach (var menu in listMenus) 
                {
                    var roleMenu = new RoleMenu { RoleId=roleId,MenuId= menu.Id};
                    db.RoleMenus.Add(roleMenu);
                    
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
