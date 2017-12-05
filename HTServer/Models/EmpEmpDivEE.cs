using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class EmpEmpDivEE
    {
        [Key]
        public int EmpDivEEID { get; set; }

      
        [ForeignKey("EmpDivID")]
        public int EmpDivID { get; set; }
        //public EmpEmployerDiv EmpEmployerDiv { get; set; }


        [ForeignKey("MemberID")]
        public int MemberID { get; set; }
        //public EmpMemberDep EmpMemberDep { get; set; }



        public DateTime? HireDate { get; set; }
        public DateTime? CoverageEffDate { get; set; }
        public DateTime? TermDate { get; set; }
        public DateTime? CoverageTermDate { get; set; }

        public int? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
