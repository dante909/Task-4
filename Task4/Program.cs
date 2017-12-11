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
                Console.WriteLine();
            }
            managerRepo.Dispose();
        }
    }
}

