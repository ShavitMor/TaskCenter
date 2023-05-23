using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDTO : DTO
    {

        public const string EmailColumnName = "email";
        public const string PasswordColumnName = "password";
        public const string UserIdColomnName = "Id";
        
        private string _email;
        private string _password;   

        public string Email { get { return _email; } set { _email = value; _controller.Update(Id, Email, value); } }
        public string Password { get { return _password; } set { _password = value; _controller.Update(Id, Email, value); } }
        


        public UserDTO(string email, string password ,int userId) : base(new UserMapper())
        {
            this.Id = userId;
            this._email = email;
            this._password = password;
        }
    }


}

