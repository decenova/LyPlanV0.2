using BussinessObject.DataAccess;
using System;
using System.Collections.Generic;
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
        private ObservableCollection<Task> weekyList;
        public GoalForm()
        {
            InitializeComponent();
        }

        public GoalForm(int goalId)
        {
            InitializeComponent();
            this.goalId = goalId;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            TaskForm taskForm = new TaskForm();
            taskForm.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
