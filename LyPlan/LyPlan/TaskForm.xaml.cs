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
    /// Interaction logic for TaskForm.xaml
    /// </summary>
    public partial class TaskForm : Window
    {

        private int taskId;
        private int supertaskId;
        public TaskForm()
        {
            InitializeComponent();
        }

        public TaskForm(int taskId)
        {
            InitializeComponent();
            this.taskId = taskId;
        }

        public TaskForm(int taskId, int supertaskId)
        {
            InitializeComponent();
            this.taskId = taskId;
            this.supertaskId = supertaskId;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
