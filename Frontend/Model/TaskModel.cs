
using System;

namespace IntroSE.Kanban.FrontEnd.Model
{
    public class TaskModel : NotifiableModelObject
    {
        private int taskId;
        public int TaskId
        {
            get => taskId;
            set => taskId = value;
        }
        private string taskTitle;
        public string TaskTitle
        {
            get => taskTitle;
            set => taskTitle = value;
        }
        private string taskDescription;
        public string TaskDescription
        {
            get => taskDescription;
            set => taskDescription = value;
        }
        private string taskAssignee;
        public string TaskAssignee
        {
            get => taskAssignee;
            set => taskAssignee = value;
        }
        private int columnOrdinal;
        public int ColumnOrdinal
        {
            get => columnOrdinal;
            set => columnOrdinal = value;
        }
        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set => dueDate = value;
        }
        private DateTime creationDate;
        public DateTime CreationDate
        {
            get => creationDate;
                set => creationDate = value;
        }
        public TaskModel(BackendController controller, int taskId, string taskTitle, string taskDescription, string taskAssignee, int columnOrdinal, DateTime dueDate, DateTime creationDate) : base(controller)
        {
            this.taskId = taskId;
            this.taskTitle = taskTitle;
            this.taskDescription = taskDescription;
            this.taskAssignee = taskAssignee;
            this.columnOrdinal = columnOrdinal;
            this.dueDate = dueDate;
            this.creationDate = creationDate;
        }
    }
}
