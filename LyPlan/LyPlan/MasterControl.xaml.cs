using BussinessObject.DataAccess;
using BussinessObject.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LyPlan
{
    /// <summary>
    /// Interaction logic for MasterControl.xaml
    /// </summary>
    public partial class MasterControl : Window
    {
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private MainWindow mainWindow;
        private DispatcherTimer timer;

        public MasterControl()
        {
            InitializeComponent();
            //SetStartWithWindows();
            notify = new System.Windows.Forms.NotifyIcon();
            try
            {
                setNotification();
            }
            catch{
            }
            setTimer();
        }

        private void SetStartWithWindows()
        {
            RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\LyPlan");
            //mo registry khoi dong cung win
            RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            string keyvalue = "1";
            //string subkey = "Software\\ManhQuyen";
            try
            {
                //chen gia tri key
                regkey.SetValue("Index", keyvalue);
                //regstart.SetValue("taoregistrytronghethong", "E:\\Studing\\Bai Tap\\CSharp\\Channel 4\\bai temp\\tao registry trong he thong\\tao registry trong he thong\\bin\\Debug\\tao registry trong he thong.exe");
                regstart.SetValue("LapLich", AppDomain.CurrentDomain.BaseDirectory + "LyPlan.exe");
                ////dong tien trinh ghi key
                //regkey.Close();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void setTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void setContextMenuComponent()
        {
            contextMenu = new System.Windows.Forms.ContextMenu();
            menuItem1 = new System.Windows.Forms.MenuItem();
            menuItem2 = new System.Windows.Forms.MenuItem();

            contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[]
            {
                menuItem1,menuItem2
            });

            menuItem1.Index = 0;
            menuItem1.Text = "Open LyPlan";
            menuItem1.Click += menuItem1_Click;

            menuItem2.Index = 1;
            menuItem2.Text = "Exit";
            menuItem2.Click += menuItem2_Click;

            notify.ContextMenu = contextMenu;
        }
        private void setNotification()
        {
            try
            {
                notify.Icon = new System.Drawing.Icon("lyplanv2.ico");
                notify.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
            }
            catch (Exception e)
            {
                notify.Icon = System.Drawing.SystemIcons.WinLogo;
            }
            notify.Visible = true;
            setContextMenuComponent();
            notify.DoubleClick += menuItem1_Click;
            notify.ShowBalloonTip(1000, "Việc cần làm", "Hôm nay còn " + getWorkInDay() + " việc chưa làm.", System.Windows.Forms.ToolTipIcon.None);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void menuItem1_Click(object Sender, EventArgs e)
        {
            if (mainWindow == null)
            {
                mainWindow = new MainWindow();
                mainWindow.Closed += mainWindow_Closed;
                mainWindow.Show();
            } else
            {
                mainWindow.Show();
            }
        }

        private void menuItem2_Click(object Sender, EventArgs e)
        {
            notify.Dispose();
            this.Close();
        }
        
        private void mainWindow_Closed(object Sender, EventArgs e)
        {
            mainWindow = null;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (dynamic row in getAlertWorkInDay().Rows)
            {
                notify.ShowBalloonTip(1000, row["Title"], "Đến hạn rồi.\nHãy bắt tay vào việc nào.", System.Windows.Forms.ToolTipIcon.None);
            }
        }

        private int getWorkInDay()
        {
            DateTime startTime = DateTime.Now.Date;
            DateTime endTime = startTime.AddDays(1).Date;
            WeekyTaskData weekyTask = new WeekyTaskData();
            return weekyTask.GetNumOfWork(startTime, endTime);
        }
        private DataTable getAlertWorkInDay()
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = new DateTime(startTime.AddSeconds(30).Ticks);
            WeekyTaskData weekyTask = new WeekyTaskData();
            return weekyTask.GetAlertWork(startTime, endTime);
        }
    }
}
