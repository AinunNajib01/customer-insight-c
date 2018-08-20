using AI.BigData.Business;
using Microsoft.Hadoop.WebHDFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.BigData
{
    class Program
    {
        static void Main(string[] args)
        {
            //WebHDFSConnector.SaveFile("C:\\Ariff\\testoutput.tsv", "/agit/inputnew", "testoutput.tsv", "http://10.0.0.4:50070");

            //WebHDFSConnector.GetDirectoryStatus("/agit", "http://10.0.0.4:50070");

            //WebHDFSConnector.GetFile("/agit/out", "testoutput.tsv", "http://10.0.0.4:50070", "C:\\Ariff\\out");

            //HiveConnector.GetDataFromHive();



            //CommonHelper.CheckSum("C:\\Ariff\\out\\testoutput.tsv");
            //WebHDFSConnector.Checksum("/agit/out/testoutput.tsv", "http://10.0.0.4:50070");

            HiveConnector.ConnectHive();
        }
    }
}
