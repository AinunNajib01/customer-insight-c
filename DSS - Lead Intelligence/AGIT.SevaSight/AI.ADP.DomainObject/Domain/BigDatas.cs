using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.ADP.DomainObject
{
    [Serializable]
    public partial class BigDatas : BaseDomain
    {

        #region Protected Variables
        protected String _astra_ID;
        protected String _name;
        protected String _dOB;
        protected String _birth_place;
        protected String _gender;
        protected String _religion;
        protected String _education;
        protected String _employee_type;
        protected String _marital_status;
        protected String _address;
        protected String _city;
        protected String _kecamatan;
        protected String _kelurahan;
        protected String _zip_code;
        protected String _bps_code;
        protected String _id_type;
        protected String _id_number;
        protected String _npwp;
        protected String _home_phone;
        protected String _office_phone;
        protected String _mobile_phone;
        protected String _fax_number;
        protected String _email;
        protected String _bUID;
        protected String _sourceSystem;
       
        #endregion

        #region Constructors/Destructors/Finalizers
        public BigDatas()
        : base()
        {

        }
        #endregion

        #region Public Properties
        public string Astra_ID
        {
            get
            {
                return _astra_ID;
            }

            set
            {
                _astra_ID = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }
        public string DOB
        {
            get
            {
                return _dOB;
            }

            set
            {
                _dOB = value;
            }
        }
        public string Birth_Place
        {
            get
            {
                return _birth_place;
            }

            set
            {
                _birth_place = value;
            }
        }
        public string Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                _gender = value;
            }
        }
        public string Religion
        {
            get
            {
                return _religion;
            }

            set
            {
                _religion = value;
            }
        }
        public string Education
        {
            get
            {
                return _education;
            }

            set
            {
                _education = value;
            }
        }
        public string Employee_Type
        {
            get
            {
                return _employee_type;
            }

            set
            {
                _employee_type = value;
            }
        }
        public string Marital_Status
        {
            get
            {
                return _marital_status;
            }

            set
            {
                _marital_status = value;
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }
        public string City
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
        public string Kecamatan
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
        public string Kelurahan
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
        public string Zip_Code
        {
            get
            {
                return _zip_code;
            }

            set
            {
                _zip_code = value;
            }
        }
        public string BPS_Code
        {
            get
            {
                return _bps_code;
            }

            set
            {
                _bps_code = value;
            }
        }
        public string ID_Type
        {
            get
            {
                return _id_type;
            }

            set
            {
                _id_type = value;
            }
        }
        
        public string ID_Number
        {
            get
            {
                return _id_number;
            }

            set
            {
                _id_number = value;
            }
        }
        public string NPWP
        {
            get
            {
                return _npwp;
            }

            set
            {
                _npwp = value;
            }
        }
        public string Home_Phone
        {
            get
            {
                return _home_phone;
            }

            set
            {
                _home_phone = value;
            }
        }
        public string Office_Phone
        {
            get
            {
                return _office_phone;
            }

            set
            {
                _office_phone = value;
            }
        }
        public string Mobile_Phone
        {
            get
            {
                return _mobile_phone;
            }

            set
            {
                _mobile_phone = value;
            }
        }
        public string Fax_Number
        {
            get
            {
                return _fax_number;
            }

            set
            {
                _fax_number = value;
            }
        }


        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }
        public string BUID
        {
            get
            {
                return _bUID;
            }

            set
            {
                _bUID = value;
            }
        }
        public string SourceSystem
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
        #endregion
    }
}
