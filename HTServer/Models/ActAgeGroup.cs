using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class ActAgeGroup
    {
        [Key]
        public int AgeGroupID { get; set; }
        public string Description { get; set; }
    }
}
