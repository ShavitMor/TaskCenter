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
    public partial class SetDateView : Window
    {
        private BackendController BackendController;
        private DateSetterViewModel dsvm;
        private UserModel user;
        private int boardId;
        private string boardName;
        public SetDateView(TaskModel t,UserModel us,int boardId,string boardName)
        {
            InitializeComponent();
            dsvm = new(t,us,boardName);
            BackendController = ViewModel.ViewModel.BackendCon;
            user = us;
            DataContext = dsvm;
            this.boardId = boardId;
            this.boardName = boardName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dsvm.Message = null;
            dsvm.SetDueDate();
            if (dsvm.Message == null)
            {
                MessageBox.Show("Changed dueDate succefully!", "", MessageBoxButton.OK);
                TaskWindow task = new TaskWindow(BackendController, user, boardId, boardName, dsvm.t);
                task.Show();
                this.Close();
            }
            else
                MessageBox.Show(dsvm.Message, "error", MessageBoxButton.OK);
        }

    }
}
