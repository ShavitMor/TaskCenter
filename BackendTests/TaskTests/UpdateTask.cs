using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackendTests.Tests
{
    internal class UpdateTask
    {

        private readonly BoardService bs;
        private readonly TaskService ts;
        private readonly UserService us;
        int id = 4;

        public UpdateTask(TaskService ts,UserService us,BoardService bs)
        {
            this.ts = ts;
            this.us = us;
            this.bs = bs;

        }


        public void RunTests()
        {


            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for backlog column.
            ///     expect: Succsess.
            ///public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate);


            /// 15. All the task data can be changed, except for the creation time.
            /// 
            ///     we check if we can change all data except creation time, firstly we try change task due date.
            ///     expect: Succsess.
            ///public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate);
            /// </summary>

            us.Register("kipod@gmail.com", "Zz123456");
            us.Login("kipod@gmail.com", "Zz123456");
            bs.AddBoard("kipod@gmail.com", "bord");
            ts.AddTask("kipod@gmail.com", "bord", "koteret", "tioor", new DateTime(1987, 8, 7));
            string check0 = ts.UpdateTaskDueDate("kipod@gmail.com", "bord", 0, id, new DateTime(1991, 9, 1));
            Dictionary<string, string> ans0 = JsonSerializer.Deserialize<Dictionary<string, string>>(check0);

            if (ans0["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's due Date changed successfully");
            }
            else
                Console.WriteLine(ans0["ErrorMassage"]);





            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for backlog column.
            ///     expect: Succsess.
            ///         public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description);


            /// 15. All the task data can be changed, except for the creation time.
            ///     we check if we can change all data except creation time, now we try change task Description.
            ///     expect: Succsess.
            ///         public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description);

            ///     
            /// </summary>
            /// 
            string check20 = ts.UpdateTaskDescription("kipod@gmail.com", "bord", 2, id, "pasta basta");
            Dictionary<string, string> ans20 = JsonSerializer.Deserialize<Dictionary<string, string>>(check20);

            if (ans20["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's Description changed successfully");
            }
            else
                Console.WriteLine(ans20["ErrorMassage"]);


            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for backlog column.
            ///     expect: Succsess.
            ///     public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title);

            /// 15. All the task data can be changed, except for the creation time.
            ///     we check if we can change all data except creation time, now we try change task title.
            ///     expect: Succsess.
            ///     public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title);
            /// </summary>
            string check30 = ts.UpdateTaskTitle("kipod@gmail.com", "bord", 2, id, "koteret hadasha");
            Dictionary<string, string> ans30 = JsonSerializer.Deserialize<Dictionary<string, string>>(check30);

            if (ans30["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's title changed successfully");
            }
            else
                Console.WriteLine(ans30["ErrorMassage"]);


            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for In-Progress column.
            ///     expect: Succsess.
            ///    public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate);


            /// 15. All the task data can be changed, except for the creation time.
            ///     we check if we can change all data except creation time, firstly we try change task due date.
            ///     expect: Succsess.
            ///    public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate);

            /// </summary>

            bs.AdvanceTask("kipod@gmail.com", "bord", 0, 2);
            string check01 = ts.UpdateTaskDueDate("kipod@gmail.com", "bord", 0, id, new DateTime(1991, 9, 1));
            Dictionary<string, string> ans01 = JsonSerializer.Deserialize<Dictionary<string, string>>(check01);

            if (ans01["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's due Date changed successfully");
            }
            else
                Console.WriteLine(ans01["ErrorMassage"]);



            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for In-Progress column.
            ///     expect: Succsess.
            ///  public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description);


            /// 15. All the task data can be changed, except for the creation time.
            ///     we check if we can change all data except creation time, now we try change task Description.
            ///     expect: Succsess.
            ///    public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description);
            /// </summary>

            string check201 = ts.UpdateTaskDescription("kipod@gmail.com", "bord", 2, id, "pasta basta");
            Dictionary<string, string> ans201 = JsonSerializer.Deserialize<Dictionary<string, string>>(check201);

            if (ans201["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's Description changed successfully");
            }
            else
                Console.WriteLine(ans201["ErrorMassage"]);


            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for In-Progress column.
            ///     expect: Succsess.
            ///     public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title);

            /// 15. All the task data can be changed, except for the creation time.
            ///     we check if we can change all data except creation time, firstly we try change task title.
            ///     expect: Succsess.
            ///     public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title);
            /// </summary>

            string check301 = ts.UpdateTaskTitle("kipod@gmail.com", "bord", 2, id, "koteret hadasha");
            Dictionary<string, string> ans301 = JsonSerializer.Deserialize<Dictionary<string, string>>(check301);

            if (ans301["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's title changed successfully");
            }
            else
                Console.WriteLine(ans301["ErrorMassage"]);







            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for done column.
            ///     expect: Error.
            ///      public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate);

            /// 15. All the task data can be changed, except for the creation time.
            ///     we check if we can change all data except creation time, firstly we try change task due date.
            ///     expect: Error.
            ///      public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate);
            /// </summary>

            bs.AdvanceTask("kipod@gmail.com", "bord", 1, 2);

            string check=ts.UpdateTaskDueDate("kipod@gmail.com", "bord", 2, id, new DateTime(1991, 9, 1));
            Dictionary<string, string> ans = JsonSerializer.Deserialize<Dictionary<string, string>>(check);

            if (ans["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's due Date changed successfully");
            }
            else
                Console.WriteLine(ans["ErrorMassage"]);


            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for done column.
            ///     expect: Error.
            /// public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description);


            /// 15. All the task data can be changed, except for the creation time.
            ///     we check if we can change all data except creation time, firstly we try change task description.
            ///     expect: Error.
            ///public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description);

            /// </summary>

            string check2 = ts.UpdateTaskDescription("kipod@gmail.com", "bord", 2, id, "pasta basta");
            Dictionary<string, string> ans2 = JsonSerializer.Deserialize<Dictionary<string, string>>(check2);

            if (ans2["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's Description changed successfully");
            }
            else
                Console.WriteLine(ans2["ErrorMassage"]);


            /// <summary>
            /// This function tests Requirement 14 and 15
            /// 14. A task that is not done can be changed by the user.
            /// 
            ///     we check if undone task's data ,except "creation time", can be changed,for done column.
            ///     expect: Error.
            ///public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title);     

            /// 15. All the task data can be changed, except for the creation time.
            ///     we check if we can change all data except creation time, firstly we try change task title.
            ///     expect: Error.
            /// public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title);    
            /// </summary>
            string check3 = ts.UpdateTaskTitle("kipod@gmail.com", "bord", 2, id, "koteret hadasha");
            Dictionary<string, string> ans3 = JsonSerializer.Deserialize<Dictionary<string, string>>(check3);

            if (ans3["ErrorMessage"] == null)
            {
                Console.WriteLine("Task's title changed successfully");
            }
            else
                Console.WriteLine(ans3["ErrorMassage"]);

        }












        

    }
}
