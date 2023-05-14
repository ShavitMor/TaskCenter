using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests
{
    internal class AddMoveTask
    {
        private readonly BoardService boardService;
        private readonly UserService userService;
        private readonly TaskService taskService;

        public AddMoveTask(BoardService boardService, UserService userService,TaskService taskService)
        {
            this.boardService = boardService;
            this.userService = userService;
            this.taskService= taskService;

        }
        public void RunTests()
        {
            /// <summary>
            /// This function tests Requirement 12
            /// 12. The board will support adding new tasks to its backlog column only.
            /// expect: Successes.
            /// public string AddTask(string email, string boardName, string title, string description, DateTime dueDate);
            /// </summary>

            userService.Register("sami@gmail.com", "Zz123456");
            userService.Login("sami@gmail.com", "Zz123456");
            boardService.AddBoard("sami@gmail.com", "Luah");
            string bdika1 = taskService.AddTask("sami@gmail.com", "Luah", "koteret", "tioor", new DateTime(1987, 8, 7));
            Dictionary<string, string> ans = JsonSerializer.Deserialize<Dictionary<string, string>>(bdika1);

            if (ans["ErrorMessage"] == null)
            {
                Console.WriteLine("Task Added successfully to backlog column");
            }
            else
                Console.WriteLine(ans["ErrorMassage"]);




            /// <summary>
            /// This function tests Requirement 13
            /// 13. Tasks can be moved from ‘backlog’ to ‘in progress’ or from ‘in progress’ to ‘done’
            ///      columns.No other movements are allowed.
            /// expect: Successes.
            /// public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId);
            /// </summary>
            
            userService.Register("hatuka@gmail.com", "Zz123456");
            userService.Login("hatuka@gmail.com", "Zz123456");
            boardService.AddBoard("hatuka@gmail.com", "Luah");
            taskService.AddTask("hatuka@gmail.com", "Luah", "koteret", "tioor", new DateTime(1987, 8, 7));
            int id = 99;
            userService.AssignTask("hatuka@gmail.com", "Luah", 0, id, "hatuka@gmail.com");
            string bdika2 = boardService.AdvanceTask("hatuka@gmail.com", "Luah", 0, id);
            Dictionary<string, string> ans2 = JsonSerializer.Deserialize<Dictionary<string, string>>(bdika2);

            if (ans["ErrorMessage"] == null)
            {
                Console.WriteLine("Task advanced successfully to the next column");
            }
            else
                Console.WriteLine(ans2["ErrorMassage"]);



        }
    }
}
