using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AGIT.DSS.LeadIntelligence.Library
{
    // Summary :
    // Represents request type enum.
    public enum DbBaseRequestType
    {
        // Summary :
        // Parameterized SQL statement.
        ParameterizedSQLText = 1,

        // Summary :
        // Strored procedure.
        StoredProcedure = 2
    }

    // Summary :
    // Provides a wrapper for using SQL Stored Procedures and Commands.
    public class DBHelper
    {
        // Summary
        // Throw an exception if something wrong happend inside SQL executor class. Will be removed in next release.
        protected static bool ThrowExceptionIsOn = true;

        // Summary :
        // DbBase object.
        protected DbBaseRequestType RequestType = DbBaseRequestType.ParameterizedSQLText;

        #region Member variables

        // Summary :
        // Parameterized SQL statement or strored procedure name.
        private String m_ProcedureName;

        // Summary :
        // SQL connection object.
        private IDbConnection m_SqlConnection;

        // Summary :
        // SQL command object.
        private IDbCommand m_SqlCommand;

        // Summary :
        // Parameters name value hash table.
        private Hashtable m_HashParameters = new Hashtable();

        // Summary :
        // SQL adapter object.
        private IDataAdapter m_SqlDataAdapter;

        // Summary :
        // SQL reader object.
        private IDataReader m_SqlDataReader;

        // Summary :
        // Connetion string value.
        private String m_strConnection;

        // Summary :
        // DataSet object
        private DataSet m_Dataset;

        // Summary :
        // DataView object.
        private DataView m_DataView;

        // Summary :
        // True if an error happened.
        private bool blnError = false;

        // Summary :
        // Error message string an error happened or emty string.
        private String strErrorMessage;

        // Summary :
        // Timeout (in seconds) for sql command.
        public int CommandTimeOut = 0;

        #endregion

        #region Constructors

        // Summary :
        // Empty Constructor.
        public DBHelper()
        {

        }

        // Summary :
        // Constructor. Needs a procedure name
        /// <param name="strConnection">Connection string.</param>
        /// <param name="ProcedureName">Stored procedure name or parameterized SQL statement.</param>
        public DBHelper(String strConnection, String ProcedureName)
        {
            m_ProcedureName = ProcedureName;
            m_strConnection = strConnection;
        }

        // Summary :
        // Constructor. Needs a procedure name.
        /// <param name="ProcedureName">Stored procedure name or parameterized SQL statement.</param>
        public DBHelper(String ProcedureName)
        {
            m_ProcedureName = ProcedureName;
        }

        // Summary :
        // Constructor. Needs a procedure name and a hashtable containing the parameters
        /// <param name="strConnection">Connection string.</param>
        /// <param name="ProcedureName">Stored procedure name or parameterized SQL statement.</param>
        /// <param name="HashParameters">Parameters hash table.</param>
        public DBHelper(String strConnection, String ProcedureName, Hashtable HashParameters)
        {
            m_ProcedureName = ProcedureName;
            m_HashParameters = HashParameters;
            m_strConnection = strConnection;
        }

        // Summary :
        // Constructor. Needs a procedure name and a hashtable containing the parameters
        /// <param name="ProcedureName">Stored procedure name or parameterized SQL statement.</param>
        /// <param name="HashParameters">Parameters hash table.</param>
        public DBHelper(String ProcedureName, Hashtable HashParameters)
        {
            m_ProcedureName = ProcedureName;
            m_HashParameters = HashParameters;
        }

        // Summary :
        // Constructor. Needs a procedure name and key/value set as Parameters
        /// <param name="strConnection">Connection string.</param>
        /// <param name="ProcedureName">Stored procedure name or parameterized SQL statement.</param>
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="ParameterValue">Parameter value.</param>
        public DBHelper(String strConnection, String ProcedureName, string ParameterName, Object ParameterValue)
        {
            m_ProcedureName = ProcedureName;
            m_strConnection = strConnection;
            AddParameter(ParameterName, ParameterValue);
        }

        // Summary :
        // Constructor. Needs a procedure name and key/value set as Parameters.
        /// <param name="ProcedureName">Stored procedure name or parameterized SQL statement.</param>
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="ParameterValue">Parameter value.</param>
        public DBHelper(String ProcedureName, string ParameterName, Object ParameterValue)
        {
            m_ProcedureName = ProcedureName;
            AddParameter(ParameterName, ParameterValue);
        }

        #endregion

        #region Public methods
        // Summary :
        // Creates a SqlParameter, no properties set
        private IDataParameter AddParameter()
        {
            try
            {
                return new SqlParameter();
            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;

                if (ThrowExceptionIsOn)
                    throw e;

                return (SqlParameter)null;
            }
        }

        // Summary :
        // Add a parameter (parameter name, parameter value)
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="ParameterValue">Parameter value.</param>
        public IDataParameter AddParameter(string ParameterName, Object ParameterValue)
        {
            return AddParameter(ParameterName, ParameterValue, ParameterDirection.Input);
        }

        // Summary :
        // Gets parameter object by name.
        /// <param name="ParameterName"></param>
        public object GetParameter(string ParameterName)
        {
            return ((IDbDataParameter)Parameters[ParameterName]).Value;
        }

        // Summary :
        // Add a parameter (parameter name, parameter value, parameter direction)
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="ParameterValue">Parameter value.</param>
        /// <param name="paramDirection">Direction: Input, Output, InputOutput.</param>
        public IDataParameter AddParameter(string ParameterName, Object ParameterValue, ParameterDirection paramDirection)
        {
            String strCleanKey = CleanKey(ParameterName);

            IDataParameter paramSql = AddParameter();
            paramSql.ParameterName = "@" + strCleanKey;
            paramSql.Value = ParameterValue;
            paramSql.Direction = paramDirection;

            m_HashParameters.Add(strCleanKey, paramSql);
            return paramSql;
        }

        // Summary :
        // Add a parameter (parameter name, parameter value, parameter direction, parameter sqldbtype)
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="ParameterValue">Parameter value.</param>
        /// <param name="paramDirection">Direction: Input, Output, InputOutput.</param>
        /// <param name="paramSqlDbType">Parameter SQL type.</param>
        public IDataParameter AddParameter(string ParameterName, Object ParameterValue, ParameterDirection paramDirection, DbType paramSqlDbType)
        {
            IDataParameter param = AddParameter(ParameterName, ParameterValue, paramDirection);
            param.DbType = paramSqlDbType;

            AddDefaultPrecisionAndScale(param);

            return param;
        }

        // Summary :
        // Add a parameter (parameter name, parameter value, parameter direction, parameter sqldbtype, int paramSize)
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="ParameterValue">Parameter value.</param>
        /// <param name="paramDirection">Direction: Input, Output, InputOutput.</param>
        /// <param name="paramSqlDbType">Parameter SQL type.</param>
        /// <param name="intSize">Parameter size.</param>
        public IDataParameter AddParameter(string ParameterName, Object ParameterValue, ParameterDirection paramDirection, DbType paramSqlDbType, int intSize)
        {
            IDataParameter param = AddParameter(ParameterName, ParameterValue, paramDirection);
            param.DbType = paramSqlDbType;

            ((SqlParameter)param).Size = intSize;

            AddDefaultPrecisionAndScale(param);

            return param;
        }

        // Summary :
        // Add a parameter (ParameterName, paramDirection, paramSqlDbType)
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="paramDirection">Direction: Input, Output, InputOutput.</param>
        /// <param name="paramSqlDbType">Parameter SQL type.</param>
        /// <returns></returns>
        public IDataParameter AddParameter(string ParameterName, ParameterDirection paramDirection, DbType paramSqlDbType)
        {
            String strCleanKey = CleanKey(ParameterName);

            IDataParameter paramSql = AddParameter();
            paramSql.ParameterName = "@" + strCleanKey;
            paramSql.DbType = paramSqlDbType;
            paramSql.Direction = paramDirection;

            AddDefaultPrecisionAndScale(paramSql);

            m_HashParameters.Add(strCleanKey, paramSql);
            return paramSql;
        }

        // Summary :
        // Add a parameter (ParameterName, paramDirection, paramSqlDbType, intSize)
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="paramDirection">Direction: Input, Output, InputOutput.</param>
        /// <param name="paramSqlDbType">Parameter SQL type.</param>
        /// <param name="intSize">Parameter size.</param>
        public IDataParameter AddParameter(string ParameterName, ParameterDirection paramDirection, DbType paramSqlDbType, int intSize)
        {
            IDataParameter param = AddParameter(ParameterName, paramDirection, paramSqlDbType);

            ((SqlParameter)param).Size = intSize;

            AddDefaultPrecisionAndScale(param);
            return param;
        }

        // Summary :
        // Add a parameter ( ParameterName, paramDirection, paramSqlDbType, intSize, intPrecision, intScale)
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="paramDirection">Direction: Input, Output, InputOutput.</param>
        /// <param name="paramSqlDbType">Parameter SQL type.</param>
        /// <param name="intSize">Parameter size.</param>
        /// <param name="intPrecision"></param>
        /// <param name="intScale"></param>
        public IDataParameter AddParameter(string ParameterName, ParameterDirection paramDirection, DbType paramSqlDbType, int intSize, int intPrecision, int intScale)
        {
            IDataParameter param = AddParameter(ParameterName, paramDirection, paramSqlDbType, intSize);

            ((SqlParameter)param).Precision = (byte)intPrecision;
            ((SqlParameter)param).Scale = (byte)intScale;

            return param;
        }

        // Summary :
        // Add a parameter ( ParameterName, paramDirection, paramSqlDbType, intPrecision, intScale).
        /// <param name="ParameterName">Parameter name.</param>
        /// <param name="paramDirection">Direction: Input, Output, InputOutput.</param>
        /// <param name="paramSqlDbType">Parameter SQL type.</param>
        /// <param name="intPrecision"></param>
        /// <param name="intScale"></param>
        public IDataParameter AddParameter(string ParameterName, ParameterDirection paramDirection, DbType paramSqlDbType, int intPrecision, int intScale)
        {
            IDataParameter param = AddParameter(ParameterName, paramDirection, paramSqlDbType);

            ((SqlParameter)param).Precision = (byte)intPrecision;
            ((SqlParameter)param).Scale = (byte)intScale;

            return param;
        }

        private void AddDefaultPrecisionAndScale(IDataParameter param)
        {
            // if direction is inputoutput or output and type is decimal: add default values for scale and precision
            if (param.DbType == DbType.Decimal && (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.InputOutput))
            {
                ((SqlParameter)param).Precision = 18;
                ((SqlParameter)param).Scale = 5;
            }
        }

        // Summary :
        // Closes the connection.
        public void Close()
        {
            if (m_SqlConnection != null && m_SqlConnection.State != ConnectionState.Closed && m_SqlConnection.State != ConnectionState.Broken)
                m_SqlConnection.Close();
        }

        // Summary :
        // Executes a command in non-query mode.
        public int ExecuteNonQuery()
        {
            Init();

            try
            {
                m_SqlCommand.ExecuteNonQuery();
                m_SqlConnection.Close();

                //var asdab = ParameterValue("ID");
                return (int)ParameterValue("RETURN_VALUE");
            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;

                if (ThrowExceptionIsOn)
                    throw e;

                return -1;
            }
        }

        // Summary :
        // Executes a command returning a SqlDataAdapter. The connection has to be closed!
        public IDataAdapter ExecuteDataAdapter()
        {
            Init();

            try
            {
                m_SqlDataAdapter = new SqlDataAdapter((SqlCommand)m_SqlCommand);

                return m_SqlDataAdapter;

            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;

                if (ThrowExceptionIsOn)
                    throw e;

                return (SqlDataAdapter)null;
            }
        }

        // Summary :
        // Executes a command in scalar mode.
        public object ExecuteScalar()
        {
            Init();

            try
            {
                object value = m_SqlCommand.ExecuteScalar();
                m_SqlConnection.Close();
                return value;
            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;

                if (ThrowExceptionIsOn)
                    throw e;

                return (object)null;
            }
        }

        // Summary :
        // Executes a command returning a XML Reader.
        public XmlReader ExecuteXMLReader()
        {
            Init();

            try
            {
                return ((SqlCommand)m_SqlCommand).ExecuteXmlReader();
            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;


                if (ThrowExceptionIsOn)
                    throw e;

                return (XmlReader)null;
            }
        }

        // Summary :
        // Executes a command returning a DataSet.
        /// <param name="strTablename">Table name.</param>
        public DataSet ExecuteDataSet(String strTablename)
        {
            ExecuteDataAdapter();

            m_Dataset = new DataSet();

            try
            {
                // Fill DataSet
                m_Dataset = new System.Data.DataSet(strTablename);
                m_SqlDataAdapter.Fill(m_Dataset);
                m_SqlConnection.Close();
            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;

                if (ThrowExceptionIsOn)
                    throw e;
            }

            return m_Dataset;
        }

        // Summary :
        // Executes a command returning a DataView.
        /// <param name="strTablename">Tabel name.</param>
        /// <returns>Result DataView object.</returns>
        public DataView ExecuteDataView(String strTablename)
        {
            ExecuteDataAdapter();

            m_DataView = new DataView();

            try
            {
                DataSet dt = new DataSet(strTablename);
                m_SqlDataAdapter.Fill(dt);

                // Fill DataView
                m_DataView = new DataView(dt.Tables[0]);
                m_SqlConnection.Close();
            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;

                if (ThrowExceptionIsOn)
                    throw e;
            }

            return m_DataView;
        }

        // Summary :
        // Executes a command returning a SqlDataReader. The SqlDataReader has to be closed!
        /// <returns>SqlDataReader object.</returns>
        public IDataReader ExecuteReader()
        {
            Init();

            try
            {
                m_SqlDataReader = m_SqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return m_SqlDataReader;
            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;

                if (ThrowExceptionIsOn)
                    throw e;

                return (SqlDataReader)null;
            }
        }

        // Summary :
        // Gets  the value of a parameter.
        /// <param name="ParameterName">Parameter name.</param>
        /// <returns>Object value.</returns>
        public Object ParameterValue(String ParameterName)
        {
            try
            {
                return ((SqlParameter)m_HashParameters[CleanKey(ParameterName)]).Value;
            }
            catch (Exception e)
            {
                this.blnError = true;
                this.strErrorMessage = e.Message;

                if (ThrowExceptionIsOn)
                    throw e;

                return (SqlParameter)null;
            }
        }

        #endregion

        #region Properties

        // Summary :
        // Gets  the Sql connection
        public IDbConnection SqlConnection
        {
            get
            {
                return m_SqlConnection;
            }
        }

        // Summary :
        // Gets  / sets the stored procedure name
        public String Procedurename
        {
            get
            {
                return m_ProcedureName;
            }
            set
            {
                m_ProcedureName = value;
            }
        }

        // Summary :
        // Gets the SqlCommand
        public IDbCommand SqlCommand
        {
            get
            {
                return m_SqlCommand;
            }
        }

        // Summary :
        // Gets  the SqlDataAdapter
        public IDataAdapter SqlDataAdapter
        {
            get
            {
                return m_SqlDataAdapter;
            }
        }

        // Summary :
        // Gets  the SqlDataReader
        public IDataReader SqlDataReader
        {
            get
            {
                return m_SqlDataReader;
            }
        }

        // Summary :
        // Gets  the dataset
        public DataSet DataSet
        {
            get
            {
                return m_Dataset;
            }
        }

        // Summary :
        // Gets  the dataset
        public DataView DataView
        {
            get
            {
                return m_DataView;
            }
        }

        // Summary :
        // Gets  / sets the parameters hashtable
        public Hashtable Parameters
        {
            get
            {
                return m_HashParameters;
            }
            set
            {
                m_HashParameters = value;
            }
        }

        // Summary :
        // Gets  / sets the parameters hashtable
        public String ConnectionString
        {
            get
            {
                string db_server = ConfigurationManager.AppSettings["db.server"];
                string db_catalog = ConfigurationManager.AppSettings["db.catalog"];
                string db_username = ConfigurationManager.AppSettings["db.username"];
                string db_password = ConfigurationManager.AppSettings["db.password"];

                //string[] key = new string[] { "ekd_db_server", "ekd_db_catalog", "ekd_db_username", "ekd_db_password" };

                //Dictionary<string, string> getDataConnString = SharepointHelper.GetCollectionSPWebPropertyBag(ConstEKD.SharepointURL, key);

                //string db_server = getDataConnString["ekd_db_server"];
                //string db_catalog = getDataConnString["ekd_db_catalog"];
                //string db_username = getDataConnString["ekd_db_username"];
                //string db_password = getDataConnString["ekd_db_password"];

                //db_server = Encrypt.DecryptString(db_server, "EKD");
                //db_catalog = Encrypt.DecryptString(db_catalog, "EKD");
                //db_username = Encrypt.DecryptString(db_username, "EKD");
                //db_password = Encrypt.DecryptString(db_password, "EKD");

                string con = string.Format("server={0};database={1};user id={2}; password={3}", db_server, db_catalog, db_username, db_password);

                return con;
            }
        }

        // Summary :
        // Gets the error setting (true is there was an error)
        public bool IsError
        {
            get { return blnError; }
        }

        // Summary :
        // Gets the last error message, if any.
        public string ErrorMessage
        {
            get { return strErrorMessage; }
        }

        #endregion

        #region Private methods

        // Summary :
        // Does the basic job of initialising the db stuff (connection, command) and adding the parameters
        private void Init()
        {
            Init(RequestType);
        }

        // Summary :
        // Does the basic job of initialising the db stuff (connection, command) and adding the parameters
        /// <param name="RequestType">Parameterized SQL text or stored procedure.</param>
        /// <param name="DatabaseProvider">Database server provider</param>
        private void Init(DbBaseRequestType RequestType)
        {
            m_strConnection = ConnectionString;

            try
            {
                m_SqlConnection = new SqlConnection(m_strConnection);

                m_SqlConnection.Open();
            }
            catch (Exception e)
            {
                if (ThrowExceptionIsOn)
                    throw e;

                return;
            }

            // Creates a SqlCommand and sets type
            m_SqlCommand = new SqlCommand(m_ProcedureName, (SqlConnection)m_SqlConnection);

            if (RequestType == DbBaseRequestType.ParameterizedSQLText)
                m_SqlCommand.CommandType = CommandType.Text;
            else if (RequestType == DbBaseRequestType.StoredProcedure)
                m_SqlCommand.CommandType = CommandType.StoredProcedure;

            if (this.CommandTimeOut > 0)
                m_SqlCommand.CommandTimeout = this.CommandTimeOut;

            // Add a return value parameter by default
            AddParameter("RETURN_VALUE", DbType.Int16, ParameterDirection.ReturnValue);

            // Adds the necessary parameters to the command
            foreach (IDbDataParameter p in m_HashParameters.Values)
                m_SqlCommand.Parameters.Add(p);
        }

        // Summary :
        // Gets a key string, without preceding '@'.
        /// <param name="ParameterName">Parameter name.</param>
        private String CleanKey(String ParameterName)
        {
            String strCleanKey = ParameterName;

            if (strCleanKey.Substring(0, 1).Equals("@"))
                strCleanKey = strCleanKey.Substring(1);

            return strCleanKey;
        }

        #endregion


    }
}
