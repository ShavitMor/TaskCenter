using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests.BoardTests
{
    internal class ownershipAssignment
    {
        private readonly BoardService boardService;
        private readonly UserService userService;
        private readonly TaskService taskService;
        public ownershipAssignment(BoardService boardService, UserService userService)
        {
            this.boardService = boardService;
            this.userService = userService;
        }

        public void RunTests()
        {
            String regitrationTest1 = userService.Register("roni@gmail.com", "1234");
            var regitrationTestValues1 = JsonSerializer.Deserialize<Dictionary<string, string>>(regitrationTest1);

            String loginTest1 = userService.Login("rona@gmail.com", "12345");
            var loginTestValues1 = JsonSerializer.Deserialize<Dictionary<string, string>>(loginTest1);

            String regitrationTest2 = userService.Register("roni@gmail.com", "1234");
            var regitrationTestValues2 = JsonSerializer.Deserialize<Dictionary<string, string>>(regitrationTest2);

            String loginTest2 = userService.Login("rona@gmail.com", "12345");
            var loginTestValues2 = JsonSerializer.Deserialize<Dictionary<string, string>>(loginTest2);

            String addBoardTest = boardService.AddBoard("roni@gmail.com", "someBoard");
            var addBoardTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(addBoardTest);

            String addTaskTest = taskService.AddTask("roni@gmail.com", "someBoard", "title", "description", new DateTime(2002, 7, 2));
            var addTaskTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(addTaskTest);

            if (!regitrationTestValues1.ContainsKey("ErrorMessage") &
                !loginTestValues1.ContainsKey("ErrorMessage") &
                !regitrationTestValues2.ContainsKey("ErrorMessage") &
                !loginTestValues2.ContainsKey("ErrorMessage") &
                !addBoardTestValues.ContainsKey("ErrorMessage") &
                !addTaskTestValues.ContainsKey("ErrorMessage"))
            {
                /// <summary>
                /// This function tests Requirement 13
                /// 13.A board owner can transfer the board ownership to another board member(a user who joined the board).
                /// expect: Succsess.
                /// public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName);
                /// </summary>
                String transferOwnershipTest = userService.TransferOwnership("roni@gmail.com", "rona@gmail.com", "someBoard");
                var transferOwnershipTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(transferOwnershipTest);
                if (transferOwnershipTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(transferOwnershipTestValues["ErrorMessage"]);
                }
                else
                {
                    Console.WriteLine("Board ownership was transfered successfully");
                }

                /// <summary>
                /// This function tests Requirement 23
                /// 23. An unassigned task can be assigned by any board member to any board member. By
                /// default, a task is unassigned.The assignment of an assigned task can be changed only by
                /// its assignee.
                /// public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName);
                /// </summary>
                String AssignTaskTest = userService.AssignTask("roni@gmail.com", "someBoard", 0, 0, "rona@gmail.com");
                var AssignTaskTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(AssignTaskTest);
                if (AssignTaskTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(AssignTaskTestValues["ErrorMessage"]);
                }
                else
                {
                    Console.WriteLine("task was assigned successfully");
                }
            }
            else
            {
                Console.WriteLine("precondition steps went wrong");
            }
        }
    }
}
