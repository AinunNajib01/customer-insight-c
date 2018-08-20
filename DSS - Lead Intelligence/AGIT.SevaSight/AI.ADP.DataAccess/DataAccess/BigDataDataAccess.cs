using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.ADP.DomainObject;
using Dapper;
using AI.ADP.DomainObject;



namespace AI.ADP.DataAccess
{
    public partial interface IBigDataDataAccess
    {
        IDbConnection GetConnection();
        void SetConnection(IDbConnection con);
        List<BigDatas> Find(List<WhereClause> whereClauses = null,
            string orderBy = "Astra_ID", bool useDefaultDeleted = true);
        List<BigDatas> Find(string criteria, List<WhereClause> whereClauses,
            string orderBy = "Astra_ID", bool useDefaultDeleted = true);
        
        int Create(BigDatas bigdata);
        int Create(List<BigDatas> bigdataList);
        int Update(BigDatas bigdata);
        int Update(List<BigDatas> bigdataList);
        int Delete(BigDatas bigdata);
        int Delete(List<BigDatas> bigdataList);
        int Count(string criteria, List<WhereClause> whereClause);
        int Count(List<WhereClause> whereClause);
        void Dispose();
    }

    public partial class BigDataDataAccess : BaseDAL, IBigDataDataAccess, IDisposable
    {
        #region public properties
        public delegate void OnSave(BigDatas bigdata);
        public event OnSave OnSaveEventHandler;

        public delegate void OnBeforeSave(BigDatas bigdata);
        public event OnBeforeSave OnBeforeSaveEventHandler;

        public delegate void OnBeforeCreate(BigDatas bigdata);
        public event OnBeforeCreate OnBeforeCreateEventHandler;

        public delegate void OnCreate(BigDatas bigdata);
        public event OnCreate OnCreateEventHandler;

        public delegate void OnBeforeUpdate(BigDatas bigdata);
        public event OnBeforeUpdate OnBeforeUpdateEventHandler;

        public delegate void OnUpdate(BigDatas bigdata);
        public event OnUpdate OnUpdateEventHandler;

        public delegate void OnBeforeDelete(BigDatas bigdata);
        public event OnBeforeDelete OnBeforeDeleteEventHandler;

        public delegate void OnDelete(BigDatas bigdata);
        public event OnDelete OnDeleteEventHandler;

        public IDbConnection Connection
        {
            get { CreateConnection(); return this.connection; }
            set { this.connection = value; }
        }
        #endregion

        #region Constructor
        public BigDataDataAccess() : base()
        {

        }
        #endregion

        #region Protected
        public string _insertString()
        {
            return @"
                    INSERT INTO [dbo].[LMS_Leads]
           ([OrderNo]
           ,[TestDriveTime]
           ,[SalesmanName]
           ,[ModelName]
           ,[SalesSource]
           ,[DropReason]
           ,[SPKCreatedTime]
           ,[DPTime]
           ,[DPAmount]
           ,[FullPaymentTime]
           ,[FullPaymentAmount]
           ,[PlanDeliveryDate]
           ,[ActualDeliveryDate]
           ,[STNKFinishedDate]
           ,[BPKBFinishedDate]
           ,[SPKCancellationTime]
           ,[SalesOrderCancellationTime]
           ,[BillingCancellationTime]
           ,[BillingReturnTime]
           ,[CreatedOn]
           ,[CreatedBy]
           ,[ModifiedOn]
           ,[ModifiedBy]
           ,[Title]
           ,[Name1]
           ,[Name2]
           ,[Phone1]
           ,[Phone2]
           ,[Email1]
           ,[Email2]
           ,[Address1]
           ,[Address2]
           ,[AreaCode]
           ,[CityCode]
           ,[PostalCode]
           ,[KelurahanCode]
           ,[KecamatanCode]
           ,[ProvinceCode]
           ,[PreferredDateToContacted]
           ,[PreferredBusinessAreaCode]
           ,[Notes1]
           ,[ProspectVariant]
           ,[Program]
           ,[Score]
           ,[LeasingCompany]
           ,[LeasingInvoiceDate]
           ,[LeasingDueDate]
           ,[BusinessAreaCode]
           ,[CompanyCode]
           ,[VerifiedCustomerSystem]
           ,[VerifiedCustomerNo]
           ,[VerifiedCustomerName]
           ,[DropReasonCode]
           ,[DropReasonDescription]
           ,[VerificationResultCode])
     VALUES
           (@OrderNo
           ,@TestDriveTime
           ,@SalesmanName
           ,@ModelName
           ,@SalesSource
           ,@DropReason
           ,@SPKCreatedTime
           ,@DPTime
           ,@DPAmount
           ,@FullPaymentTime
           ,@FullPaymentAmount
           ,@PlanDeliveryDate
           ,@ActualDeliveryDate
           ,@STNKFinishedDate
           ,@BPKBFinishedDate
           ,@SPKCancellationTime
           ,@SalesOrderCancellationTime
           ,@BillingCancellationTime
           ,@BillingReturnTime
           ,@CreatedOn
           ,@CreatedBy
           ,@ModifiedOn
           ,@ModifiedBy
           ,@Title
           ,@Name1
           ,@Name2
           ,@Phone1
           ,@Phone2
           ,@Email1
           ,@Email2
           ,@Address1
           ,@Address2
           ,@AreaCode
           ,@CityCode
           ,@PostalCode
           ,@KelurahanCode
           ,@KecamatanCode
           ,@ProvinceCode
           ,@PreferredDateToContacted
           ,@PreferredBusinessAreaCode
           ,@Notes1
           ,@ProspectVariant
           ,@Program
           ,@Score
           ,@LeasingCompany
           ,@LeasingInvoiceDate
           ,@LeasingDueDate
           ,@BusinessAreaCode
           ,@CompanyCode
           ,@VerifiedCustomerSystem
           ,@VerifiedCustomerNo
           ,@VerifiedCustomerName
           ,@DropReasonCode
           ,@DropReasonDescription
           ,@VerificationResultCode)
                    ";
        }
        public string _updateString()
        {
            return @"";
        }
        public string _deleteString()
        {
            return @"";
        }
        #endregion

        public IDbConnection GetConnection()
        {
            return this.Connection;
        }

        public void SetConnection(IDbConnection con)
        {
            this.Connection = con;
        }
        public int Count(List<WhereClause> whereClause)
        {
            throw new NotImplementedException();
        }

        public int Count(string criteria, List<WhereClause> whereClause)
        {
            throw new NotImplementedException();
        }

        public int Create(List<BigDatas> bigdataList)
        {
            throw new NotImplementedException();
        }

        public int Create(BigDatas bigdata)
        {
            int affected = 0;
            try
            {
                if (OnBeforeSaveEventHandler != null)
                    OnBeforeSaveEventHandler(bigdata);
                if (OnBeforeCreateEventHandler != null)
                    OnBeforeCreateEventHandler(bigdata);
                var q = _insertString();
                affected = connection.Execute(q, bigdata);
                if (OnSaveEventHandler != null)
                    OnSaveEventHandler(bigdata);
                if (OnCreateEventHandler != null)
                    OnCreateEventHandler(bigdata);
            }
            catch (Exception e)
            {
                throw e;
            }
            return affected;
        }

        public int Delete(List<BigDatas> bigdataList)
        {
            throw new NotImplementedException();
        }

        public int Delete(BigDatas bigdata)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.connection.Close();
            this.connection = null;
        }

        public List<BigDatas> Find(List<WhereClause> whereClauses = null, string orderBy = "LeadID", bool useDefaultDeleted = true)
        {

            string wh = "";
            if (whereClauses != null)
            {
                List<string> listWhere = new List<string>();
                foreach (var whereClause in whereClauses)
                {
                    if (whereClause.SqlOperator.ToLower().Contains("like"))
                        listWhere.Add(string.Format("{0} {1} '%'+@{2}+'%'", whereClause.Property, whereClause.SqlOperator, whereClause.Property));
                    else
                        listWhere.Add(string.Format("{0} {1} @{2}", whereClause.Property, whereClause.SqlOperator, whereClause.Property));
                }
                wh = string.Join(" and ", listWhere.ToArray());
            }
            return Find(wh, whereClauses, orderBy, useDefaultDeleted);
        }

        public List<BigDatas> Find(string criteria, List<WhereClause> whereClauses, string orderBy = "LeadID", bool useDefaultDeleted = true)
        {
            List<BigDatas> bigDataList = new List<BigDatas>();

            try
            {
                var q = @"SELECT 
      ASTRA_ID
      ,NAME
      ,EMAIL
      ,Mobile_Phone
      ,BUID
      ,CreatedOn
      ,CreatedBy
      ,ModifiedOn
      ,ModifiedBy
      ,DOB
      ,Birth_Place
      ,GENDER
      ,RELIGION
      ,EDUCATION
      ,EMPLOYEE_TYPE
      ,MARITAL_STATUS
      ,Address
      ,CITY
      ,KECAMATAN
      ,KELURAHAN
      ,ZIP_CODE
      ,BPS_CODE
      ,ID_TYPE
      ,ID_number
      ,NPWP
      ,Home_phone
      ,Office_phone
      ,fax_number
      ,SourceSystem
                        FROM CustomerData 
                        WHERE";
                if (useDefaultDeleted)
                    q += "(CustomerData.RowStatus=0 OR CustomerData.RowStatus IS NULL) {0} ORDER BY {1}";
                else
                    q += "1=1 {0} ORDER BY {1}";

                string wh = "";
                var param = new DynamicParameters();
                if (!string.IsNullOrEmpty(criteria))
                {
                    foreach (var whereClause in whereClauses)
                    {
                        param.Add("@" + whereClause.Property, whereClause.Value, null, null, null);
                    }
                    wh = " and " + criteria;
                }
                q = string.Format(q, wh, orderBy);
                bigDataList = connection.Query<BigDatas>(q, param, null, true, commandTimeOut, CommandType.Text).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return bigDataList;
        }
        
        
        public Lead FindFirst(string criteria, List<WhereClause> whereClauses, string orderBy = "LeadID", bool useDefaultDeleted = true)
        {
            throw new NotImplementedException();
        }



        public int Update(List<BigDatas> leadList)
        {
            throw new NotImplementedException();
        }

        public int Update(BigDatas lead)
        {
            throw new NotImplementedException();
        }
        
    }
    
}
