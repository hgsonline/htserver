using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace HTServer.Models
{
    public class ActHealthIntensityFactor
    {
        [Key]
        public int HealthIntensityFactorID { get; set; }
        public int MedicalCategoryID { get; set; }
        public int AgeGroupID { get; set; }
        public string Sex { get; set; }
        public int HealthyLevel { get; set; }
       public int HealthIntensityFactor { get; set; }


        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
