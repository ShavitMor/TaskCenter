using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class BoardModel : NotifiableModelObject
    {

        private string _boardName;
        public string BoardName
        {
            get => _boardName;
            set
            {
                _boardName = value;
                RaisePropertyChanged("BoardName");
            }
        }
        private int _boardId;
        public int BoardId { get=>_boardId; }

        private string _owner;
        public string Owner { get => _owner; set { _owner = value; RaisePropertyChanged("Owner"); } }

        private int _backlogTaskLimit;
        public int BacklogTaskLimit { get => _backlogTaskLimit; set { _backlogTaskLimit = value; RaisePropertyChanged("BacklogTaskLimit"); } }

        private int _inProgressTaskLimit;
        public int InProgressTaskLimit{ get => _inProgressTaskLimit; set { _inProgressTaskLimit = value; RaisePropertyChanged("InProgressTaskLimit"); } }

        private int _doneTaskLimit;
        public int DoneTaskLimit { get => _doneTaskLimit; set { _doneTaskLimit = value; RaisePropertyChanged("DoneTaskLimit"); } }

        private List<TaskModel> _backlog;
        public List<TaskModel> Backlog { get => _backlog; set { _backlog = value; RaisePropertyChanged("Backlog"); } }

        private List<TaskModel> _inProgress;
        public List<TaskModel> InProgress { get => _inProgress; set { _inProgress = value; RaisePropertyChanged("InProgress"); } }
        private List<TaskModel> _done;
        public List<TaskModel> Done { get => _done; set { _done = value; RaisePropertyChanged("Backlog"); } }

        public BoardModel(BackendController controller, string boardName, int boardId, string owner, int backlogLimit,int inProgressLimit, int doneLimit ) : base(controller)
        {
            _boardName = boardName;
            _boardId = boardId;
            _owner = owner;
            _backlogTaskLimit = backlogLimit;
            _inProgressTaskLimit = inProgressLimit;
            _doneTaskLimit = doneLimit;
        }
    }
}
