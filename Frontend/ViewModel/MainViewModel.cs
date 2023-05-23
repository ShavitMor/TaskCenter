using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
     public class MainViewModel: ViewModel
    {
        
        private string _email;
        public string email
        {
            get => _email;
            set
            {
                this._email = value;
                RaisePropertyChanged("Email");
            }
        }
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                this._password = value;
                RaisePropertyChanged("Password");
            }
        }
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                this._message = value;
                RaisePropertyChanged("Message");
            }
        }
        public MainViewModel()
        {
            BackendCon = new BackendController();
            this.email = "";
            this.Password = "";
        }
        public MainViewModel(BackendController backendController)
        {
            BackendCon = backendController;
            this.email = "";
            this.Password = "";
        }
        public UserModel Login()
        {
            Message = "";
            try
            {
                return BackendCon.Login(email, Password);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }
        public UserModel Register()
        {
            Message = "";
            try
            {
                return BackendCon.Register(email, Password);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }
        public void LoadData()
        {
            Message = "";
            try
            {
                BackendCon.LoadData();
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
    }
}
