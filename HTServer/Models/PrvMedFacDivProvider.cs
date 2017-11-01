using System;
using System.ComponentModel.DataAnnotations;

namespace HTServer.Models
{
    public class PrvMedFacDivProvider
    {
        [Key]
        public int MedFacDivPrvID { get; set; }
        public int MedFacDivID { get; set; }
        public int ProviderID { get; set; }
        public DateTime? HireDate { get; set; } 
        public DateTime? TermDate { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
