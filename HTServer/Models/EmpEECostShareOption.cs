using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class EmpEECostShareOption
    {
        [Key]
        public int EECostShareOptionID { get; set; }
 
        [ForeignKey("EEWageTypeID")]
        public int EEWageTypeID { get; set; }
        public EmpEEWageType EmpEEWageType { get; set; }

        [ForeignKey("MedicalCategoryID")]
        public int MedicalCategoryID { get; set; }
        public ActMedicalCategory ActMedicalCategory { get; set; }

        public string MemberCopayType { get; set; }
        public int MemberCopayamount { get; set; }
        public int CostShareOptionFactor { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
