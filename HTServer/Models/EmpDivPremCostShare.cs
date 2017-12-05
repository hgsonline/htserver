using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class EmpDivPremCostShare
    {
        [Key]
        public int EmpPremCostShareID { get; set; }
               
        [ForeignKey("EmpDivWageTypeID")]
        public int EmpDivWageTypeID { get; set; }
        public EmpDivWageType EmpDivWageType { get; set; }

        public string RelationCode { get; set; }
        public string EmpSharePercentage { get; set; } 

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
