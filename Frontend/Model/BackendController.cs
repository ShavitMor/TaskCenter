using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend;
using IntroSE.Kanban.Backend.BusinessLayer;
using Frontend.ViewModel;
using Newtonsoft.Json;

namespace IntroSE.Kanban.FrontEnd.Model
{
    public class BackendController
    {
        
        private ServiceFactory Service { get; set; }
        public BackendController(ServiceFactory service)
        {
            this.Service = service;
        }

        public BackendController()
        {
            this.Service = new ServiceFactory();
            Service.LoadData();
        }

        public UserModel Login(string username, string password)
        {
            string user = Service.Login(username, password);
            Dictionary<string, string> userResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(user)!;
            if (userResponse.ContainsKey("ErrorMessage"))
            {
                throw new Exception(userResponse["ErrorMessage"]);
            }
            return new UserModel(this, username);
        }
        internal void Register(string username, string password)
        {
            string user = Service.Register(username, password);
            Dictionary<string, string> userResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(user)!;
            if (userResponse.ContainsKey("ErrorMessage"))
            {
                throw new Exception(userResponse["ErrorMessage"]);
            }
            Service.Logout(username);

        }

        internal List<Task> GetAllTasks(string email, string boardName, int columnOrdinal)
        {
            string toDeserialize = Service.GetColumn(email, boardName, columnOrdinal);
            // columns 1,2 later

            Response<List<Task>> response = JsonConvert.DeserializeObject<Response<List<Task>>>(toDeserialize);
            if (response.ErrorMessage != null)
            {
                throw new Exception(response.ErrorMessage);
            }
            return response.ReturnValue;


        }

        internal List<long> GetAllBoardsIds(string email)
        {
            string toDeserialize = Service.GetUserBoards(email);

            Response<List<long>> response = JsonConvert.DeserializeObject<Response<List<long>>>(toDeserialize);

       
            if (response.ErrorMessage!=null)
            {
                throw new Exception(response.ErrorMessage);
            }            
            return response.ReturnValue;      
        }

        internal (long id, string name, string email) GetBoard(string userEmail, long boardId)
        {
            Board board = Service.getBoard(userEmail, boardId);
            return (boardId, board.BoardName, userEmail);
        }

        internal Task GetTask(string userEmail, long boardId, int taskId)
        {
            return Service.getTask(userEmail, boardId, taskId);
        }

        internal void addBoard(String email, string boardName)
        {
            string user = Service.AddBoard(email, boardName);
            Dictionary<string, string> userResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(user)!;
            if (userResponse.ContainsKey("ErrorMessage"))
            {
                throw new Exception(userResponse["ErrorMessage"]);
            }
            

        }
    }
}
