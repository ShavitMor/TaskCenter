using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Model;

namespace Frontend.ViewModel
{
    internal class DateSetterViewModel : ViewModel
    {
        public TaskModel t { get; set; }
        private UserModel user;
        private string _day;
        private string _month;  
        private string _year;
        private string _hour;
        private string _minutes;
        private string boardName;
        public string day
        {
            get { return _day; }
            set
            {
                _day = value;
                RaisePropertyChanged("day");
            }
        }
        public string month
        {
            get { return _month; }
            set
            {
                _month = value;
                RaisePropertyChanged("month");
            }
        }
        public string year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged("year");
            }
        }

        public string hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                RaisePropertyChanged("hour");
            }
        }
        public string minutes
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
                RaisePropertyChanged("minutes");
            }
        }

        public void SetDueDate()
        {
            try
            {
                DateTime due = DateTime.Parse($"{day}/{month}/{year} {hour}:{minutes}");
                BackendCon.UpdateTaskDueDate(user.Email, boardName, t.TaskId, due);
                t.DueDate = due;
                
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }


        }

        public DateSetterViewModel(TaskModel t, UserModel user,string boardName)
        {
            this.t = t;
            this.user = user;
            this.boardName = boardName;
        }

    }
}
