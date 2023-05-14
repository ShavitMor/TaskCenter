using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace BackendTests.UserServiceTests
{
    internal class TestRegist
    {

        private readonly UserService userService;

        public TestRegist(UserService userService)
        {
            this.userService = userService;
        }
        public void RunTests()
        {
            /// <summary>
            /// This function tests Requirement:
            /// 2. A valid password is in the length of 6 to 20 characters and must include at least one
            ///    uppercase letter, one small character and a number
            /// 
            /// -expect error
            /// 
            /// </summary>

            string test1 = userService.Register("guy@gmail.com", "1235");
            Dictionary<string, string> data = JsonSerializer.Deserialize<Dictionary<string, string>>(test1);
            if (data["ErrorMessage"]==null)
            {
                Console.WriteLine("Created user successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }


            /// <summary>
            /// This function tests Requirement:
            /// 7. The program will allow registration of new users.
            /// 
            /// 2. A valid password is in the length of 6 to 20 characters and must include at least one
            ///    uppercase letter, one small character and a number
            /// 
            /// 3. Each email address must be unique in the system.
            /// 
            /// -expect success
            /// 
            /// </summary>

            string test1b = userService.Register("guy@gmail.com", "Pass12");
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(test1b);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("Created user successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }


            /// <summary>
            /// This function tests Requirement:
            /// 3. Each email address must be unique in the system.
            /// 
            /// -expect error
            /// 
            /// </summary>

            string test2 = userService.Register("guy@gmail.com", "Pass12");
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(test2);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("Created user successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }


            /// <summary>
            /// This function tests Requirement:
            /// 1. A user is identified by an email and is authenticated by a password.
            /// 
            /// -expect error
            /// 
            /// </summary>

            string test3 = userService.Register(null, "Pass12");
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(test3);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("Created user successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }



            /// <summary>
            /// This function tests Requirement:
            /// 1. A user is identified by an email and is authenticated by a password.
            /// 
            /// -expect error
            /// 
            /// </summary>

            string test3b = userService.Register("guy@gmail.com", null);
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(test3b);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("Created user successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }
        }
    }
}
