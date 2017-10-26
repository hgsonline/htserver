using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class MbrFamilyHealthHx
    {
        [Key]
        public int FamilyHealthHxID { get; set; }
        public int MemberID { get; set; }
        public string ICD10CM_Code { get; set; }
        public string FamilyMbr { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
