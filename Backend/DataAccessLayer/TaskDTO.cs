using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDTO : DTO
    {
        public const string BoardIdCol = "Related_Board_Id";
        public const string CreatDatCol = "Creation_Date";
        public const string DueDateCol = "Due_Date";
        public const string TitleCol  = "Title";
        public const string DescCol = "Description";
        public const string TaskStatCol = "Task_State";
        public const string AssigneCol = "Assignee";

        private int _boardId;
        private string _dueDate;
        private string _creationTime;
        private string _title;
        private string _description;
        private int _taskState;
        private string _assignee;

        public int BoardID { get { return _boardId; } }

        public int TaskID { get { return Id; } }
        public DateTime DueDate { 
            get { return DateTime.Parse(_dueDate); } 
            set { _dueDate = value.ToString(); _controller.Update(Id,BoardIdCol,BoardID ,DueDateCol, value.ToString()); }
        }
        public DateTime CreationDate { get { return DateTime.Parse(_creationTime); } }
        public string Title { 
            get { return _title; }
            set { _title = value; _controller.Update(Id, BoardIdCol, BoardID, TitleCol, value); }
        }
        public string Description {
            get { return _description; }
            set { _description = value; _controller.Update(Id, BoardIdCol, BoardID, DescCol, value); }
        }
        public int TaskState {
            get { return _taskState; }
            set { _taskState = value; _controller.Update(Id, BoardIdCol, BoardID, TaskStatCol, value); } 
        }
        public string Assignee {
            get { return _assignee; }
            set { _assignee = value; _controller.Update(Id, BoardIdCol, BoardID, AssigneCol, value); }
        }
        public TaskDTO(int boardId, int taskID, string dueDate, string creationTime, string title,string description, int taskState, string assignee) : base(new TaskMapper())
        {
            Id = taskID;
            _boardId = boardId;
            _dueDate = dueDate;
            _creationTime = creationTime;
            _title = title;
            _description = description;
            _taskState = taskState;
            _assignee = assignee;

        }
    }
}
