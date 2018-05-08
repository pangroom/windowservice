using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Collections;

namespace WindowsServiceClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string serviceFilePath = $"{Application.StartupPath}\\MyWindowsService.exe";
        string serviceName = "MyService";
        //事件：安装服务
        private void button1_Click(object sender, EventArgs e)
        {
            if(this.IsServiceExisted(serviceName)) this.UninstallService(serviceName);
            this.InstallService(serviceFilePath);

        }


        //卸载服务
        private void UninstallService(string serviceName)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                installer.Uninstall(null);
            }

            //throw new NotImplementedException();
        }
        //安装服务
        private void InstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);

            }
            //throw new NotImplementedException();
        }
        //判断服务是否存在
        private bool IsServiceExisted(string serviceName)
        {

            ServiceController[] services = ServiceController.GetServices();
            foreach(ServiceController sc in services)
            {
                if(sc.ServiceName.ToLower()==serviceName.ToLower())
                {
                    return true;
                }
            }
            return false;
            
        }
        //启动服务
        private void ServiceStart(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if(control.Status==ServiceControllerStatus.Stopped)
                {
                    control.Start();
                }
            }
        }

        //停止服务
        private void ServiceStop(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if(control.Status==ServiceControllerStatus.Running)
                {
                    control.Stop();
                }
            }
        }
        //事件：启动服务
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName)) this.ServiceStart(serviceName);

        }

        //事件：停止服务

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.IsServiceExisted(serviceName)) this.ServiceStop(serviceName);

        }
        //事件：卸载服务
        private void button4_Click(object sender, EventArgs e)
        {
            if(this.IsServiceExisted(serviceName))
            {
                this.ServiceStop(serviceName);
                this.UninstallService(serviceFilePath);
            }

        }
    }
}
