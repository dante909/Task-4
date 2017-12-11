using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using DAL.Repositories;
using DAL.Interfaces;
using DAL.Models;

namespace FileWatcherService
{
    public class FileWatcher
    {
        private FileSystemWatcher watcher;
        private RecordsHandler recordHandler;
        object obj = new object();
        bool enabled = true;

        public FileWatcher()
        {
            watcher = new FileSystemWatcher();
            recordHandler = new RecordsHandler();
            watcher.Path = ConfigurationManager.AppSettings["pathFolder"];
            watcher.Filter = "*.csv";
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        public void CallParse(object sender, FileSystemEventArgs e)
        {
            string path;
            path = e.FullPath;
            recordHandler.SaveRecords(path);
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            var outer = Task.Factory.StartNew(() =>      
            {
                RecordEntry(fileEvent, filePath);
                var inner = Task.Factory.StartNew(() =>  
                {
                    CallParse(sender, e);
                });
            });
            outer.Wait();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "создан";
            string filePath = e.FullPath;
            var outer = Task.Factory.StartNew(() =>      
            {
                RecordEntry(fileEvent, filePath);
                var inner = Task.Factory.StartNew(() =>  
                {
                    CallParse(sender, e);
                });
            });
            outer.Wait(); 
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                using (StreamWriter writer = new StreamWriter(ConfigurationManager.AppSettings["pathLog"], true))
                {
                    writer.WriteLine(string.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                    writer.Flush();
                }
            }
        }
    }
}

