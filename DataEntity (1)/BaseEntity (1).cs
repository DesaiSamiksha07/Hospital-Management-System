using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity
{
    public class BaseEntity
    {
        private decimal Created_By;
        private decimal? Updated_By;
        private decimal? Deleted_By;
        private DateTime Created_On;
        private DateTime? Updated_On;
        private DateTime? Deleted_On;
        private Boolean Is_Active;

        public decimal CreatedBy
        {
            get { return Created_By; }
            set { Created_By = value; }
        }

        public decimal? UpdatedBy
        {
            get { return Updated_By; }
            set { Updated_By = value; }
        }
        public decimal? DeletedBy
        {
            get { return Deleted_By; }
            set { Deleted_By = value; }
        }

        public DateTime CreatedOn
        {
            get { return Created_On; }
            set { Created_On = value; }
        }

        public DateTime? UpdatedOn
        {
            get { return Updated_On; }
            set { Updated_On = value; }
        }

        public DateTime? DeletedOn
        {
            get { return Deleted_On; }
            set { Deleted_On = value; }
        }
        public Boolean IsActive
        {
            get { return Is_Active; }
            set { Is_Active = value; }
        }
    }
}
