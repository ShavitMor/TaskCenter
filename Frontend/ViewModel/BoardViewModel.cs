using Frontend.Model;
using System;
using System.Collections.Generic;



namespace Frontend.ViewModel
{
    internal class BoardViewModel : ViewModel
    {
        private Model.BackendController BackendCon;
        private Model.UserModel User;
        private string name;
        private int id;

        public BoardViewModel(UserModel user,int id,string name)
        {
            User = user;
            this.id = id;
            this.name = name;
            backLogLimit =GetBacklogLimit();
            inprogressLimit =GetInProgressLimit();
            doneLimit =GetDoneLimit();
            Owner=GetOwner();

        }
        private string _owner;
        public string Owner
        {
            get { return _owner; }
            set { _owner = value;RaisePropertyChanged("Owner"); }
        }

        public string GetBoardName()
        {
            return name;
        }
        public int GetBoardId()
        {
            return id;
        }
        public string GetUserMail()
        {
            return User.Email;
        }
        public string GetOwner()
        {
            try
            {
                return ViewModel.BackendCon.GetOwner(User.Email, name);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return "";
            }
        }

       
        public void ChangeOwner()
        {
            try
            {
                ViewModel.BackendCon.TransferOwnership(User.Email, Owner, name);
                Owner=GetOwner();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        public string GetBacklogLimit()
        {
            try
            {
                return ViewModel.BackendCon.GetColumnLimitToString(User.Email, name, 0);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return "Error";
            }
        }

        public string GetInProgressLimit()
        {
            try
            {
                return ViewModel.BackendCon.GetColumnLimitToString(User.Email, name, 1);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return "Error";
            }
        }

        public string GetDoneLimit()
        {
            try
            {
                return ViewModel.BackendCon.GetColumnLimitToString(User.Email, name, 2);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return "Error";
            }
        }

        public void SetBacklogLimit()
        {
            try
            { 
                ViewModel.BackendCon.SetColumnLimit(User.Email, name, 0, int.Parse(backLogLimit));
                backLogLimit=GetBacklogLimit();

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }
        public void SetInProgressLimit()
        {
            try
            {

                ViewModel.BackendCon.SetColumnLimit(User.Email, name, 1, int.Parse(inprogressLimit));
                Message = $"Limit changed to {backLogLimit}";
                inprogressLimit=GetInProgressLimit();


            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }
        private string _backLogLimit;
        private string _inprogressLimit;
        private string _doneLimit;
        public string backLogLimit { get { return _backLogLimit; } set { _backLogLimit = value; RaisePropertyChanged("backLogLimit"); } }
        public string inprogressLimit { get { return _inprogressLimit; } set { _inprogressLimit = value; RaisePropertyChanged("inprogressLimit"); } }
        public string doneLimit { get { return _doneLimit; } set { _doneLimit = value; RaisePropertyChanged("doneLimit"); } }

        public void SetDoneLimit()
        {
            try
            {

                ViewModel.BackendCon.SetColumnLimit(User.Email, name, 2,int.Parse( doneLimit));
                Message = $"Limit changed to {doneLimit}";
                _doneLimit=GetDoneLimit();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }

        public List<TaskModel> GetBacklogTasks()
        {
            try
            {
                return ViewModel.BackendCon.ColumnToTaskModelList(User.Email, name, 0);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return new List<TaskModel>();
            }
        }
        public List<TaskModel> GetInProgressTasks()
        {
            try
            {
                return ViewModel.BackendCon.ColumnToTaskModelList(User.Email, name, 1);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return new List<TaskModel>();
            }
        }
        public List<TaskModel> GetDoneTasks()
        {
            try
            {
                return ViewModel.BackendCon.ColumnToTaskModelList(User.Email, name, 2);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return new List<TaskModel>();
            }
        }

        public void LeaveBoard()
        {
            try
            {
                ViewModel.BackendCon.LeaveBoard(User.Email, id);
                Message = null;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public string newName { get => newName; set { newName = value; RaisePropertyChanged("newName"); } }
        public void ChangeBoardName()
        {
            try
            {
                ViewModel.BackendCon.ChangeBoardName(User.Email, name, newName);
                Message = "You left the board.";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public void AddTask(string taskName, string taskDescription, string dueDate)
        {

            try
            {
                ViewModel.BackendCon.AddTask(User.Email, name, taskName, taskDescription, DateTime.Parse(dueDate));
                Message = "New Task added to the board!";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }
        public TaskModel GetTask(int id)
        {
            try
            {
                return ViewModel.BackendCon.GetTaskModel(User.Email, name, id);

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return null;
            }
        }

        public string GetTaskName(int id)
        {
            try
            {
                return ViewModel.BackendCon.GetTaskModel(User.Email, name, id).Title;

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return "Error";
            }
        }

        public string GetTaskAssignee(int id)
        {
            try
            {
                return ViewModel.BackendCon.GetTaskModel(User.Email, name, id).Asignnee;

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return "Error";
            }
        }

        public string GetTaskDueDate(int id)
        {
            try
            {
                return ViewModel.BackendCon.GetTaskModel(User.Email, name, id).DueDate.ToString();

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return "Error";
            }
        }
        public void DeleteTask(int boardId,int taskId,string email)
        {
            try
            {
                ViewModel.BackendCon.DeleteTask(boardId, taskId, email);
            }catch (Exception ex)
            {
                Message=ex.Message;
            }
        }


    }
}
