using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.View;


namespace Frontend.ViewModel
{
    internal class UserBoardsViewModel : ViewModel
    {
        private Model.UserModel User;
        private List<string> _boards;
        public string Title { get; private set; }
        public List<string> Board
        {
            get { return _boards; }

            set { _boards = value; RaisePropertyChanged("SelectedBoard"); }

        }
        private string _newBoardName;
        public string NewBoardName { get { return _newBoardName; } set { _newBoardName = value; RaisePropertyChanged("boardName"); } }


        public UserBoardsViewModel(UserModel user, BackendController backendController)
        {
            User = user;
            Board = GetBoardNames();
            BackendCon = backendController;
            Title = "Boards";

        }
        private int _boardJoinId;
        public int BoardJoinId
        {
            get { return _boardJoinId; } set { _boardJoinId = value;RaisePropertyChanged("joinId"); }
        }
        
        public string GetEmail()
        {
            return User.Email;
        }

        public List<string> GetBoardNames()
        {
            try
            {
                return ViewModel.BackendCon.GetUserBoardNames(User.Email);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return new List<string>();
            }
        }

        public void JoinToBoard()
        {
            try
            {
                ViewModel.BackendCon.JoinBoard(User.Email, _boardJoinId);
                _boardJoinId = -1;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

        }

        public List<TaskModel> InProgressTasks()
        {
            try
            {
                return ViewModel.BackendCon.InProgress(User.Email);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return new List<TaskModel>();
            }

        }

        public void LogOut()
        {
            try
            {
                ViewModel.BackendCon.Logout(User.Email);
                Message = "Logged out successfully!";

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public void NewBoard()
        {
            try
            {
                ViewModel.BackendCon.AddBoard(User.Email, NewBoardName);
                Message = $"New board {NewBoardName} created successfully!";
                Board = GetBoardNames();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        public void DeleteBoard(string boardName)
        {
            try
            {
                ViewModel.BackendCon.RemoveBoard(User.Email, boardName);
                Message = null;
            }
            catch (Exception ex)
            {
                Message = ex.Message;

            }
        }
        public BoardModel Getboard(string name)
        {
            try
            {
                BoardModel boardModel = ViewModel.BackendCon.GetBoard(User.Email, name);
                return boardModel;
            }catch (Exception ex)
            {
                Message = ex.Message;
                return null;

            }

        }

        public int GetboardId(string name)
        {
            try
            {
               int id= ViewModel.BackendCon.GetBoardId(User.Email, name);
                return id;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return -1;
            }

        }




    }
}
