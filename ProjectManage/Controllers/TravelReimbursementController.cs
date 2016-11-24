using ProjectManage.Comm;
using ProjectManage.DAL;
using ProjectManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManage.Controllers
{
    public class TravelReimbursementController : Controller
    {


        private ProjectContext db = new ProjectContext();
        //
        // GET: /TravelReimbursement/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadAllByPage(int page, int rows)
        {
            try
            {

                long total = 0;
                var list = db.Reimbursements.OrderBy(a => a.StartingTime).Where(a => a.ReimbursementType == 1).Skip((page - 1) * rows).
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
                        a.Subtotal
                    });


                total = list.Count();

                var result = new { total = total, rows = list.ToList() };
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //
        // GET: /TravelReimbursement/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /TravelReimbursement/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TravelReimbursement/Create

        [HttpPost]
        public ActionResult Create(ReimbursementVo entity)
        {
            try
            {
                var obj = new Reimbursement
                {
                    Contract = db.Contracts.Find(entity.ContractId),
                    ReimbursementType = 1,
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

                    Memo = entity.Memo,

                };
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
        // GET: /TravelReimbursement/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /TravelReimbursement/Edit/5

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
        // GET: /TravelReimbursement/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /TravelReimbursement/Delete/5

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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
