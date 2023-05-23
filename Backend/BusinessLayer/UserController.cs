using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using IntroSE.Kanban.Backend.DataAccessLayer;
using log4net.Config;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class UserController
    {
        public UserController()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
        }
        private Dictionary<string, string> EmailsAndPasswords = new Dictionary<string, string>();
        public Dictionary<string, User> Users = new Dictionary<string, User>();
        UserMapper mapper = new UserMapper();
        private int freeId = 0;
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Register(string Email, string password)    //add user to the system
        {
            log.Info(Email + " tries to register");
            Email = Email.ToLower();
            if (!IsMailRegistered(Email))
            {
                if (!IsValidPassword(password))
                    throw new ArgumentException("This is not a valid password");
                if (!IsValidMail2(Email))
                    throw new ArgumentException("This is not a valid mail");
                mapper.Insert(new UserDTO(Email, password, freeId + 1));
                Users.Add(Email, new User(Email, this, ++freeId));
                EmailsAndPasswords.Add(Email, password);
                User user = Users[Email];
                user.Login(password);
                log.Info(user.Email + "is logged in");
                log.Info("user is created succesfuly");


            }
            else
            {
                log.Warn(Email + "is already registerd");
                throw new ArgumentException("This email is already registered");
            }
        }
        internal void InsertToDictionries(User us, UserDTO userDTO)
        {
            log.Info("user controler is loading");
            string email = userDTO.Email;
            string password = userDTO.Password;
            Users.Add(email, us);
            EmailsAndPasswords.Add(email, password);
        }


        public void RemoveUser(string Email)
        {  //removing user from the system
            log.Info("removing user " + Email);
            Email = Email.ToLower();
            if (!EmailsAndPasswords.ContainsKey(Email))
            {
                log.Warn("user doesnt exist in the system");
                throw new ArgumentException("No such mail");
            }
            foreach (Board b in GetUser(Email).Boards)
            {
                if (b.Owner != Email)
                    GetUser(Email).LeaveBoard(b);
                else
                    GetUser(Email).DeleteBoard(b);
            }
            if (!mapper.Delete(mapper.GetUser(Email)))
            {
                log.Error("Failed deleting user");
                throw new Exception("Error while deleting user from db");
            }
            Users.Remove(Email);
            EmailsAndPasswords.Remove(Email);
            log.Info(Email + " is removed");
        }


        internal bool CheckForLogin(string email, string password)   //checking in if the user can login, if the password fits the email
        {
            email = email.ToLower();
            log.Info("checking for loging for " + email);
            if (!EmailsAndPasswords.ContainsKey(email))
            {
                log.Warn("user doesnt exist in the system");
                throw new ArgumentException("No such mail");
            }
            return EmailsAndPasswords[email] == password;
        }
        internal void SetNewPassword(string email, string currPassword, string newPassword)  //setting new password 
        {
            log.Info(email + "tries to change password");
            if (!CheckForLogin(email, currPassword))
            {
                log.Warn("wrong password");
                throw new ArgumentException("Wrong Password");
            }
            if (!IsValidPassword(newPassword))
            {
                log.Warn("password isnt valid");
                throw new ArgumentException("A valid password is in the length of 6 to 20 characters and must include at least one uppercase letter, one small character and a number");
            }
            if (mapper.Update(GetUser(email).ID, "password", newPassword))
                log.Info("password has changed");
            else
            {
                log.Error("Failed changed password in db");
                throw new Exception("Failed changing password on db");
            }
            EmailsAndPasswords[email] = newPassword;
        }
        internal void SetNewEmail(string currEmail, string newEmail)   //setting new email for the user
        {
            log.Info(currEmail + " tries to change email...");
            if (!IsValidMail(newEmail))
            {
                log.Warn("new email is not valid");
                throw new ArgumentException("new email is not valid");
            }
            string pass = EmailsAndPasswords[currEmail];
            User user = Users[currEmail];
            EmailsAndPasswords.Remove(currEmail);
            Users.Remove(currEmail);
            EmailsAndPasswords.Add(newEmail, pass);
            Users.Add(newEmail, user);
            log.Info("email is changed to " + newEmail);
        }

        public User GetUser(string email)
        {
            email = email.ToLower();
            log.Info("tries to get user " + email);
            if (!Users.ContainsKey(email))
            {
                log.Warn("user doesnt exist in the system");
                throw new ArgumentException("Email does not exist in the system.");
            }
            log.Info("got user " + email);
            return Users[email];
        }

        private bool IsValidPassword(string password)
        {   //check if the password is valid by definition
            bool valid = true;
            if (password.Length < 6 || password.Length > 20)
                valid = false;
            if (valid && !password.Any(char.IsUpper))
                valid = false;
            if (valid && !password.Any(char.IsLower))
                valid = false;
            if (valid && !password.Any(char.IsDigit))
                valid = false;
            return valid;
        }
        private bool IsValidMail(string email)
        {   //check if the Email is valid by definition
            email = email.ToLower();
            if (string.IsNullOrEmpty(email))
                return false;
            Regex reg1 = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex reg2 = new Regex(@"^\w+([.-]?\w+)@\w+([.-]?\w+)(.\w{2,3})+$");
            Regex reg3 = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            Regex reg4 = new Regex(@"^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-z0-9.-]+$");
            if (!reg1.IsMatch(email) || !reg2.IsMatch(email) || !reg3.IsMatch(email) || !reg4.IsMatch(email))
                return false;
            return true;
        }


        private bool IsValidMail2(string email)
        {   //check if the Email is valid by definition
            if (email == null)
                return false;
            bool validMail = true;
            Regex reg = new Regex(@"^[-!#$%&'+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'+/0-9=?A-Z^_a-z{|}~]){0,63}@[a-zA-Z]((-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+){0,255}$");
            if (!reg.IsMatch(email.ToLower()))
                validMail = false;
            return validMail;
        }

        public bool IsValidMail3(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool IsMailRegistered(string email)
        {   // checks if the mail already registered 
            email = email.ToLower();
            bool ans = true;
            if (!EmailsAndPasswords.ContainsKey(email))
                ans = false;
            return ans;
        }

        public bool IsLoggedIn(string email)
        {
            email = email.ToLower();
            log.Info("checking for loging for " + email);
            if (!Users.ContainsKey(email))
            {
                log.Warn("user doesnt exist in the system");
                throw new ArgumentException("user not found");
            }
            User user = Users[email];
            return user.IsLoggedIn;

        }
        public void LogOut(string Email)
        {
            Email = Email.ToLower();
            User user = GetUser(Email);
            if (user.IsLoggedIn)
                user.Logout();
            else
                throw new Exception("user is log out");
        }
        public List<int> GetUserBoards(string email)
        {
            email = email.ToLower();
            log.Info("try to get user boards");
            List<int> boardsId = new List<int>();
            User user = GetUser(email);
            List<Board> boardsList = user.Boards;
            foreach (Board board in boardsList)
                boardsId.Add(board.BoardId);
            return boardsId;

        }

        public void ClearAll()
        {
            Users = new();
            EmailsAndPasswords = new();
            freeId = 0;
        }

    }
}