using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace _2C2P_Test.Controllers
{
    internal class ValidateManager
    {
        public bool IsDecimal(String inString)
        {
            bool result = false;
            
            try
            {
                Decimal.Parse(inString);
                result = true;
            }
            catch 
            {
                result = false;
            }

            return result;
        }

        public bool IsDateTime(String inString)
        {
            bool result = false;

            try
            {
                DateTime.Parse(inString);
                result = true;
            }
            catch 
            {
                result = false;
            }

            return result;
        }

        public bool validateFileType(String inFileName)
        {
            bool result = false;
            String[] arrFileType = new string[] { ".CSV", ".XML" };
            try
            {
                if (arrFileType.Contains(Path.GetExtension(inFileName).ToUpper()))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public void validateData(List<String> inListData)
        {
            String errorMessage = String.Empty;
            String tmpErrorMessage = String.Empty;
            int i = 1;
            try
            {
                foreach (String data in inListData)
                {
                    tmpErrorMessage = String.Empty;
                    if (data.Split(new char[] { ',' }).Length != 5)
                    {
                        tmpErrorMessage += String.Format("({0})", "Data in unkonw format");
                    }
                    else if (!IsDecimal(data.Split(new char[] { ',' })[1]))
                    {
                        tmpErrorMessage += String.Format("({0})", "Amount is not decimal");
                    }
                    else if (!IsDateTime(data.Split(new char[] { ',' })[3]))
                    {
                        tmpErrorMessage += String.Format("({0})", "Transaction date is not date time");
                    }

                    if (!String.IsNullOrEmpty(tmpErrorMessage))
                    {
                        errorMessage += String.Format("(Record {0}:: {1}) |", i, tmpErrorMessage);
                    }
                    i += 1;
                }

                if (!String.IsNullOrEmpty(errorMessage))
                    throw new InvalidCastException(errorMessage);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}