using System;
using System.Collections.Generic;
/*using IntroSE.Kanban.Backend.ServiceLayer.ServiceClasses;*/
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        Response response;
        private static BoardController BoardCon;

        Newtonsoft.Json.JsonSerializer serializer;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public BoardService(BoardController boardController)
        {
            BoardService.BoardCon = boardController;
        }
        public string NewBoard(string email, string boardName)      //Creates new board
        {
            log.Info($"Creating new board...");

            try
            {
                if (email[0] == '"' && email[email.Length - 1] == '"')
                {  //checking if string is Json string
                    email = JsonSerializer.Deserialize<string>(email); //converting from Json string                   
                }
                if (boardName[0] == '"' && boardName[boardName.Length - 1] == '"')
                {  //checking if string is Json string
                    boardName = JsonSerializer.Deserialize<string>(boardName);
                }
                BoardCon.CreateNewBoard(boardName, email);
                log.Info($"New Board {boardName} created. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }


        public string GetBoardId(string email, string boardName)      //Creates new board
        {
            log.Info($"Getting board id");

            try
            {
                if (email[0] == '"' && email[email.Length - 1] == '"')
                {  //checking if string is Json string
                    email = JsonSerializer.Deserialize<string>(email); //converting from Json string                   
                }
                if (boardName[0] == '"' && boardName[boardName.Length - 1] == '"')
                {  //checking if string is Json string
                    boardName = JsonSerializer.Deserialize<string>(boardName);
                }
                int id = BoardCon.GetBoardId(email, boardName);
                log.Info($"New Board {boardName} created. ");
                return JsonSerializer.Serialize(Response.ResponseValue(id));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoardById(string email, string boardId)      //Gets board by id
        {

            log.Info($"Getting board...");
            email = JsonSerializer.Deserialize<string>(email); //converting from Json string
            int id = JsonSerializer.Deserialize<int>(boardId);
            try
            {
                Board b = BoardCon.GetBoard(id, email);
                log.Info($"got board {id}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(b));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoardById(string email, int boardId)      //Gets board by id
        {

            log.Info($"Getting board...");
            try
            {
                Board b = BoardCon.GetBoard(boardId, email);
                log.Info($"got board {boardId}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(b));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoardByName(string email, string name)      //Gets board by Name
        {
            try
            {
                log.Info($"Getting board...");
                if (email[0] == '"' && email[email.Length - 1] == '"')
                {
                     email = JsonSerializer.Deserialize<string>(email); //converting from Json string
                    name = JsonSerializer.Deserialize<string>(name);     //converting from Json string
                }

                Board b = BoardCon.GetBoard(name, email);
                log.Info($"got board {name} of {email}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(b));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }


        public string GetOwnerByName(string email, string name)      //Gets owner of board by Name
        {
            try
            {
                log.Info($"Getting owner of board...");
                if (email[0] == '"' && email[email.Length - 1] == '"')
                {
                    email = JsonSerializer.Deserialize<string>(email); //converting from Json string
                    name = JsonSerializer.Deserialize<string>(name);     //converting from Json string
                }

                string b = BoardCon.GetBoard(name, email).Owner;
                log.Info($"got board {name} of {email}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(b));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoardName(string email, string boardId)      //Gets board name by id
        {

            log.Info($"Getting board...");
            email = JsonSerializer.Deserialize<string>(email); //converting from Json string
            int id = JsonSerializer.Deserialize<int>(boardId);
            try
            {
                string b = BoardCon.GetBoard(id, email).Name;
                log.Info($"got board {id}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(b));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoardName(string email, int boardId)      //Gets board name by id
        {

            log.Info($"Getting board...");
            try
            {
                string b = BoardCon.GetBoard(boardId, email).Name;
                log.Info($"got board {boardId}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(b));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string ChangeBoardName(string email, string boardName,string newName)      //Creates new board
        {
            log.Info($"Changing board Name...");

            try
            {
                if (email[0] == '"' && email[email.Length - 1] == '"')
                {  //checking if string is Json string
                    email = JsonSerializer.Deserialize<string>(email); //converting from Json string                   
                }
                if (boardName[0] == '"' && boardName[boardName.Length - 1] == '"')
                {  //checking if string is Json string
                    boardName = JsonSerializer.Deserialize<string>(boardName);
                    newName = JsonSerializer.Deserialize<string>(newName);
                }
                BoardCon.GetBoard(boardName, email).Name = newName;
                log.Info($"changef Board {boardName} to {newName}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string LeaveBoardByName(string email, string boardname)      //Allows user to leave a board
        {
            log.Info($"Trying toleave board...");
            if (email[0] == '"' && email[email.Length - 1] == '"')
            {
                email = JsonSerializer.Deserialize<string>(email); //converting from Json string
                boardname = JsonSerializer.Deserialize<string>(boardname);     //converting from Json string
            }
            try
            {
                BoardCon.LeaveBoard(boardname, email);
                log.Info($"{email} left board {boardname}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string LeaveBoardById(string email, string boardId)      //Allows user to leave a board
        {
            log.Info($"Trying toleave  board...");
            email = JsonSerializer.Deserialize<string>(email); //converting from Json string
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                BoardCon.LeaveBoard(id, email);
                log.Info($"{email} left board {id}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string LeaveBoardById(string email, int id)      //Allows user to leave a board
        {
            log.Info($"Trying leave board...");
            try
            {
                BoardCon.LeaveBoard(id, email);
                log.Info($"{email} left board {id}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string DeleteBoardByName(string email, string boardname)      //Allows user to leave a board
        {
            log.Info($"Trying delete board...");
            if (email[0] == '"' && email[email.Length - 1] == '"')
            {
                email = JsonSerializer.Deserialize<string>(email); //converting from Json string
                boardname = JsonSerializer.Deserialize<string>(boardname);     //converting from Json string
            }
            try
            {
                BoardCon.DeleteBoard(boardname, email);
                log.Info($"board {boardname} deleted. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string DeleteBoardById(string email, string boardId)      //Allows user to leave a board
        {
            log.Info($"Trying to Delete board...");
            email = JsonSerializer.Deserialize<string>(email); //converting from Json string
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                BoardCon.DeleteBoard(id, email);
                log.Info($"board {id} deleted. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string DeleteBoardById(string email, int id)      //Allows user to leave a board
        {
            log.Info($"Trying delete board...");
            try
            {
                BoardCon.DeleteBoard(id, email);
                log.Info($"Board {id} deleted. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string ChangeOwner(string boardId, string currOwnerMail, string newOwnerMail)      //Changes board owner
        {
            log.Info($"Trying to change board's owner...");
            currOwnerMail = JsonSerializer.Deserialize<string>(currOwnerMail); //converting from Json string
            newOwnerMail = JsonSerializer.Deserialize<string>(newOwnerMail);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                BoardCon.ChangeOwner(id, currOwnerMail, newOwnerMail);
                log.Info($"board {id} owner changed to {newOwnerMail}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string ChangeOwner(int boardId, string currOwnerMail, string newOwnerMail)      //Changes board owner
        {
            log.Info($"Trying to change board's owner...");
            try
            {
                BoardCon.ChangeOwner(boardId, currOwnerMail, newOwnerMail);
                log.Info($"board {boardId} owner changed to {newOwnerMail}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string ChangeOwnerByName(string boardName, string currOwnerMail, string newOwnerMail)      //Changes board owner
        {
            if (currOwnerMail[0] == '"' && currOwnerMail[currOwnerMail.Length - 1] == '"')
            {  //checking if string is Json string
                log.Info($"Trying to change board's owner...");
                currOwnerMail = JsonSerializer.Deserialize<string>(currOwnerMail); //converting from Json string
                newOwnerMail = JsonSerializer.Deserialize<string>(newOwnerMail);
                boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            }
            try
            {
                BoardCon.ChangeOwner(boardName, currOwnerMail, newOwnerMail);
                log.Info($"board {boardName} owner changed to {newOwnerMail}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string NewTaskById(string boardId, string title, string description, string dueDate, string email)      //Creates new task in board
        {
            log.Info($"Creating new Task...");
            title = JsonSerializer.Deserialize<string>(title); //converting from Json string
            description = JsonSerializer.Deserialize<string>(description);
            DateTime due = JsonSerializer.Deserialize<DateTime>(dueDate);
            email = JsonSerializer.Deserialize<string>(email);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                BoardCon.AddNewTask(title, description, due, id, email);
                log.Info($"New Task added to board{id}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string NewTaskById(int boardId, string title, string description, DateTime dueDate, string email)      //Creates new task in board
        {
            log.Info($"Creating new Task...");
            try
            {
                BoardCon.AddNewTask(title, description, dueDate, boardId, email);
                log.Info($"New Task added to board{boardId}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string NewTask(string boardName, string title, string description, string dueDate, string email)      //Creates new task in board
        {
            log.Info($"Creating new Task...");
            title = JsonSerializer.Deserialize<string>(title); //converting from Json string
            description = JsonSerializer.Deserialize<string>(description);
            DateTime due = JsonSerializer.Deserialize<DateTime>(dueDate);
            email = JsonSerializer.Deserialize<string>(email);
            boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            try
            {
                BoardCon.AddNewTask(title, description, due, boardName, email);
                log.Info($"New Task added to board{boardName}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string NewTask(string boardName, string title, string description, DateTime dueDate, string email)      //Creates new task in board
        {
            log.Info($"Creating new Task...");
            try
            {
                BoardCon.AddNewTask(title, description, dueDate, boardName, email);
                log.Info($"New Task added to board{boardName}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string RemoveTask(string boardId, string taskId, string email)      //Removes task in board
        {
            log.Info($"Trying to remove task from board...");
            int taskid = JsonSerializer.Deserialize<int>(taskId); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                BoardCon.RemoveTask(taskid, id, email);
                log.Info($"Task {taskid} removed from board {id}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string RemoveTask(int boardId, int taskId, string email)      //Removes task in board
        {
            log.Info($"Trying to remove task from board...");
            try
            {
                BoardCon.RemoveTask(taskId, boardId, email);
                log.Info($"Task {taskId} removed from board {boardId}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetTaskByBoardName(string boardName, int taskid, string email)      //Gets task from board
        {
            log.Info($"Getting task from board...");
            try
            {
                Task t = BoardCon.GetTask(boardName, taskid, email);
                log.Info($"Got Task {taskid} removed from board {boardName}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(t));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetTaskByBoardName(string boardName, string taskId, string email)      //Gets task from board
        {
            log.Info($"Getting task from board...");
            int taskid = JsonSerializer.Deserialize<int>(taskId); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            try
            {
                Task t = BoardCon.GetTask(boardName, taskid, email);
                log.Info($"Got Task {taskid} removed from board {boardName}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(t));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetTaskByBoardID(string boardId, string taskId, string email)       //Gets task from board
        {
            log.Info($"Getting task from board...");
            int taskid = JsonSerializer.Deserialize<int>(taskId); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                Task t = BoardCon.GetTask(id, taskid, email);
                log.Info($"Got Task {taskid} removed from board {id}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(t));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetTaskByBoardID(int boardId, int taskId, string email)       //Gets task from board
        {
            log.Info($"Getting task from board...");
            try
            {
                Task t = BoardCon.GetTask(boardId, taskId, email);
                log.Info($"Got Task {taskId}  from board {boardId}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(t));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string AssignToTask(string boardId, string taskId, string exec, string newAssignee)      //Changes tasks assignee
        {
            log.Info($"Trying to change task's assignee...");
            exec = JsonSerializer.Deserialize<string>(exec); //converting from Json string
            newAssignee = JsonSerializer.Deserialize<string>(newAssignee);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            int taskid = JsonSerializer.Deserialize<int>(taskId);     //converting from Json string
            try
            {
                BoardCon.AssignTask(id, taskid, exec, newAssignee);
                log.Info($"task {taskid} assigned to {newAssignee}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string AssignToTask(int boardId, int taskId, string exec, string newAssignee)      //Changes tasks assignee
        {
            log.Info($"Trying to change task's assignee...");
            try
            {
                BoardCon.AssignTask(boardId, taskId, exec, newAssignee);
                log.Info($"task {taskId} assigned to {newAssignee}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string AssignToTaskByName(string BoardName, string taskId, string exec, string newAssignee)      //Changes tasks assignee
        {
            log.Info($"Trying to change task's assignee...");
            exec = JsonSerializer.Deserialize<string>(exec); //converting from Json string
            newAssignee = JsonSerializer.Deserialize<string>(newAssignee);
            string id = JsonSerializer.Deserialize<string>(BoardName);     //converting from Json string
            int taskid = JsonSerializer.Deserialize<int>(taskId);     //converting from Json string
            try
            {
                BoardCon.AssignTask(id, taskid, exec, newAssignee);
                log.Info($"task {taskid} assigned to {newAssignee}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string AssignToTaskByName(string boardName, int taskId, string exec, string newAssignee)      //Changes tasks assignee
        {
            log.Info($"Trying to change task's assignee...");
            try
            {
                BoardCon.AssignTask(boardName, taskId, exec, newAssignee);
                log.Info($"task {taskId} assigned to {newAssignee}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string SetTaskTitleByBoardName(string newTitle, string taskId, string boardName, string email)      //Changes task title
        {
            log.Info($"Trying to change task's title...");
            newTitle = JsonSerializer.Deserialize<string>(newTitle); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            int taskid = JsonSerializer.Deserialize<int>(taskId);     //converting from Json string
            try
            {
                BoardCon.SetTaskTitle(email, boardName, taskid, newTitle);
                log.Info($"task {taskid} title changed to{newTitle} ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetTaskTitleByBoardName(string newTitle, int taskId, string boardName, string email)      //Changes task title
        {
            log.Info($"Trying to change task's title...");
            try
            {
                BoardCon.SetTaskTitle(email, boardName, taskId, newTitle);
                log.Info($"task {taskId} title changed to{newTitle} ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetTaskTitleByBoardID(string newTitle, string taskId, string boardId, string email)      //Changes task title
        {
            log.Info($"Trying to change task's title...");
            newTitle = JsonSerializer.Deserialize<string>(newTitle); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            int Id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            int taskid = JsonSerializer.Deserialize<int>(taskId);     //converting from Json string
            try
            {
                BoardCon.SetTaskTitle(email, Id, taskid, newTitle);
                log.Info($"task {taskid} title changed to{newTitle} ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }


        public string SetTaskTitleByBoardId(string newTitle, int taskId, int boardId, string email)      //Changes task title
        {
            log.Info($"Trying to change task's title...");
            try
            {
                BoardCon.SetTaskTitle(email, boardId, taskId, newTitle);
                log.Info($"task {taskId} title changed to{newTitle} ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetTaskDescriptionByBoardName(string newDesc, string taskId, string boardName, string email)      //Changes task description
        {
            log.Info($"Trying to change task's title...");
            newDesc = JsonSerializer.Deserialize<string>(newDesc); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            int taskid = JsonSerializer.Deserialize<int>(taskId);     //converting from Json string
            try
            {
                BoardCon.SetTaskDescription(email, boardName, taskid, newDesc);
                log.Info($"task {taskid} descrription changed. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetTaskDescriptionByBoardName(string newDesc, int taskId, string boardName, string email)      //Changes task description
        {
            log.Info($"Trying to change task's description...");
            try
            {
                BoardCon.SetTaskDescription(email, boardName, taskId, newDesc);
                log.Info($"task {taskId} description changed. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetTaskDescriptionByBoardID(string newDesc, string taskId, string boardId, string email)      //Changes task description
        {
            log.Info($"Trying to change task's title...");
            newDesc = JsonSerializer.Deserialize<string>(newDesc); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            int Id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            int taskid = JsonSerializer.Deserialize<int>(taskId);     //converting from Json string
            log.Info($"Trying to change task's description...");
            try
            {
                BoardCon.SetTaskDescription(email, Id, taskid, newDesc);
                log.Info($"task {taskId} description changed. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }


        public string SetTaskDescriptionByBoardId(string newDesc, int taskId, int boardId, string email)      //Changes task desscription
        {
            log.Info($"Trying to change task's description...");
            try
            {
                BoardCon.SetTaskDescription(email, boardId, taskId, newDesc);
                log.Info($"task {taskId} description changed. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetTaskDueDateByBoardName(string date, string taskId, string boardName, string email)      //Changes task duedate
        {
            log.Info($"Trying to change task's duedate...");
            DateTime time = JsonSerializer.Deserialize<DateTime>(date); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            int taskid = JsonSerializer.Deserialize<int>(taskId);     //converting from Json string
            try
            {
                BoardCon.SetTaskDueDate(email, boardName, taskid, time);
                log.Info($"task {taskid} due date changed. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetTaskDueDateByBoardName(DateTime time, int taskId, string boardName, string email)      //Changes task dueDate
        {
            log.Info($"Trying to change task's duedate...");
            try
            {
                BoardCon.SetTaskDueDate(email, boardName, taskId, time);
                log.Info($"task {taskId} due date changed. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetTaskDueDuateByBoardID(string date, string taskId, string boardId, string email)      //Changes task duedate
        {
            log.Info($"Trying to change task's duedate...");
            DateTime time = JsonSerializer.Deserialize<DateTime>(date); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            int taskid = JsonSerializer.Deserialize<int>(taskId);     //converting from Json string
            try
            {
                BoardCon.SetTaskDueDate(email, id, taskid, time);
                log.Info($"task {taskid} due date changed. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }


        public string SetTaskDueDuateByBoardID(DateTime time, int taskId, int boardId, string email)      //Changes task duedate
        {
            log.Info($"Trying to change task's duedate...");
            try
            {
                BoardCon.SetTaskDueDate(email, boardId, taskId, time);
                log.Info($"task {taskId} due date changed. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }

        }

        public string AdvanceTaskById(string boardId, string taskId, string email)      //Advnces task to the next column board
        {
            log.Info($"Trying to advance task from board...");
            int taskid = JsonSerializer.Deserialize<int>(taskId); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                BoardCon.MoveTaskToNextCollumn(id, taskid, email);
                log.Info($"Task {taskid} from board {id} advnced to next column. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string AdvanceTaskById(int boardId, int taskid, string email)      //Advnces task to the next column board
        {
            log.Info($"Trying to advance task from board...");
            try
            {
                BoardCon.MoveTaskToNextCollumn(boardId, taskid, email);
                log.Info($"Task {taskid} from board {boardId} advnced to next column. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string AdvanceTask(string boardName, string taskId, string email)      //Advnces task to the next column board
        {
            log.Info($"Trying to advance task from board...");
            int taskid = JsonSerializer.Deserialize<int>(taskId); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            try
            {
                BoardCon.MoveTaskToNextCollumn(BoardCon.GetBoard(boardName, email).BoardId, taskid, email);
                log.Info($"Task {taskid} from board {boardName} advnced to next column. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string AdvanceTask(string boardName, int taskid, string email)      //Advnces task to the next column board
        {
            log.Info($"Trying to advance task from board...");
            try
            {
                BoardCon.MoveTaskToNextCollumn(BoardCon.GetBoard(boardName, email).BoardId, taskid, email);
                log.Info($"Task {taskid} from board {boardName} advnced to next column. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumn(string boardId, string columnID, string email)      //REturns column from board
        {
            log.Info($"Trying to get column from board...");
            int colId = JsonSerializer.Deserialize<int>(columnID); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                Column col = BoardCon.GetColumn(id, colId, email);
                log.Info($"got Column {colId} from board {id}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(col));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumn(int boardId, int columnID, string email)      //REturns column from board
        {
            log.Info($"Trying to get column from board...");
            try
            {
                Column col = BoardCon.GetColumn(boardId, columnID, email);
                log.Info($"got Column {columnID} from board {boardId}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(col));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumnByName(string boardName, string columnID, string email)      //REturns column from board
        {
            log.Info($"Trying to get column from board...");
            int colId = JsonSerializer.Deserialize<int>(columnID); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            string id = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            try
            {
                Column col = BoardCon.GetColumn(id, colId, email);
                log.Info($"got Column {colId} from board {id}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(col));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string GetColumnByName(string boardName, int columnID, string email)      //REturns column from board
        {
            log.Info($"Trying to get column from board...");
            try
            {
                Column col = BoardCon.GetColumn(boardName, columnID, email);
                log.Info($"got Column {columnID} from board {boardName}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(col));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetTasksOfColumn(string boardName, string columnID, string email)      //REturns column from board
        {
            log.Info($"Trying to get  Tasks of a column {columnID} from board {boardName}...");
            int colId = JsonSerializer.Deserialize<int>(columnID); //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);
            string id = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            try
            {
                List<Task> col = BoardCon.GetTasksOfColumn(id, colId, email);
                log.Info($"got Column {colId} from board {id}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(col));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string GetTasksOfColumn(string boardName, int columnID, string email)      //REturns column from board
        {
            log.Info($"Trying to get  Tasks of a column {columnID} from board {boardName}...");
            try
            {
                List<Task> col = BoardCon.GetTasksOfColumn(boardName, columnID, email);
                log.Info($"got Column {columnID} from board {boardName}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(col));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string JoinToBoard(string boardId, string email)      //Adds user to board
        {
            log.Info($"Trying to add user board...");
            email = JsonSerializer.Deserialize<string>(email);
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                BoardCon.JoinBoard(id, email);
                log.Info($"user {email}  added board {id}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string JoinToBoard(int boardId, string email)      //Adds user to board
        {
            log.Info($"Trying to add user board...");
            try
            {
                BoardCon.JoinBoard(boardId, email);
                log.Info($"user {email}  added board {boardId}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoardName(string boardId)      //Getting name of board
        {
            log.Info($"Trying to add user board...");
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            try
            {
                string n = BoardCon.GetName(id);
                log.Info($"got name of board {id}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(n));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoardName(int boardId)      //Getting name of board
        {
            log.Info($"Trying to add user board...");
            try
            {
                string n = BoardCon.GetName(boardId);
                log.Info($"got name of board {boardId}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(n));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumnLimit(string boardId, string columnID, string email)      //REturns columns limit from board
        {
            log.Info($"Trying to getlimit  column from board...");
            int colId = JsonSerializer.Deserialize<int>(columnID); //converting from Json string
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);     //converting from Json string
            try
            {
                int ans = BoardCon.GetLimitOfColumn(id, colId, email);
                log.Info($"got Column {colId} limit from board {id}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(ans));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumnLimit(int boardId, int columnID, string email)      //REturns columns limit from board
        {
            log.Info($"Trying to getlimit  column from board...");
            try
            {
                int asns = BoardCon.GetLimitOfColumn(boardId, columnID, email);
                log.Info($"got Column {columnID} limit from board {boardId}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(asns));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumnLimitByName(string boardName, string columnID, string email)      //REturns columns limit from board
        {
            log.Info($"Trying to getlimit  column from board...");
            int colId = JsonSerializer.Deserialize<int>(columnID); //converting from Json string
            boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);     //converting from Json string
            try
            {
                int ans = BoardCon.GetLimitOfColumn(boardName, colId, email);
                log.Info($"got Column {colId} limit from board {boardName}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(ans));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumnName(string colID, string email)
        {
            log.Info($"Trying to get  column name...");
            email = JsonSerializer.Deserialize<string>(email);     //converting from Json string
            int columnName = JsonSerializer.Deserialize<int>(colID);
            try
            {
                string asns = BoardCon.GetColumnName(columnName, email);
                log.Info($"got Column {colID} name  ");
                return JsonSerializer.Serialize(Response.ResponseValue(asns));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumnName(int colID, string email)
        {
            log.Info($"Trying to get  column name...");
            try
            {
                string asns = BoardCon.GetColumnName(colID, email);
                log.Info($"got Column {colID} name  ");
                return JsonSerializer.Serialize(Response.ResponseValue(asns));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetColumnLimitByName(string boardName, int columnID, string email)      //REturns columns limit from board
        {
            log.Info($"Trying to getlimit  column from board...");
            try
            {
                int asns = BoardCon.GetLimitOfColumn(boardName, columnID, email);
                log.Info($"got Column {columnID} limit from board {boardName}. ");
                return JsonSerializer.Serialize(Response.ResponseValue(asns));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetColumnLimitByBoardID(string boardId, string columnID, string limit, string email)      //set columns limit from board
        {
            log.Info($"Trying to set limit  column from board...");
            int colId = JsonSerializer.Deserialize<int>(columnID); //converting from Json string
            int id = JsonSerializer.Deserialize<int>(boardId);     //converting from Json string
            int lim = JsonSerializer.Deserialize<int>(limit);     //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);     //converting from Json string
            try
            {
                BoardCon.SetLimitColumn(id, colId, lim, email);
                log.Info($"set Column {colId} limit from board {id}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetColumnLimitByBoardID(int boardId, int columnID, int newLimit, string email)      //set columns limit from board
        {
            log.Info($"Trying to getlimit  column from board...");
            try
            {
                BoardCon.SetLimitColumn(boardId, columnID, newLimit, email);
                log.Info($"set Column {columnID} limit from board {boardId}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }


        public string SetColumnLimit(string email, string boardName, string columnID, string limit)      //set columns limit from board
        {
            log.Info($"Trying to set limit  column from board...");
            int colId = JsonSerializer.Deserialize<int>(columnID); //converting from Json string
            boardName = JsonSerializer.Deserialize<string>(boardName);     //converting from Json string
            int lim = JsonSerializer.Deserialize<int>(limit);     //converting from Json string
            email = JsonSerializer.Deserialize<string>(email);     //converting from Json string
            try
            {
                BoardCon.SetLimitColumn(boardName, colId, lim, email);
                log.Info($"set Column {colId} limit from board {boardName}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetColumnLimit(string email, string boardName, int columnID, int limit)      //set columns limit from board
        {
            log.Info($"Trying to set limit  column from board...");
            try
            {
                BoardCon.SetLimitColumn(boardName, columnID, limit, email);
                log.Info($"set Column {columnID} limit from board {boardName}. ");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
    }
}
