using System;
using System.Collections.Generic;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class BoardController
    {
        private Dictionary<int, Board> boards;
        private static int boardNum;
        private static UserController userController;

        private BoardMapper boardMapper;
        private BoardMemberMapper memberMapper;
        private ColumnMapper columnMapper;

        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public BoardController()
        {
            log.Info("creating board controller");
            boards = new Dictionary<int, Board>();
            boardMapper = new BoardMapper();
            memberMapper = new BoardMemberMapper();
            columnMapper = new ColumnMapper();
            boardNum = 0;
        }

        public BoardController(UserController us)
        {
            log.Info("creating board controller");
            boards = new Dictionary<int, Board>();
            userController = us;
            boardNum = boardMapper.SelectAllBoards().Count;
            boardMapper = new BoardMapper();
            memberMapper = new BoardMemberMapper();
        }
        internal static UserController UC { get { return userController; } set { userController = value; } }
        internal void InsertBoard(int id, Board board)
        {
            boards.Add(id, board);
        }
        internal List<int> GetKeys()
        {
            List<int> keys = new List<int>();
            foreach (int key in boards.Keys)
                keys.Add(key);
            return keys;
        }
        public Board CreateNewBoard(string BoardName, string email)
        {
            //creates new board
            email = email.ToLower();
            User user = UC.GetUser(email);
            /*            BoardName=BoardName.ToLower();*/
            log.Info("tries to create new board " + BoardName);
            if (!UC.IsLoggedIn(email))
            {
                log.Error("user is Not logged in");
                throw new Exception("you need to be logged in to do this action");
            }
            if (user.HasSameBoardName(BoardName))
            {
                log.Error("has board with this name");
                throw new Exception("has board with this name");
            }
            while (boards.ContainsKey(boardNum))
                boardNum++;
            Board board = new Board(BoardName, boardNum, email);
            user.NewBoard(board);
            BoardDTO bt = new BoardDTO(BoardName, boardNum, board.FreeTaskId, email);
            BoardMembersDTO boardMembersDTO = new BoardMembersDTO(boardNum, email);
            boardMapper.Insert(bt);
            memberMapper.Insert(boardMembersDTO);
            boards.Add(boardNum, board);
            boardNum++;
            log.Info("board added successfuly");
            return board;

        }

        public Board GetBoard(int id, string email)
        {
            log.Info("tries to get board " + id);
            if (!userController.IsLoggedIn(email))
            {
                log.Error("user " + email + " isnt logged in");
                throw new Exception("you need to be logged in to do this action");
            }
            //return board by id
            if (boards.ContainsKey(id))
            {
                log.Info("return board number " + id);
                return boards[id];
            }
            else
            {
                log.Error("Board with id: " + id + " Does not exist");
                throw new ArgumentException("Board with id: " + id + " Does not exist.");
            }
        }
        public int GetBoardId(string email, string boardName)
        {
            log.Info("tries to get board " + boardName);
            if (!userController.IsLoggedIn(email))
            {
                log.Error("user " + email + " isnt logged in");
                throw new Exception("you need to be logged in to do this action");
            }
            return userController.GetUser(email).GetBoard(boardName).BoardId;

        }

        public Board GetBoard(string boardName, string email)
        {
            log.Info("tries to get board " + boardName);
            User user = userController.GetUser(email);
            if (!user.IsLoggedIn)
            {
                log.Error("user " + email + " isnt logged in");
                throw new Exception("you need to be logged in to do this action");
            }
            //return board by BoardName
            return user.GetBoard(boardName);
        }
        internal Board GetBoard(int id)
        {

            //return board by id
            if (boards.ContainsKey(id))
            {
                log.Info("return board number " + id);
                return boards[id];
            }
            else
            {
                log.Error("Board with id: " + id + " Does not exist");
                throw new ArgumentException("Board with id: " + id + " Does not exist.");
            }
        }

        public void LeaveBoard(string boardName, string email)
        {
            //leavingg board by his name 
            log.Info(email + " tries to leave board " + boardName);
            User user = userController.GetUser(email);
            Board board = user.GetBoard(boardName);
            if (user.Email == board.Owner)
                throw new Exception("owner get leave board");
            user.LeaveBoard(board);
        }
        public void LeaveBoard(int id, string email)
        {
            //leavingg board by this id 
            log.Info(email + " tries to leave board " + id);
            if (!boards.ContainsKey(id))
            {
                log.Warn("id isnot exist in the system");
                throw new Exception("please put valid id");
            }
            Board board = GetBoard(id, email);
            User getter = userController.GetUser(email);
            if (!board.Members.Contains(getter.Email))
            {
                throw new ArgumentException($"User {email} is not a member of board{id}.");
            }
            getter.LeaveBoard(board);
        }

        public void DeleteBoard(string boardName, string email)
        {
            // deletes board by board name
            log.Info($"tring to remove board {boardName} from user {email}");
            User user = userController.GetUser(email);
            Board board = user.GetBoard(boardName);
            if (user.Email != board.Owner)
                throw new Exception("Only owner can delete board.");
            int id = board.BoardId;
            user.DeleteBoard(board);
            BoardDTO bt = boardMapper.GetBoard(id);
            if (boardMapper.Delete(bt))
            {
                boards.Remove(id);
                log.Info("board " + id + " deleted.");
            }
            else
                log.Error("failed to change in the data base");
        }
        public void DeleteBoard(int id, string email)
        {
            // deletes board by board name
            log.Info($"tring to delete board ");
            User user = userController.GetUser(email);
            Board board = GetBoard(id);
            if (user.Email != board.Owner)
                throw new Exception("Only owner can delete board.");
            user.DeleteBoard(board);
            BoardDTO bt = boardMapper.GetBoard(id);
            if (boardMapper.Delete(bt))
            {
                boards.Remove(id);
                log.Info("board " + id + " deleted.");
            }
            else
                log.Error("failed to change in the data base");
        }



        public void ChangeOwner(int boardNumber, string currOwner, string newOwner)
        {
            //change owner of the board
            log.Info(" try to move ownership of board " + boardNumber + " to " + newOwner);
            Board board = GetBoard(boardNumber, currOwner);
            board.ChangeOwner(userController.GetUser(currOwner), userController.GetUser(newOwner));
        }

        public void ChangeOwner(string boardName, string currOwner, string newOwner)
        {
            //change owner of the board
            log.Info(" trying to move ownership of board " + boardName + " to " + newOwner);
            Board board = GetBoard(boardName, currOwner);
            board.ChangeOwner(userController.GetUser(currOwner), userController.GetUser(newOwner));
        }

        public Task AddNewTask(string title, string description, DateTime dueDate, int boardID, string executor)
        {
            log.Info($"Creating new task and adding to board {boardID}");
            Board board = GetBoard(boardID, executor);
            return board.AddTask(title, description, dueDate);
        }

        public void AddNewTask(string title, string description, DateTime dueDate, string boardName, string executor)
        {
            log.Info($"Creating new task and adding to board {boardName}");
            Board board = GetBoard(boardName, executor);
            board.AddTask(title, description, dueDate);
        }

        private void AddTask(Task ts, int boardId, string excuter)
        {
            //adding task to backlog
            log.Info("tries to add task to board" + boardId);
            Board board = GetBoard(boardId, excuter);
            board.AddTask(ts);
        }
        public void RemoveTask(int taskId, int boardId, string email)
        {
            //deleting task
            log.Info("tries to remove task " + taskId);
            Board board = GetBoard(boardId, email);
            board.RemoveTask(taskId);
            TaskMapper tsMapper=new TaskMapper();
            TaskDTO taskDTO=tsMapper.GetTask(boardId, taskId);
            tsMapper.Delete(taskDTO);
        }
        public Task GetTask(int boardID, int taskID, string email)
        {
            if (!userController.IsLoggedIn(email))
            {
                throw new Exception("User is not logged in");
            }
            Board board = GetBoard(boardID, email);
            return board.GetTask(taskID);
        }

        public Task GetTask(string boardName, int TaskId, string email)
        {
            User user = userController.GetUser(email);
            if (!user.IsLoggedIn)
            {
                throw new Exception($"User {email} is not logged in");
            }
            return user.GetBoard(boardName).GetTask(TaskId);

        }
        public void AssignTask(int boardNumber, int taskNumber, string excuter, string getter = null)
        {
            //assigns task
            log.Info("tries to assign task " + taskNumber + " from board " + boardNumber + " to " + getter);
            Board board = GetBoard(boardNumber, excuter);
            board.GetTask(taskNumber).SetAssignee(excuter, getter);
        }

        public void AssignTask(string boardName, int taskNumber, string excuter, string getter = null)
        {
            //assigns task
            log.Info("tries to assign task " + taskNumber + " from board " + boardName + " to " + getter);
            Board board = GetBoard(boardName, excuter);
            board.GetTask(taskNumber).SetAssignee(excuter, getter);
        }

        public void SetTaskTitle(string email, string boardName, int TaskId, string newTitle)
        {
            GetTask(boardName, TaskId, email).SetTitle(newTitle, userController.GetUser(email));
        }
        public void SetTaskTitle(string email, int BoardId, int TaskId, string newTitle)
        {
            GetTask(BoardId, TaskId, email).SetTitle(newTitle, userController.GetUser(email));
        }
        public void SetTaskDescription(string email, string boardName, int TaskId, string newDesc)
        {
            GetTask(boardName, TaskId, email).SetDescription(newDesc, userController.GetUser(email));
        }
        public void SetTaskDescription(string email, int BoardId, int TaskId, string newDesc)
        {
            GetTask(BoardId, TaskId, email).SetTitle(newDesc, userController.GetUser(email));
        }
        public void SetTaskDueDate(string email, string boardName, int TaskId, DateTime newdueDate)
        {
            GetTask(boardName, TaskId, email).SetDueDate(newdueDate, userController.GetUser(email));
        }
        public void SetTaskDueDate(string email, int BoardId, int TaskId, DateTime newdueDate)
        {
            GetTask(BoardId, TaskId, email).SetDueDate(newdueDate, userController.GetUser(email));
        }
        public void MoveTaskToNextCollumn(int boardId, int taskId, string email)
        {
            //moving task to next state
            if (!userController.IsLoggedIn(email))
            {
                log.Warn("user isnt logged in");
                throw new Exception("you should log in to do this action");
            }
            log.Info("tries to move task " + taskId + " to next state");
            Board board = GetBoard(boardId, email);
            board.MoveTaskToNextcolumn(taskId);
        }
        public Column GetColumn(int boardId, int collumnId, string email)
        {
            //getting collumn of tasks
            if (!userController.IsLoggedIn(email))
            {
                log.Warn("user isnt logged in");
                throw new Exception("you should log in to do this action");
            }
            log.Info("tries to get task from column");
            Board board = GetBoard(boardId, email);
            switch (collumnId)
            {
                case 0:
                    return board.Backlog;
                case 1:
                    return board.InProgress;
                case 2:
                    return board.Done;
                default:
                    throw new ArgumentOutOfRangeException("ColId must be between 0-2.");
            }
        }

        public Column GetColumn(string boardName, int collumnId, string email)
        {
            //getting collumn from a board
            if (!userController.IsLoggedIn(email))
            {
                log.Warn("user isnt logged in");
                throw new Exception("you should log in to do this action");
            }
            log.Info($"tries to get column {collumnId} from Board {boardName}...");
            Board board = GetBoard(boardName, email);
            switch (collumnId)
            {
                case 0:
                    return board.Backlog;
                case 1:
                    return board.InProgress;
                case 2:
                    return board.Done;
                default:
                    throw new ArgumentOutOfRangeException("ColId must be between 0-2.");
            }
        }
        public List<Task> GetTasksOfColumn(string boardName, int collumnId, string email)
        {
            //getting list<Task> from column
            return GetColumn(boardName, collumnId, email).Tasks;

        }

        public void JoinBoard(int id, string email)
        {
            //joining board by this id
            log.Info(email + " tries to join board " + id);
            if (!boards.ContainsKey(id))
                throw new Exception("please put valid id");
            Board board = GetBoard(id, email);
            User getter = userController.GetUser(email);
            getter.JoinBoard(board);
        }

        public string GetName(int id)
        {
            log.Info("tries to get name of board " + id);
            if (!boards.ContainsKey(id))
            {
                log.Warn("id isnot exist in the system");
                throw new Exception("please put valid id");
            }
            log.Info("returning name of board " + id);
            return boards[id].Name;
        }
        public int GetLimitOfColumn(int boardId, int colId, string email)
        {
            Board board = GetBoard(boardId, email);
            int limit = board.GetLimitOfColumn(colId);
            return limit;
        }

        public string GetColumnName(int colId, string email)
        {
            if (!UC.IsLoggedIn(email))
            {
                log.Warn("user isnt logged in");
                throw new Exception("you should log in to do this action");
            }
            if (colId < 0 || colId > 2)
            {
                log.Warn("Col id is between 0-2");
                throw new Exception("invalid number inserted");
            }
            switch (colId)
            {
                case (int)TaskState.Backlog:
                    return "backlog";
                case (int)TaskState.InProgress:
                    return "inProgress";
                case (int)TaskState.Done:
                    return "done";
                default:
                    return null;
            }
        }

        public int GetLimitOfColumn(string boardName, int colId, string email)
        {
            Board board = UC.GetUser(email).GetBoard(boardName);
            int limit = board.GetLimitOfColumn(colId);
            return limit;
        }
        public void SetLimitColumn(int boardId, int colId, int limit, string email)
        {
            Board board = GetBoard(boardId, email);
            board.GetColumn(colId).TaskLimit = limit;
            columnMapper.ChangeTaskLimit(boardId, (int)colId, limit);
        }

        public void SetLimitColumn(string boardName, int colId, int limit, string email)
        {
            Board board = UC.GetUser(email).GetBoard(boardName);
            board.GetColumn(colId).TaskLimit = limit;
            columnMapper.ChangeTaskLimit(board.BoardId, (int)colId, limit);
        }

        public void ClearAll()
        {
            boards = new();
            boardNum = 0;

        }

    }

}
