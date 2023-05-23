using System;
using System.Collections.Generic;
/*using IntroSE.Kanban.Backend.ServiceLayer.ServiceClasses;*/
using IntroSE.Kanban.Backend.BusinessLayer;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        Response response;
        private static UserController UserCon;

        Newtonsoft.Json.JsonSerializer serializer;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UserService(UserController Uc)
        {
            UserService.UserCon = Uc;
        }

        internal UserController UC { get; set; }

        //User controller actions:
        public string Register(string email, string password)   //Registers new user
        {
            log.Info("Registering new user...");
            try
            {
                if (email[0] == '"' && email[email.Length - 1] == '"')
                {  //checking if string is Json string
                    email = JsonSerializer.Deserialize<string>(email); //converting from Json string
                    password = JsonSerializer.Deserialize<string>(password);
                }
                UserCon.Register(email, password);
                log.Info(email + " Registered as new user");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string RemoveUser(string email)      //Removes existing user
        {
            log.Info($"Removing user...");
            if (email[0] == '"' && email[email.Length - 1] == '"')
            {  //checking if string is Json string
                email = JsonSerializer.Deserialize<string>(email); //converting from Json string
            }
            try
            {
                UserCon.RemoveUser(email);
                log.Info($"User {email} removed from system.");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetUser(string email)      //Returns existing user
        {
            log.Info($"Getting user...");
            if (email[0] == '"' && email[email.Length - 1] == '"')
            {  //checking if string is Json string
                email = JsonSerializer.Deserialize<string>(email); //converting from Json string
            }
            try
            {
                User user = UserCon.GetUser(email);
                log.Info($"Got user {email}.");
                return JsonSerializer.Serialize(Response.ResponseValue(user));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetIsLoggedIn(string email)      //Returns wether user is logged in
        {
            log.Info($"Getting user is logged...");
            if (email[0] == '"' && email[email.Length - 1] == '"')
            {  //checking if string is Json string
                email = JsonSerializer.Deserialize<string>(email); //converting from Json string
            }
            try
            {
                bool logged = UserCon.IsLoggedIn(email);
                log.Info($"User {email} is logged : {logged}");
                return JsonSerializer.Serialize(Response.ResponseValue(logged));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        //User actions:

        public string SetMail(string newMail, User user)      //Changes user mail
        {
            log.Info($"Changing user mail...");
            try
            {
                user.Email = newMail;
                log.Info($"User changed mail to {newMail}");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetMail(string newMail, string jUser)      //Changes user mail
        {
            log.Info($"Changing user mail...");
            User user = JsonSerializer.Deserialize<User>(jUser); //converting from Json string
            newMail = JsonSerializer.Deserialize<string>(newMail); //converting from Json string
            try
            {
                user.Email = newMail;
                log.Info($"User changed mail to {newMail}");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string GetMail(User user)      //Changes user mail
        {
            log.Info($"Getiing user mail...");
            try
            {
                string mail = user.Email;
                log.Info($"Got user {mail} mail.");
                return JsonSerializer.Serialize(Response.ResponseValue(mail));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetMail(string jUser)      //Changes user mail
        {
            log.Info($"Getiing user mail...");
            User user = JsonSerializer.Deserialize<User>(jUser); //converting from Json string
            try
            {
                string mail = user.Email;
                log.Info($"Got user {mail} mail.");
                return JsonSerializer.Serialize(Response.ResponseValue(mail));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }


        public string GetBoards(User user)      //Gets user's Board
        {
            log.Info($"Getiing user Boards...");
            try
            {
                List<Board> boards = user.Boards;
                log.Info($"Got user {user.Email} Boards.");
                return JsonSerializer.Serialize(Response.ResponseValue(boards));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoards(string jUser)      //Gets user's Board
        {
            log.Info($"Getiing user Boards...");
            User user = JsonSerializer.Deserialize<User>(jUser); //converting from Json string
            try
            {
                string mail = user.Email;
                log.Info($"Got user {user.Email} Boards.");
                return JsonSerializer.Serialize(Response.ResponseValue(mail));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoardNames(string email)   //Registers new user
        {
            log.Info($"Getting {email}'s board names...");
            try
            {
                if (email[0] == '"' && email[email.Length - 1] == '"')
                {  //checking if string is Json string
                    email = JsonSerializer.Deserialize<string>(email); //converting from Json string
                }
                List<string> boards = UserCon.GetUser(email).GetBoardNames();
                log.Info($"Got {email}'s board names.");
                return JsonSerializer.Serialize(Response.ResponseValue(boards));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string IsLoggd(User user)      //Gets user's is logged
        {
            log.Info($"Checking is user logged...");
            try
            {
                bool logged = user.IsLoggedIn;
                log.Info($"{user.Email} is loggd: {logged}");
                return JsonSerializer.Serialize(Response.ResponseValue(logged));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string IsLogged(string jUser)      //Gets USer is logged
        {
            log.Info($"Checking is user logged...");
            User user = JsonSerializer.Deserialize<User>(jUser); //converting from Json string
            try
            {
                bool logged = user.IsLoggedIn;
                log.Info($"{user.Email} is loggd: {logged}");
                return JsonSerializer.Serialize(Response.ResponseValue(logged));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetId(User user)      //Gets user id
        {
            log.Info($"Getting user id...");
            try
            {
                int ans = user.ID;
                log.Info($"{user.Email} id is: {ans}");
                return JsonSerializer.Serialize(Response.ResponseValue(ans));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetId(string jUser)      //Gets users id
        {
            log.Info($"Getting user id...");
            User user = JsonSerializer.Deserialize<User>(jUser); //converting from Json string
            try
            {
                int ans = user.ID;
                log.Info($"{user.Email} id is: {ans}");
                return JsonSerializer.Serialize(Response.ResponseValue(ans));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string SetPassword(User user, string oldPass, string newPass)      //Changes user password
        {
            log.Info($"Changing password");
            try
            {
                user.SetPassword(oldPass, newPass);
                log.Info($"{user.Email} changed passwords");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string SetPassword(string jUser, string oldPass, string newPass)      //Changes user mail
        {
            log.Info($"Changing password");
            User user = JsonSerializer.Deserialize<User>(jUser); //converting from Json string
            newPass = JsonSerializer.Deserialize<string>(newPass); //converting from Json string
            oldPass = JsonSerializer.Deserialize<string>(oldPass); //converting from Json string

            try
            {
                user.SetPassword(oldPass, newPass);
                log.Info($"{user.Email} changed passwords");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public static string Login(string Email, string password)      //Logs user in
        {
            log.Info($"logging in...");
            if (Email[0] == '"' && Email[Email.Length - 1] == '"')
            {  //checking if string is Json string
                Email = JsonSerializer.Deserialize<string>(Email); //converting from Json string
                password = JsonSerializer.Deserialize<string>(password); //converting from Json string
            }
            try
            {
                UserCon.GetUser(Email).Login(password);
                log.Info($"{Email} logged in");
                return JsonSerializer.Serialize(Response.ResponseValue(Email));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string LogOut(User user)      //Logs Out
        {
            log.Info($"Logging out user");
            try
            {
                user.Logout();
                log.Info($"{user.Email} logged out");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string LogOut(string Email)      //logs out users
        {
            log.Info($"Logging user out");
            try
            {
                if (Email[0] == '"' && Email[Email.Length - 1] == '"')
                    Email = JsonSerializer.Deserialize<string>(Email);//converting from Json string
                UserCon.LogOut(Email);
                log.Info($"{Email} logged out");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }


        public string GetInProgress(string email)      //gets in progress tasks of the user
        {
            if (email[0] == '"' && email[email.Length - 1] == '"')
                email = JsonSerializer.Deserialize<string>(email);//converting from Json string
            try
            {
                List<Task> ans = UserCon.GetUser(email).GetInProgress();
                log.Info($"Got {email} in progress tasks. ");
                return JsonSerializer.Serialize(Response.ResponseValue(ans));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoard(User user, string boardName)      //gets specific board
        {
            log.Info($"Getting Board...");
            try
            {
                Board ans = user.GetBoard(boardName);
                log.Info($"Got {user.Email} board {boardName} ");
                return JsonSerializer.Serialize(Response.ResponseValue(ans));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string GetBoard(string jUser, string boardName)      //gets specific board
        {
            log.Info($"Getting board...");
            boardName = JsonSerializer.Deserialize<string>(boardName); //converting from Json string
            User user = JsonSerializer.Deserialize<User>(jUser); //converting from Json string
            try
            {
                Board ans = user.GetBoard(boardName);
                log.Info($"Got {user.Email} board {boardName} ");
                return JsonSerializer.Serialize(Response.ResponseValue(ans));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
        public string GetUserBoards(string Email)
        {
            log.Info($"Getting boards...");
            if (Email[0] == '"' && Email[Email.Length - 1] == '"')
                Email = JsonSerializer.Deserialize<string>(Email);//converting from Json string
            try
            {
                List<int> ids = UserCon.GetUserBoards(Email);
                log.Info($"return boards ids...");
                return JsonSerializer.Serialize(Response.ResponseValue(ids));
            }
            catch (Exception ex)
            {
                log.Info($"failed to get boards ids...");
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));

            }
        }
    }

}
