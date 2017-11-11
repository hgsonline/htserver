using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class States
    {
        [Key]
        public int id { get; set; }
        public string abbreviation { get; set; }
        public string state { get; set; }
    }
}
