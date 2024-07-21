using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity
{
    [Keyless]
    public class ResponseEntity<T>
    {
        public Boolean Status { get; set; } = false;

        public string ErrorMsg { get; set; } = string.Empty;

        public T Entity { get; set; }
            
        public List<T> listEntity { get; set; }
    }
}
