using ProjectManage.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManage.Models;
using ProjectManage.Comm;
using System.Data.Entity;

namespace ProjectManage.Controllers
{
    public class OutsourcingCostController : Controller
    {

        private ProjectContext db = new ProjectContext();

        //
        // GET: /OutsourcingCost/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexView(int id)
        {
            ViewBag.ContractName = db.Contracts.Find(id).ContractName;
            ViewBag.Id = id;
            return View();
        }

        public ActionResult LoadAllByPage(int page, int rows)
        {
            try
            {
                var outsourcingPeopleName = Request.Params["outsourcingPeopleName"];
                var contractName = Request.Params["ContractName"];
                long total = 0;
                var list = db.OutsourcingCosts.OrderBy(a => a.BeginTime) as IQueryable<OutsourcingCost>;

                if (!String.IsNullOrEmpty(outsourcingPeopleName))
                {
                    list = list.Where(a => a.OutsourcingPeopleName.Contains(outsourcingPeopleName));
                }
                if (!string.IsNullOrEmpty(contractName))
                {
                    list = list.Where(a => a.Contract.ContractName.Contains(contractName));
                }
                var footer = new List<object> { new { AuditAmount = list.Count() > 0 ? list.Sum(a => a.AuditAmount) : 0, Subtotal = list.Count() > 0 ? list.Sum(a => a.Subtotal) : 0, ContractNum = "合计" } };
                var newlist = list.Skip((page - 1) * rows).
                    Take(rows).Select(a => new
                    {
                        a.Id,
                        a.OutsourcingPeopleName,
                        a.BeginTime,
                        ContractNum = a.Contract.ContractNum,
                        ContractName = a.Contract.ContractName,
                        a.EndTime,
                        a.Days,
                        a.Memo,
                        a.Pay,
                        a.Subtotal,
                        a.AuditAmount

                    });


                total = list.Count();

                var result = new { total = total, rows = newlist.ToList(), footer = footer };
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //
        // GET: /OutsourcingCost/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /OutsourcingCost/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /OutsourcingCost/Create

        [HttpPost]
        public ActionResult Create(OutsourcingCostVo entity)
        {
            try
            {
                var obj = new OutsourcingCost
                {
                    Contract = db.Contracts.Find(entity.ContractId),
                    BeginTime = entity.BeginTime,
                    EndTime = entity.EndTime,
                    Days = entity.Days,
                    Pay = entity.Pay,
                    OutsourcingPeopleName = entity.OutsourcingPeopleName,
                    Memo = entity.Memo,
                    Subtotal = entity.Days * entity.Pay * (1 + ManageHelper.Rate)
                };
                obj.AuditAmount = obj.Subtotal;
                db.OutsourcingCosts.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /OutsourcingCost/Edit/5

        public ActionResult Edit(int id)
        {
            var model = db.OutsourcingCosts.Find(id);
            return View(model);
        }

        //
        // POST: /OutsourcingCost/Edit/5

        [HttpPost]
        public ActionResult Edit(OutsourcingCost model, int ContractId)
        {
            try
            {
                model.Contract = db.Contracts.Find(ContractId);
                model.Subtotal = model.Days * model.Pay * (1 + ManageHelper.Rate);
                model.AuditAmount = model.Subtotal;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Audit(int id)
        {
            var model = db.OutsourcingCosts.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Audit(OutsourcingCost model)
        {

            try
            {
                var entity = db.OutsourcingCosts.Find(model.Id);
                entity.AuditAmount = model.AuditAmount;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /OutsourcingCost/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /OutsourcingCost/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList)
        {
            foreach (var id in idList)
            {
                OutsourcingCost obj = db.OutsourcingCosts.Find(id);
                db.OutsourcingCosts.Remove(obj);
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
