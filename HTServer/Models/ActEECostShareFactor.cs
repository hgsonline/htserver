using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class ActEECostShareFactor
    {
        [Key]
        public int EECostShareFactorID { get; set; }
        public int MedicalCategoryID { get; set; }
        public string MbrCopayType { get; set; }
        public decimal MbrCopayAmount { get; set; }
        public decimal EECostShareFactor { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
