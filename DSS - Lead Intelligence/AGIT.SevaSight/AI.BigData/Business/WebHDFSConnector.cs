using Microsoft.Hadoop.WebHDFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AI.BigData.Business
{
    public class WebHDFSConnector
    {
        public static void SaveFile(string srcFileName, string destFolderName, string destFileName, string uri)
        {
            try
            {
                //connect to hadoop cluster
                Uri myUri = new Uri(uri);
                string userName = "hdfs";
                WebHDFSClient myClient = new WebHDFSClient(myUri, userName);

                //drop destination directory (if exists)
                myClient.DeleteDirectory(destFolderName, true);

                //create destination directory
                myClient.CreateDirectory(destFolderName);

                string newpathfile = destFolderName + "/" + destFileName;
                //load file to destination directory
                var s = myClient.CreateFile(srcFileName, newpathfile);

                Console.WriteLine(s.Result);

                //keep command window open until user presses enter
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void GetDirectoryStatus(string destFolderName, string uri)
        {
            try
            {
                //connect to hadoop cluster
                Uri myUri = new Uri(uri);
                string userName = "hdfs";
                WebHDFSClient myClient = new WebHDFSClient(myUri, userName);

                //list file contents of destination directory
                Console.WriteLine();
                Console.WriteLine("Contents of " + destFolderName);

                myClient.GetDirectoryStatus(destFolderName).ContinueWith(
                     ds => ds.Result.Files.ToList().ForEach(
                     f => Console.WriteLine("- " + f.PathSuffix)
                     ));


                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void GetFile(string folderName, string fileName, string uri, string localfolder)
        {
            try
            {
                //connect to hadoop cluster
                Uri myUri = new Uri(uri);
                string userName = "hdfs";
                WebHDFSClient myClient = new WebHDFSClient(myUri, userName);

                string newpathfile = folderName + "/" + fileName;

                //list file contents of destination directory
                Console.WriteLine();
                Console.WriteLine("Get of file on " + newpathfile);

                var ss = myClient.OpenFile(newpathfile);

                //Console.WriteLine(ss.Result);

                string localFilePath = localfolder + "\\" + fileName;

                Stream output = File.OpenWrite(localFilePath);

                Task taskx = ss.Result.Content.ReadAsStreamAsync().ContinueWith(t =>
                {
                    var stream = t.Result;

                    stream.CopyTo(output);
                });

                Console.WriteLine("Download succeeded.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }

        public static void Checksum(string filepath, string uri)
        {
            try
            {
                //connect to hadoop cluster
                Uri myUri = new Uri(uri);
                string userName = "hdfs";
                WebHDFSClient myClient = new WebHDFSClient(myUri, userName);

                var data = myClient.GetFileChecksum(filepath);

                //data.Result.by

                Console.WriteLine(data.Result.Checksum);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
