using System;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{

    public class Task
    {
        readonly private int taskId;
        readonly private DateTime creationDate;
        private string taskTitle;
        private string taskDescription;
        private DateTime dueDate;
        private TaskState state;
        private string assignee;
        private int boardID;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static TaskMapper taskMapper = new TaskMapper();

        //Constructor:
        public Task(string title, string description, DateTime dueDate, int taskID, int boardID)
        {
            log.Debug("Trying to create a new task...");
            if (!(IsLegalTitle(title))) //check for a legal title
                throw new ArgumentException("Title must not be empty, or longer than 50 chars");
            if (description.Length > 200) //checks for a legal description
                throw new ArgumentException("Description cannot be longer than 200 chars");
            this.taskTitle = title;
            this.dueDate = dueDate;
            this.taskDescription = description;
            creationDate = DateTime.Now; //sets today's date as creation date
            state = TaskState.Backlog;
            taskId = taskID;
            this.boardID = boardID;
            assignee = null;    //sets no assignee as default
            taskMapper.Insert(new TaskDTO(boardID, taskId, dueDate.ToString(), creationDate.ToString(), title, description, 0, null));
            log.Debug("New task created in task class");

        }


        public Task(TaskDTO task, string assignee)
        {
            taskId = task.TaskID;
            creationDate = task.CreationDate;
            dueDate = task.DueDate;
            taskTitle = task.Title;
            taskDescription = task.Description;
            state = (TaskState)task.TaskState;
            boardID = task.BoardID;
            if (assignee != null && assignee != task.Assignee)
                throw new ArgumentException("User  must be same as Assignee");
            this.assignee = assignee;


        }

        //Getters:

        public int ID { get { return taskId; } }
        public DateTime CreationDate { get { return creationDate; } }
        public DateTime DueDate { get { return dueDate; } }
        public string Title { get { return taskTitle; } }
        public string Description { get { return taskDescription; } }
        public int State { get { return (int)state; } }
        public string Assignee { get { return assignee; } }
        public int BoardID { get { return boardID; } }


        //Setters:
        public void SetTitle(string newTitle, User executor)
        {
            log.Debug("Trying to change title of a task");
            if (!IsExecutorCanChange(executor))
            { //check if the user perform the change is th assignee
                log.Warn("non assignee performed action-changing title failed.");
                throw new ArgumentException("Only the asignee can modify this task ");
            }
            IsDoneException(); //Prohibits changing tasks marked as done.
            if (!(IsLegalTitle(newTitle)))
            {  //check for a legal title
                log.Warn("Illegal title-changing title failed");
                throw new ArgumentException("Title must not be empty, or longer than 50 chars");
            }
            if (taskMapper.ChangetTitle(BoardID, taskId, newTitle))
            {
                this.taskTitle = newTitle;
                log.Debug("Task title changed succesfully in task class");
            }
            else
                log.Error("not updated in data");
        }
        public void SetDescription(string newDescription, User executor)
        {
            log.Debug("Trying to change task's description");
            if (!IsExecutorCanChange(executor))     //check if the user perform the change is th assignee
            {
                log.Warn("non assignee performed action-changing description failed.");
                throw new ArgumentException("Only the asignee can modify this task ");
            }
            IsDoneException(); //Prohibits changing tasks marked as done.

            if (newDescription.Length > 200)
            {
                log.Warn("Illegal description - changing description failed.");
                throw new ArgumentException("Description cannot be longer than 200 chars");
            }
            this.taskDescription = newDescription;
            taskMapper.ChangetDescription(BoardID, ID, newDescription);
            log.Debug("Task description changed succesfully in task class");
        }
        public void SetDueDate(DateTime newDueDate, User executor)
        {
            if (!IsExecutorCanChange(executor))//check if the user perform the change is th assignee
            {
                log.Warn("non assignee performed action-changing description failed.");
                throw new ArgumentException("Only the asignee can modify this task ");
            }
            IsDoneException(); //Prohibits changing tasks marked as done.
            this.dueDate = newDueDate;
            taskMapper.ChangetDueDate(BoardID, ID, newDueDate);
            log.Debug("Task dueDate changed succesfully in task class");
        }

        public void SetAssignee(string executor, string newAss = null)
        {
            log.Debug("Trying to change task's assignee");
            if (assignee == null || assignee.Equals(executor))
            {
                assignee = newAss;
                taskMapper.ChangeAssignee(BoardID, ID, newAss);
                log.Debug("assignee changed succesfully");
            }

            else
            {
                log.Warn("Assignee changing failed");
                throw new ArgumentException("Only the asignee can modify this task ");
            }

        }


        //Next State:
        public void NextState() //changes task.state to next state: 
        {
            log.Debug("Trying to change task state");
            IsDoneException(); //Prohibits changing tasks marked as done.
            switch (state)
            {
                case TaskState.Backlog:
                    state = TaskState.InProgress;
                    taskMapper.ChangetState(BoardID, ID, (int)state);
                    log.Debug("task state changed from BACKLOG to INPROGRESS ");
                    break;
                case TaskState.InProgress:
                    state = TaskState.Done;
                    taskMapper.ChangetState(BoardID, ID, (int)state);
                    log.Debug("task state changed from INPROGRESS to DONE ");
                    break;
            }
        }

        //Auxialiary functions:
        private static bool IsLegalTitle(string s) //Check wether title is legal
        {
            if (String.IsNullOrEmpty(s) || s.Length > 50)
                return false;
            return true;
        }

        private bool IsExecutorCanChange(User exec)
        {
            if (assignee != null && !exec.Email.Equals(assignee))
                //check if the user perform the change is th assignee
                return false;
            return true;
        }

        private void IsDoneException() //Prohibits changing tasks marked as done.
        {
            if (state == TaskState.Done)
            {
                log.Warn("Trying to modify completed task - action failed");
                throw new InvalidOperationException("Cannot modify completed task.");
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Task))
                return false;
            Task task = (Task)obj;
            if (ID == task.taskId && BoardID == task.BoardID)
                return true;
            return false;
        }
    }
}
