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
using Frontend.Model;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {

        BackendController backendController;
        UserModel user;
        private int boardId;
        private string boardName;
        TaskModel task;
        public TaskView t;
        public TaskWindow(BackendController bc,UserModel user, int boardId, string boardName, TaskModel task) : this(bc, boardId, boardName, user, task)
        {
            InitializeComponent();

            this.t = new(user,boardId,boardName,task);
            this.DataContext = t;
            backendController = ViewModel.ViewModel.BackendCon;
            newtitle.Visibility = Visibility.Hidden;
            SetTitle.Visibility = Visibility.Hidden;
            toAss.Visibility = Visibility.Hidden;
            assignb.Visibility = Visibility.Hidden;
            currAssignee.Content = task.Asignnee;
        }

        private TaskWindow(BackendController backendController, int id, string name, UserModel userModel, TaskModel task)
        {
            this.backendController = backendController;
            this.boardId = id;
            this.boardName = name;
            this.user = userModel;
            this.task = task;
        }

        private void changeTitle_Click(object sender, RoutedEventArgs e)
        {
            changeTitle.Visibility = Visibility.Hidden;
            newtitle.Visibility = Visibility.Visible;
            SetTitle.Visibility = Visibility.Visible;
        }

        private void SetTitle_Click(object sender, RoutedEventArgs e)
        {
            t.Title = newtitle.Text;
            if (t.Message != null)
            {
                MessageBox.Show(t.Message, "Error", MessageBoxButton.OK);
                t.Message = null;
                
            }
            changeTitle.Visibility = Visibility.Visible;
            newtitle.Visibility = Visibility.Hidden;
            SetTitle.Visibility = Visibility.Hidden;
        }

        private void assignTob_Click(object sender, RoutedEventArgs e)
        {
            assignTob.Visibility = Visibility.Hidden;
            toAss.Visibility = Visibility.Visible;
            assignb.Visibility = Visibility.Visible;
        }

        private void assignb_Click(object sender, RoutedEventArgs e)
        {
            t.Message = null;
            t.Assignee = toAss.Text;
            if (t.Message != null)
            {
                MessageBox.Show(t.Message, "Error", MessageBoxButton.OK);
                t.Message = null;
                currAssignee.Content = task.Asignnee;
            }
            else
            {
                MessageBox.Show("Task assigned succesfully!", "", MessageBoxButton.OK);
                currAssignee.Content = task.Asignnee;
            }
            assignTob.Visibility = Visibility.Visible;
            toAss.Visibility = Visibility.Hidden;
            assignb.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e) //unassign button
        {
            t.Message = null;
            t.Unassign();
            if (t.Message != null)
            {
                MessageBox.Show(t.Message, "Error", MessageBoxButton.OK);
                t.Message = null;
            }
            else
            {
                MessageBox.Show("Unassignd task!", "", MessageBoxButton.OK);
            }
        }

        private void NexState_Click(object sender, RoutedEventArgs e)
        {
            {
                t.AdvanceTask();
                if (t.Message != null)
                {
                    MessageBox.Show(t.Message, "Error", MessageBoxButton.OK);
                    t.Message = null;
                }
                else
                {
                    MessageBox.Show("Task Advanced to next state!", "", MessageBoxButton.OK);
                }
            }
        }

        private void ToBoard_Click(object sender, RoutedEventArgs e)
        {
            Board board = new Board(ViewModel.ViewModel.BackendCon, boardId, boardName, user);
            board.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //setDesc
        {
            ShortTextChangeView shortTextChangeView = new ShortTextChangeView(user,boardName,boardId, task,backendController);
            shortTextChangeView.Show();
            this.Close();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SetDateView setDateView = new SetDateView(task,user,boardId,boardName);
            setDateView.Show();
            this.Close();
        }
    }
}
