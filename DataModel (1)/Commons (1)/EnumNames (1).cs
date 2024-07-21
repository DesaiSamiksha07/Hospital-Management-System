using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Commons
{
    public class EnumNames
    {
        public enum MyEnum
        {
            [Display(Name = "Male")]
            Male,
            [Display(Name = "Female")]
            Female,
            [Display(Name = "Other")]
            Others
        }


    }
}
