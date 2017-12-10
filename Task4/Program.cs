using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Contexts;
using System.Data.Entity;
using DAL.Repositories;

namespace Task4
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (DAL.Contexts.AppContext db = new DAL.Contexts.AppContext())
            {
                //Manager manager1 = new Manager { ManagerName = "Petrov" };
                //db.Managers.Add(manager1);
                //db.SaveChanges();
                //SaleInfo info1 = new SaleInfo { Date = "2017", ClientName = "Sidorov", Cost = "2500", Manager = manager1 };
                //SaleInfo info2 = new SaleInfo { Date = "2017", ClientName = "Sidorov", Cost = "2500", Manager = manager1 };
                //db.SalesInfo.AddRange(new List<SaleInfo> { info1, info2 });
                //db.SaveChanges();

                //foreach (Manager m in db.Managers.Include(t => t.Info))
                //{
                //    Console.WriteLine("Менеджер: {0}", m.ManagerName);
                //    foreach (SaleInfo inf in m.Info)
                //    {
                //        Console.WriteLine("{0}, {1}, {2}", inf.Date, inf.ClientName, inf.Cost);
                //    }
                //    Console.WriteLine();
                //}

                EFGenericRepository<Manager> managerRepo = new EFGenericRepository<Manager>
                    (new DAL.Contexts.AppContext());
                EFGenericRepository<SaleInfo> salesRepo = new EFGenericRepository<SaleInfo>
                    (new DAL.Contexts.AppContext());
                IEnumerable<Manager> managers = managerRepo.GetWithInclude(p => p.Info);
                foreach (Manager m in managers)
                {
                    Console.WriteLine($"Manager: {m.ManagerName}");
                    foreach (SaleInfo p in m.Info)
                    {
                        Console.WriteLine($"{p.Date}, {p.ClientName}, {p.Cost}");
                    }
                }
            }
        }
    }
}
