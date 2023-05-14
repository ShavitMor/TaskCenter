using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests
{
    internal class ColumnLimit
    {
        private readonly BoardService boardService;
        private readonly UserService userService;
        public ColumnLimit(BoardService boardService, UserService userService)
        {
            this.boardService = boardService;
            this.userService = userService;
        }
        public void RunTests()
        {

            /// <summary>
            /// This function tests Requirement 9
            /// 11. By default, there will be no limit on the number of the tasks.
            /// expect: Succsess.
            /// public string GetColumnLimit(string email, string boardName, int columnOrdinal);
            /// </summary>
            String regitrationTest = userService.Register("ronen@gmail.com", "1234");
            var regitrationTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(regitrationTest);

            String loginTest = userService.Login("ronen@gmail.com", "1234");
            var loginTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(loginTest);

            String addBoardTest = boardService.AddBoard("ronen@gmail.com", "someBoard");
            var addBoardTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(addBoardTest);

            // tests
            if (!regitrationTestValues.ContainsKey("ErrorMessage") & 
                !loginTestValues.ContainsKey("ErrorMessage") &
                !addBoardTestValues.ContainsKey("ErrorMessage"))
            {
                
                String getFirstColumnLimitTest = boardService.GetColumnLimit("ronen@gmail.com", "someBoard", 0);
                var getFirstColumnLimitTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(getFirstColumnLimitTest);
                if (getFirstColumnLimitTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(getFirstColumnLimitTestValues["ErrorMessage"]);
                }
                else
                {
                    if (getFirstColumnLimitTestValues["ReturnValue"] == "-1")
                    {
                        Console.WriteLine("default limit of first column is correct");
                    }
                    else
                    {
                        Console.WriteLine("default limit of first column is wrong");
                    }
                }


                /// <summary>
                /// This function tests Requirement 9
                /// 11. By default, there will be no limit on the number of the tasks.(we check the second column)
                /// expect: Succsess.
                ///public string GetColumnLimit(string email, string boardName, int columnOrdinal);
                /// </summary>
                String getSecondColumnLimitTest = boardService.GetColumnLimit("ronen@gmail.com", "someBoard", 1);
                var getSecondColumnLimitTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(getSecondColumnLimitTest);
                if (getSecondColumnLimitTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(getSecondColumnLimitTestValues["ErrorMessage"]);
                }
                else
                {
                    if (getSecondColumnLimitTestValues["ReturnValue"] == "-1")
                    {
                        Console.WriteLine("default limit of second column is correct");
                    }
                    else
                    {
                        Console.WriteLine("default limit of second column is wrong");
                    }
                }

                /// <summary>
                /// This function tests Requirement 9
                /// 11. By default, there will be no limit on the number of the tasks.(we check the third column)
                /// expect: Succsess.
                /// public string GetColumnLimit(string email, string boardName, int columnOrdinal);
                /// </summary>
                String getThirdColumnLimitTest = boardService.GetColumnLimit("ronen@gmail.com", "someBoard", 2);
                var getThirdColumnLimitTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(getThirdColumnLimitTest);
                if (getThirdColumnLimitTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(getThirdColumnLimitTestValues["ErrorMessage"]);
                }
                else
                {
                    if (getFirstColumnLimitTestValues["ReturnValue"] == "-1")
                    {
                        Console.WriteLine("default limit of third column is correct");
                    }
                    else
                    {
                        Console.WriteLine("default limit of third column is wrong");
                    }
                }

                /// <summary>
                /// This function tests Requirement 10
                ///10.Each column should support limiting the maximum number of its tasks. (limit first column)
                /// expect: Succsess.
                /// public string LimitColumn(string email, string boardName, int columnOrdinal, int limit);
                /// </summary>

                String LimitFirstColumnTest = boardService.LimitColumn("ronen@gmail.com", "someBoard", 0, 10);
                var LimitFirstColumnTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(LimitFirstColumnTest);
                if (LimitFirstColumnTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(LimitFirstColumnTestValues["ErrorMessage"]);
                }
                else
                {
                    Console.WriteLine("First column was limited successfully");
                }

                /// <summary>
                /// This function tests Requirement 10
                ///10.Each column should support limiting the maximum number of its tasks. (limit second column)
                /// expect: Succsess.
                /// public string LimitColumn(string email, string boardName, int columnOrdinal, int limit);
                /// </summary>
                String LimitSecondColumnTest = boardService.LimitColumn("ronen@gmail.com", "someBoard", 1, 20);
                var LimitSecondColumnTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(LimitSecondColumnTest);
                if (LimitSecondColumnTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(LimitSecondColumnTestValues["ErrorMessage"]);
                }
                else
                {
                    Console.WriteLine("Second column was limited successfully");
                }

                /// <summary>
                /// This function tests Requirement 10
                ///10.Each column should support limiting the maximum number of its tasks. (limit third column)
                /// expect: Succsess.
                /// public string LimitColumn(string email, string boardName, int columnOrdinal, int limit);
                /// </summary>
                String LimitThirdColumnTest = boardService.LimitColumn("ronen@gmail.com", "someBoard", 2, 30);
                var LimitThirdColumnTestValues = JsonSerializer.Deserialize<Dictionary<string, string>>(LimitThirdColumnTest);
                if (LimitThirdColumnTestValues.ContainsKey("ErrorMessage"))
                {
                    Console.WriteLine(LimitThirdColumnTestValues["ErrorMessage"]);
                }
                else
                {
                    Console.WriteLine("Third column was limited successfully");
                }
            }
            else
            {
                Console.WriteLine("precondition steps went wrong");
            }
        }
    }
}
