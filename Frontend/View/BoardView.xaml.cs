using Frontend.ViewModel;
using IntroSE.Kanban.FrontEnd.Model;
using System.Windows;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Window
    {
        private BoardViewModel viewModel;

        public BoardView(BoardModel b)
        {
            InitializeComponent();
            this.viewModel = new BoardViewModel(b);
            this.DataContext = viewModel;
        }

        private void Remove_Button(object sender, RoutedEventArgs e)
        {
           // viewModel.RemoveMessage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Logout();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
