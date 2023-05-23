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
using Frontend.Model;
using Frontend.ViewModel;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for AddNewTask.xaml
    /// </summary>
    public partial class AddNewTask : Window

    {
        private AddTaskViewModel viewModel;
        private UserModel userModel;
        BackendController backendController;
        private string boardName;
        private int id;

        public AddNewTask()
        {
            InitializeComponent();

        }

        public AddNewTask(int id,string name, UserModel userModel)
        {
            InitializeComponent();
            boardName = name;
            this.id = id;
            this.userModel = userModel;
            viewModel = new(userModel, name);
            DataContext = viewModel;
            backendController = ViewModel.ViewModel.BackendCon;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Message = null;
            viewModel.AddNewTask();
            if (viewModel.Message == null)
            {
                MessageBox.Show("Task added to board!", "", MessageBoxButton.OK);
                Board board = new Board(ViewModel.ViewModel.BackendCon, id, boardName,userModel);
                board.Show();
                this.Close();
            }
            else
                MessageBox.Show(viewModel.Message, "Error", MessageBoxButton.OK);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Board board = new Board(ViewModel.ViewModel.BackendCon, id, boardName, userModel);
            board.Show();
            this.Close();
        }
    }
}
