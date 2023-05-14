using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests
{
    internal class ColumnName
    {
        private readonly BoardService boardService;
        private readonly UserService userService;
        public ColumnName(BoardService boardService, UserService userService)
        {
            this.boardService = boardService;
            this.userService = userService;
        }
        public void RunTests()
        {
            /// <summary>
            /// This function tests Requirement 5
            /// 5. A board has a name, and three columns: ‘backlog’, ‘in progress’, and ‘done’.(test backlog)
            /// expect: Succsess.
            /// public string GetColumnName(string email, string boardName, int columnOrdinal);
            /// </summary>
            String regitrationTest = userService.Register("ronen@gmail.com", "1234");
            var regitrationTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(regitrationTest);

            String loginTest = userService.Login("ronen@gmail.com", "1234");
            var loginTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(loginTest);

            String addBoardTest = boardService.AddBoard("ronen@gmail.com", "someBoard");
            var addBoardTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(addBoardTest);

            if (!regitrationTestValues.ContainsKey("ErrorMessage") &
                !loginTestValues.ContainsKey("ErrorMessage") &
                !addBoardTestValues.ContainsKey("ErrorMessage"))
            {
                
                String getFirstColumnNameTest = boardService.GetColumnName("ronen@gmail.com", "someBoard", 0);
                var getFirstColumnNameTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(getFirstColumnNameTest);
                if (getFirstColumnNameTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(getFirstColumnNameTestValues["ErrorMessage"]);
                }
                else
                {
                    if (getFirstColumnNameTestValues["ReturnValue"] == "backlog")
                    {
                        Console.WriteLine("first column name is correct");
                    }
                    else
                    {
                        Console.WriteLine("first column name is wrong");
                    }
                }

                /// <summary>
                /// This function tests Requirement 5
                /// 5. A board has a name, and three columns: ‘backlog’, ‘in progress’, and ‘done’.(test in progress)
                /// expect: Succsess.
                /// public string GetColumnName(string email, string boardName, int columnOrdinal);
                /// </summary>
                String getSecondColumnNameTest = boardService.GetColumnName("ronen@gmail.com", "someBoard", 1);
                var getSecondColumnNameTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(getSecondColumnNameTest);
                if (getSecondColumnNameTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(getSecondColumnNameTestValues["ErrorMessage"]);
                }
                else
                {
                    if (getSecondColumnNameTestValues["ReturnValue"] == "in progress")
                    {
                        Console.WriteLine("second column name is correct");
                    }
                    else
                    {
                        Console.WriteLine("second column name is wrong");
                    }
                }

                /// <summary>
                /// This function tests Requirement 5
                /// 5. A board has a name, and three columns: ‘backlog’, ‘in progress’, and ‘done’.(test done)
                /// expect: Succsess.
                /// public string GetColumnName(string email, string boardName, int columnOrdinal);
                /// </summary>
                String getThirdColumnNameTest = boardService.GetColumnName("ronen@gmail.com", "someBoard", 2);
                var getThirdColumnNameTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(getThirdColumnNameTest);
                if (getThirdColumnNameTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(getThirdColumnNameTestValues["ErrorMessage"]);
                }
                else
                {
                    if (getThirdColumnNameTestValues["ReturnValue"] == "done")
                    {
                        Console.WriteLine("third column name is correct");
                    }
                    else
                    {
                        Console.WriteLine("third column name is wrong");
                    }
                }
            }
            else
            {
                Console.WriteLine("precondition steps went wrong");
            }
        }
    }
}
