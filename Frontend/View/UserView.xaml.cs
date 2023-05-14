using Frontend.ViewModel;
using IntroSE.Kanban.FrontEnd.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private UserViewModel viewModel;

        public UserView(UserModel u)
        {
            InitializeComponent();
            viewModel = new UserViewModel(u);
            DataContext = viewModel;
        }

        private void toBoard(object sender, RoutedEventArgs e)
        {
            BoardModel b = viewModel.SelectedBoard;
            if (b != null)
            {
                BoardView board = new BoardView(b);

                board.Show();
                this.Close();
            }
        }
        private void AddBoard_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddBoard_Click(sender, e);
        }



        /*        public void InitializeComponent()
                {
                    throw new System.NotImplementedException();
                }*/
    }
}
