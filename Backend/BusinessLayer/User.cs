using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;



namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class User
    {
        private static UserController UC;
        private Dictionary<int, Board> boards;
        private bool isLogged;
        private string email;
        private int userId;

        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static UserMapper usMapper = new UserMapper();
        private static TaskMapper tastkMapper = new TaskMapper();
        private static BoardMapper boardMapper = new BoardMapper();
        private static BoardMemberMapper boardMemberMapper = new BoardMemberMapper();
        public User(string email, UserController UC, int id)
        {
            log.Info("creates new user " + email);
            boards = new Dictionary<int, Board>();
            isLogged = false;
            this.email = email.ToLower();
            User.UC = UC;
            this.userId = id;
        }
        public User(string email, int id)
        {
            log.Info("creates new user " + email);
            boards = new Dictionary<int, Board>();
            isLogged = false;
            this.email = email;
            this.userId = id;
        }
        public User(UserDTO userDTO, UserController uc)
        {
            log.Info("Loading user " + email + " from DB");
            this.email = userDTO.Email;
            isLogged = false;
            boards = new Dictionary<int, Board>();
            UC = uc;
            log.Info("User " + email + " loaded successfully");
        }
        public User(UserDTO userDTO)
        {
            log.Info("Loading user " + email + " from DB");
            this.email = userDTO.Email;
            isLogged = false;
            boards = new Dictionary<int, Board>();
            log.Info("User " + email + " loaded successfully");
        }

        internal static UserController UserCon { get { return UC; } set { UC = value; } }


        public string Email
        {
            get { return email; }
            set
            {
                if (!isLogged)
                {
                    log.Warn(email + "isnt log in");
                    throw new ArgumentException("user is not logged in");
                }
                if (usMapper.Update(ID, "email", value))
                {
                    UC.SetNewEmail(this.email, value);
                    this.email = value;
                }
            }
        }
        public List<Board> Boards
        {
            get
            {
                log.Info(email + " tries to get boards");
                if (!isLogged)
                {
                    log.Warn(email + "isnt logged in");
                    throw new ArgumentException("user is not logged in");
                }
                List<Board> boardslist = new List<Board>();
                foreach (Board board in boards.Values)
                {
                    boardslist.Capacity++;
                    boardslist.Add(board);
                }
                log.Info(email + " got his/her boards");
                return boardslist;
            }
        }

        public List<string> GetBoardNames()
        {
            List<string> boardnames = new List<string>();
            foreach (Board b in Boards)
                boardnames.Add(b.Name);
            return boardnames;
        }

        public int ID
        {
            get { return userId; }
        }

        public bool IsLoggedIn
        {
            get { return isLogged; }
        }

        public void SetPassword(string currentPass, string newPass)
        {
            //function that changes password
            log.Info(email + " tries to change his/her password");
            if (!isLogged)
            {
                log.Warn(email + "isnt logged in");
                throw new ArgumentException("user is not logged in");
            }
            UC.SetNewPassword(Email, currentPass, newPass);
        }
        public void Login(string password)
        {  //login the user
            log.Info(Email + " tries to log in");
            if (!UC.CheckForLogin(Email, password))//checks if account exist in the system
            {
                log.Error("incorrect email or password");
                throw new ArgumentException("invalid mail or password");
            }
            if (isLogged)
            {
                log.Error("user already logged in");
                throw new ArgumentException("user already logged in");
            }
            isLogged = true;
        }

        public void Logout()
        {  //logout the user
            log.Info(email + " tries to logout");
            if (!isLogged)
            {
                log.Warn(email + "isnt logged in");
                throw new ArgumentException("user is not logged in");
            }
            log.Info(email + " logged out.");
            isLogged = false;
        }

        public List<Task> GetInProgress()
        {    //get the list of all task that in progress
            log.Info(email + " tries to get in progress tasks");
            if (!isLogged)
            {
                log.Warn(email + "isnt logged in");
                throw new ArgumentException("user is not logged in");
            }

            List<Task> list = new List<Task>();
            foreach (Board board in boards.Values)
            {

                Column c = board.InProgress;
                List<Task> templist = c.Tasks;
                foreach (Task t in templist)
                {
                    if (t.Assignee!=null&&t.Assignee.Equals(this.Email))
                    {
                        list.Capacity++;
                        list.Add(t);
                    }
                }
            }
            log.Info(email + " got in progress tasks");
            return list;
        }

        public Board GetBoard(string boardName)   //getting board by board name
        {
            log.Info(email + "tries to reach board " + boardName);
            if (!isLogged)
            {
                log.Warn(email + "isnt logged in");
                throw new ArgumentException("user is not logged in");
            }
            foreach (int i in boards.Keys)
            {
                if (boards[i].Name == boardName) {
                    log.Info("got board " + boardName);
                    return boards[i];
                }
            }
            log.Error("board doesnt exist");
            throw new ArgumentException("board name doesnt exist");
        }

        internal void JoinBoard(Board board)   //Joining to a board
        {
            if (HasSameBoardName(board.Name))
                throw new ArgumentException("Cannot join board - already have board with same name");
            log.Info(email + " tries to join board " + board.Name);
            if (!isLogged)
            {
                log.Warn(email + "isnt logged in");
                throw new ArgumentException("user is not logged in");
            }
            BoardMembersDTO boardMembersDTO = new BoardMembersDTO(board.BoardId, email);
            if (boardMemberMapper.Insert(boardMembersDTO))
            {
                board.AddMember(this);
                boards.Add(board.BoardId, board);
                log.Info(email + " joined board " + board.Name);
            }
            else
                log.Error("failed to insert to data");
        }


        internal void LeaveBoard(Board b)  //Lets this user leave board 
        {
            log.Info(email + " tries to leave board " + b.Name);
            if (!isLogged)
            {
                log.Warn(email + "isnt logged in");
                throw new ArgumentException("user is not logged in");
            }
            BoardMembersDTO boardMembersDTO = new BoardMembersDTO(b.BoardId, email);
            if (boardMemberMapper.Delete(boardMembersDTO))
            {
                b.RemoveMember(this);
                log.Info(email + " left board " + b.Name);
                boards.Remove(b.BoardId);
            }
        }

        internal void DeleteBoard(Board b)  //Lets this user leave board 
        {
            log.Info(email + " tries to Delete board " + b.Name);
            if (!isLogged)
            {
                log.Warn(email + "isnt logged in");
                throw new ArgumentException("user is not logged in");
            }
            b.DeleteBoard(this);
            boards.Remove(b.BoardId);
            foreach (string memberEmail in b.Members)
            {
                User member = UC.GetUser(memberEmail);
                BoardMembersDTO boardMembersDTO = new BoardMembersDTO(b.BoardId, member.Email);
                log.Info($"Board {b.Name} removed");
                member.boards.Remove(b.BoardId);
            }
        }

        internal void NewBoard(Board b)
        {
            boards.Add(b.BoardId, b);

        }


        internal bool HasSameBoardName(string boardName) //checks if a User already has a board with the same name
        {
            foreach (Board b in boards.Values)
            {
                if (b.Name.Equals(boardName))
                    return true;
            }
            return false;
        }
        public void AddBoardForData(Board board)
        {
            if (HasSameBoardName(board.Name))
                throw new ArgumentException("Cannot join board - already have board with same name");

            boards.Add(board.BoardId, board);
            log.Info(email + " joined board " + board.Name);

        }
    }
}
