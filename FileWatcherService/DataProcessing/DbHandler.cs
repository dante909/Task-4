﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contexts;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.Models;


namespace FileWatcherService
{
    public class DbHandler
    {
        private IGenericRepository<Manager> _managerRepo;
        private IGenericRepository<SaleInfo> _saleInfoRepo;

        public DbHandler()
        {
            _managerRepo = new EFGenericRepository<Manager>(new DAL.Contexts.AppContext());
            _saleInfoRepo = new EFGenericRepository<SaleInfo>(new DAL.Contexts.AppContext());
        }

        public void AddToDatabase(Report report)
        {
            lock (this)
            {
                try
                {
                    var newManager = new Manager { ManagerName = report.ManagerName };
                    _managerRepo.Create(newManager);

                    _saleInfoRepo.Create(new SaleInfo()
                    {
                        Date = report.DateOfSale,
                        ClientName = report.ClientName,
                        Cost = report.ProductCost,
                        Manager = newManager
                    });
                }
                catch (FormatException e)
                {
                    throw new Exception("The order of items  is  incorrect!");
                }
            }
        }
    }
}

