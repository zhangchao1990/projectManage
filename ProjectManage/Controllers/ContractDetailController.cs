using Comm;
using Newtonsoft.Json;
using ProjectManage.DAL;
using ProjectManage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManage.Controllers {
    public class ContractDetailController : Controller {

        private ProjectContext db = new ProjectContext();
        //
        // GET: /ContractDetail/

        public ActionResult Index(int Id) {
            ViewBag.ContractId = Id;
            return View();
        }

        public ActionResult LoadPage(int Id) {
            db.Configuration.ProxyCreationEnabled = false;
            var list = db.ContractDetails.Where(a => a.Contract.Id == Id).OrderBy(a => a.Id).ToList();
            return Json(list);
        }

        //
        // GET: /ContractDetail/Details/5

        public ActionResult Details(int id) {
            return View();
        }


        //
        // GET: /ContractDetail/Create

        public ActionResult Create(int Id = 0) {
            ViewBag.ContractId = Id;
            return View();
        }

        //
        // POST: /ContractDetail/Create

        [HttpPost]
        public ActionResult Create(ContractDetail model, int ContractId) {
            try {
                // TODO: Add insert logic here
                model.Contract = db.Contracts.Find(ContractId);
                db.ContractDetails.Add(model);
                db.SaveChanges();
            } catch (Exception ex) {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ContractDetail/Edit/5

        public ActionResult Edit(int id) {



            var model = db.ContractDetails.Find(id);

            if (model == null) {
                return HttpNotFound();
            }

            return View(model);
        }

        //
        // POST: /ContractDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(ContractDetail model, int ContractId) {
            try {
                model.Contract = db.Contracts.Find(ContractId);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            } catch (Exception ex) {
                return Json(new { IsSuccess = false, Message = ex.Message }, "text/html", JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsSuccess = true, Message = "保存成功" }, "text/html", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ContractDetail/Delete/5

        public ActionResult Delete(int id) {
            return View();
        }

        //
        // POST: /ContractDetail/Delete/5

        [HttpPost]
        public ActionResult Delete(IList<int> idList) {
            try {

                foreach (var id in idList) {
                    ContractDetail contract = db.ContractDetails.Find(id);
                    db.ContractDetails.Remove(contract);
                    db.SaveChanges();
                }
                return Json(new { IsSuccess = true, Message = "保存成功" });
            } catch {
                return View();
            }
        }


        public ActionResult Import(int id) {
            var model = db.Contracts.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file) {
            try {
                string message = "";
                if (string.IsNullOrEmpty(Request.Params["sheetName"])) {
                    throw new Exception("工作簿不能为空！");
                }

                string sheetName = Request.Params["sheetName"].ToString();
                if (file != null) {
                    //文件大小  
                    long size = file.ContentLength;
                    //文件类型  
                    string type = file.ContentType;
                    //文件名  
                    string name = file.FileName;
                    //文件格式  
                    string _tp = System.IO.Path.GetExtension(name);

                    if (_tp == ".xls" || _tp == ".xlsx") {
                        System.IO.Stream stream = file.InputStream;

                        //保存文件  
                        ImportContractDetail(stream, sheetName);
                        message = "导入成功";

                    }
                    else {
                        message = "文件格式不对，请上传Excel文件！";
                    }
                    
                }
                return Content(message, "text/plain");

            } catch (Exception ex) {
                return Content(ex.Message, "text/plain");
            }
        }


        public void ImportContractDetail(Stream stream, string sheetName) {
            NPOIHelper<string> nopi = new NPOIHelper<string>();
            List<object> list = new List<object>();
            list = nopi.GetExcel(stream, sheetName);
            foreach (var obj in list) {
                Dictionary<string, string> dic = obj as Dictionary<string, string>;
                var contractDetail = new ContractDetail();
                contractDetail.Contract = db.Contracts.Find(int.Parse(Request.Params["ContractId"]));
                if (dic.ContainsKey("设备材料名称") && !string.IsNullOrEmpty(dic["设备材料名称"])) {
                    contractDetail.EquipmentMaterialName = dic["设备材料名称"];
                }
                if (dic.ContainsKey("品牌") && !string.IsNullOrEmpty(dic["品牌"])) {
                    contractDetail.Brand = dic["品牌"];
                }

                if (dic.ContainsKey("规格") && !string.IsNullOrEmpty(dic["规格"])) {
                    contractDetail.Specifications = dic["规格"];
                }

                if (dic.ContainsKey("单位") && !string.IsNullOrEmpty(dic["单位"])) {
                    contractDetail.Unit = dic["单位"];
                }
                if (dic.ContainsKey("数量") && !string.IsNullOrEmpty(dic["数量"])) {
                    contractDetail.Count = int.Parse(dic["数量"]);
                }

                if (dic.ContainsKey("单价") && !string.IsNullOrEmpty(dic["单价"])) {

                    decimal unit = 0;

                    if (decimal.TryParse(dic["单价"], out unit)) {
                        contractDetail.UnitPrice = unit;
                    }
                    else {
                        contractDetail.UnitPrice = 0;
                    }
                }
                contractDetail.PriceSum = contractDetail.UnitPrice * contractDetail.Count;

                if (dic.ContainsKey("备注") && !string.IsNullOrEmpty(dic["备注"])) {
                    contractDetail.Memo = dic["备注"];
                }
                db.ContractDetails.Add(contractDetail);
            }
            db.SaveChanges();

        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
