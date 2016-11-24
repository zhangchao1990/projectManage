using ProjectManage.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManage.Controllers
{
    public class CommModelController : Controller
    {

        private ProjectContext db = new ProjectContext();
        //
        // GET: /CommModel/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CommModel/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CommModel/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CommModel/Create

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
        // GET: /CommModel/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CommModel/Edit/5

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
        // GET: /CommModel/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CommModel/Delete/5

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
        /// 获取客户单位
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCustmersDept()
        {
            var list = from t in db.CommModels where t.Type == "2" select new { id = t.Name, text = t.Value };
            var result = list.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取税率
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTaxRate()
        {
            var list = from t in db.CommModels where t.Type == "1" select new { id = t.Name, text = t.Name };
            var result = list.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取项目状态
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProjectStatus()
        {
            var list = from t in db.CommModels where t.Type == "3" select new { id = t.Name, text = t.Value };
            var result = list.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetContractNumList() 
        {
            var list = from t in db.Contracts.OrderByDescending(a => a.ContractNum).Skip(0).Take(10) select new { id = t.ContractNum, text = t.ContractNum };
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
