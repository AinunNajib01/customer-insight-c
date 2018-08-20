using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.ADP.DomainObject;
using Dapper;
using AI.ADP.DomainObject.Models;
using Agit.Sevasight.Models;


namespace AI.ADP.DataAccess
{
    public partial interface ISalesOrderDataAccess
    { 
        IDbConnection GetConnection();
        void SetConnection(IDbConnection con);
        List<Lead> Find(List<WhereClause> whereClauses = null,
            string orderBy = "OrderNo", bool useDefaultDeleted = true);
        List<Lead> Find(string criteria, List<WhereClause> whereClause,
            string orderBy = "OrderNo", bool useDefaultDeleted = true);
        Lead FindFirst(string criteria, List<WhereClause> whereClauses,
            string orderBy = "OrderNo", bool useDefaultDeleted = true);

        List<LeadResponse> FindLead();
        int Create(SalesOrder lead);
        int Create(List<Lead> leadList);
        int Update(SalesOrder salesOrder);
        int UpdateStatusLead(SalesOrder lead);
        int Update(List<Lead> leadList);
        int Delete(Lead lead);
        int Delete(List<Lead> leadList);
        int Count(string criteria, List<WhereClause> whereClauses);
        int Count(List<WhereClause> whereClause);
        void Dispose();
        List<Lead> FindbyDate(DateTime dateFrom, DateTime dateTo, string orderBy, bool useDefaultDeleted);
    }

    public partial class SalesOrderDataAccess : BaseDAL, ISalesOrderDataAccess, IDisposable
    {
        #region public properties
        public delegate void OnSave(SalesOrder account);
        public event OnSave OnSaveEventHandler;

        public delegate void OnBeforeSave(SalesOrder account);
        public event OnBeforeSave OnBeforeSaveEventHandler;

        public delegate void OnBeforeCreate(SalesOrder account);
        public event OnBeforeCreate OnBeforeCreateEventHandler;

        public delegate void OnCreate(SalesOrder account);
        public event OnCreate OnCreateEventHandler;

        public delegate void OnBeforeUpdate(SalesOrder account);
        public event OnBeforeUpdate OnBeforeUpdateEventHandler;

        public delegate void OnUpdate(SalesOrder account);
        public event OnUpdate OnUpdateEventHandler;

        public delegate void OnBeforeDelete(SalesOrder account);
        public event OnBeforeDelete OnBeforeDeleteEventHandler;

        public delegate void OnDelete(SalesOrder account);
        public event OnDelete OnDeleteEventHandler;

        public IDbConnection Connection
        {
            get { CreateConnection(); return this.connection; }
            set { this.connection = value; }
        }
        #endregion

        #region Constructor
        public SalesOrderDataAccess() :base()
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
        public string _updateAllString()
        {
            return @"
               UPDATE [dbo].[TB_R_SALES_ORDER_TEST] WITH (ROWLOCK)
               SET
                [ID] = @ID,
                [DealerID] = @DealerID,
                [CustomerID] = @CustomerID,
                [KodeDealer] = @KodeDealer,
                [Nama] = @Nama,
                [NoKTP] = @NoKTP,
                [TanggalCetak] = @TanggalCetak,
                [TanggalMohon] = @TanggalMohon,
                [NoFaktur] = @NoFaktur,
                [NoRangka] = @NoRangka,
                [KodeMesin] = @KodeMesin,
                [NoMesin] = @NoMesin,
                [TipeMotor] = @TipeMotor,
                [Warna] = @Warna,
                [JenisKelamin] = @JenisKelamin,
                [TanggalLahir] = @TanggalLahir,
                [Alamat] = @Alamat,
                [Kelurahan] = @Kelurahan,
                [Kecamatan] = @Kecamatan,
                [Kota] = @Kota,
                [KodePos] = @KodePos,
                [KodeMesin] = @KodeMesin,
                [NoMesin] = @NoMesin,
                [TipeMotor] = @TipeMotor,
                [Warna] = @Warna,
                [JenisKelamin] = @JenisKelamin,
                [TanggalLahir] = @TanggalLahir,
                [Alamat] = @Alamat,
                [Kelurahan] = @Kelurahan,
                [Kecamatan] = @Kecamatan,
                [Kota] = @Kota,
                [KodePos] = @KodePos,
                [Provinsi] = @Provinsi,
                [Email] = @Email,
                [CashCredit] = @CashCredit,
                [UangMuka] = @UangMuka,
                [UangMukaAktual] = @UangMukaAktual,
                [Cicilan] = @Cicilan,
                [TenorBulan] = @TenorBulan,
                [TenorTahun] = @TenorTahun,
                [Agama] = @Agama,
                [Pekerjaan] = @Pekerjaan,
                [Pengeluaran] = @Pengeluaran,
                 [Pendidikan] = @Pendidikan,
                [StatusHP] = @StatusHP,
                [NoTelp] = @NoTelp,
                [NoHP] = @NoHP,
                [Umur] = @Umur,
                [RangeUmur] = @RangeUmur,
                [KebersediaanDihubungi] = @KebersediaanDihubungi,
                [Jenis3] = @Jenis3,
                [Jenis6] = @Jenis6,
                [MerkSebelumnya] = @MerkSebelumnya,
                [JenisSebelumnya] = @JenisSebelumnya,
                 [Fungsi] = @Fungsi,
                [Pemakai] = @Pemakai,
                [SalesPerson] = @SalesPerson,
                [TanggalVerifikasi] = @TanggalVerifikasi,
                [StatusVerifikasi] = @StatusVerifikasi,
                [Hobi] = @Hobi,
                [Facebook] = @Facebook,
                [Twitter] = @Twitter,
                [Instagram] = @Instagram,
                [Youtube] = @Youtube,
                [PIC] = @PIC,
                [StatusRumah] = @StatusRumah,
                [TipeATM] = @TipeATM,
                [TipeVarPlus] = @TipeVarPlus,
                [NoCustomer] = @NoCustomer,
                [ROType] = @ROType,
                [ROData] = @ROData,
                [NoAnggota] = @NoAnggota,
                [Region] = @Region,
                [Remark] = @Remark,
                [JenisSales] = @JenisSales,
                [ROIndex] = @ROIndex,
                [ROCount] = @ROCount,
                [UpdateRoIndexOn] = @UpdateRoIndexOn,
                [TanggalBeliSebelumnya] = @TanggalBeliSebelumnya,
                [TipeSebelumnya] = @TipeSebelumnya,
                [Jenis3Sebelumnya] = @Jenis3Sebelumnya,
                [Jenis6Sebelumnya] = @Jenis6Sebelumnya,
                [BuyingCycleMonth] = @BuyingCycleMonth,
                [BuyingCycleYear] = @BuyingCycleYear,
                [UpdateSebelumnyaOn] = @UpdateSebelumnyaOn,
                [MLJenisKe] = @MLJenisKe,
                [MLJenisUpdateOn] = @MLJenisUpdateOn,
                [Jenis6Selanjutnya] = @Jenis6Selanjutnya,
                [MLPeriodeKe] = @MLPeriodeKe,
                [MLPeriodeUpdateOn] = @MLPeriodeUpdateOn,
                [TanggalBeliSelanjutnya] = @TanggalBeliSelanjutnya,
                [LogID] = @LogID,
                [RowStatus] = @RowStatus,
                [CreatedBy] = @CreatedBy,
                [CreatedDate] = @CreatedDate,
                [TanggalBeliSelanjutnyaActual] = @TanggalBeliSelanjutnyaActual,
                [TipeSelanjutnyaActual] = @TipeSelanjutnyaActual,
                [Jenis3SelanjutnyaActual] = @Jenis3SelanjutnyaActual,
                [Jenis6SelanjutnyaActual] = @Jenis6SelanjutnyaActual,
                [BuyingCycleMonthSelanjutnyaActual] = @BuyingCycleMonthSelanjutnyaActual,
                [BuyingCycleYearSelanjutnyaActual] = @BuyingCycleYearSelanjutnyaActual,
                [UpdateSelanjutnyaActualOn] = @UpdateSelanjutnyaActualOn,
                [SeriesSelanjutnyaActual] = @SeriesSelanjutnyaActual,
                [SeriesSebelumnya] = @SeriesSebelumnya,
                [SeriesLead] = @SeriesLead,
                [StatusLead] = @StatusLead,
                [AppointmentSchedule] = @AppointmentSchedule,
                [Note] = @Note
               

                WHERE [ID] = @ID
            ";
        }

        public string _updateString()
        {
            return @"
               UPDATE [dbo].[TB_R_SALES_ORDER_TEST] WITH (ROWLOCK)
               SET
              
                [Nama] = @Nama,
                [NoKTP] = @NoKTP,
                [NoTelp] = @NoTelp,
                [Email] = @Email,
                [Jenis6] = @Jenis6,
                [TipeMotor] = @TipeMotor,
                [Jenis6Selanjutnya] = @Jenis6Selanjutnya,
                [TanggalBeliSelanjutnya] = @TanggalBeliSelanjutnya,
                [SalesPerson] = @SalesPerson,
                [statuslead] = @statuslead

                WHERE [ID] = @ID
            ";
        }

        public string _updateLeadStatusString()
        {
            return @"
               UPDATE [dbo].[TB_R_SALES_ORDER_TEST] WITH (ROWLOCK)
               SET
                [statuslead] = '2'

                WHERE [ID] = @ID
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

        public int Create(SalesOrder lead)
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

        public int Update(SalesOrder salesOrder)
        {
            int affected = 0;
            try
            {
                if (OnBeforeSaveEventHandler != null)
                    OnBeforeSaveEventHandler(salesOrder);
                if (OnBeforeCreateEventHandler != null)
                    OnBeforeCreateEventHandler(salesOrder);
                var q = _updateString();
                affected = connection.Execute(q, salesOrder);
                if (OnSaveEventHandler != null)
                    OnSaveEventHandler(salesOrder);
                if (OnCreateEventHandler != null)
                    OnCreateEventHandler(salesOrder);
            }
            catch (Exception e)
            {
                throw e;
            }
            return affected;
        }

        public int UpdateStatusLead(SalesOrder lead)
        {
            int affected = 0;
            try
            {
                if (OnBeforeSaveEventHandler != null)
                    OnBeforeSaveEventHandler(lead);
                if (OnBeforeCreateEventHandler != null)
                    OnBeforeCreateEventHandler(lead);
                var q = _updateLeadStatusString();
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

        public List<LeadResponse> FindLead()
        {
            List<LeadResponse> leadList = new List<LeadResponse>();

            try
            {
                var q = @"SELECT 
                           
                            [ID]
                          ,[Nama]
                          ,[NoKTP]
                          ,[NoTelp]
                          ,[Email]
                          ,[TanggalCetak]
                          ,[TipeMotor]
                          ,[Jenis6]
                          ,[Jenis6Selanjutnya]
                          ,[TanggalBeliSelanjutnya]
                          ,[SalesPerson]
                         
                    
                         
                        FROM TB_R_SALES_ORDER_TEST 
                        WHERE statuslead = 1 ";
                

              
                var param = new DynamicParameters();
               
                leadList = connection.Query<LeadResponse>(q, param, null, true, commandTimeOut, CommandType.Text).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return leadList;
        }
    }
}
