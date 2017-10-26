using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTServer.Models
{
    public class HTDataContext : DbContext
    {
        public HTDataContext(DbContextOptions<HTDataContext> options)
            : base(options)
        {
        }
        public DbSet<UserMasterTB> usermastertb { get; set; }
        public DbSet<TokenManager> tokenmanager { get; set; }
        public DbSet<UserType> usertype { get; set; }

        public DbSet<EmpEntityType> empentitytype { get; set; }

    }
}
