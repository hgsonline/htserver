using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class MbrHealthLevel
    {
        [Key]
        public int MbrHealthLevelID { get; set; }
        public int MemberID { get; set; }
        public int HealthLevel { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
