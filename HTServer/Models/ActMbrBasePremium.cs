using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace HTServer.Models
{
    public class ActMbrBasePremium
    {
        [Key]
        public int MbrBasePremID { get; set; }
        public int MedicalCategoryID { get; set; }
        public int AgeGroupID { get; set; }
        public string Sex { get; set; }
        public int BasePremPMPM { get; set; }
        
        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
