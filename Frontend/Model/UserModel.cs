using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Frontend.Model
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
        private List<string> _boardNames;
        public List<string> BoardNames { get => _boardNames; set { _boardNames = value;  } }

        public UserModel(BackendController controller, string email, List<string>? boards = null) : base(controller)
        {
            this.Email = email;
            this.BoardNames = boards ?? new List<string>();
        }
    }
}
