using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Model;

namespace Frontend.ViewModel
{
    internal class TaskDescriptionSetter : ViewModel
    {
        TaskModel t;

        private string desc;
        private string email;
        private string boardName;
        public string Description
        {
            get => desc;
            set { desc = value; RaisePropertyChanged("desc"); }
        }

        public void SetDescription()
        {
            try
            {
                BackendCon.UpdateTaskDescription(email, boardName, t.TaskId, Description);
                t.Description = desc;
                Message = null;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }


        }

        public TaskDescriptionSetter(TaskModel t,string boardName,string email)
        {
            this.t = t;
            Description = t.Description;
            this.email = email;
            this.boardName = boardName;
        }
    }
}
