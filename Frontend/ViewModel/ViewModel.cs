using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    public abstract class ViewModel : NotifiableObject
    {
        public static Model.BackendController BackendCon { get; set; }
        private string message;
        public string Message { get { return message; } set { message = value; RaisePropertyChanged("Message"); } }
    }
}
