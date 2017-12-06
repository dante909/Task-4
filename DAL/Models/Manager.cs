using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string ManagerName { get; set; }

        public IList<SaleInfo> Info { get; set; }
        public Manager()
        {
            Info = new List<SaleInfo>();
        }
    }
}
