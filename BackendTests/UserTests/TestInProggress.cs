using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests.UserServiceTests
{
    internal class TestInProggress
    {
        private readonly UserService userService;

        public TestInProggress(UserService userService)
        {
            this.userService = userService;
        }
        public void RunTests()
        {
            userService.Register("guy@gmail.com", "Pass12");
            //-----

            /// <summary>
            /// This function tests Requirement:
            /// 16. As a user, I want to be able to list my 'in progress’ tasks from all of my boards, so that I
            /// can plan my schedule.
            /// 
            /// -expect success
            /// 
            /// </summary>

            string test1 = userService.InProgressTasks("guy@gmail.com");
            Dictionary<string, string> data = JsonSerializer.Deserialize<Dictionary<string, string>>(test1);
            if (data["ErrorMessage"] == null)
            {
                Console.WriteLine("all in proggress tasks were shown successfully");
            }
            else
            {
                Console.WriteLine(data["Errormessage"]);
            }

        }
    }
}
