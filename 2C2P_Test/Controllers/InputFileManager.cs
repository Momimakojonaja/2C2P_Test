using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2C2P_Test.Controllers
{
    public class InputFileManager
    {
        private List<String> ReadCSVFile(String inFileName)
        {
            List<string> result = new List<string>();
            String[] arrLine = null;

            try
            {
                arrLine = File.ReadAllLines(inFileName);

                foreach (String line in arrLine)
                    result.Add(line.Replace(", \"",",\"").Replace("\",\"", "|").Trim(new char[] { '\"' }).Replace(",", String.Empty).Replace("|", ","));

                File.Delete(inFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<String> ReadXMLFile(String inFileName)
        {
            List<string> result = new List<string>();

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<String> ReadInputFile(String inFileName)
        {
            List<string> result = new List<string>();

            try
            {
                if (".CSV".Equals(Path.GetExtension(inFileName).ToUpper()))
                    result = ReadCSVFile(inFileName);
                else
                    result = ReadXMLFile(inFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


    }
}