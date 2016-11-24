using Newtonsoft.Json;
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
    public class PurchaseOrderController : Controller
    {

        private ProjectContext db = new ProjectContext();

        //
        // GET: /PurchaseOrder/

        public ActionResult Index()
        {
            ViewBag.page = Request.Params["page"];
            ViewBag.rows = Request.Params["rows"];
            return View();
        }

        public ActionResult LoadDetailPage(int id, int page, int rows)
        {
            try
            {
                
                long total = 0;
                var list = db.PurchaseDetail.OrderBy(a => a.PurchaseOrder.PurchaseTime).OrderBy(a => a.Status).Where(a => a.PurchaseOrder.Contract.Id == id).Select(a => new
                {
                    a.Id,
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
                    a.Brand,
                    a.Status,
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


        public ActionResult LoadContractDetail(int id, int page, int rows) 
        {
            
            long total = 0;
            var list = db.ContractDetails.Where(a => a.Contract.Id == id).OrderBy(a => a.Id).Select(a => new 
            {
                
                a.Brand,
                a.Count,
                a.EquipmentMaterialName,
                a.Memo,
                a.PriceSum,
                a.Specifications,
                a.Unit,
                a.UnitPrice
            });
            total = list.Count();
            list = list.Skip((page - 1) * rows).Take(rows);
            var result = new { total = total, rows = list.ToList() };
            return Json(result);
        }

        //
        // GET: /PurchaseOrder/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PurchaseOrder/Create

        public ActionResult Create()
        {
            ViewBag.page = Request.Params["page"];
            ViewBag.rows = Request.Params["rows"];
            ViewBag.contractId = Request.Params["contractId"];
            return View();
        }

        //
        // POST: /PurchaseOrder/Create

        [HttpPost]
        public ActionResult Create(PurchaseOrder model, string PurchaseOrderDetail,int ContractId)
        {
            try
            {
   
                model.Contract = db.Contracts.Find(ContractId);
                model.PurchaseDetails = JsonConvert.DeserializeObject<List<PurchaseDetail>>(PurchaseOrderDetail);
                model.PurchaseDetails.ToList().ForEach(a=>a.Status =1);
                db.PurchaseOrder.Add(model);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /PurchaseOrder/Edit/5

        public ActionResult Edit(int id)
        {
            try
            {
                var model = db.PurchaseDetail.Find(id).PurchaseOrder;
                var list = model.PurchaseDetails.Select(a => new
                {
                    a.Id,
                    a.MaterialNum,
                    a.Memo,
                    a.PriceSum,
                    a.Receiver,
                    a.ReceiveTime,
                    a.Specifications,
                    a.Unit,
                    a.UnitPrice,
                    a.EquipmentMaterialName,
                    a.Count,
                    a.Brand,
                    a.ArrivalTime,
                    a.ArrivalAddress
                });
                ViewBag.PurchaseDetail = JsonConvert.SerializeObject(list);
                return View(model);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
            
        }

        //
        // POST: /PurchaseOrder/Edit/5

        [HttpPost]
        public ActionResult Edit(PurchaseOrder model,int PurchaseId,string PurchaseOrderDetail, int ContractId)
        {
            try
            {
                var oldmodel = db.PurchaseOrder.Find(PurchaseId);
                var oldPurchaseDetails = oldmodel.PurchaseDetails.ToList();
                foreach (var obj in oldPurchaseDetails)
                {
                    db.PurchaseDetail.Remove(obj);
                }
                var list = JsonConvert.DeserializeObject<List<PurchaseDetail>>(PurchaseOrderDetail);
                oldmodel.PurchaseDetails = list;
                db.Entry(oldmodel).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /PurchaseOrder/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PurchaseOrder/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList)
        {
            try
            {
                foreach (var id in idList)
                {
                    var obj = db.PurchaseDetail.Find(id);
                    db.PurchaseDetail.Remove(obj);
                    db.SaveChanges();
                }
                return Json(new { IsSuccess = true, Message = "保存成功" });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeletePurchaseOrder(IList<int> idList)
        {
            try
            {
                foreach (var id in idList)
                {
                    var obj = db.PurchaseOrder.Find(id);
                    db.PurchaseOrder.Remove(obj);
                    db.SaveChanges();
                }
                return Json(new { IsSuccess = true, Message = "保存成功" });
            }
            catch(Exception ex)
            {

                return Json(new { IsSuccess = false, Message = "该采购单下存在采购明细！" });
            }
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
