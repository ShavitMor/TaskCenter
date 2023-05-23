using Frontend.Model;
using Frontend.ViewModel;
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


namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : Window
    {
        private int id;
        private string boardName;
        private BoardViewModel viewModel;
        UserModel userModel;
        BackendController backendController;
        public Board(BackendController backendController,int id,string name,UserModel userModel)
        {
            InitializeComponent();
            this.id = id;
            this.boardName = name;
            this.DataContext = new BoardViewModel(userModel,id,name);
            this.viewModel = (BoardViewModel)DataContext;
            this.userModel = userModel;
            this.backendController=backendController;
            nameLable.Content = boardName;
            _chosenTask = -1;
            List<TaskModel> tasks = viewModel.GetBacklogTasks();
            List<string> taskTitle=new List<string>();
            foreach (TaskModel task in tasks)
                taskTitle.Add(task.ToString());
            this.BackLogTasks.ItemsSource = new ObservableCollection<string>(taskTitle);
            List<TaskModel> tasksinprogress = viewModel.GetInProgressTasks();
            List<string> taskTitleIn = new List<string>();
            foreach (TaskModel task in tasksinprogress)
                taskTitleIn.Add(task.ToString());
            this.InProgressTasks.ItemsSource = new ObservableCollection<string>(taskTitleIn);
            List<TaskModel> tasksDone = viewModel.GetDoneTasks();
            List<string> taskTitleDone = new List<string>();
            foreach (TaskModel task in tasksDone)
                taskTitleDone.Add(task.ToString());
            this.Done.ItemsSource = new ObservableCollection<string>(taskTitleDone);
            BackLogLim.IsEnabled = false;
            inprogressLim.IsEnabled = false;
            doneLim.IsEnabled = false;
            OwnerMail.IsEnabled = false;
        }
        private int _chosenTask;
        public int ChoosenTask
        {
            get { return _chosenTask; }
            set { _chosenTask = value; }
        }
        private void setbackloglimit_Click(object sender, RoutedEventArgs e)
        {
            if (BackLogLim.IsEnabled)
            {
                viewModel.SetBacklogLimit();
                BackLogLim.IsEnabled = false;
            }
            else
                BackLogLim.IsEnabled = true;
        }

        private void setInprogressLimit_Click(object sender, RoutedEventArgs e)
        {
            if (inprogressLim.IsEnabled)
            {
                viewModel.SetInProgressLimit();
                inprogressLim.IsEnabled = false;
            }
            else
                inprogressLim.IsEnabled = true;
        }

        private void setDoneLimit_Click(object sender, RoutedEventArgs e)
        {
            if (doneLim.IsEnabled)
            {
                viewModel.SetDoneLimit();
                doneLim.IsEnabled = false;
            }
            else
                doneLim.IsEnabled = true;
        }

        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            AddNewTask task = new AddNewTask(id,boardName,userModel);
            task.Show();
            this.Close();
        }

        private void ChangeOwner_Click(object sender, RoutedEventArgs e)
        {
            if (OwnerMail.IsEnabled)
            {
                viewModel.ChangeOwner();
                if (viewModel.Message != null)
                {
                    MessageBox.Show(viewModel.Message, "error", MessageBoxButton.OK);
                    viewModel.Message = null;
                }
                else
                    OwnerMail.IsEnabled = false;
            }
            else
                OwnerMail.IsEnabled = true;
        }

        private void BackLogTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string task = (string)e.AddedItems[0];
                string[] taskid = task.Split(":");
                string taskId = taskid[0];
                _chosenTask = int.Parse(taskId);
            }catch (Exception )
            { }
        }

        private void InProgressTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string task = (string)e.AddedItems[0];
                string[] taskid = task.Split(":");
                string taskId = taskid[0];
                _chosenTask = int.Parse(taskId);
            }catch(Exception)
            {

            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string task = (string)e.AddedItems[0];
                string[] taskid = task.Split(":");
                string taskId = taskid[0];
                _chosenTask = int.Parse(taskId);
            }catch(Exception )
            {

            }
        }

        private void getTask_Click(object sender, RoutedEventArgs e)
        {
            if (_chosenTask != -1)
            {
                TaskModel task = viewModel.GetTask(ChoosenTask);
                if (task != null)
                {
                    TaskWindow taskWindow = new TaskWindow(backendController,userModel,id,boardName, task);
                    taskWindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("didnt find Task", "error", MessageBoxButton.OK);
            }

        }

        private void logout_Click_1(object sender, RoutedEventArgs e)
        {
            userModel.Controller.Logout(userModel.Email);
            Window1 window1 = new Window1(backendController);
            window1.Show();
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2(userModel, backendController);
            window2.Show();
            this.Close();
        }

        private void deleteTask_Click(object sender, RoutedEventArgs e)
        {
            if(_chosenTask != -1)
            {
                viewModel.DeleteTask(id,ChoosenTask,userModel.Email);
                if (viewModel.Message != null)
                    throw new Exception(viewModel.Message);
                else
                {
                    _chosenTask = -1;
                    viewModel.Message = null;
                    List<TaskModel> tasks = viewModel.GetBacklogTasks();
                    List<string> taskTitle = new List<string>();
                    foreach (TaskModel task in tasks)
                        taskTitle.Add(task.ToString());
                    this.BackLogTasks.ItemsSource = new ObservableCollection<string>(taskTitle);
                    List<TaskModel> tasksinprogress = viewModel.GetInProgressTasks();
                    List<string> taskTitleIn = new List<string>();
                    foreach (TaskModel task in tasksinprogress)
                        taskTitleIn.Add(task.ToString());
                    this.InProgressTasks.ItemsSource = new ObservableCollection<string>(taskTitleIn);
                    List<TaskModel> tasksDone = viewModel.GetDoneTasks();
                    List<string> taskTitleDone = new List<string>();
                    foreach (TaskModel task in tasksDone)
                        taskTitleDone.Add(task.ToString());
                    this.Done.ItemsSource = new ObservableCollection<string>(taskTitleDone);
                }
            }
        }

        private void LeaveBoard_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LeaveBoard();
            if (viewModel.Message != null)
                MessageBox.Show(viewModel.Message, "error", MessageBoxButton.OK);
            else
            {
                MessageBox.Show("You left the board.", "Notice", MessageBoxButton.OK);
                Window2 window2 = new Window2(userModel, backendController);
                window2.Show();
                this.Close();
            }
        }
    }
}
