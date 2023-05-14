using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace IntroSE.Kanban.FrontEnd.Model
{
    public class BoardModel : NotifiableModelObject
    {
        private BackendController controller;
        private long boardId;
        public long BoardId
        {
            get => boardId;
            set
            {
                this.boardId = value;
                RaisePropertyChanged("boardId");
            }
        }
        private string boardName;
        public string BoardName
        {
            get => boardName;
            set
            {
                this.boardName = value;
                RaisePropertyChanged("boardName");
            }
        }
        private string userEmail;
        public string UserEmail
        {
            get => userEmail;
        }

        private ObservableCollection<TaskModel> _backLog;
        public ObservableCollection<TaskModel> BackLog { get => _backLog; set => _backLog = value; }

        private ObservableCollection<TaskModel> _inProgress;
        public ObservableCollection<TaskModel> InProgress { get => _inProgress; set => _inProgress = value; }

        private ObservableCollection<TaskModel> _done;
        public ObservableCollection<TaskModel> Done { get => _done; set => _done = value; }

        public BoardModel(BackendController controller, (long boardId, string boardName, string userEmail) board) : base(controller)
        {
            this.controller = controller;
            BoardId = board.boardId;
            BoardName = board.boardName;
            this.userEmail = board.userEmail;
            this.BackLog = create(userEmail, boardName, 0);
            this.InProgress = create(userEmail, boardName, 1);
            this.Done = create(userEmail, boardName, 2);
            BackLog.CollectionChanged += HandleChange;
            InProgress.CollectionChanged += HandleChange;
            Done.CollectionChanged += HandleChange;
        }

        private ObservableCollection<TaskModel> create(string userEmail, string boardName, int columnOrdinal)
        {
            return new ObservableCollection<TaskModel>(controller.GetAllTasks(userEmail, boardName, columnOrdinal).
                Select((c, i) => new TaskModel(controller, c.Id, c.Title,
                c.Description,
                c.Assignee,
                c.CoulmnOrdinal,
                c.DueDate,
                c.CreationTime)).ToList()); 
        }
        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TaskModel y in e.OldItems)
                {

                    // Controller.RemoveMessage(user.Email, y.Id);                    
                }

            }
        }
    }
}
