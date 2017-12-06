using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SaleInfo
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string ClientName { get; set; }
        public string Cost { get; set; }

        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
