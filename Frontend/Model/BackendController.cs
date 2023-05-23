using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Globalization;

namespace Frontend.Model
{
    public class BackendController
    {
        private ServiceController serviceController;

        public BackendController()
        {
            serviceController = ServiceFactory.MakeServiceController();

        }
        public UserModel Login(string username, string password)
        {
            Response user = JsonConvert.DeserializeObject<Response>(UserService.Login(username, password));

            if (user.IsError())
            {
                throw new Exception(user.ErrorMessage);
            }
            return new UserModel(this, username);
        }
        public UserModel Register(string email, string password)
        {
            Response user = JsonConvert.DeserializeObject<Response>(serviceController.US.Register(email, password));

            if (user.IsError())


            {
                throw new Exception(user.ErrorMessage);
            }
            return new UserModel(this, email);
        }
        public void Logout(string email)
        {
            Response user = JsonConvert.DeserializeObject<Response>(serviceController.US.LogOut(email));
            if (user.IsError())

            {
                throw new Exception(user.ErrorMessage);
            }

        }
        public void SetColumnLimit(string email, string boardName, int columnOrdinal, int newLimit)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.SetColumnLimit(email, boardName, columnOrdinal, newLimit));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public int GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.GetColumnLimitByName(boardName, columnOrdinal, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            return (int)response.ReturnValue;
        }
        public void AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.NewTask(boardName, title, description, dueDate, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);

        }
        public void UpdateTaskDueDate(string email, string boardName, int taskId, DateTime dueDate)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.SetTaskDueDateByBoardName(dueDate, taskId, boardName, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public void UpdateTaskTitle(string email, string boardName, int taskId, string title)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.SetTaskTitleByBoardName(title, taskId, boardName, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public void UpdateTaskDescription(string email, string boardName, int taskId, string description)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.SetTaskDescriptionByBoardName(description, taskId, boardName, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public void AdvanceTask(string email, string boardName, int taskId)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.AdvanceTask(boardName, taskId, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public List<TaskModel> GetColumn(string email, string boardName, int columnOrdinal)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.GetTasksOfColumn(boardName, columnOrdinal, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            Newtonsoft.Json.Linq.JArray jArray= (JArray)response.ReturnValue;
            List<TaskModel> l = new();
            foreach (JObject task in jArray)
            {
                int id = (int)task.GetValue("ID");
                DateTime Cdate=DateTime.Parse((string)task.GetValue("CreationDate"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                string title = (string)task.GetValue("Title");
                string description = (string)task.GetValue("Description");
                DateTime date= DateTime.Parse((string)task.GetValue("DueDate"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind); ;
                int boardId = (int)task.GetValue("BoardID");
                string name = (string)task.GetValue("Assignee");
                TaskModel task1 = new TaskModel(this,id,title,description,Cdate, date,name);
                l.Add(task1);
            }
                 
            return l;
        }
        public void AddBoard(string email, string name)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.NewBoard(email, name));
            if (response.IsError())

                throw new Exception(response.ErrorMessage);

        }
        public void RemoveBoard(string email, string name)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.DeleteBoardByName( email,name));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public string GetOwner(string email, string boardName)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.GetOwnerByName(email, boardName));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            return (string)response.ReturnValue;
        }

        public List<TaskModel> InProgress(string email)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.US.GetInProgress(email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            Newtonsoft.Json.Linq.JArray jArray = (JArray)response.ReturnValue;
            List<TaskModel> l = new();
            foreach (JObject task in jArray)
            {
                int id = (int)task.GetValue("ID");
                DateTime Cdate = DateTime.Parse((string)task.GetValue("CreationDate"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
                string title = (string)task.GetValue("Title");
                string description = (string)task.GetValue("Description");
                DateTime date = DateTime.Parse((string)task.GetValue("DueDate"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind); ;
                int boardId = (int)task.GetValue("BoardID");
                string name = (string)task.GetValue("Assignee");
                TaskModel task1 = new TaskModel(this, id, title, description, Cdate, date, name);
                l.Add(task1);
            }

            return l;
        }

        public List<int> GetUserBoards(string email)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.US.GetUserBoards(email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            return (List<int>)response.ReturnValue;
        }

        public List<string> GetUserBoardNames(string email)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.US.GetBoardNames(email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            List<string> output = new List<string>();
            Newtonsoft.Json.Linq.JArray arrayAttribute = (Newtonsoft.Json.Linq.JArray)response.ReturnValue;
            if (arrayAttribute != null)
                foreach (string name in arrayAttribute)
                    output.Add(name);
            return output;
        }

        public string GetBoardName(int board)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.GetBoardName(board));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            return (string)response.ReturnValue;
        }
        public void JoinBoard(string email, int boardID)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.JoinToBoard(boardID, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public void LeaveBoard(string email, int boardID)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.LeaveBoardById(email, boardID));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);

        }
        public void AssignTask(string email, string boardName, int taskID, string emailAssignee)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.AssignToTaskByName(boardName, taskID, email, emailAssignee));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public void LoadData()
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.LoadData());
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public void TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.ChangeOwnerByName(boardName, currentOwnerEmail, newOwnerEmail));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }

        public string GetColumnLimitToString(string email, string boardName, int columnOrdinal)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.GetColumnLimitByName(boardName, columnOrdinal, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            long limit = (long)response.ReturnValue;
            int lim = (int)limit;
            if (lim == -1)
                return "No limit";
            return lim.ToString();
        }

        public TaskModel GetTaskModel(string email, string boardName, int idtaskId)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.GetTaskByBoardName(boardName, idtaskId, email));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
            Newtonsoft.Json.Linq.JObject task = (JObject)response.ReturnValue;
            
            int id = (int)task.GetValue("ID");
            DateTime Cdate = DateTime.Parse((string)task.GetValue("CreationDate"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            string title = (string)task.GetValue("Title");
            string description = (string)task.GetValue("Description");
            DateTime date = DateTime.Parse((string)task.GetValue("DueDate"), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind); 
            int boardId = (int)task.GetValue("BoardID");
            string name = (string)task.GetValue("Assignee");
            return new TaskModel(this, id, title,description,Cdate,date,name);
        }


        public List<TaskModel> ColumnToTaskModelList(string email, string boardName, int columnOrdinal)
        {
            return /*ToTaskModelList(*/GetColumn(email, boardName, columnOrdinal)/*)*/;
        }

        private TaskModel ToTaskModel(IntroSE.Kanban.Backend.BusinessLayer.Task t)
        {
            return new TaskModel(this, t.ID, t.Title, t.Description, t.CreationDate, t.DueDate, t.Assignee);
        }

        public void ChangeBoardName(string email, string boardName, string newName)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.ChangeBoardName(email, boardName, newName));
            if (response.IsError())
                throw new Exception(response.ErrorMessage);
        }
        public BoardModel GetBoard(string Email, string name)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.GetBoardByName(Email, name));
            if (response.ErrorMessage != null)
                throw new Exception(response.ErrorMessage);
            
            IntroSE.Kanban.Backend.BusinessLayer.Board board = (IntroSE.Kanban.Backend.BusinessLayer.Board)response.ReturnValue;
            return new BoardModel(this, board.Name, board.BoardId, board.Owner, board.GetLimitOfColumn(0), board.GetLimitOfColumn(1), board.GetLimitOfColumn(2));

        }
        public int GetBoardId(string Email, string name)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.GetBoardId(Email,name));
            if (response.ErrorMessage != null)
                throw new Exception(response.ErrorMessage);

            long id = (long)response.ReturnValue;
            int boardId = ((int)id);
            return boardId;
        }
        public  void DeleteTask(int boardId,int taskId,string email)
        {
            Response response = JsonConvert.DeserializeObject<Response>(serviceController.BS.RemoveTask(boardId, taskId, email));
            if(response.ErrorMessage != null)
                throw new Exception(response.ErrorMessage);
        }
    }
}
