using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contexts;

namespace FileWatcherService
{
    public class DbHandler
    {
        private DAL.Contexts.AppContext db;

        public DbHandler()
        {
            db = new DAL.Contexts.AppContext();
        }

        public void AddToDatabase(Report report)
        {
            lock (this)
            {
                using (db = new DAL.Contexts.AppContext())
                {
                    var newManager = new DAL.Models.Manager { ManagerName = report.ManagerName };
                    db.Managers.Add(newManager);
                    db.SaveChanges();

                    var newSaleInfo = new DAL.Models.SaleInfo
                    {
                        Date = report.DateOfSale,
                        ClientName = report.ClientName,
                        Cost = report.ProductCost,
                        Manager = newManager

                    };

                    db.SalesInfo.Add(newSaleInfo);
                    db.SaveChanges();
                }
            }
        }
    }
}
