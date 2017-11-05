using BussinessObject.DataAccess;
using BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LyPlan
{
    /// <summary>
    /// Interaction logic for WorkForm.xaml
    /// </summary>
    public partial class WorkForm : Window
    {
        private WeekyWork weekyWork;
        private ObservableCollection<WeekyWork> weekyWorkList;
        public WorkForm()
        {
            InitializeComponent();
        }

        public WorkForm(WeekyWork weekyWork, ObservableCollection<WeekyWork> weekyWorkList)
        {
            InitializeComponent();
            this.weekyWork = weekyWork;
            this.weekyWorkList = weekyWorkList;
            txtTitle.Text = weekyWork.Title;
            txtDescription.Text = weekyWork.Description;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            WeekyTaskData weekyTaskData = new WeekyTaskData();
            if (weekyTaskData.DeleteWork(weekyWork))
            {
                weekyWorkList.Remove(weekyWork);
                this.Close();
            }
            else
            {
                tbMessage.Text = "Delete fail! Please try again";
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            WeekyTaskData weekyTaskData = new WeekyTaskData();
            weekyWork.Description = txtDescription.Text;
            if (weekyTaskData.UpdateWeekyWork(weekyWork))
            {
                this.Close();
            }
            else
            {
                tbMessage.Text = "Update fail! Please try again";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
