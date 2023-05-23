using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class Column
    {
        private readonly TaskState columnID;
        private List<Task> tasks;
        public readonly string Name;
        private int taskLimit;
        private readonly int boardID;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ColumnMapper columnMapper = new();

        public const int NoTaskLim = -1; //counts as no task limit

        //Constructor:
        public Column(TaskState columnName, int BoardID)
        {
            log.Debug($"trying to create column");
            tasks = new List<Task>();
            Name = columnName.ToString();
            taskLimit = NoTaskLim;
            columnID = columnName;
            boardID = BoardID;
            /*            if (columnMapper.GetColumn(BoardID, GetColumnId(columnName)) == null)*/
            columnMapper.Insert(new ColumnDTO(BoardID, ((short)columnID), NoTaskLim));
            log.Debug($"new column created in column class");
        }

        public Column(TaskState columnName, int taskLimit, int boardID)
        {
            log.Debug($"trying to create column");
            this.tasks = new List<Task>();
            Name = columnName.ToString();
            this.taskLimit = taskLimit;
            columnID = columnName;
            boardID = BoardID;
            log.Debug($"new column created in column class");
        }





        //getters:
        public List<Task> Tasks { get { return tasks; } }
        public string ColumnName { get { return Name; } }
        public int TaskLimit
        {
            get { return taskLimit; }
            set
            {
                log.Debug($"setting new task limit for column");
                if (taskLimit < NoTaskLim)
                {
                    log.Warn("task limit change failed- Illegal value given");
                    throw new ArgumentOutOfRangeException("Limit must be >= -1 (-1 counts as no limit");

                }
                if (taskLimit != NoTaskLim && tasks.Count > value)
                {
                    log.Warn("task limit change failed- more tasks in column than given limit");
                    throw new ArgumentException("More tasks in column than the requested limit - remove tasks to match requested imit and try again");

                }
                this.taskLimit = value;
                log.Debug($"task limit is set to {value}.");
            }
        }
        public int ID { get { return (short)columnID; } }
        public int BoardID { get { return boardID; } }


        //Add Task:
        public void AddTask(Task task)
        {
            log.Debug($"Adding task to column {Name}...");
            if (taskLimit != NoTaskLim && tasks.Count >= taskLimit) //Prohibits adding tasks above tasks limit
            {
                log.Warn("Adding task to column failed.");
                throw new InvalidOperationException("Cannot add tasks above given limit");
            }
            tasks.Add(task);
            log.Debug($"task added to column {Name}.");
        }
        //Remove Task:
        public void RemoveTask(Task task)
        {
            log.Debug($"Removing task from column {Name}...");
            if (!ContainTask(task))
            {
                log.Warn("Removing Task frm column failed");
                throw new InvalidOperationException("Task is not found in requested column");
            }
            tasks.Remove(task);
            log.Debug($"task removed from column {Name}.");
        }

        //Contain Task:
        public bool ContainTask(Task task)
        {
            return tasks.Contains(task);
        }

        public bool ContainTask(int taskID)
        {
            foreach (Task task in tasks)
            {
                if (task.ID == taskID)
                    return true;
            }
            return false;
        }

        //GetTask(By id):
        public Task GetTask(int id)
        {
            log.Debug($"Getting task {id} from column...");
            foreach (Task task in tasks)
            {
                if (task.ID == id)
                {
                    log.Debug("Got task succesfully.");
                    return task;
                }

            }
            log.Error("Failed getting task from column.");
            throw new ArgumentException("Column does not contain requested task.");
        }
        private int GetColumnId(TaskState taskState)
        {
            //help function for database that gives the collumn number by task state
            if (taskState == TaskState.Backlog)
                return 0;
            else if (taskState == TaskState.InProgress)
                return 1;
            else
                return 2;
        }
    }
}
