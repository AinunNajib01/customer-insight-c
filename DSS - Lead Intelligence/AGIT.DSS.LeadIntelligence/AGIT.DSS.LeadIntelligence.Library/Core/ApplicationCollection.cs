using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library.Core
{
    /// <summary>
    /// Provides the possibility to wrap SQL specific object to business logic object and collection.
    /// </summary>
    /// <typeparam name="TObject">Type of business object which must implement IApplicationObject interface.</typeparam>
    /// <typeparam name="TStore">Type of database object which must inherit from DbBase class.</typeparam>
    /// <typeparam name="TProcedure">Type of database command object which must inherit from DbHelper class.</typeparam>
    public abstract class ApplicationCollection<TObject, TProcedure>
        where TObject : IApplicationObject
        where TProcedure : DBHelper
    {
        /// <summary>
        /// Executes SQL statement and wraps database objects to .NET collection. 
        /// </summary>
        /// <param name="Sp">Bussines logic command object instance.</param>
        /// <returns>Collection of bussines logic object instances.</returns>
        protected static List<TObject> GetApplicationCollection(TProcedure Sp)
        {
            List<TObject> _collection = new List<TObject>();

            using (IDataReader _dr = (IDataReader)Sp.GetType().InvokeMember("ExecuteReader", BindingFlags.InvokeMethod, null, Sp, null))
            {
                while (_dr.Read())
                {
                    TObject _object = (TObject)Activator.CreateInstance(typeof(TObject));
                    _collection.Add(_object);
                    _object.Load(_dr);
                }

                if (_dr != null)
                    _dr.Close();

                Sp.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, Sp, null);
            }

            return _collection;
        }

        protected static DataSet GetApplicationDataSet(DBHelper Sp)
        {

            DataSet _collection = new DataSet();

            /*using (DataSet _dr = (DataSet)Sp.GetType().InvokeMember("ExecuteDataset", BindingFlags.InvokeMethod, null, Sp, null))
            {
                _collection = _dr;

                Sp.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, Sp, null);
            }*/

            _collection = Sp.ExecuteDataSet("tblReport");

            return _collection;
        }

        /// <summary>
        /// Executes SQL statement and wraps database object to .NET object. 
        /// </summary>
        /// <param name="Sp">Bussines logic command object instance.</param>
        /// <returns>Instance of bussines logic object.</returns>
        protected static TObject GetApplicationObject(DBHelper Sp)
        {
            TObject _object;

            using (IDataReader _dr = Sp.ExecuteReader())
            {
                if (_dr.Read())
                {
                    _object = (TObject)Activator.CreateInstance(typeof(TObject));
                    _object.Load(_dr);
                }
                else
                    _object = (TObject)Activator.CreateInstance(typeof(TObject));

                if (_dr != null)
                    _dr.Close();

                Sp.Close();
            }

            return (TObject)_object;
        }

        /// <summary>
        /// Gets table name by collection type.
        /// </summary>
        protected static string Table
        {
            get { return ApplicationTables.TableByTypeName(typeof(TObject).ToString()); }
        }
    }

}