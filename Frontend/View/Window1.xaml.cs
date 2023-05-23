using Frontend.Model;
using Frontend.ViewModel;
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


namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private MainViewModel mainViewModel;
        
        
        public Window1()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            this.mainViewModel = (MainViewModel)DataContext;
            mainViewModel.LoadData();
            
        }
        
        public Window1(BackendController backendController)
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(backendController);
            this.mainViewModel = (MainViewModel)DataContext;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserModel u = mainViewModel.Login();
            if (u != null)
            {
                Window2 boardView = new Window2(u, MainViewModel.BackendCon);
                boardView.Show();
                this.Close();
            }
            else
                MessageBox.Show(mainViewModel.Message, "error", MessageBoxButton.OK);

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            UserModel u = mainViewModel.Register();
            if (u != null)
            {
                Window2 boardView = new Window2(u, MainViewModel.BackendCon);
                boardView.Show();
                this.Close();
            }
            else
                MessageBox.Show(mainViewModel.Message, "error", MessageBoxButton.OK);

        }


    }
}
