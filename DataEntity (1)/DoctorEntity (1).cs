using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity
{
    public class DoctorEntity : BaseEntity
    {

        private decimal Doctor_ID;
        private string Doctor_Name;
        private string Doctor_Specialization;
        private string Doctor_Contact;

        [Key]
        public decimal DoctorID
        { 
            get { return Doctor_ID; } 
            set { Doctor_ID = value; } 
        }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please input Full Name")]
        [StringLength(50)]

        public string DoctorName
        {
            get { return Doctor_Name; }
            set { Doctor_Name = value; }
        }

        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "Please input Specialization")]
        [StringLength(50)]
        public string Specialization
        {
            get { return Doctor_Specialization; }
            set { Doctor_Specialization = value; }
        }

        [Display(Name = "Contact")]
        [StringLength(12)]
        [Required(ErrorMessage = "Please input Contact")]
        [RegularExpression("^([0-9]{12})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Contact
        {
            get { return Doctor_Contact; }
            set { Doctor_Contact = value; }
        }

    }

}

