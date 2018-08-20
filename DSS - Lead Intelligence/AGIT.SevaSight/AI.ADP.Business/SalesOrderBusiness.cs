using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.ADP.DomainObject;
using AI.ADP.DataAccess;
using Ninject;
using Ninject.Modules;
using System.Transactions;
using AI.ADP.DomainObject;
using AI.ADP.DataAccess;
using Agit.Sevasight.Models;
using AI.ADP.DomainObject.Models;

namespace AI.ADP.Business
{
    public interface ISalesOrderBusiness
    {
        List<string> Error { get; set; }
        List<Lead> Find(string criteria,
            List<WhereClause> criteriaValue, string orderBy = "AccountID", Boolean useDefaultDeleted = true);
        List<Lead> FindAll(Boolean useDefaultDeleted = true);
        Lead FindByOrderNo(string orderNo);
        List<LeadResponse> FindLead();
        List<Lead> FindByDate(DateTime dateFrom, DateTime dateTo, string orderBy = "SourceSystemNo", Boolean useDefaultDeleted = true);
        int Create(SalesOrder lead);
        int Create(List<Lead> leadList);
        int Update(SalesOrder lead);
        int UpdateStatusLead(SalesOrder lead);
        int Update(List<Lead> leadList);
        int Delete(Lead lead);
        int Delete(List<Lead> leadList);

    }


    public partial class SalesOrderBusiness : ISalesOrderBusiness
    {


        protected List<string> _error;
        public List<string> Error { get { return _error; } set { _error = value; } }

        protected ISalesOrderDataAccess _soDal;

        [Inject]
        public SalesOrderBusiness(ISalesOrderDataAccess leadDal)
        {
            _soDal = leadDal;
            Error = new List<string>();
        }

        public int Create(List<Lead> leadList)
        {
            int i = 0;
            var con = _soDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _soDal.Create(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _soDal.Dispose();
            }
            return i;
        }

        public int Create(SalesOrder lead)
        {

            int i = 0;
            var con = _soDal.GetConnection();
            try
            {
                con.Open();
                i = _soDal.Create(lead);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _soDal.Dispose();
            }
            return i;
        }

        public int Delete(List<Lead> leadList)
        {
            int i = 0;
            var con = _soDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _soDal.Delete(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _soDal.Dispose();
            }
            return i;
        }

        public int Delete(Lead lead)
        {
            int i = 0;
            var con = _soDal.GetConnection();
            try
            {
                con.Open();
                i = _soDal.Delete(lead);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _soDal.Dispose();
            }
            return i;
        }

        public int Update(List<Lead> leadList)
        {
            int i = 0;
            var con = _soDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _soDal.Update(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _soDal.Dispose();
            }
            return i;
        }

        public int Update(SalesOrder salesOrder)
        {
            int i = 0;
            var con = _soDal.GetConnection();
            try
            {
                con.Open();
                i = _soDal.Update(salesOrder);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _soDal.Dispose();
            }
            return i;
        }

        public int UpdateStatusLead(SalesOrder salesOrder)
        {
            int i = 0;
            var con = _soDal.GetConnection();
            try
            {
                con.Open();
                i = _soDal.UpdateStatusLead(salesOrder);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _soDal.Dispose();
            }
            return i;
        }

        public List<Lead> Find(string criteria, List<WhereClause> criteriaValue, string orderBy = "AccountID", bool useDefaultDeleted = true)
        {
            throw new NotImplementedException();
        }

        public List<Lead> FindAll(bool useDefaultDeleted = true)
        {
            throw new NotImplementedException();
        }

        public Lead FindByOrderNo(string orderNo)
        {
            List<Lead> leadList = new List<Lead>();
            var con = _soDal.GetConnection();
            try
            {

                con.Open();
                List<WhereClause> criteria = new List<WhereClause>();
                criteria.Add(new WhereClause { Property = "OrderNo", SqlOperator = "=", Value = orderNo });
                leadList = _soDal.Find(criteria);
            }
            catch (Exception e)
            {
                Error.Add(e.Message);
            }
            finally
            {
                _soDal.Dispose();
            }
            return leadList.SingleOrDefault();
        }
        public List<LeadResponse> FindLead()
        {
            List<LeadResponse> leadList = new List<LeadResponse>();
            var con = _soDal.GetConnection();
            try
            {

                con.Open();
                leadList = _soDal.FindLead();
            }
            catch (Exception e)
            {
                Error.Add(e.Message);
            }
            finally
            {
                _soDal.Dispose();
            }
            return leadList;
        }
        public List<Lead> FindByDate(DateTime dateFrom, DateTime dateTo, string orderBy = "SourceSystemNo", bool useDefaultDeleted = true)
        {
            List<Lead> leadList = new List<Lead>();
            var con = _soDal.GetConnection();
            try
            {

                con.Open();
                List<WhereClause> criteria = new List<WhereClause>();
                leadList = _soDal.FindbyDate(dateFrom,dateTo,orderBy,useDefaultDeleted);
            }
            catch (Exception e)
            {
                Error.Add(e.Message);
            }
            finally
            {
                _soDal.Dispose();
            }
            return leadList;

        }

      
    }
}
