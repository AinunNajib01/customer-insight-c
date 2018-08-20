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
using AI.ADP.DomainObject.Models;
namespace AI.ADP.Business
{
    public interface IBigDataBusiness
    {
        List<string> Error { get; set; }
        BigDatas FindById(String orderno);
        List<BigDatas> Find(string criteria,
            List<WhereClause> criteriaValue, string orderBy = "orderno", Boolean useDefaultDeleted = true);
        List<BigDatas> FindAll(Boolean useDefaultDeleted = true);
        BigDatas checkCustomer(RequestCheckCustomer customer);
        BigDatas getPIIData(string astraID);
        int Create(BigDatas lead);
        int Create(List<BigDatas> leadList);
        int Update(BigDatas lead);
        int Update(List<BigDatas> leadList);
        int Delete(BigDatas lead);
        int Delete(List<BigDatas> leadList);

    }


    public partial class BigDataBusiness : IBigDataBusiness
    {


        protected List<string> _error;
        public List<string> Error { get { return _error; } set { _error = value; } }

        protected IBigDataDataAccess _bigDal;

        [Inject]
        public BigDataBusiness(IBigDataDataAccess bigdataDal)
        {
            _bigDal = bigdataDal;
            Error = new List<string>();
        }

        public int Create(List<BigDatas> leadList)
        {
            int i = 0;
            var con = _bigDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _bigDal.Create(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                Error.Add(e.Message);
            }
            finally
            {
                _bigDal.Dispose();
            }
            return i;
        }

        public int Create(BigDatas lead)
        {

            int i = 0;
            var con = _bigDal.GetConnection();
            try
            {
                con.Open();
                i = _bigDal.Create(lead);
            }
            catch (Exception e)
            {

                Error.Add(e.Message);
            }
            finally
            {
                _bigDal.Dispose();
            }
            return i;
        }

        public int Delete(List<BigDatas> leadList)
        {
            int i = 0;
            var con = _bigDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _bigDal.Delete(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                Error.Add(e.Message);
            }
            finally
            {
                _bigDal.Dispose();
            }
            return i;
        }

        public int Delete(BigDatas lead)
        {
            int i = 0;
            var con = _bigDal.GetConnection();
            try
            {
                con.Open();
                i = _bigDal.Delete(lead);
            }
            catch (Exception e)
            {

                Error.Add(e.Message);
            }
            finally
            {
                _bigDal.Dispose();
            }
            return i;
        }

        public List<BigDatas> Find(string criteria, List<WhereClause> criteriaValue, string orderBy = "AccountID", bool useDefaultDeleted = true)
        {
            throw new NotImplementedException();
        }

        public List<BigDatas> FindAll(bool useDefaultDeleted = true)
        {
            throw new NotImplementedException();
        }

        public BigDatas FindById(string AccountID)
        {
            throw new NotImplementedException();
        }

        public int Update(List<BigDatas> leadList)
        {
            int i = 0;
            var con = _bigDal.GetConnection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    con.Open();
                    i = _bigDal.Update(leadList);
                    ts.Complete();
                }
            }
            catch (Exception e)
            {

                Error.Add(e.Message);
            }
            finally
            {
                _bigDal.Dispose();
            }
            return i;
        }

        public int Update(BigDatas lead)
        {
            int i = 0;
            var con = _bigDal.GetConnection();
            try
            {
                con.Open();
                i = _bigDal.Update(lead);
            }
            catch (Exception e)
            {

                Error.Add(e.Message);
            }
            finally
            {
                _bigDal.Dispose();
            }
            return i;
        }

        public BigDatas checkCustomer(RequestCheckCustomer customer)
        {
            List<BigDatas> bigDataList = new List<BigDatas>();
            var con = _bigDal.GetConnection();
            try
            {
                con.Open();
                List<WhereClause> criteria = new List<WhereClause>();
                criteria.Add(new WhereClause { Property = "BUID", SqlOperator = "=", Value = customer.BUID });
                criteria.Add(new WhereClause { Property = "SourceSystem", SqlOperator = "=", Value = customer.SourceSystem });
                bigDataList = _bigDal.Find(criteria);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _bigDal.Dispose();
            }
            return bigDataList.SingleOrDefault();
        }

        public BigDatas getPIIData(string astra_ID)
        {
            List<BigDatas> bigDataList = new List<BigDatas>();
            var con = _bigDal.GetConnection();
            try
            {
                con.Open();
                List<WhereClause> criteria = new List<WhereClause>();
                criteria.Add(new WhereClause { Property = "Astra_ID", SqlOperator = "=", Value = astra_ID });
                bigDataList = _bigDal.Find(criteria);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _bigDal.Dispose();
            }
            return bigDataList.SingleOrDefault();
        }
    }
}
