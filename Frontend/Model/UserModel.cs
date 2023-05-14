using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;

namespace IntroSE.Kanban.FrontEnd.Model
{



    public class UserModel : NotifiableModelObject
    {

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }

        private ObservableCollection<BoardModel> _boards;
        public ObservableCollection<BoardModel> Boards
        {
            get => _boards;
            set
            {
                _boards = value;
            }
        }

       


        public UserModel(BackendController controller, string email) : base(controller)
        {
            this.Email = email;
            Boards = new ObservableCollection<BoardModel>(controller.GetAllBoardsIds(email).
                Select((c, i) => new BoardModel(controller, controller.GetBoard(email, (long)i+1))).ToList());
        }
    }
}