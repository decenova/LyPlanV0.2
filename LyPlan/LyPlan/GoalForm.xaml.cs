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
            nodeList = new ObservableCollection<BussinessObject.Entities.Task>();
            lvNodeTask.ItemsSource = nodeList;
            btnAdd.Visibility = Visibility.Hidden;
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
            WeekyTaskData weekyTaskData = new WeekyTaskData();
            BussinessObject.Entities.Task roottask = new BussinessObject.Entities.Task()
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text
            };

            weekyList.Add(roottask);
            weekyTaskData.SaveRootTask(roottask);
            DataTable dtSuperId = weekyTaskData.GetInsertRootTaskId();
            dynamic superId = dtSuperId.Select()[0].ItemArray[0];
            foreach (dynamic node in nodeList)
            {
                node.SuperTask = superId;
                weekyTaskData.SaveNodeTask(node);
                roottask.Items.Add(node);
            }
        }
    }
}
