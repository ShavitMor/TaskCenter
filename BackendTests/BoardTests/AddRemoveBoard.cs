using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests
{
    internal class AddRemoveBoard
    {
        private readonly BoardService boardService;
        private readonly UserService userService;
        public AddRemoveBoard(BoardService boardService, UserService userService)
        {
            this.boardService = boardService;
            this.userService = userService;
        }
        public void RunTests()
        {


            /// <summary>
            /// This function tests Requirement 9
            /// 9.As a user, I want to be able to add and remove boards.
            /// expect: Succsess.
            /// public string AddBoard(string email, string name);
            /// </summary>

            String regitrationTest = userService.Register("ronen@gmail.com", "1234");
            var regitrationTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(regitrationTest);

            String loginTest = userService.Login("ronen@gmail.com", "1234");
            var loginTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(loginTest);
            if (!regitrationTestValues.ContainsKey("ErrorMessage") &
                !loginTestValues.ContainsKey("ErrorMessage")){

                String addBoardTest = boardService.AddBoard("ronen@gmail.com", "someBoard");
                var addBoardTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(addBoardTest);
                if (addBoardTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(addBoardTestValues["ErrorMessage"]);
                }
                else
                {
                    Console.WriteLine("Board was added successfully");
                }


                /// <summary>
                /// This function tests Requirement 6
                /// 6.A user cannot have two boards with the same name
                /// expect: ErrorMessage.
                /// public string AddBoard(string email, string name);
                /// </summary>

                String sameNameTest = boardService.AddBoard("ronen@gmail.com", "someBoard");
                var sameNameTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(sameNameTest);
                if (sameNameTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(addBoardTestValues["ErrorMessage"]);
                }
                else
                {
                    Console.WriteLine("Board was added although user already have a board with the same name");
                }

                /// <summary>
                /// This function tests Requirement 9
                /// 9.As a user, I want to be able to add and remove boards.(we test the removing)
                /// expect: Succsess.
                /// public string RemoveBoard(string email, string name);
                /// </summary>
                String removeBoardTest = boardService.RemoveBoard("ronen@gmail.com", "someBoard");
                var removeBoardTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(removeBoardTest);
                if (removeBoardTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(removeBoardTestValues["ErrorMessage"]);
                }
                else
                {
                    Console.WriteLine("Board was removed successfully");
                }
            }
            else
            {
                Console.WriteLine("precondition steps went wrong");
            }
        }
    }
}
