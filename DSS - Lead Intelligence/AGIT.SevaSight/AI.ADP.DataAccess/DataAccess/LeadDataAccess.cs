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
    public partial interface ILeadDataAccess
    {
        IDbConnection GetConnection();
        void SetConnection(IDbConnection con);
        List<Lead> Find(List<WhereClause> whereClauses = null,
            string orderBy = "OrderNo", bool useDefaultDeleted = true);
        List<Lead> Find(string criteria, List<WhereClause> whereClause,
            string orderBy = "OrderNo", bool useDefaultDeleted = true);
        Lead FindFirst(string criteria, List<WhereClause> whereClauses,
            string orderBy = "OrderNo", bool useDefaultDeleted = true);

        List<Lead> FindBySyncStatus();
        int Create(Lead lead);
        int Create(List<Lead> leadList);
        int Update(Lead lead);
        int Update(List<Lead> leadList);
        int Delete(Lead lead);
        int Delete(List<Lead> leadList);
        int Count(string criteria, List<WhereClause> whereClauses);
        int Count(List<WhereClause> whereClause);
        void Dispose();
        List<Lead> FindbyDate(DateTime dateFrom, DateTime dateTo, string orderBy, bool useDefaultDeleted);
    }

    public partial class LeadDataAccess : BaseDAL, ILeadDataAccess, IDisposable
    {
        #region public properties
        public delegate void OnSave(Lead account);
        public event OnSave OnSaveEventHandler;

        public delegate void OnBeforeSave(Lead account);
        public event OnBeforeSave OnBeforeSaveEventHandler;

        public delegate void OnBeforeCreate(Lead account);
        public event OnBeforeCreate OnBeforeCreateEventHandler;

        public delegate void OnCreate(Lead account);
        public event OnCreate OnCreateEventHandler;

        public delegate void OnBeforeUpdate(Lead account);
        public event OnBeforeUpdate OnBeforeUpdateEventHandler;

        public delegate void OnUpdate(Lead account);
        public event OnUpdate OnUpdateEventHandler;

        public delegate void OnBeforeDelete(Lead account);
        public event OnBeforeDelete OnBeforeDeleteEventHandler;

        public delegate void OnDelete(Lead account);
        public event OnDelete OnDeleteEventHandler;

        public IDbConnection Connection
        {
            get { CreateConnection(); return this.connection; }
            set { this.connection = value; }
        }
        #endregion

        #region Constructor
        public LeadDataAccess() :base()
        {

        }
        #endregion

        #region Protected
        public string _insertString()
        {
            return @"
                    INSERT INTO [dbo].[LMS_Leads]
           (
              [OpportunityID]
              ,[OrderNo]
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
              ,[CreatedBy]
              ,[ModifiedOn]
              ,[ModifiedBy]
              ,[RowStatus]
              ,[Title]
              ,[Name1]
              ,[Name2]
              ,[Phone1]
              ,[Phone2]
              ,[Email1]
              ,[Email2]
              ,[Address1]
              ,[Address2]
              ,[Area]
              ,[City]
              ,[Postal]
              ,[Kelurahan]
              ,[Kecamatan]
              ,[Province]
              ,[PreferredDateToContacted]
              ,[PreferredBusinessArea]
              ,[Notes1]
              ,[ProspectVariant]
              ,[Program]
              ,[Score]
              ,[LeasingCompany]
              ,[LeasingInvoiceDate]
              ,[LeasingDueDate]
              ,[BusinessArea]
              ,[Company]
              ,[VerifiedCustomerSystem]
              ,[VerifiedCustomerNo]
              ,[VerifiedCustomerName]
              ,[DropReasonCode]
              ,[DropReasonDescription]
              ,[VerificationResultCode]
              ,[TestDriveStatus]
              ,[SourceSystem]
              ,[SourceSystemNo]
)
     VALUES
           (   
               @OpportunityID
              ,@OrderNo
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
              ,@CreatedBy
              ,@ModifiedOn
              ,@ModifiedBy
              ,@RowStatus
              ,@Title
              ,@Name1
              ,@Name2
              ,@Phone1
              ,@Phone2
              ,@Email1
              ,@Email2
              ,@Address1
              ,@Address2
              ,@Area
              ,@City
              ,@Postal
              ,@Kelurahan
              ,@Kecamatan
              ,@Province
              ,@PreferredDateToContacted
              ,@PreferredBusinessArea
              ,@Notes1
              ,@ProspectVariant
              ,@Program
              ,@Score
              ,@LeasingCompany
              ,@LeasingInvoiceDate
              ,@LeasingDueDate
              ,@BusinessArea
              ,@Company
              ,@VerifiedCustomerSystem
              ,@VerifiedCustomerNo
              ,@VerifiedCustomerName
              ,@DropReasonCode
              ,@DropReasonDescription
              ,@VerificationResultCode
              ,@TestDriveStatus
              ,@SourceSystem
              ,@SourceSystemNo
                )
                    ";
        }
        public string _updateString()
        {
            return @"
               UPDATE [dbo].[LMS_Leads] WITH (ROWLOCK)
               SET
                [SourceSystemNo] = @SourceSystemNo,
                [TestDriveTime] = @TestDriveTime,
                [TestDriveStatus] = @TestDriveStatus,
                [SalesmanName] = @SalesmanName,
                [ModelName] = @ModelName,
                [SalesSource] = @SalesSource,
                [DropReason] = @DropReason,
                [SPKCreatedTime] = @SPKCreatedTime,
                [DPTime] = @DPTime,
                [DPAmount] = @DPAmount,
                [FullPaymentTime] = @FullPaymentTime,
                [FullPaymentAmount] = @FullPaymentAmount,
                [PlanDeliveryDate] = @PlanDeliveryDate,
                [ActualDeliveryDate] = @ActualDeliveryDate,
                [STNKFinishedDate] = @STNKFinishedDate,
                [BPKBFinishedDate] = @BPKBFinishedDate,
                [SPKCancellationTime] = @SPKCancellationTime,
                [SalesOrderCancellationTime] = @SalesOrderCancellationTime,
                [BillingCancellationTime] = @BillingCancellationTime,
                [BillingReturnTime] = @BillingReturnTime,
                [SyncStatus] = @SyncStatus

                WHERE [SourceSystemNo] = @SourceSystemNo
            ";
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
        public int Count(List<WhereClause> whereClauses)
        {
            throw new NotImplementedException();
        }

        public int Count(string criteria, List<WhereClause> whereClauses)
        {
            throw new NotImplementedException();
        }

        public int Create(List<Lead> leadList)
        {
            throw new NotImplementedException();
        }

        public int Create(Lead lead)
        {
            int affected = 0;
            try
            {
                if (OnBeforeSaveEventHandler != null)
                    OnBeforeSaveEventHandler(lead);
                if (OnBeforeCreateEventHandler != null)
                    OnBeforeCreateEventHandler(lead);
                var q = _insertString();
                affected = connection.Execute(q, lead);
                if (OnSaveEventHandler != null)
                    OnSaveEventHandler(lead);
                if (OnCreateEventHandler != null)
                    OnCreateEventHandler(lead);
            }
            catch (Exception e)
            {
                throw e;
            }
            return affected;
        }

        public int Delete(List<Lead> leadList)
        {
            throw new NotImplementedException();
        }

        public int Delete(Lead lead)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.connection.Close();
            this.connection = null;
        }

        public List<Lead> Find(List<WhereClause> whereClauses = null, string orderBy = "LeadID", bool useDefaultDeleted = true)
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

        public List<Lead> Find(string criteria, List<WhereClause> whereClauses, string orderBy = "LeadID", bool useDefaultDeleted = true)
        {
            List<Lead> leadList = new List<Lead>();

            try
            {
                var q = @"SELECT 
                            OrderNo,
                            TestDriveTime,
                            TestDriveStatus,
                            SalesmanName,
                            ModelName,SalesSource,
                            DropReason,
                            SPKCreatedTime,
                            DPTime,
                            DPAmount,
                            FullPaymentTime,FullPaymentAmount,
                            PlanDeliveryDate,
                            ActualDeliveryDate,
                            STNKFinishedDate,BPKBFinishedDate,
                            SPKCancellationTime,
                            SalesOrderCancellationTime,
                            BillingCancellationTime,
                            CreatedOn,
                            BillingReturnTime,
                               OpportunityID
                        FROM LMS_Leads 
                        WHERE";
                if (useDefaultDeleted)
                    q += "(LMS_Leads.RowStatus=0 OR LMS_Leads.RowStatus IS NULL) {0} ORDER BY {1}";
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
                leadList = connection.Query<Lead>(q, param, null, true, commandTimeOut, CommandType.Text).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return leadList;
        }

       
        public Lead FindFirst(string criteria, List<WhereClause> whereClauses, string orderBy = "LeadID", bool useDefaultDeleted = true)
        {
            throw new NotImplementedException();
        }

       

        public int Update(List<Lead> leadList)
        {
            throw new NotImplementedException();
        }

        public int Update(Lead lead)
        {
            int affected = 0;
            try
            {
                if (OnBeforeSaveEventHandler != null)
                    OnBeforeSaveEventHandler(lead);
                if (OnBeforeCreateEventHandler != null)
                    OnBeforeCreateEventHandler(lead);
                var q = _updateString();
                affected = connection.Execute(q, lead);
                if (OnSaveEventHandler != null)
                    OnSaveEventHandler(lead);
                if (OnCreateEventHandler != null)
                    OnCreateEventHandler(lead);
            }
            catch (Exception e)
            {
                throw e;
            }
            return affected;
        }

        public List<Lead> FindbyDate(DateTime dateFrom, DateTime dateTo, string orderBy, bool useDefaultDeleted)
        {
            List<Lead> leadList = new List<Lead>();
            try
            {
                var q = @"
                            SELECT 
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
                          ,[RowStatus]
                          ,[Title]
                          ,[Name1]
                          ,[Name2]
                          ,[Phone1]
                          ,[Phone2]
                          ,[Email1]
                          ,[Email2]
                          ,[Address1]
                          ,[Address2]
                          ,[Area]
                          ,[City]
                          ,[Postal]
                          ,[Kelurahan]
                          ,[Kecamatan]
                          ,[Province]
                          ,[PreferredDateToContacted]
                          ,[PreferredBusinessArea]
                          ,[Notes1]
                          ,[ProspectVariant]
                          ,[Program]
                          ,[Score]
                          ,[LeasingCompany]
                          ,[LeasingInvoiceDate]
                          ,[LeasingDueDate]
                          ,[BusinessArea]
                          ,[Company]
                          ,[VerifiedCustomerSystem]
                          ,[VerifiedCustomerNo]
                          ,[VerifiedCustomerName]
                          ,[DropReasonCode]
                          ,[DropReasonDescription]
                          ,[VerificationResultCode]
                          ,[TestDriveStatus]
                          ,[SourceSystem]
                          ,[SourceSystemNo]
                          ,[OpportunityID]
                      FROM [dbo].[LMS_Leads] 
                        WHERE";
                if (useDefaultDeleted)
                    q += "(LMS_Leads.RowStatus=0 OR LMS_Leads.RowStatus IS NULL) {0} ORDER BY {1}";
                else
                    q += "1=1 {0} ORDER BY {1}";

                string wh = "AND CreatedOn BETWEEN '{2}' AND '{3}'";
                var param = new DynamicParameters();
               
                q = string.Format(q, wh, orderBy,dateFrom,dateTo);
                leadList = connection.Query<Lead>(q, param, null, true, commandTimeOut, CommandType.Text).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return leadList;
        }

        public List<Lead> FindBySyncStatus()
        {
            List<Lead> leadList = new List<Lead>();

            try
            {
                var q = @"SELECT 
                           
                            [TestDriveTime]
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
                          ,[RowStatus]
                          ,[Title]
                          ,[Name1]
                          ,[Name2]
                          ,[Phone1]
                          ,[Phone2]
                          ,[Email1]
                          ,[Email2]
                          ,[Address1]
                          ,[Address2]
                          ,[Area]
                          ,[City]
                          ,[Postal]
                          ,[Kelurahan]
                          ,[Kecamatan]
                          ,[Province]
                          ,[PreferredDateToContacted]
                          ,[PreferredBusinessArea]
                          ,[Notes1]
                          ,[ProspectVariant]
                          ,[Program]
                          ,[Score]
                          ,[LeasingCompany]
                          ,[LeasingInvoiceDate]
                          ,[LeasingDueDate]
                          ,[BusinessArea]
                          ,[Company]
                          ,[VerifiedCustomerSystem]
                          ,[VerifiedCustomerNo]
                          ,[VerifiedCustomerName]
                          ,[DropReasonCode]
                          ,[DropReasonDescription]
                          ,[VerificationResultCode]
                          ,[TestDriveStatus]
                          ,[SourceSystem]
                          ,[SourceSystemNo]
                          ,[OpportunityID]
                          ,[SyncStatus]
                    
                         
                        FROM LMS_Leads 
                        WHERE isNull(SyncStatus,0) = 0";
                

              
                var param = new DynamicParameters();
               
                leadList = connection.Query<Lead>(q, param, null, true, commandTimeOut, CommandType.Text).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return leadList;
        }
    }
}
