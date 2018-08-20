using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AI.BigData.Business
{
    public class CommonHelper
    {
        public static void CheckSum(string filePath)
        {
            //using (var md5 = MD5.Create())
            //{
            //    using (var stream = File.OpenRead(filePath))
            //    {
            //        byte[] checksum = md5.ComputeHash(stream);
            //        //string abc = BitConverter.ToString(checksum).Replace("-", string.Empty);

            //        var md5Check = System.Security.Cryptography.MD5.Create();
            //        md5Check.TransformBlock(checksum, 0, checksum.Length, null, 0);
            //        md5Check.TransformFinalBlock(new byte[0], 0, 0);
            //        // Get Hash Value
            //        byte[] hashBytes = md5Check.Hash;
            //        string abc = Convert.ToBase64String(hashBytes);


            //        string abcd = Convert.ToBase64String(checksum);

            //        //return abc;
            //    }
            //}

            //using (var stream = File.OpenRead(filePath))
            //using (var md5 = MD5.Create())
            //{
            //    var hash = md5.ComputeHash(stream);
            //    var base64String = Convert.ToBase64String(hash);
            //}

            using (var stream = File.OpenRead(filePath))
            {
                MD5 md5x = new MD5CryptoServiceProvider();
                try
                {
                    byte[] result = md5x.ComputeHash(stream);

                    // Build the final string by converting each byte
                    // into hex and appending it to a StringBuilder
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        sb.Append(result[i].ToString("X4"));
                    }

                    // And return it
                    string abc = sb.ToString();
                }
                catch (ArgumentNullException ane)
                {
                    //If something occurred during serialization, 
                    //this method is called with a null argument.
                }
            }
        }
    }
}
