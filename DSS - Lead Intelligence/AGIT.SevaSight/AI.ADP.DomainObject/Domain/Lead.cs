using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.ADP.DomainObject
{
    [Serializable]
    public partial class Lead : BaseDomain
    {

        #region Protected Variables
        protected string _opportunityID;
        protected String _orderNo;
        protected DateTime _testDriveTime;
        protected String _salesmanName;
        protected String _modelName;
        protected String _salesSource;
        protected String _dropReason;
        protected DateTime _SPKCreatedTime;
        protected DateTime _DPTime;
        protected Decimal _DPAmount;
        protected DateTime _fullPaymentTime;
        protected Decimal _fullPaymentAmount;
        protected DateTime _planDeliveryDate;
        protected DateTime _actualDeliveryDate;
        protected DateTime _STNKFinishedDate;
        protected DateTime _BPKPFinishedDate;
        protected DateTime _SPKCancellationTime;
        protected DateTime _salesOrderCancellationTime;
        protected DateTime _billingCancellationTime;
        protected DateTime _billingReturnTime;
        protected String _title;
        protected String _name1;
        protected String _name2;
        protected String _phone1;
        protected String _phone2;
        protected String _email1;
        protected String _email2;
        protected String _address1;
        protected String _address2;
        protected String _area;
        protected String _city;
        protected String _postal;
        protected String _kelurahan;
        protected String _kecamatan;
        protected String _province;
        protected DateTime _preferredDateToContacted;
        protected String _preferredBusinessArea;
        protected String _notes1;
        protected String _prospectVariant;
        protected String _program;
        protected String _score;
        protected String _leasingCompany;
        protected DateTime _leasingInvoiceDate;
        protected DateTime _leasingDueDate;
        protected String _businessArea;
        protected String _company;
        protected String _verifiedCustomerSystem;
        protected String _verifiedCustomerNo;
        protected String _customerName;
        protected String _dropReasonCode;
        protected String _dropReasonDescription;
        protected String _verificationResultCode;
        protected String _verifiedCustomerName;
        protected String _testDriveStatus;
        protected String _sourceSystem;
        protected String _sourceSystemNo;
        private int _syncStatus;
        #endregion

        #region Constructors/Destructors/Finalizers
        public Lead()
        : base()
        {

        }
        #endregion

        #region Public Properties

        public string OpportunityID
        {
            get
            {
                return _opportunityID;
            }

            set
            {
                _opportunityID = value;
            }
        }
        public string OrderNo
        {
            get
            {
                return _orderNo;
            }

            set
            {
                _orderNo = value;
            }
        }

        public DateTime TestDriveTime
        {
            get
            {
                if (_testDriveTime == DateTime.MinValue || _testDriveTime == null)
                    return new DateTime(1900, 1, 1);
                return _testDriveTime;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _testDriveTime = dateSet;
                else
                    _testDriveTime = value;
            }
        }
        public string SalesmanName
        {
            get
            {
                return _salesmanName;
            }

            set
            {
                _salesmanName = value;
            }
        }
        public string ModelName
        {
            get
            {
                return _modelName;
            }

            set
            {
                _modelName = value;
            }
        }
        public string SalesSource
        {
            get
            {
                return _salesSource;
            }

            set
            {
                _salesSource = value;
            }
        }
        public string DropReason
        {
            get
            {
                return _dropReason;
            }

            set
            {
                _dropReason = value;
            }
        }
        public DateTime SPKCreatedTime
        {
            get
            {
                if (_SPKCreatedTime == DateTime.MinValue || _SPKCreatedTime == null)
                    return new DateTime(1900, 1, 1);
                return _SPKCreatedTime;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _SPKCreatedTime = dateSet;
                else
                    _SPKCreatedTime = value;
            }
        }
        public DateTime DPTime
        {
            get
            {
                if (_DPTime == DateTime.MinValue || _DPTime == null)
                    return new DateTime(1900, 1, 1);
                return _DPTime;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _DPTime = dateSet;
                else
                    _DPTime = value;
            }
        }
        public Decimal DPAmount
        {
            get
            {
                return _DPAmount;
            }

            set
            {
                _DPAmount = value;
            }
        }
        public DateTime FullPaymentTime
        {
            get
            {
                if (_fullPaymentTime == DateTime.MinValue || _fullPaymentTime == null)
                    return new DateTime(1900, 1, 1);
                return _fullPaymentTime;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _fullPaymentTime = dateSet;
                else
                    _fullPaymentTime = value;
            }
        }
        public Decimal FullPaymentAmount
        {
            get
            {
                return _fullPaymentAmount;
            }

            set
            {
                _fullPaymentAmount = value;
            }
        }
        public DateTime PlanDeliveryDate
        {
            get
            {
                if (_planDeliveryDate == DateTime.MinValue || _planDeliveryDate == null)
                    return new DateTime(1900, 1, 1);
                return _planDeliveryDate;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _planDeliveryDate = dateSet;
                else
                    _planDeliveryDate = value;
            }
        }
        public DateTime ActualDeliveryDate
        {
            get
            {
                if (_actualDeliveryDate == DateTime.MinValue || _actualDeliveryDate == null)
                    return new DateTime(1900, 1, 1);
                return _actualDeliveryDate;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _actualDeliveryDate = dateSet;
                else
                    _actualDeliveryDate = value;
            }
        }

        public DateTime STNKFinishedDate
        {
            get
            {
                if (_STNKFinishedDate == DateTime.MinValue || _STNKFinishedDate == null)
                    return new DateTime(1900, 1, 1);
                return _STNKFinishedDate;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _STNKFinishedDate = dateSet;
                else
                    _STNKFinishedDate = value;
            }
        }

        public DateTime BPKBFinishedDate
        {
            get
            {
                if (_BPKPFinishedDate == DateTime.MinValue || _BPKPFinishedDate == null)
                    return new DateTime(1900, 1, 1);
                return _BPKPFinishedDate;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _BPKPFinishedDate = dateSet;
                else
                    _BPKPFinishedDate = value;
            }
        }

        public DateTime SPKCancellationTime
        {
            get
            {
                if (_SPKCancellationTime == DateTime.MinValue || _SPKCancellationTime == null)
                    return new DateTime(1900, 1, 1);
                return _SPKCancellationTime;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _SPKCancellationTime = dateSet;
                else
                    _SPKCancellationTime = value;
            }
        }
        public DateTime SalesOrderCancellationTime
        {
            get
            {
                if (_salesOrderCancellationTime == DateTime.MinValue || _salesOrderCancellationTime == null)
                    return new DateTime(1900, 1, 1);
                return _salesOrderCancellationTime;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _salesOrderCancellationTime = dateSet;
                else
                    _salesOrderCancellationTime = value;
            }
        }

        public DateTime BillingCancellationTime
        {
            get
            {
                if (_billingCancellationTime == DateTime.MinValue || _billingCancellationTime == null)
                    return new DateTime(1900, 1, 1);
                return _billingCancellationTime;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _billingCancellationTime = dateSet;
                else
                    _billingCancellationTime = value;
            }
        }
        public DateTime BillingReturnTime
        {
            get
            {
                if (_billingReturnTime == DateTime.MinValue || _billingReturnTime == null)
                    return new DateTime(1900, 1, 1);
                return _billingReturnTime;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _billingReturnTime = dateSet;
                else
                    _billingReturnTime = value;
            }
        }

        public String Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public String Name1
        {
            get
            {
                return _name1;
            }
            set
            {
                _name1 = value;
            }
        }
        public String Name2
        {
            get
            {
                return _name2;
            }
            set
            {
                _name2 = value;
            }
        }

        public String Phone1
        {
            get
            {
                return _phone1;
            }
            set
            {
                _phone1 = value;
            }
        }
        public String Phone2
        {
            get
            {
                return _phone2;
            }
            set
            {
                _phone2 = value;
            }
        }

        public String Email1
        {
            get
            {
                return _email1;
            }
            set
            {
                _email1 = value;
            }
        }

        public String Email2
        {
            get
            {
                return _email2;
            }
            set
            {
                _email2 = value;
            }
        }

        public String Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                _address1 = value;
            }
        }

        public String Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
            }
        }

        public String Area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
            }
        }

        public String City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        public String Postal
        {
            get
            {
                return _postal;
            }
            set
            {
                _postal = value;
            }
        }
        public String Kelurahan
        {
            get
            {
                return _kelurahan;
            }
            set
            {
                _kelurahan = value;
            }
        }
        public String Kecamatan
        {
            get
            {
                return _kecamatan;
            }
            set
            {
                _kecamatan = value;
            }
        }
        public String Province
        {
            get
            {
                return _province;
            }
            set
            {
                _province = value;
            }
        }
        public DateTime PreferredDateToContacted
        {
            get
            {
                if (_preferredDateToContacted == DateTime.MinValue || _preferredDateToContacted == null)
                    return new DateTime(1900, 1, 1);
                return _preferredDateToContacted;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _preferredDateToContacted = dateSet;
                else
                    _preferredDateToContacted = value;
            }
        }

        public String PreferredBusinessArea
        {
            get
            {
                return _preferredBusinessArea;
            }
            set
            {
                _preferredBusinessArea = value;
            }
        }

        public String Notes1
        {
            get
            {
                return _notes1;
            }
            set
            {
                _notes1 = value;
            }
        }

        public String ProspectVariant
        {
            get
            {
                return _prospectVariant;
            }
            set
            {
                _prospectVariant = value;
            }
        }

        public String Program
        {
            get
            {
                return _program;
            }
            set
            {
                _program = value;
            }
        }

        public String Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        public String LeasingCompany
        {
            get
            {
                return _leasingCompany;
            }
            set
            {
                _leasingCompany = value;
            }
        }

        public DateTime LeasingInvoiceDate
        {
            get
            {
                if (_leasingInvoiceDate == DateTime.MinValue || _leasingInvoiceDate == null)
                    return new DateTime(1900, 1, 1);
                return _leasingInvoiceDate;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _leasingInvoiceDate = dateSet;
                else
                    _leasingInvoiceDate = value;
            }
        }

        public DateTime LeasingDueDate
        {
            get
            {
                if (_leasingDueDate == DateTime.MinValue || _leasingDueDate == null)
                    return new DateTime(1900, 1, 1);
                return _leasingDueDate;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _leasingDueDate = dateSet;
                else
                    _leasingDueDate = value;
            }
        }

        public String BusinessArea
        {
            get
            {
                return _businessArea;
            }
            set
            {
                _businessArea = value;
            }
        }

        public String Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
            }
        }

        public String VerifiedCustomerSystem
        {
            get
            {
                return _verifiedCustomerSystem;
            }
            set
            {
                _verifiedCustomerSystem = value;
            }
        }

        public String DropReasonCode
        {
            get
            {
                return _dropReasonCode;
            }
            set
            {
                _dropReasonCode = value;
            }
        }

        public String DropReasonDescription
        {
            get
            {
                return _dropReasonDescription;
            }
            set
            {
                _dropReasonDescription = value;
            }
        }

        public String VerificationResultCode
        {
            get
            {
                return _verificationResultCode;
            }
            set
            {
                _verificationResultCode = value;
            }
        }

        public String VerifiedCustomerNo
        {
            get
            {
                return _verifiedCustomerNo;
            }
            set
            {
                _verifiedCustomerNo = value;
            }
        }
        public String VerifiedCustomerName
        {
            get
            {
                return _verifiedCustomerName;
            }
            set
            {
                _verifiedCustomerName = value;
            }
        }

        public String TestDriveStatus
        {
            get
            {
                return _testDriveStatus;
            }
            set
            {
                _testDriveStatus = value;
            }
        }
        public String SourceSystem
        {
            get
            {
                return _sourceSystem;
            }
            set
            {
                _sourceSystem = value;
            }
        }
        public String SourceSystemNo
        {
            get
            {
                return _sourceSystemNo;
            }
            set
            {
                _sourceSystemNo = value;
            }
        }

        public int SyncStatus
        {
            get
            {
                return _syncStatus;
            }
            set
            {
                _syncStatus = value;
            }
        }
        #endregion
    }
}
