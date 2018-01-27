using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class datNAICS
    {
        [Key]
        public int Code { get; set; }
        public string Sector { get; set; }
        public string SubSector { get; set; }
        public string IndustryGroup { get; set; }
    }

    public class shortCodes
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
