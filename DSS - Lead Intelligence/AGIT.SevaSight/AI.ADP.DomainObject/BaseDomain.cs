using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.ADP.DomainObject
{
    public class BaseDomain
    {

        protected DateTime _createdOn;
        protected String _createdBy;
        protected DateTime _modifiedOn;
        protected String _modifiedBy;
        protected String _rowStatus;
        protected DateTime dateSet;

        public BaseDomain()
        {
            dateSet = DateTime.MinValue;
        }

        private DateTime getDateConfig()
        {
            try
            {
                return DateTime.MinValue;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public DateTime CreatedOn
        {
            get
            {
                if (_createdOn == DateTime.MinValue || _createdOn == null)
                    return new DateTime(1900, 1, 1);
                return _createdOn;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _createdOn = dateSet;
                else
                    _createdOn = value;
            }
        }
        public String CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }
        public DateTime ModifiedOn
        {
            get
            {
                if (_modifiedOn == DateTime.MinValue || _modifiedOn == null)
                    return new DateTime(1900, 1, 1);
                return _modifiedOn;
            }
            set
            {
                if (dateSet != DateTime.MinValue)
                    _modifiedOn = dateSet;
                else
                    _modifiedOn = dateSet = value;
            }
        }
        public String ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }
            set
            {
                _modifiedBy = value;
            }
        }
        
        public String RowStatus
        {
            get
            {
                return _rowStatus;
            }
            set
            {
                _rowStatus = value;
            }
        }
    }
}
