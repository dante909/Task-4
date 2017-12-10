using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcherService
{
    public partial class Service1 : ServiceBase
    {
        FileWatcher watcher;
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            watcher = new FileWatcher();
            Thread watcherThread = new Thread(new ThreadStart(watcher.Start));
            watcherThread.Start();
        }

        protected override void OnStop()
        {
            watcher.Stop();
            Thread.Sleep(1000);
        }
    }
}

