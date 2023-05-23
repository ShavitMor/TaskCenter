using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class BusinessInitialize
    {

        BoardMapper boardMapper = new BoardMapper();
        UserMapper userMapper = new UserMapper();
        TaskMapper taskMapper = new TaskMapper();
        ColumnMapper columnMapper = new ColumnMapper();
        BoardMemberMapper boardMemberMapper = new BoardMemberMapper();

        public void LoadData(UserController uc, BoardController bc)
        {
            BoardController.UC = uc;
            List<UserDTO> users = userMapper.SelectAllUsers();
            foreach (UserDTO user in users)
            {
                User currUser = new User(user, uc);
                uc.InsertToDictionries(currUser, user);
            }
            List<BoardDTO> boards = boardMapper.SelectAllBoards();
            foreach (BoardDTO board in boards)
            {
                Board currBoard = new Board(board, board.Owner);
                bc.InsertBoard(board.Id, currBoard);
                uc.GetUser(board.Owner).AddBoardForData(currBoard);
            }
            List<TaskDTO> tasks = taskMapper.SelectAllTasks();
            foreach (int key in bc.GetKeys())
            {
                Column backlog = new Column(TaskState.Backlog, columnMapper.GetTaskLimit(key, 0), key);
                List<TaskDTO> taskDTOs = taskMapper.GetAllTasksOfColumn(key, 0);
                foreach (TaskDTO taskDTO in taskDTOs)
                {
                    Task task;
                    if (taskDTO.Assignee != null)
                        task = new Task(taskDTO, taskDTO.Assignee);
                    else
                        task = new Task(taskDTO, null);
                    backlog.AddTask(task);

                }
                bc.GetBoard(key).Backlog=backlog;
                Column inProgress = new Column(TaskState.InProgress, columnMapper.GetTaskLimit(key, 1), key);
                inProgress.TaskLimit = columnMapper.GetTaskLimit(key, 1);
                taskDTOs = taskMapper.GetAllTasksOfColumn(key, 1);
                foreach (TaskDTO taskDTO in taskDTOs)
                {
                    Task task;
                    if (taskDTO.Assignee != null)
                        task = new Task(taskDTO, taskDTO.Assignee);
                    else
                        task = new Task(taskDTO, null);
                    inProgress.AddTask(task);

                }
                bc.GetBoard(key).InProgress = inProgress;
                Column done = new Column(TaskState.Done, columnMapper.GetTaskLimit(key, 2), key);
                taskDTOs = taskMapper.GetAllTasksOfColumn(key, 2);
                foreach (TaskDTO taskDTO in taskDTOs)
                {
                    Task task;
                    if (taskDTO.Assignee != null)
                        task = new Task(taskDTO, taskDTO.Assignee);
                    else
                        task = new Task(taskDTO, null);
                    done.AddTask(task);
                }
                bc.GetBoard(key).Done = done;
                List<string> boardMembersDTOs = boardMemberMapper.GetMembers(key);
                foreach (string email in boardMembersDTOs)
                {
                    User us = uc.GetUser(email);
                    if (us.Email != bc.GetBoard(key).Owner)
                    {
                        bc.GetBoard(key).AddMemberData(us);
                        us.AddBoardForData(bc.GetBoard(key));
                    }
                }
            }
        }
        public void DeleteData(BoardController bc, UserController uc)
        {
            uc.ClearAll();
            bc.ClearAll();
            int a = boardMemberMapper.DeleteAll();
            int b = boardMapper.DeleteAll();
            int c = taskMapper.DeleteAll();
            int d = userMapper.DeleteAll();
            int e = columnMapper.DeleteAll();
            if (a >= 0 && b >= 0 && c >= 0 && d >= 0 && e >= 0)
                return;
            else
                throw new Exception("Failed deleting data.");
        }
    }
}
