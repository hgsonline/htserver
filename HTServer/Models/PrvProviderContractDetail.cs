using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class PrvProviderContractDetail
    {
        [Key]
        public int ProviderContractDetailID { get; set; }
        public int ProviderContractID { get; set; }
        public string DetailDesc { get; set; }
        public string AgreementIntials { get; set; }
 
        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
