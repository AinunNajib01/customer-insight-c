using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.Transactions;
using System.Data;

namespace AI.ADP.DataAccess
{
    public static class TransactionmanagerHelper
    {
        public static void OverrideMaximumTimeout(TimeSpan timeout)
        {
            try
            {
                //TransactionScope inherits a *maximum* timeout from Machine.config.  There's no way to override it from
                //code unless you use reflection.  Hence this code!
                //TransactionManager._cachedMaxTimeout
                var type = typeof(TransactionManager);
                var cachedMaxTimeout = type.GetField("_cachedMaxTimeout", BindingFlags.NonPublic | BindingFlags.Static);
                cachedMaxTimeout.SetValue(null, true);

                //TransactionManager._maximumTimeout
                var maximumTimeout = type.GetField("_maximumTimeout", BindingFlags.NonPublic | BindingFlags.Static);
                maximumTimeout.SetValue(null, TimeSpan.FromMinutes(30));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class BaseDAL
    {
        public IDbConnection connection
        {
            get;
            set;
        }

        public string connectionString
        {
            get;
            private set;
        }

        protected int commandTimeOut;

        public BaseDAL()
        {
            try
            {
                var appServices = ConfigurationManager.ConnectionStrings["ApplicationServices"];
                if (appServices != null)
                    ConfigHelper.ConnectionString = appServices.ConnectionString;
                commandTimeOut = ConfigurationManager.AppSettings["SqlCommandTimeOut"] == null ? 7200 : Convert.ToInt32(ConfigurationManager.AppSettings["SqlCommandTimeOut"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateConnection(string connectionName)
        {
            try
            {
                ConfigHelper.ConnectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                var dbHelper = new DBHelper();
                dbHelper.CreateDBObjects(ConfigHelper.ConnectionString, DBHelper.Providers.SqlServer);
                connection = dbHelper.connection;
                connectionString = ConfigHelper.ConnectionString;
                TransactionmanagerHelper.OverrideMaximumTimeout(TimeSpan.FromSeconds(commandTimeOut));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateConnection()
        {
            try
            {
                if (this.connection == null || this.connection.ConnectionString == string.Empty)
                {
                    var dbHelper = new DBHelper();
                    dbHelper.CreateDBObjects(ConfigHelper.ConnectionString, DBHelper.Providers.SqlServer);
                    connection = dbHelper.connection;
                    connectionString = ConfigHelper.ConnectionString;
                }
                TransactionmanagerHelper.OverrideMaximumTimeout(TimeSpan.FromSeconds(commandTimeOut));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public static class ConfigHelper
    {
        public static string ConnectionString
        {
            get;
            set;
        }
    }

    public class ConnectionString
    {

        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string DBName
        {

            get;
            set;
        }

        public string Host
        {
            get;
            set;
        }

        public string GetValue()
        {
            //var str = "Data Source="+Host+";Initial Catalog="+DBName+";User ID="+UserName+";Password="+Password+"";
            return string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                Host, DBName, UserName, Password);
        }
    }

    public class WhereClause
    {
        private string _sqlOperator;

        public string Property { get; set; }
        public string SqlOperator
        {
            get
            {
                return _sqlOperator == null ? "=" : _sqlOperator;
            }
            set
            {
                _sqlOperator = value;
            }
        }
        public object Value { get; set; }
    }

    public class TransactionUtils
    {
        public static TransactionScope CreateTransactionScope()
        {
            try
            {
                var transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                transactionOptions.Timeout = TimeSpan.FromSeconds(31536000);//TimeSpan.MaxValue;//TimeSpan.FromSeconds(31536000);
                return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
