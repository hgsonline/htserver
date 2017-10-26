using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class MbrPremPMPM
    {
        [Key]
        public int MbrPremPMPMID { get; set; }
        public int MemberID { get; set; }
        public int MedicalCategoryID { get; set; }
        public int BasePremPMPM { get; set; }
        public int HealthIntensityFactor { get; set; }
        public int EECostShareFactor { get; set; }
        public int PLANFactorTotal { get; set; }
        public int PLANPremPMPM { get; set; }
        public int EmployerSharePer { get; set; }
        public int EmployerPremPMPM { get; set; }
        public int EECostShareOptionFactor { get; set; }
        public int MbrPremPM { get; set; }
        public DateTime? CalculationDate { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
