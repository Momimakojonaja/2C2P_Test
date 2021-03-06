using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2C2P_Test.Controllers
{
    public class Test2C2PController : Controller
    {
        // GET: Test2C2P
        public ActionResult UploadFile()
        {
            ViewBag.ErrorMessage = String.Empty;
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase UploadTransaction)
        {
            ValidateManager vMgr = new ValidateManager();
            InputFileManager inMgr = new InputFileManager();
            DatabaseManager dbMgr = new DatabaseManager();
            List<String> lstData = new List<string>();
            String uploadFilePath = String.Empty;
            try
            {
                if (UploadTransaction.ContentLength > 1024)
                    throw new InvalidCastException("File size more than 1 Mb");


                if(vMgr.validateFileType(UploadTransaction.FileName))
                {
                    uploadFilePath = Path.Combine(Server.MapPath("~/UploadedFiles"), UploadTransaction.FileName);
                    UploadTransaction.SaveAs(uploadFilePath);

                    lstData = inMgr.ReadInputFile(uploadFilePath);

                    vMgr.validateData(lstData);

                    throw new InvalidCastException(dbMgr.InsertData(lstData));
                }
                else
                {
                    throw new InvalidCastException("Unknow File Format");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

            return View();
        }

        // GET: Test2C2P/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test2C2P/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test2C2P/Create
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

        // GET: Test2C2P/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test2C2P/Edit/5
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

        // GET: Test2C2P/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test2C2P/Delete/5
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
    }
}
