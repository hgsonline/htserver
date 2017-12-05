using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class EmpEECostShareFactor
    {
        [Key]
        public int EECostShareFactorID { get; set; }


        [ForeignKey("EmpDivWageTypeID")]
        public int EmpDivWageTypeID { get; set; }
        public EmpDivWageType EmpDivWageType { get; set; }


        [ForeignKey("MedicalCategoryID")]
        public int MedicalCategoryID { get; set; }
        public ActMedicalCategory ActMedicalCategory { get; set; } 

        public string MemberCopayType { get; set; }
        public int MemberCopayamount { get; set; }
        public int CostShareFactor { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
