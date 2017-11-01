using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace HTServer.Models
{
    public class ActEmployerPremiumCostShare
    {
        [Key]
        public int EmpPremCostShareID { get; set; }
        public string RelationCode { get; set; }
        public int EmployerSharePercent { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
