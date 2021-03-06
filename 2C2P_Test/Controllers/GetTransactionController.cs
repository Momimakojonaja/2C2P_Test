using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2C2P_Test.Models;
using Newtonsoft.Json;

namespace _2C2P_Test.Controllers
{
    public class GetTransactionController : Controller
    {
        public JsonResult GetDataByCurrency(String inCurrency)
        {
            List<APIResult> result = new List<APIResult>();
            DatabaseManager dbMgr = new DatabaseManager();

            try
            {
                result = dbMgr.GetDataByCurrency(inCurrency);
            }
            catch (Exception ex)
            {
                APIResult error = new APIResult();
                error.ErrorMessage = ex.Message;

                result.Add(error);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByDateRange(String inStartDate, String inEndDate)
        {
            List<APIResult> result = new List<APIResult>();
            DatabaseManager dbMgr = new DatabaseManager();
            ValidateManager vMgr = new ValidateManager();

            try
            {
                if(!vMgr.IsDateTime(inStartDate) || !vMgr.IsDateTime(inEndDate))
                {
                    throw new InvalidCastException("Start Date or End Date not in correct format");
                }

                result = dbMgr.GetDataByDateRange(Convert.ToDateTime(inStartDate).ToString("yyyy-MM-dd")
                                                                        , Convert.ToDateTime(inEndDate).ToString("yyyy-MM-dd"));
            }
            catch (Exception ex)
            {
                APIResult error = new APIResult();
                error.ErrorMessage = ex.Message;

                result.Add(error);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataByStatus(String inStatus)
        {
            List<APIResult> result = new List<APIResult>();
            DatabaseManager dbMgr = new DatabaseManager();

            try
            {
                result = dbMgr.GetDataByStatus(inStatus);
            }
            catch (Exception ex)
            {
                APIResult error = new APIResult();
                error.ErrorMessage = ex.Message;

                result.Add(error);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


       
    }
}
