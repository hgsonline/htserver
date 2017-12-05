using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class EmpEEFamily
    {
        [Key]
        public int EEFamilyID { get; set; }

        [ForeignKey("EmpDivEEID")]
        public int EmpDivEEID { get; set; }
        public EmpEmpDivEE EmpEmpDivEE { get; set; }


        [ForeignKey("MemberID")]
        public int MemberID { get; set; }
        //public EmpMemberDep EmpMemberDep { get; set; }


        public string  RelationCode { get; set; } 

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
