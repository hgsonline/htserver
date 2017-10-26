using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class PrvProvider
    {
        [Key]
        public int ProviderID { get; set; }
        public int NPI { get; set; }
        public string GovtID { get; set; }
        public DateTime? DOB { get; set; }
        public string Sex { get; set; }
        public DateTime? DateAssumed { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NameSuffix { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
