using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherService
{
    public class Report
    {
        public string ManagerName { get; private set; }
        public string DateOfSale { get; private set; }
        public string ClientName { get; private set; }
        public string ProductCost { get; private set; }

        public Report(string managerName, string saleDate, string clientName, string productCost)
        {
            ManagerName = managerName;
            DateOfSale = saleDate;
            ClientName = clientName;
            ProductCost = productCost;
        }
    }
}
