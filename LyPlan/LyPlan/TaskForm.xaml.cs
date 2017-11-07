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
    /// Interaction logic for TaskForm.xaml
    /// </summary>
    public partial class TaskForm : Window
    {
        private BussinessObject.Entities.Task task;
        private ObservableCollection<BussinessObject.Entities.Task> nodeList;
        public TaskForm()
        {
            InitializeComponent();
        }

        public TaskForm(ObservableCollection<BussinessObject.Entities.Task> nodeList)
        {
            InitializeComponent();
            this.nodeList = nodeList;
            btnUpdate.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
        }
        public TaskForm(BussinessObject.Entities.Task task, ObservableCollection<BussinessObject.Entities.Task> nodeList)
        {
            InitializeComponent();
            this.nodeList = nodeList;
            this.task = task;
            btnAdd.Visibility = Visibility.Hidden;
            txtTitle.Text = task.Title;
            txtDescription.Text = task.Description;
        }

        private bool validInput()
        {
            if (txtTitle.Text.Trim().Length == 0)
            {
                tbMessage.Text = "Title can't be blank";
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!validInput())
            {
                return;
            }
            nodeList.Add(new BussinessObject.Entities.Task()
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text
            });
            txtTitle.Text = "";
            txtDescription.Text = "";
            tbHeader.Text = "Add more";
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            WeekyTaskData weekyTaskData = new WeekyTaskData();
            if (!validInput())
            {
                return;
            }
            task.Title = txtTitle.Text;
            task.Description = txtDescription.Text;
            if (weekyTaskData.UpdateTask(task))
            {
                CollectionViewSource.GetDefaultView(nodeList).Refresh();
                this.Close();
            }
            else
            {
                tbMessage.Text = "Update fail! Please try again";
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            WeekyTaskData weekyTaskData = new WeekyTaskData();
            if (weekyTaskData.RemoveTask(task))
            {
                this.Close();
            }
            else
            {
                tbMessage.Text = "Delete fail! Please try again";
            }
        }
    }
}
