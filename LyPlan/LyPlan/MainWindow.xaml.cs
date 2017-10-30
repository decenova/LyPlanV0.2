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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LyPlan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task task = new Task() { Name = "Reading book" };
            tvTodolist.Items.Add(task);
            tvTodolist.Items.Add(new Task() { Name = "Play game" });
            tvTodolist.Items.Add(new Task() { Name = "Learn" });
            tvTodolist.Items.Add(new Task() { Name = "Vu vu" });
        }
    }
}
