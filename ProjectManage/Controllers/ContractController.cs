using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManage.Models;
using ProjectManage.DAL;
using Newtonsoft.Json;
using System.Data.Entity.SqlServer;

namespace ProjectManage.Controllers {
    public class ContractController : Controller {
        private ProjectContext db = new ProjectContext();



        public ActionResult LoadAllByPage(int page, int rows) {

            if (Session["user"] == null) {
                return Redirect("/Employer/Login");
            }
            var employer = Session["user"] as Employer;
            try {



                var salesManagers = Request.Params["SalesManagers"];
                var contractName = Request.Params["ContractName"];
                var year = Request.Params["Year"];
                long total = 0;
                var list = db.Contracts.OrderByDescending(a => a.ContractNum).Select(a =>
                    new ContractVo {
                        Id = a.Id,
                        ContractNum = a.ContractNum,
                        ContractName = a.ContractName,
                        SalesManagers = a.SalesManagers,
                        Memo = a.Memo,
                        CustmersDept = a.CustmersDept,
                        ProjectStatus = a.ProjectStatus,
                        EstimatedAmount = a.EstimatedAmount,
                        PayOrders = a.PayOrders,
                        OrderTime = a.OrderTime,
                        ContractCount = a.ContractCount,
                        ContractedCount =
                        (db.Reimbursements.Where(b => b.ReimbursementType == 0 && b.Contract.Id == a.Id).Count() > 0
                        ? db.Reimbursements.Where(b => b.ReimbursementType == 0 && b.Contract.Id == a.Id).Sum(b => b.AuditAmount) : 0)
                        + (db.OutsourcingCosts.Where(c => c.Contract.Id == a.Id).Count() > 0 ? db.OutsourcingCosts.Where(c => c.Contract.Id == a.Id).Sum(c => c.AuditAmount) : 0)
                        + (db.BusinessCosts.Where(d => d.Contract.Id == a.Id).Count() > 0 ? db.BusinessCosts.Where(d => d.Contract.Id == a.Id).Sum(d => d.CostAmount) : 0)
                        + (db.PurchaseDetail.Where(e => e.PurchaseOrder.Contract.Id == a.Id).Count() > 0 ? db.PurchaseDetail.Where(e => e.PurchaseOrder.Contract.Id == a.Id).Sum(e => e.PriceSum) : 0) ?? 0
                                       ,
                        BillingTime = a.BillingTime,
                        TaxRate = a.TaxRate,
                        OrderNum = a.OrderNum,
                        Status = db.PurchaseDetail.Where(b => b.PurchaseOrder.Contract.Id == a.Id).Count() > 0 ? db.PurchaseDetail.Where(b => b.PurchaseOrder.Contract.Id == a.Id && b.Status == 1).Count() : 0,
                        BillNum = a.BillNum

                    });

                if (!string.IsNullOrEmpty(salesManagers)) {
                    list = list.Where(a => a.SalesManagers.Contains(salesManagers));
                }
                else {
                    list = list.Where(a => a.SalesManagers.Contains(employer.Name));
                }
                if (!string.IsNullOrEmpty(contractName)) {
                    list = list.Where(a => a.ContractName.Contains(contractName));
                }
                if (!string.IsNullOrEmpty(year)) {
                    list = list.Where(a => a.OrderTime.ToString().Contains(year) || a.OrderTime == null);
                }

                var footer = new List<object> { new { ContractCount = list.Count() > 0 ? list.Sum(a => a.ContractCount) : 0, ContractedCount = list.Count() > 0 ? list.Sum(a => a.ContractedCount) : 0, ContractNum = "合计" } };
                var newlist = list.Skip((page - 1) * rows).Take(rows);

                total = list.Count();
                var rowList = newlist.ToList();
                rowList.ForEach(a => {
                    if (string.IsNullOrEmpty(a.PayOrders)) {
                        a.ArrivalCount = 0;

                    }
                    else {
                        a.ArrivalCount = JsonConvert.DeserializeObject<List<PayOrder>>(a.PayOrders).Sum(b => b.PayCount);
                    }
                    if (string.IsNullOrEmpty(a.PayOrders)) {
                        a.RemainCount = a.ContractCount;

                    }
                    else {
                        a.RemainCount = a.ContractCount - JsonConvert.DeserializeObject<List<PayOrder>>(a.PayOrders).Sum(b => b.PayCount);
                    }
                    a.ContractedCount = a.ContractedCount * (decimal.Parse(db.CommModels.Where(b => b.Type == "1" && b.Name == a.TaxRate.ToString()).Count() > 0 ? db.CommModels.Where(b => b.Type == "1" && b.Name == a.TaxRate.ToString()).First().Value.ToString() : a.TaxRate.ToString()) + 1);

                });


                var result = new { total = total, rows = rowList, footer = footer };
                return Json(result);
            } catch (Exception ex) {

                throw ex;
            }



        }


        public ActionResult Index() {
            if (Session["user"] == null) {
                return Redirect("/Employer/Login");
            }
            var employer = Session["user"] as Employer;
            return View(employer);
        }

        public ActionResult IndexView() {
            return View();
        }
        //
        // GET: /Contract/Create

        public ActionResult Create() {
            if (Session["user"] == null) {
                return Redirect("/Employer/Login");
            }
            var employer = Session["user"] as Employer;
            ViewBag.EmployerName = employer.Name;
            return View();
        }

        //
        // POST: /Contract/Create

        [HttpPost]
        public ActionResult Create(Contract entity) {
            try {
                if (db.Contracts.Where(a => a.ContractNum == entity.ContractNum).Count() > 0) {
                    return Json(new { IsSuccess = false, Message = "该合同编号已存在" }, JsonRequestBehavior.AllowGet);
                }
                db.Contracts.Add(entity);
                db.SaveChanges();
            } catch (Exception ex) {
                return Json(new { IsSuccess = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, JsonRequestBehavior.AllowGet);

        }

        //
        // GET: /Contract/Edit/5

        public ActionResult Edit(int id = 0) {
            Contract contract = db.Contracts.Find(id);
            if (contract == null) {
                return HttpNotFound();
            }
            return View(contract);
        }

        //
        // POST: /Contract/Edit/5

        [HttpPost]
        public ActionResult Edit(Contract contract) {

            db.Entry(contract).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPurchaseDetail(int id) {
            ViewBag.id = id;

            return View();
        }

        [HttpPost]
        public ActionResult GetPurchaseDetailList(int id) {
            var contract = db.Contracts.Find(id);

            var taxRate = decimal.Parse(db.CommModels.Where(b => b.Type == "1" && b.Name == contract.TaxRate.ToString()).First().Value);



            var d1 = db.Reimbursements.Where(b => b.ReimbursementType == 0 && b.Contract.Id == id).Count() > 0
                      ? db.Reimbursements.Where(b => b.ReimbursementType == 0 && b.Contract.Id == id).Sum(b => b.AuditAmount) : 0;
            var d2 = db.OutsourcingCosts.Where(c => c.Contract.Id == id).Count() > 0 ? db.OutsourcingCosts.Where(c => c.Contract.Id == id).Sum(c => c.AuditAmount) : 0;
            var d3 = db.BusinessCosts.Where(d => d.Contract.Id == id).Count() > 0 ? db.BusinessCosts.Where(d => d.Contract.Id == id).Sum(d => d.CostAmount) : 0;
            var d4 = db.PurchaseDetail.Where(e => e.PurchaseOrder.Contract.Id == id).Count() > 0 ? db.PurchaseDetail.Where(e => e.PurchaseOrder.Contract.Id == id).Sum(e => e.PriceSum) : 0;

            var d5 = contract.ContractCount - (d1 + d2 + d3 + d4) * (1 + taxRate);
            var d6 = d5 * taxRate;

            var d7 = d5 - d6;

            var list = new List<object> {
                new {Name = "合同名称",Count = contract.ContractName}, 
                new {Name = "销售合同编号",Count = contract.ContractNum}, 
                new {Name = "销售人",Count = contract.SalesManagers}, 
                new {Name = "合同金额",Count = contract.ContractCount}, 
                new { Name = "差旅", Count = d1,Memo="查看" },
                new { Name = "外协", Count = d2 ,Memo="查看" },
                new { Name ="其它",Count = d3 ,Memo="查看"},
                new { Name ="采购",Count = d4 ,Memo="查看"},
                new { Name = "合同差额", Count = d5},
                new { Name = "差额税费", Count = d6},
                new { Name = "合同利润", Count = d7}
            };
            var result = new { total = 4, rows = list };
            return Json(result);

        }

        //
        // POST: /Contract/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(IList<int> idList) {
            try {


                foreach (var id in idList) {
                    Contract contract = db.Contracts.Find(id);
                    db.Contracts.Remove(contract);
                    db.SaveChanges();
                }
                return Json(new { IsSuccess = true, Message = "保存成功" });
            } catch (Exception ex) {

                return Json(new { IsSuccess = false, Message = "该合同下面存在合同明细或者采购明细！" });
            }
        }


        public ActionResult GetContractList() {
            var list = from t in db.Contracts orderby t.OrderTime select new { id = t.Id, text = t.ContractName };
            var result = list.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}