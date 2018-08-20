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

namespace AI.ADP.Business
{
    public interface ILeadBusiness
    {
        List<string> Error { get; set; }
        List<Lead> Find(string criteria,
            List<WhereClause> criteriaValue, string orderBy = "AccountID", Boolean useDefaultDeleted = true);
        List<Lead> FindAll(Boolean useDefaultDeleted = true);
        Lead FindByOrderNo(string orderNo);
        List<Lead> FindBySyncStatus();
        List<Lead> FindByDate(DateTime dateFrom, DateTime dateTo, string orderBy = "SourceSystemNo", Boolean useDefaultDeleted = true);
        int Create(Lead lead);
        int Create(List<Lead> leadList);
        int Update(Lead lead);
        int Update(List<Lead> leadList);
        int Delete(Lead lead);
        int Delete(List<Lead> leadList);

    }


    public partial class LeadBusiness : ILeadBusiness
    {


        protected List<string> _error;
        public List<string> Error { get { return _error; } set { _error = value; } }

        protected ILeadDataAccess _leadDal;

        [Inject]
        public LeadBusiness(ILeadDataAccess leadDal)
        {
            _leadDal = leadDal;
            Error = new List<string>();
        }

        public int Create(List<Lead> leadList)
        {
            int i = 0;
            var con = _leadDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _leadDal.Create(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _leadDal.Dispose();
            }
            return i;
        }

        public int Create(Lead lead)
        {

            int i = 0;
            var con = _leadDal.GetConnection();
            try
            {
                con.Open();
                i = _leadDal.Create(lead);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _leadDal.Dispose();
            }
            return i;
        }

        public int Delete(List<Lead> leadList)
        {
            int i = 0;
            var con = _leadDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _leadDal.Delete(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _leadDal.Dispose();
            }
            return i;
        }

        public int Delete(Lead lead)
        {
            int i = 0;
            var con = _leadDal.GetConnection();
            try
            {
                con.Open();
                i = _leadDal.Delete(lead);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _leadDal.Dispose();
            }
            return i;
        }

        public int Update(List<Lead> leadList)
        {
            int i = 0;
            var con = _leadDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _leadDal.Update(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _leadDal.Dispose();
            }
            return i;
        }

        public int Update(Lead lead)
        {
            int i = 0;
            var con = _leadDal.GetConnection();
            try
            {
                con.Open();
                i = _leadDal.Update(lead);
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                _leadDal.Dispose();
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
            var con = _leadDal.GetConnection();
            try
            {

                con.Open();
                List<WhereClause> criteria = new List<WhereClause>();
                criteria.Add(new WhereClause { Property = "OrderNo", SqlOperator = "=", Value = orderNo });
                leadList = _leadDal.Find(criteria);
            }
            catch (Exception e)
            {
                Error.Add(e.Message);
            }
            finally
            {
                _leadDal.Dispose();
            }
            return leadList.SingleOrDefault();
        }
        public List<Lead> FindBySyncStatus()
        {
            List<Lead> leadList = new List<Lead>();
            var con = _leadDal.GetConnection();
            try
            {

                con.Open();
                leadList = _leadDal.FindBySyncStatus();
            }
            catch (Exception e)
            {
                Error.Add(e.Message);
            }
            finally
            {
                _leadDal.Dispose();
            }
            return leadList;
        }
        public List<Lead> FindByDate(DateTime dateFrom, DateTime dateTo, string orderBy = "SourceSystemNo", bool useDefaultDeleted = true)
        {
            List<Lead> leadList = new List<Lead>();
            var con = _leadDal.GetConnection();
            try
            {

                con.Open();
                List<WhereClause> criteria = new List<WhereClause>();
                leadList = _leadDal.FindbyDate(dateFrom,dateTo,orderBy,useDefaultDeleted);
            }
            catch (Exception e)
            {
                Error.Add(e.Message);
            }
            finally
            {
                _leadDal.Dispose();
            }
            return leadList;

        }

      
    }
}
