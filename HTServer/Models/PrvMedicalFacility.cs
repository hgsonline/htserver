using System;
using System.ComponentModel.DataAnnotations;

namespace HTServer.Models
{
    public class PrvMedicalFacility
    {
        [Key]
        public int MedFacID { get; set; }
        public int NPI { get; set; }
        public string IRS_EIN { get; set; }
        public string CompanyName { get; set; }
        public string DBAName { get; set; } 

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
