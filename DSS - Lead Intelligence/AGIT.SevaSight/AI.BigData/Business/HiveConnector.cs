using Microsoft.Hadoop.Hive;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.BigData.Business
{
    public class HiveConnector
    {
        public static void GetDataFromHive()
        {
            var conn = new OdbcConnection
            {
                ConnectionString = @"DRIVER={Microsoft Hive ODBC Driver};                                        
                                        Host=10.0.0.4;
                                        Port=10000;
                                        Schema=default;
                                        DefaultTable=table_name;
                                        HiveServerType=1;
                                        ApplySSPWithQueries=1;
                                        AsyncExecPollInterval=100;
                                        AuthMech=0;
                                        CAIssuedCertNamesMismatch=0;
                                        TrustedCerts=C:\Program Files\Microsoft Hive ODBC Driver\lib\cacerts.pem;"
            };
            try
            {
                conn.Open();

                var adp = new OdbcDataAdapter("Select * from customers limit 10", conn);
                var ds = new DataSet();
                adp.Fill(ds);

                foreach (var table in ds.Tables)
                {
                    var dataTable = table as DataTable;

                    if (dataTable == null)
                        continue;

                    var dataRows = dataTable.Rows;

                    if (dataRows == null)
                        continue;

                    //log.Info("Records found " + dataTable.Rows.Count);

                    foreach (var row in dataRows)
                    {
                        var dataRow = row as DataRow;
                        if (dataRow == null)
                            continue;

                        //log.Info(dataRow[0].ToString() + " " + dataRow[1].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                // log.Info("Failed to connect to data source");
            }
            finally
            {
                conn.Close();
            }
        }

        public static void ConnectHive()
        {
            try
            { 
            var db = new MyHiveDatabase(webHcatUri: new Uri("http://10.0.0.4:50111"), username:(string)"hadoop",
                password:(string)null, hadoopUserName:(string)"hdfs");

            //var q = from x in
            //            (from a in db.Actors
            //             select new { a.ActorId, foo = a.AwardsCount })
            //        group x by x.ActorId into g
            //        select new { ActorId = g.Key, bar = g.Average(z => z.foo) };

            //q.ExecuteQuery().Wait();
            //var results1 = q.ToList();

            var result = db.ExecuteHiveQuery("select * from web_logs");
            result.Wait();

                //Console.WriteLine(result);
                //Console.ReadLine();

                //result.Wait();


                //var c = (from i in db.WebLogs
                //         select i);

                //c.ExecuteQuery().Wait();
                //var results2 = c.ToList();


                //var projectionQuery = from aw in db.Awards
                //                      join t in db.Titles
                //                          on aw.MovieId equals t.MovieId
                //                      where t.Year == 1994 && aw.Won == "True"
                //                      select new { MovieId = t.MovieId, Name = t.Name, Type = aw.Type, Category = aw.Category, Year = t.Year };


                //var newTable = projectionQuery.CreateTable("AwardsIn1994");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    public class MyHiveDatabase : HiveConnection
    {
        public MyHiveDatabase(Uri webHcatUri, string username, string password, string hadoopUserName)
            : base(webHcatUri, username, password, hadoopUserName) { }

        public HiveTable<WebLogsRow> WebLogs
        {
            get
            {
                return this.GetTable<WebLogsRow>("web_logs");
            }
        }
    }

    public class WebLogsRow : HiveRow
    {
        public string city { get; set; }
        public string code { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
    }
}
