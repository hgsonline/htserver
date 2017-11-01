using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTServer.Models
{
    public class PrvMedFacDivision
    {
        [Key]
        public int MedFacDivID { get; set; }
         
         
        //[ForeignKey("PrvMedicalFacility")] // Name of your navigation property below.
        public int MedFacID { get; set; }

       // public PrvMedicalFacility PrvMedicalFacility { get; set; }

        public int DivisionNumber { get; set; }
        public int NPI { get; set; }
        public string IRS_EIN { get; set; }
        public string DivisionRelation { get; set; }
        public string DivisionName { get; set; }
        public string DBAName { get; set; }  
         
        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
