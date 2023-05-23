using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardMembersDTO : DTO
    {
        public const string userEmail="MemberEmail";

        private string _userEmail;

        public string UserEmail { get { return _userEmail; } set { _userEmail = value; } }

        public BoardMembersDTO(int id,string memberEmail) : base(new BoardMemberMapper())
        {
            this.Id = id;
            this.UserEmail = memberEmail;
        }

    }
}
