using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests.UserServiceTests
{
    internal class TestLoginLogout
    {
        private readonly UserService userService;

        public TestLoginLogout(UserService userService)
        {
            this.userService = userService;
        }
        public void RunTests()
        {
            userService.Register("guy@gmail.com", "Pass12");
            //-----

            /// <summary>
            /// This function tests Requirement:
            /// 1. A user is identified by an email and is authenticated by a password
            /// 8. As a user, I want to be able to login using an email and a password, and logout.
            /// 
            /// -expect success if registered
            /// 
            /// </summary>

            string test1 = userService.Login("guy@gmail.com", "Pass12");
            Dictionary<string, string> data = JsonSerializer.Deserialize<Dictionary<string, string>>(test1);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("logged user successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }

            /// <summary>
            /// This function tests Requirement:
            /// 1. A user is identified by an email and is authenticated by a password
            /// 
            /// -expect error
            /// 
            /// </summary>

            string test2 = userService.Login(null, "Pass12");
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(test2);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("logged user successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }


            /// <summary>
            /// This function tests Requirement:
            /// 1. A user is identified by an email and is authenticated by a password
            /// 
            /// -expect error
            /// 
            /// </summary>

            string test2b = userService.Login("guy@gmail.com",null);
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(test2b);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("logged user successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }


            /// <summary>
            /// This function tests Requirement:
            /// 8. As a user, I want to be able to login using an email and a password, and logout.
            /// 
            /// -expect success if logged in.
            /// 
            /// </summary>

            string test3 = userService.Logout("guy@gmail.com");
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(test3);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("logged user out successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }
        }
    }
}
