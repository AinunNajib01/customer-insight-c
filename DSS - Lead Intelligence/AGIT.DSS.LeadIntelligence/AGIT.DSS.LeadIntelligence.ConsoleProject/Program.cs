using AGIT.DSS.LeadIntelligence.Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //var abc = MappingLogic.GetAll();

            string abcc = ExcelConversion.UploadSalesOrder();

            //DateTime adab = DateTime.ParseExact("12022009", "ddMMyyyy", CultureInfo.InvariantCulture);



            //int a = CommonHelper.ColumnExcelToNumber("J");

            //var balikan = CommonHelper.ConvertToDateTime("ddMMyyyy", "10021989");

            //string filename = @"D:\New Text Document.txt";

            //using (var md5 = MD5.Create())
            //{
            //    using (var stream = File.OpenRead(filename))
            //    {
            //        //return md5.ComputeHash(stream);
            //        //string abc = Encoding.Default.GetString(md5.ComputeHash(stream));
            //        byte[] checksum = md5.ComputeHash(stream);
            //        string abc = BitConverter.ToString(checksum).Replace("-", string.Empty);
            //    }
            //}

            //using (var stream = File.OpenRead(filename))
            //{

            //    MD5 md5x = new MD5CryptoServiceProvider();
            //    try
            //    {
            //        byte[] result = md5x.ComputeHash(stream);

            //        // Build the final string by converting each byte
            //        // into hex and appending it to a StringBuilder
            //        StringBuilder sb = new StringBuilder();
            //        for (int i = 0; i < result.Length; i++)
            //        {
            //            sb.Append(result[i].ToString("X4"));
            //        }

            //        // And return it
            //        string abc = sb.ToString();
            //    }
            //    catch (ArgumentNullException ane)
            //    {
            //        //If something occurred during serialization, 
            //        //this method is called with a null argument. 


            //    }
        //}

        }
    }
}
