using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class TaskModel : NotifiableModelObject
    {
        private int _taskId;
        public int TaskId { get => _taskId; }

        private string _title;
        public string Title { get => _title; set { _title = value; RaisePropertyChanged("Title"); } }

        private string _description;
        public string Description { get => _description; set { _description = value; RaisePropertyChanged("Description"); } }

        private DateTime _creationDate;
        public DateTime CreationDate { get => _creationDate; }

        private DateTime _dueDate;
        public DateTime DueDate { get => _dueDate; set { _dueDate = value; RaisePropertyChanged("DueDate"); } }

        private string _assignee;
        public string Asignnee { get => _assignee; set { _assignee = value; RaisePropertyChanged("Assignee"); } }

        public TaskModel(BackendController controller, int taskId, string title, string description, DateTime CreationDate, DateTime dueDate, string assignee ) : base(controller)
        {
            _taskId = taskId;
            _title = title;
            _description = description;
            _creationDate = CreationDate;
            _dueDate = dueDate;
            _assignee = assignee;
        }
        override
            public string ToString()
        {
            return _taskId + ": " + _title;
        }
    }
}
