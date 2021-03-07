using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text;
using System.Xml.Linq;

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

        private String readXMLNode(XmlNodeList NodeList)
        {
            String result = String.Empty;
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                foreach (XmlNode node in NodeList)
                {
                    result += node["Transaction"].Attributes["Id"].Value;

                    result += node["PaymentDetails"].ChildNodes[0]["Amount"].Value;
                    result += node["PaymentDetails"].ChildNodes[0]["CurrencyCode"].Value;

                    result += node["TransactionDate"].Value;
                    result += node["Status"].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private List<String> ReadXML(List<XmlElement> inElements)
        {
            List<string> result = new List<string>();
            String data = String.Empty;

            //foreach (XElement level1Element in XElement.Load(inFileName).Elements("Transaction"))
            //{
            //    data = String.Empty;
                


            //}
            return result;
        }

        private String ReadElement(XElement inElement)
        {
            String result = String.Empty;

            

            return result;
        }



        private List<String> ReadXMLFile(String inFileName)
        {
            List<string> result = new List<string>();
            String data = String.Empty;
            XmlDocument xml = new XmlDocument();
            XmlNodeList nodeList = null;

            try
            {
                xml.Load(inFileName);
                nodeList = xml.SelectNodes("/Transactions/Transaction");


                foreach (XmlNode node in xml.SelectNodes("/Transactions/Transaction"))
                {
                    data = String.Empty;
                    
                    data += node.Attributes["id"].Value + ",";

                    data += node.SelectSingleNode("PaymentDetails").SelectSingleNode("Amount").InnerText + ",";
                    data += node.SelectSingleNode("PaymentDetails").SelectSingleNode("CurrencyCode").InnerText + ",";

                    data += node.SelectSingleNode("TransactionDate").InnerText + ",";
                    data += node.SelectSingleNode("Status").InnerText;

                    result.Add(data);
                }

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