using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class MbrHealthHx
    {
        [Key]
        public int MbrHealthHxID { get; set; }
        public int MemberID { get; set; }
        public string ICD10CM_Code { get; set; }
        public DateTime? OnSetDate { get; set; }
        public string AcuteChronic { get; set; }
        public int ConditionStatus { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
