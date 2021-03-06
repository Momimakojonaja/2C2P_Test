using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using _2C2P_Test.Models;
using System.Configuration;

namespace _2C2P_Test.Controllers
{
    public class DatabaseManager
    {
        private String CreateInsertCommand(String inString)
        {
            String result = String.Empty;
            String insertCommand = "INSERT INTO [TB_Transaction] ([TransactionId],[Amount],[CurrencyCode],[TransactionDate],[TransactionStatus]) VALUES ('{0}','{1}','{2}','{3}','{4}')";

            try
            {
                result = String.Format(insertCommand
                                                , inString.Split(new char[] { ',' })[0]
                                                , inString.Split(new char[] { ',' })[1]
                                                , inString.Split(new char[] { ',' })[2]
                                                , DateTime.Parse(inString.Split(new char[] { ',' })[3]).ToString("yyyy-MM-dd HH:mm:ss")
                                                , inString.Split(new char[] { ',' })[4]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<APIResult> ConvertDataTableToList(DataTable inDT)
        {
            List<APIResult> result = new List<APIResult>();

            try
            {
                if ((inDT != null) && (inDT.Rows.Count > 0))
                {
                    foreach (DataRow dr in inDT.Rows)
                    {
                        APIResult ent = new APIResult();
                        ent.Id = dr["TransactionId"].ToString();
                        ent.Payment = String.Format("{0} {1}", dr["Amount"].ToString(), dr["CurrencyCode"].ToString());

                        switch(dr["TransactionStatus"].ToString().ToUpper())
                        {
                            case "APPROVED":
                                ent.Status = "A";
                                break;

                            case "FAILED":
                                ent.Status = "R";
                                break;

                            case "REJECTED":
                                ent.Status = "R";
                                break;

                            case "FINISHED":
                                ent.Status = "D";
                                break;

                            case "DONE":
                                ent.Status = "D";
                                break;

                            default:
                                ent.Status = "Error";
                                break;
                        }

                        result.Add(ent);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public String InsertData(List<String> inListData)
        {
            SqlCommand Command = null;

            try
            {
                using (SqlConnection Conn = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]))
                {
                    Conn.Open();

                    using (SqlTransaction trans = Conn.BeginTransaction())
                    {
                        Command = new SqlCommand();
                        Command.Connection = Conn;
                        Command.Transaction = trans;

                        foreach(String data in inListData)
                        {
                            Command.CommandText = CreateInsertCommand(data);
                            Command.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "Code :: 200";
        }

        public List<APIResult> GetDataByCurrency(String inCurrency)
        {
            List<APIResult> result = new List<APIResult>();
            DataTable dt = new DataTable();
            String sqlQuery = "Select * From TB_Transaction Where CurrencyCode = '{0}' ";

            try
            {
                dt = GetData(String.Format(sqlQuery, inCurrency));

                result = ConvertDataTableToList(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<APIResult> GetDataByDateRange(String inStartDate, String inEndDate)
        {
            List<APIResult> result = new List<APIResult>();
            DataTable dt = new DataTable();
            String sqlQuery = "Select * From TB_Transaction Where TransactionDate Between '{0}' And '{1}' ";

            try
            {
                dt = GetData(String.Format(sqlQuery, inStartDate, inEndDate));

                result = ConvertDataTableToList(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<APIResult> GetDataByStatus(String inStatus)
        {
            List<APIResult> result = new List<APIResult>();
            DataTable dt = new DataTable();
            String sqlQuery = "Select * From TB_Transaction Where TransactionStatus = '{0}' ";

            try
            {
                dt = GetData(String.Format(sqlQuery, inStatus));

                result = ConvertDataTableToList(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private DataTable GetData(String inQuery)
        {
            DataTable result = new DataTable();
            SqlCommand sqlComm = null;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]))
                {
                    sqlConn.Open();

                    sqlComm = new SqlCommand(inQuery);

                    sqlComm.CommandType = CommandType.Text;

                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandTimeout = sqlConn.ConnectionTimeout;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlComm))
                    {
                        result = new DataTable();
                        adapter.Fill(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

    }
}