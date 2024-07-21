using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity
{
    public class PatientEntity : BaseEntity
    {
        private decimal Patient_ID;
        private string Patient_Name;
        private decimal Patient_Age;
        private string Patient_Gender;
        private string Patient_Address;

        [Key]
        public decimal PatientID
        {
            get { return Patient_ID; }
            set { Patient_ID = value; }
        }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please input Full Name")]
        [StringLength(50)]
        public string PatientName
        {
            get { return Patient_Name; }
            set { Patient_Name = value; }
        }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "Please input Age")]
        [StringLength(50)]
        public decimal Age
        {
            get { return Patient_Age; }
            set { Patient_Age = value; }
        }

     
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please select Gender")]
        public string Gender
        {
            get { return Patient_Gender; }
            set { Patient_Gender = value; }
        }
  
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please input Address")]
        [StringLength(250)]

        public string Address
        { 
            get { return Patient_Address; } 
            set { Patient_Address = value; } 
        }


     /*  [Display(Name = "PhoneNo")]
        [StringLength(12)]
        [Required(ErrorMessage = "Please input PhoneNo")]
        [RegularExpression("^([0-9]{12})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Contact
        {
            get { return Patient_Contact; }
            set { Patient_Contact = value; }
        }
*/
    }
}