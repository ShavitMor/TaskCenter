using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Board
    {
        private string boardName;
        private readonly int boardId;
        private Column backLog;
        private Column inProgress;
        private Column done;
        private int freeTaskId;
        private string owner;
        private List<string> members;

        BoardMapper mapper = new BoardMapper();
        BoardMemberMapper memberMapper = new BoardMemberMapper();

        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Board(string name, int boardId, string owner)
        {
            this.boardName = name;
            this.boardId = boardId;
            this.backLog = new Column(TaskState.Backlog, boardId);
            this.inProgress = new Column(TaskState.InProgress, boardId);
            this.done = new Column(TaskState.Done, boardId);
            freeTaskId = 0;
            this.owner = owner;
            members = new List<string>();
            members.Add(owner);
            log.Info("new board is created");

        }
        public string Name
        {
            get { return boardName; }
            set
            {
                if (value == null) throw new Exception(" input name that isnt null"); if (mapper.Update(boardId, "BoardName", value))
                    boardName = value;
            }
        }
        public int BoardId { get { return boardId; } }
        public string Owner { get { return owner; } set { owner = value; } }
        public List<string> Members { get { return members; } }
        public int FreeTaskId { get { log.Info("Got free id"); return freeTaskId; } }
        public Column Backlog { get { return backLog; } set { backLog = value; } }
        public Column InProgress { get { return inProgress; } set { inProgress = value; } }
        public Column Done { get { return done; } set { done = value; } }

        public Board(BoardDTO bd, string owner)
        {
            boardId = bd.Id;
            boardName = bd.BoardName;
            freeTaskId = bd.FreeTaskId;
            this.owner = owner;
            members = new List<string>();
            /*          members.Add(owner.Email);*/
        }
        public void AddTask(Task task)
        {
            //adding new task to the backlog

            backLog.AddTask(task);
            log.Info("task has added to backlog");
        }
        public void RemoveTask(int taskId)
        {
            //removing requried task from requried board
            Task ts = GetTask(taskId);
            if (backLog.ContainTask(taskId))
            {
                log.Info("task has removed from backlog");
                backLog.RemoveTask(ts);
            }
            else if (inProgress.ContainTask(taskId))
            {
                inProgress.RemoveTask(ts);
                log.Info("task has removed from inProgress");
            }
            else if (done.ContainTask(taskId))
            {
                done.RemoveTask(ts);
                log.Info("task has removed from done");
            }
            else
            {
                log.Error("task hasnt Found");
                throw new Exception("board doesnt contain this task number");
            }

        }
        public void MoveTaskToNextcolumn(int taskId)
        {
            //move requried task to next state by finding it by id
            Task task = GetTask(taskId);
            int next = GetColumnOfTask(task);
            if (next == 0)
            {
                task.NextState();
                RemoveTask(task.ID);
                inProgress.AddTask(task);
                log.Info("task has moved from backlog to in progress");
            }
            else if (next == 1)
            {
                task.NextState();
                RemoveTask(task.ID);
                done.AddTask(task);
                log.Info("task has moved from in progress to done");
            }
            else
            {
                log.Warn("task has already in done column");
                throw new ArgumentOutOfRangeException("Cannot change task which is already done.");
            }
        }

        public List<Task> GetTasksOfColumn(int columnid)
        {
            //return the requried task list
            if (columnid > 2 | columnid < 0)
            {
                log.Error("value is Not in range");
                throw new ArgumentOutOfRangeException("please input value between 0 and 2");
            }
            if (columnid == 0)
            {

                log.Info("return backlog");
                return backLog.Tasks;
            }
            else if (columnid == 1)
            {
                log.Info("return inProgrss");
                return inProgress.Tasks;
            }
            else
            {
                log.Info("return done");
                return done.Tasks;
            }
        }
        public Task GetTask(int taskid)
        {
            if (backLog.ContainTask(taskid))
            {
                log.Info("got task " + taskid + " from backlog");
                return backLog.GetTask(taskid);
            }
            if (inProgress.ContainTask(taskid))
            {
                log.Info("got task " + taskid + " from inprogress");
                return inProgress.GetTask(taskid);
            }
            if (done.ContainTask(taskid))
            {
                log.Info("got task " + taskid + " from done");
                return done.GetTask(taskid);
            }
            log.Error("task not found");
            throw new ArgumentException("Task with given id does not exist in this board");
        }
        private int GetColumnOfTask(Task t)
        {
            //returns which coolumn contains task t
            log.Info("tries to get column of task..");
            if (backLog.ContainTask(t))
                return 0;
            else if (inProgress.ContainTask(t))
                return 1;
            else if (done.ContainTask(t))
                return 2;
            else
                return -1;
        }

        public void ChangeOwner(User mover, User reciever)
        {
            //Function that changes the owner of the board
            log.Info("tries to change owner of the board " + boardName);
            if (members.Contains(reciever.Email) && mover.Email.Equals(owner))
            {
                if (mapper.Update(boardId, "owner", reciever.Email))
                {
                    log.Info("ownership transfered to " + reciever.Email);
                    owner = reciever.Email;
                }
            }
            else
            {
                log.Error("non owner tried to transfer ownership");
                throw new ArgumentException("you need to be the owner of the board to assign this");
            }
        }
        public void AssignTask(User mover, User reciever, int taskId)
        {
            //move task from user to another one
            if (members.Contains(reciever.Email))
            {
                log.Error("assignee isnot member");
                throw new ArgumentException("only members can get task from this board");
            }
            log.Info("try to assign task to" + reciever.Email);
            GetTask(taskId).SetAssignee(mover.Email, reciever.Email);
        }
        public void AddMember(User added)
        {
            //add user to the members list
            if (added == null | members.Contains(added.Email))
                throw new ArgumentException("please input correct input");
            BoardMembersDTO boardMemberDto = new BoardMembersDTO(boardId, added.Email);
            if (memberMapper.Insert(boardMemberDto))
            {
                members.Add(added.Email);
                log.Info(added.Email + " is added to member list of" + boardName);
            }
            else
                log.Error("data update failed");
        }
        public void AddMemberData(User added)//data help function
        {
            //add user to the members list

            members.Add(added.Email);
            
            log.Info(added.Email + " is added to member list of" + boardName);

        }

        public void RemoveMember(User remove)
        {
            //remove user from the members list
            if (remove.Email == Owner)
            {
                log.Warn($"Trying to remove owner, action failed ");
                throw new AccessViolationException("Owner cannot leave board");
            }

            if (members.Contains(remove.Email))
            {
                BoardMembersDTO boardMembersDTO = new BoardMembersDTO(boardId, remove.Email);
                if (memberMapper.Delete(boardMembersDTO))
                {
                    members.Remove(remove.Email);
                    log.Info(remove.Email + " is removed of member list of" + boardName);
                }
            }
            else
                throw new ArgumentException("please input user that inside the members list");
        }
        public void DeleteBoard(User user)
        {
            if (user.Email != Owner)
                throw new Exception("only owner can delete Board");
            foreach(string member in members)
            {
                BoardMembersDTO boardMembersDTO = new BoardMembersDTO(boardId, member);
                memberMapper.Delete(boardMembersDTO);
            }
        }
        public Column GetColumn(int id)
        {
            log.Info("tries to get column " + id);
            if (id < 0 || id > 2)
            {
                log.Warn("id is out of range");
                throw new ArgumentException("id isnt legal");
            }
            if (id == 0)
            {
                log.Info("return backlog");
                return backLog;
            }
            else if (id == 1)
            {
                log.Info("return inProgress");
                return inProgress;
            }
            else
            {
                log.Info("return done");
                return done;
            }
        }
        public int GetLimitOfColumn(int id)
        {
            if (id < 0 || id > 2)
            {
                log.Warn("id is out of range");
                throw new ArgumentException("id is out of range");
            }

            log.Info("returning limit of column " + id);
            Column column = GetColumn(id);
            return column.TaskLimit;
        }
        public string GetColumnName(int id)
        {
            log.Info("tries to return column name of column number " + id);
            if (id < 0 || id > 2)
            {
                log.Warn("id is out of range");
                throw new ArgumentException("id isnt legal");
            }
            if (id == 0)
                return "backLog";
            else if (id == 1)
                return "inProgress";
            else
                return "done";
        }
        public Task AddTask(string title, string description, DateTime dueDate)
        {
            log.Info("tries to create task...");
            Task ts = new Task(title, description, dueDate, freeTaskId, boardId);
            freeTaskId++;
            mapper.Update(boardId, "freeTaskId", freeTaskId);
            backLog.AddTask(ts);
            log.Info("task is added succesfully");
            return ts;
        }
        public void UpdateTaskDueDate(int id, DateTime dt, User ex)
        {
            log.Info("tries to update task due date...");
            Task ts = GetTask(id);
            ts.SetDueDate(dt, ex);
            log.Info("due date is updated successfuly");
        }
        public void UpdateTaskTitle(int id, string title, User ex)
        {
            log.Info("tries to update task title...");
            Task ts = GetTask(id);
            ts.SetTitle(title, ex);
            log.Info("title is updated successfuly");
        }
        public void UpdateDescription(int id, string description, User ex)
        {
            log.Info("tries to update task description...");
            Task ts = GetTask(id);
            ts.SetDescription(description, ex);
            log.Info("description is updated successfuly");
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Board))
                return false;
            Board b = (Board)obj;
            if (b.BoardId == BoardId)
                return true;
            return false;
        }
    }
}
