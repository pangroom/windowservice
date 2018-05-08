using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsService
{
    public partial class MyService1 : ServiceBase
    {
        public MyService1()
        {
            InitializeComponent();
        }

        string filePath= @"D:\MyServiceLog.txt";

        protected override void OnStart(string[] args)
        {
            //写入一个服务启动
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{DateTime.Now},服务启动！");
            }

        }

        protected override void OnStop()
        {
            //写入了一个服务停止
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{DateTime.Now},服务停止！");
            }

        }
    }
}
