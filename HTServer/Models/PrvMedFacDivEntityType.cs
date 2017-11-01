using System;
using System.ComponentModel.DataAnnotations;

namespace HTServer.Models
{
    public class PrvMedFacDivEntityType
    {
        [Key]
        public int MedFacDivEntityTypeID { get; set; }
        public int MedFacDivID { get; set; }
        public string EntityType { get; set; } 
        public string NAICS_ID { get; set; } 

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
