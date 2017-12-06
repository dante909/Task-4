using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Models;

namespace DAL.Contexts
{
    public class AppContext : DbContext
    {
        public AppContext() 
            : base("DBConnection")
    { }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<SaleInfo> SalesInfo { get; set; }
    }
}
