using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Frontend.ViewModel
{
    internal class AddTaskViewModel : ViewModel
    {

        private UserModel user;
        private string boardName;


        public AddTaskViewModel(UserModel user, string board)
        {
            this.user = user;
            this.boardName = board;
        }
        private string _title;
        private string _description;
        public string Title { get { return _title; } set { _title = value; RaisePropertyChanged("Title"); } }
        public string Description { get { return _description; } set { _description = value; RaisePropertyChanged("Description"); } }
        private string _day;
        public string day
        {
            get { return _day; }
            set
            {
                _day = value;
                RaisePropertyChanged("day");
            }
        }
        private string _month;
        public string month
        {
            get { return _month; }
            set
            {
                _month = value;
                RaisePropertyChanged("month");
            }
        }
        private string _year;
        public string year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged("year");
            }
        }
        private string _hour;
        public string hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                RaisePropertyChanged("hour");
            }
        }
        private string _minutes;
        public string minutes
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
                RaisePropertyChanged("minutes");
            }
        }

        public void AddNewTask()
        {
            try
            {
                BackendCon.AddTask(user.Email, boardName, Title, Description, DateTime.Parse($"{day}/{month}/{year} {hour}:{minutes}"));
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }
    }
}
