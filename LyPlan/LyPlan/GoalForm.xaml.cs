using BussinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LyPlan
{
    /// <summary>
    /// Interaction logic for GoalForm.xaml
    /// </summary>
    public partial class GoalForm : Window
    {
        private int goalId;
        private ObservableCollection<BussinessObject.Entities.Task> weekyList;
        private ObservableCollection<BussinessObject.Entities.Task> nodeList;
        private BussinessObject.Entities.Task task;
        public GoalForm()
        {
            InitializeComponent();
        }

        public GoalForm(int goalId)
        {
            InitializeComponent();
            this.goalId = goalId;
        }

        public GoalForm(ObservableCollection<BussinessObject.Entities.Task> weekyList)
        {
            InitializeComponent();
            this.weekyList = weekyList;
            nodeList = new ObservableCollection<BussinessObject.Entities.Task>();
            lvNodeTask.ItemsSource = nodeList;
            btnUpdate.Visibility = Visibility.Hidden;
        }

        public GoalForm(BussinessObject.Entities.Task task, ObservableCollection<BussinessObject.Entities.Task> weekyList)
        {
            InitializeComponent();
            this.weekyList = weekyList;
            this.task = task;
            txtTitle.Text = task.Title;
            txtDescription.Text = task.Description;
            nodeList = new ObservableCollection<BussinessObject.Entities.Task>();
            lvNodeTask.ItemsSource = nodeList;
            lvNodeTaskOld.ItemsSource = task.Items;
            btnAdd.Visibility = Visibility.Hidden;
            lvNodeTaskOld.Visibility = Visibility.Visible;

        }

        private bool validInput()
        {
            if (txtTitle.Text.Length == 0)
            {
                tbMessage.Text = "Title can't be blank";
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            TaskForm taskForm = new TaskForm(nodeList);
            taskForm.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!validInput())
            {
                return;
            }
            WeekyTaskData weekyTaskData = new WeekyTaskData();
            BussinessObject.Entities.Task roottask = new BussinessObject.Entities.Task()
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text
            };

            weekyList.Add(roottask);
            if (weekyTaskData.SaveRootTask(roottask))
            {
                DataTable dtId = weekyTaskData.GetInsertTaskId();
                dynamic superId = dtId.Select()[0].ItemArray[0];
                foreach (dynamic node in nodeList)
                {
                    node.SuperTask = superId;
                    if (weekyTaskData.SaveNodeTask(node))
                    {
                        dtId = weekyTaskData.GetInsertTaskId();
                        node.Id = dtId.Select()[0].ItemArray[0];
                        roottask.Items.Add(node);
                    }
                }
            }
            else
            {
                tbMessage.Text = "Add fail! Please try again.";
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            WeekyTaskData weekyTaskData = new WeekyTaskData();
            if (weekyTaskData.RemoveTask(task))
            {
                weekyList.Remove(task);
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
            task.Title = txtTitle.Text;
            task.Description = txtDescription.Text;
            if (weekyTaskData.UpdateTask(task))
            {
                
                foreach (dynamic node in nodeList)
                {
                    node.SuperTask = task.Id;
                    if (weekyTaskData.SaveNodeTask(node))
                    {
                        DataTable dtId = weekyTaskData.GetInsertTaskId();
                        node.Id = dtId.Select()[0].ItemArray[0] as dynamic;
                        task.Items.Add(node);
                    }
                }
                this.Close();
            }
            else
            {
                tbMessage.Text = "Update fail! Please try again";
            }
        }
    }
}
