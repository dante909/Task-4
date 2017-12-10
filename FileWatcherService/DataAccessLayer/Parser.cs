using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherService
{
    public class Parser
    {

        public IList<Report> ParseData(string path)
        {
            string managerName;
            string[] param;
            managerName = Path.GetFileNameWithoutExtension(path).Split('_').First(); //GetFileName
            IList<Report> records = new List<Report>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    param = sr.ReadLine().Split(',');
                    if (param != null)
                    {
                            records.Add(new Report(managerName, param[0], param[1], param[2]));
                    }
                }
            }
            return records;
        }
    }
}
