using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Configs
{

    public interface IManualLog
    {
        public void WriteLog(string JsonLog, string FileName);
    }

    public class ManualLog : IManualLog
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ManualLog(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public void WriteLog(string JsonLog,string FileName)
        {
            string logPath = _hostingEnvironment.ContentRootPath + @"\ManualLogs\" + string.Format(FileName+"-{0:yyyy-MM-dd_hh-mm-tt}", DateTime.Now) +".json";

            using (StreamWriter TW = File.AppendText(logPath))
            {
      
                    TW.WriteLine(JsonLog);
     
            }
        }
    }
}
