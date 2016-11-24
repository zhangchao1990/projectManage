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
    public class PurchaseDetailController : Controller
    {

        private ProjectContext db = new ProjectContext();
        //
        // GET: /PurchaseDetail/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexView()
        {
            return View();
        }

        public ActionResult IndexQueryView(int id)
        {
            ViewBag.ContractName = db.Contracts.Find(id).ContractName;
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// 获取采购申请单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult LoadAllByPage(int id, int page, int rows)
        {
            long total = 0;

            var list = db.PurchaseOrder.OrderBy(a => a.Contract.Id).AsQueryable();
            if (id != 0)
            {

                list = list.Where(a => a.Contract.Id == id);
            }

            var listresult = list.Select(a => new
            {
                ContractName = a.Contract.ContractName,
                ContractNum = a.Contract.ContractNum,
                SalesManagers = a.Contract.SalesManagers,
                PurchaseNum = a.PurchaseNum,
                PurchaseTime = a.PurchaseTime,
                a.Id,
                a.ProjectManager,
                a.Memo,
                Status = db.PurchaseDetail.Where(b => b.PurchaseOrder.Id == a.Id).Count() > 0 ? db.PurchaseDetail.Where(b => b.PurchaseOrder.Id == a.Id && b.Status <= 2).Count() : 0,
                a.PurchaseApproved,
                a.PurchasePerson

            }).Skip((page - 1) * rows).Take(rows);



            total = list.Count();

            var result = new { total = total, rows = listresult.ToList() };
            return Json(result);
        }

        /// <summary>
        /// 获取采购明细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult LoadDetailPage(int id, int page, int rows)
        {
            try
            {

                long total = 0;
                var list = db.PurchaseDetail.OrderBy(a => a.PurchaseOrder.PurchaseTime).OrderBy(a=>a.Status).Where(a => a.PurchaseOrder.Id == id).Select(a => new
                {
                    a.Id,
                    PurchaseOrderId = a.PurchaseOrder.Id,
                    a.MaterialNum,
                    a.Memo,
                    a.PriceSum,
                    PurchaseTime = a.PurchaseOrder.PurchaseTime,
                    PurchaseNum = a.PurchaseOrder.PurchaseNum,
                    PurchasePerson = a.PurchaseOrder.PurchasePerson,
                    PurchaseApproved = a.PurchaseOrder.PurchaseApproved,
                    a.Receiver,
                    a.ReceiveTime,
                    a.Specifications,
                    a.Unit,
                    a.UnitPrice,
                    a.EquipmentMaterialName,
                    a.Count,
                    a.Status,
                    a.Brand,
                    a.ArrivalTime,
                    a.ArrivalAddress
                });
                total = list.Count();
                list = list.Skip((page - 1) * rows).Take(rows);
                var result = new { total = total, rows = list.ToList() };
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //
        // GET: /PurchaseDetail/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PurchaseDetail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PurchaseDetail/Create

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
        // GET: /PurchaseDetail/Edit/5

        public ActionResult Edit(int id)
        {
            var model = db.PurchaseDetail.Find(id);
            return View(model);
        }

        //
        // POST: /PurchaseDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(PurchaseDetail model,int PurchaseOrderId)
        {
            try
            {

                model.PurchaseOrder = db.PurchaseOrder.Find(PurchaseOrderId);
                //model.TaxRate = 0;
                model.Status = 3;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /PurchaseDetail/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PurchaseDetail/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList)
        {
            try
            {


                foreach (var id in idList)
                {
                    var model = db.PurchaseDetail.Find(id);
                    db.PurchaseDetail.Remove(model);
                    db.SaveChanges();
                }
                return Json(new { IsSuccess = true, Message = "保存成功" });
            }
            catch (Exception ex)
            {

                return Json(new { IsSuccess = false, Message = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
