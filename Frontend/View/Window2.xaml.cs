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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private UserBoardsViewModel boardsViewModel;
        public ObservableCollection<string> boards;
        private UserModel model;
        private string selectedBoard;
        public Window2(UserModel userModel,BackendController backendController)
        {
            InitializeComponent();
            this.DataContext = new UserBoardsViewModel(userModel,backendController);
            this.boardsViewModel = (UserBoardsViewModel)DataContext;
            model = userModel;
            boards = new ObservableCollection<string>(boardsViewModel.GetBoardNames());
            this.boardList.ItemsSource = boards;
            newBoardName.Visibility = Visibility.Hidden;
            AddBoard.Visibility = Visibility.Hidden;
            selectedBoard = null;
            inprogressTasks.Visibility = Visibility.Hidden;
            boardId.Visibility = Visibility.Hidden;
        }

        private void NewBoard_Click(object sender, RoutedEventArgs e)
        {
            newBoardName.Visibility=Visibility.Visible;
            AddBoard.Visibility=Visibility.Visible;
            NewBoard.Visibility = Visibility.Hidden;
        }

        private void AddBoard_Click(object sender, RoutedEventArgs e)
        {
            boardsViewModel.NewBoard();
            if (boardsViewModel.Message != null)
            {
                MessageBox.Show(boardsViewModel.Message, "error", MessageBoxButton.OK);
                boardsViewModel.Message = null;
            }
            newBoardName.Visibility = Visibility.Hidden;
            AddBoard.Visibility = Visibility.Hidden;
            NewBoard.Visibility = Visibility.Visible;
            boards = new ObservableCollection<string>(boardsViewModel.GetBoardNames());
            boardList.ItemsSource = boards;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            boardsViewModel.LogOut();
            Window1 main = new Window1(UserBoardsViewModel.BackendCon);
            main.Show();
            this.Close();
        }

     

     

        private void boardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedBoard = (string)e.AddedItems[0];
            }catch (Exception)
            {

            }
        }

        private void GetToBoard_Click(object sender, RoutedEventArgs e)
        {
            if(selectedBoard != null) {
                int id = boardsViewModel.GetboardId(selectedBoard);
                if (id != -1)
                {
                    Board board1 = new Board(MainViewModel.BackendCon, id, selectedBoard,model);
                    board1.Show();
                    this.Close();
                }
            }

        }

        private void GetInprogress_Click(object sender, RoutedEventArgs e)
        {
            List <TaskModel> l= boardsViewModel.InProgressTasks();
            ObservableCollection<string> tasks = new ObservableCollection<string>();
            foreach (TaskModel task in l)
                tasks.Add(task.ToString());
            inprogressTasks.ItemsSource = tasks;
            inprogressTasks.Visibility= Visibility.Visible;
        }

        private void joinBoard_Click(object sender, RoutedEventArgs e)
        {
            if (boardId.Visibility == Visibility.Visible)
            {
                boardsViewModel.JoinToBoard();
                if (boardsViewModel.Message != null)
                {
                    MessageBox.Show(boardsViewModel.Message, "error", MessageBoxButton.OK);
                    boardsViewModel.Message = null;
                }
                else
                {
                    boards = new ObservableCollection<string>(boardsViewModel.GetBoardNames());
                    this.boardList.ItemsSource = boards;
                    boardId.Visibility = Visibility.Hidden;
                }     
            }
            else
            {
                boardId.Visibility = Visibility.Visible;
            }
        }

        private void DeleteBoard_Click(object sender, RoutedEventArgs e)
        {
            boardsViewModel.DeleteBoard(selectedBoard);
            if (boardsViewModel.Message == null)
            {
                MessageBox.Show("board deleted", "messege", MessageBoxButton.OK);
                boards = new ObservableCollection<string>(boardsViewModel.GetBoardNames());
                this.boardList.ItemsSource = boards;

            }
            else
                MessageBox.Show(boardsViewModel.Message, "error", MessageBoxButton.OK);
        }
    }
}
