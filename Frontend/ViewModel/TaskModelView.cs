using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Model;

namespace Frontend.ViewModel
{
    public class TaskView : ViewModel
    {
        UserModel user;
        private int boardId;
        private string boardName;
        TaskModel task;

        public TaskView(UserModel user, int boardId, string boardName, TaskModel task)
        {
            this.user = user;
            this.boardId = boardId;
            this.boardName = boardName;
            this.task = task;
            Assignee = task.Asignnee;
        }

        public int TaskId { get => task.TaskId; }
        public string Title
        {
            get => task.Title;
            set
            {
                try
                {
                    BackendCon.UpdateTaskTitle(user.Email, boardName, task.TaskId, value);
                    task.Title = value;
                    RaisePropertyChanged("Title");
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
        }
        public string Description
        {
            get => task.Description;
            set
            {
                try
                {
                    BackendCon.UpdateTaskDescription(user.Email, boardName, task.TaskId, value);
                    task.Description = value;
                    RaisePropertyChanged("Description");
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
        }

        public string DueDate
        {
            get => task.DueDate.ToString("dd/MM/yyyy HH:mm");
            set
            {
                try
                {
                    BackendCon.UpdateTaskDueDate(user.Email, boardName, task.TaskId, DateTime.Parse(value));
                    task.DueDate = DateTime.Parse(value);
                    RaisePropertyChanged("DueDate");
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
        }

        public string CreationDate
        {
            get => task.CreationDate.ToString("dd/MM/yyyy");

        }

        public string Assignee
        {
            get => task.Asignnee ?? "Unassigned";
            set
            {
                try
                {
                    BackendCon.AssignTask(user.Email, boardName, task.TaskId, value);
                    task.Asignnee = value;
                    RaisePropertyChanged("Assignee");
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }
        }

        public void Unassign()
        {
            try
            {
                BackendCon.AssignTask(user.Email, boardName, task.TaskId, null);
                task.Asignnee = null;
                RaisePropertyChanged("Assignee");
            }

            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        override
        public string ToString()
        {
            return task.TaskId + ": " + task.Title;
        }


        public void AdvanceTask()
        {
            try
            {
                BackendCon.AdvanceTask(user.Email, boardName, TaskId);

            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
        }





    }
}
