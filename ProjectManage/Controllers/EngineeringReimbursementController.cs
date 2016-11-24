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
    public class EngineeringReimbursementController : Controller
    {


        private ProjectContext db = new ProjectContext();

        //
        // GET: /EngineeringReimbursement/

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
                var reimbursementPerson = Request.Params["ReimbursementPerson"];
                var contractName = Request.Params["ContractName"];
                long total = 0;
                var list = db.Reimbursements.OrderBy(a => a.StartingTime).Where(a => a.ReimbursementType == 0);
                if (!string.IsNullOrEmpty(reimbursementPerson))
                {
                    list = list.Where(a => a.ReimbursementPerson.Contains(reimbursementPerson));
                }
                if (!string.IsNullOrEmpty(contractName)) 
                {
                    list = list.Where(a=>a.Contract.ContractName.Contains(contractName));
                }
                var footer = new List<object> { new { AuditAmount = list.Count() > 0 ? list.Sum(a => a.AuditAmount) : 0, Subtotal = list.Count() > 0 ? list.Sum(a => a.Subtotal) : 0, ContractNum = "合计" } };
                var newlist = list.Skip((page - 1) * rows).
                    Take(rows).Select(a => new
                    {
                        a.Id,
                        a.AccommodationAllowance,
                        ContractNum = a.Contract.ContractNum,
                        ContractName = a.Contract.ContractName,
                        a.Fare,
                        a.FoodAllowance,
                        a.Memo,
                        a.OtherExpenses,
                        a.Participant,
                        a.ReimbursementPerson,
                        a.ReimbursementTime,
                        a.ReturnTime,
                        a.StartingTime,
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
        // GET: /EngineeringReimbursement/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /EngineeringReimbursement/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /EngineeringReimbursement/Create

        [HttpPost]
        public ActionResult Create(ReimbursementVo entity)
        {
            try
            {
                var obj = new Reimbursement
                {
                    Contract = db.Contracts.Find(entity.ContractId),
                    ReimbursementType = 0,
                    ReimbursementPerson = entity.ReimbursementPerson,
                    ReimbursementTime = entity.ReimbursementTime,
                    StartingTime = entity.StartingTime,
                    ReturnTime = entity.ReturnTime,
                    Participant = entity.Participant,
                    Fare = entity.Fare,
                    FoodAllowance = entity.FoodAllowance,
                    AccommodationAllowance = entity.AccommodationAllowance,
                    OtherExpenses = entity.OtherExpenses,
                    Subtotal = (entity.Fare + entity.FoodAllowance + entity.AccommodationAllowance + entity.OtherExpenses) * (1 + ManageHelper.Rate),

                    Memo = entity.Memo

                };
                obj.AuditAmount = obj.Subtotal;
                db.Reimbursements.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /EngineeringReimbursement/Edit/5

        public ActionResult Edit(int id)
        {
            var model = db.Reimbursements.Find(id);
            return View(model);
        }

        //
        // POST: /EngineeringReimbursement/Edit/5

        [HttpPost]
        public ActionResult Edit(Reimbursement model, int ContractId)
        {

            try
            {
                model.Contract = db.Contracts.Find(ContractId);
                model.Subtotal = (model.Fare + model.FoodAllowance + model.AccommodationAllowance + model.OtherExpenses) * (1 + ManageHelper.Rate);
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
            var model = db.Reimbursements.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Audit(Reimbursement model)
        {

            try
            {

                var entity = db.Reimbursements.Find(model.Id);
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
        // GET: /EngineeringReimbursement/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /EngineeringReimbursement/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList)
        {
            foreach (var id in idList)
            {
                Reimbursement obj = db.Reimbursements.Find(id);
                db.Reimbursements.Remove(obj);
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
