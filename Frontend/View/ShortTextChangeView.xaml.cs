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
    /// Interaction logic for ShortTextChangeView.xaml
    /// </summary>
    public partial class ShortTextChangeView : Window
    {
        private UserModel user;
        private TaskModel task;
        private string boardName;
        private int boardId;
        private TaskDescriptionSetter setter;
        private BackendController backendController;
        public ShortTextChangeView(UserModel user,string boardName, int boardId ,TaskModel t, BackendController bc)
        {
            InitializeComponent();
            this.setter = new(t,boardName,user.Email);
            this.DataContext = setter;
            this.user = user;
            this.backendController = bc;
            this.boardName = boardName;
            this.boardId = boardId;
            task = t;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            setter.Message = null;
            setter.SetDescription();
            if (setter.Message==null)
            {
                MessageBox.Show("Changed Description succefully!","", MessageBoxButton.OK);
                TaskWindow taskWindow = new TaskWindow(backendController, user, boardId, boardName, task);
                taskWindow.Show();
                this.Close();
            }
            else
                MessageBox.Show(setter.Message, "error", MessageBoxButton.OK);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow(backendController, user, boardId, boardName, task);
            taskWindow.Show();
            this.Close();
        }
    }
}
