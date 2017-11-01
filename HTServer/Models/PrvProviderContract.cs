using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class PrvProviderContract
    {
        [Key]
        public int ProviderContractID { get; set; }
        public int ProviderID { get; set; }
        public DateTime? CoverageEffDate { get; set; }
        public DateTime? CoverageTermDate { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
