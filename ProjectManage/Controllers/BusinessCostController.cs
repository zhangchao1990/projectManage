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
    public class BusinessCostController : Controller
    {

        private ProjectContext db = new ProjectContext();
        //
        // GET: /BusinessCost/

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

                var contractName = Request.Params["contractName"];
                var contractId = Request.Params["Id"];
                long total = 0;
                var list = db.BusinessCosts.OrderBy(a => a.Id).Select(a => new
                    {
                        a.Id,
                        a.CostAmount,
                        ContractNum = a.Contract.ContractNum,
                        ContractName = a.Contract.ContractName,
                        a.CostName,
                        a.Memo,
                        a.Subtotal
                    });
                if (!string.IsNullOrEmpty(contractName))
                {
                    list = list.Where(a => a.ContractName.Contains(contractName));
                }
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
        // GET: /BusinessCost/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BusinessCost/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BusinessCost/Create

        [HttpPost]
        public ActionResult Create(BusinessCostVo entity)
        {
            try
            {
                var obj = new BusinessCost
                {
                    Contract = db.Contracts.Find(entity.ContractId),
                    CostAmount = entity.CostAmount,
                    CostName = entity.CostName,
                    Memo = entity.Memo,
                    Subtotal = entity.CostAmount * (1 + ManageHelper.Rate)
                };
                db.BusinessCosts.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /BusinessCost/Edit/5

        public ActionResult Edit(int id)
        {
            var model = db.BusinessCosts.Find(id);
            return View(model);
        }

        //
        // POST: /BusinessCost/Edit/5

        [HttpPost]
        public ActionResult Edit(BusinessCost model, int ContractId)
        {
            try
            {
                model.Contract = db.Contracts.Find(ContractId);
                model.Subtotal = model.CostAmount * (1 + ManageHelper.Rate);
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
        // GET: /BusinessCost/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BusinessCost/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList)
        {
            foreach (var id in idList)
            {
                BusinessCost businessCost = db.BusinessCosts.Find(id);
                db.BusinessCosts.Remove(businessCost);
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
