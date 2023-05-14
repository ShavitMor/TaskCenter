using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;


namespace BackendTests
{
    internal class subMain
    {
       /* static void Main(string[] args)
        {

            GradingService grading = new GradingService();


            Console.WriteLine(grading.DeleteData());
            Console.WriteLine(grading.Register("email@email.com", "Password123"));
            Console.WriteLine(grading.AddBoard("email@email.com", "Board1"));
            // Console.WriteLine(grading.AddTask("email@email.com", "Board1", "Title", "desc", new DateTime(2022, 7, 1)));
            //Console.WriteLine(grading.AdvanceTask("email@email.com", "Board1", 0, 0));

            int b2Id = 2;
            Console.WriteLine(grading.Login("email@email.com", "Password123"));
            Console.WriteLine(grading.RemoveBoard("email@email.com", "Board1"));
            Console.WriteLine(grading.GetUserBoards("email@email.com"));
            Console.WriteLine(grading.Register("email2@email.com", "Password123"));
            Console.WriteLine(grading.AddBoard("email@email.com", "Board1"));
            Console.WriteLine(grading.GetUserBoards("email@email.com"));
            Console.WriteLine(grading.JoinBoard("email2@email.com", b2Id));
            Console.WriteLine(grading.AddBoard("email2@email.com", "Board2"));
            Console.WriteLine(grading.GetUserBoards("email2@email.com"));
            Console.WriteLine(grading.GetUserBoards("email@email.com"));
            Console.WriteLine(grading.LeaveBoard("email2@email.com", b2Id));
            Console.WriteLine(grading.GetUserBoards("email2@email.com"));
            Console.WriteLine(grading.JoinBoard("email2@email.com", b2Id));
            Console.WriteLine(grading.LimitColumn("email@email.com", "Board1", 0, 3));
            Console.WriteLine(grading.GetColumnLimit("email@email.com", "Board1", 0));
            Console.WriteLine(grading.GetColumnName("email2@email.com", "Board2", 2));
            Console.WriteLine(grading.AddTask("email2@email.com", "Board2", "Title", "desc", new DateTime(2022, 7, 1)));
            Console.WriteLine(grading.GetColumn("email2@email.com", "Board2", 0));
            Console.WriteLine(grading.AssignTask("email2@email.com", "Board2", 0, 0, "email2@email.com"));
            Console.WriteLine(grading.AdvanceTask("email2@email.com", "Board2", 1, 0));
            Console.WriteLine(grading.GetColumn("email2@email.com", "Board2", 1));
            Console.WriteLine(grading.GetColumn("email2@email.com", "Board2", 2));
            Console.WriteLine(grading.GetColumn("email2@email.com", "Board2", 1));

            //Console.WriteLine(grading.UpdateTaskDescription("email2@email.com", "Board2",1,);


            //Console.WriteLine(grading.DeleteData());







            //a.register("email@email.com", "Password123");
            //a.register("email2@email.com", "Password123");



            //
            //             string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            //             Console.WriteLine(path);
            //             string connectionString = $"Data Source={path}; Version=3;";
            //
            //             const string MessageTableName = "Message";
            //             const string IDColumnName = "ID";
            //             const string MessageTitleColumnName = "Title";
            //             const string MessageBodyColumnName = "Body";
            //             const string MessageForumColumnName = "Forum";
            //
            //
            //
            //             using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            //             {
            //                 SQLiteCommand command = new SQLiteCommand(null, connection);
            //                 int res = -1;
            //                 try
            //                 {
            //                     connection.Open();
            //                     /* command.CommandText = $"UPDATE {MessageTableName} SET {MessageBodyColumnName} = @body_val WHERE {MessageTitleColumnName} = @title_val; ";
            //                     SQLiteParameter bodyParam = new SQLiteParameter(@"body_val", "new body");
            //                     SQLiteParameter titleParam = new SQLiteParameter(@"title_val", "title");
            //
            //                     command.Parameters.Add(bodyParam);
            //                     command.Parameters.Add(titleParam);
            //                                     
            //
            //                     command.Prepare(); 
            //
            //                     res = command.ExecuteNonQuery();
            //
            //                     Console.WriteLine(res);
            //                     */
            //
            //                     command.CommandText = $"SELECT * FROM {MessageTableName}";
            //
            //                     SQLiteDataReader reader = command.ExecuteReader();
            //
            //                     while (reader.Read())
            //                     {
            //                         // Console.WriteLine(reader[MessageTitleColumnName] + ", " + reader[MessageBodyColumnName]);
            //
            //                         int ID = reader.GetInt32(0);
            //                         string title = reader.GetString(1);
            //
            //                         Console.WriteLine("ID: " + ID + " - " + "Title: " + title);
            //                     }
            //
            //
            //                 }
            //                 catch (Exception e)
            //                 {
            //                     Console.WriteLine(command.CommandText);
            //                     Console.WriteLine(e.ToString());
            //                     //log error
            //                 }
            //                 finally
            //                 {
            //                     command.Dispose();
            //                     connection.Close();
            //                 }
            //
            //             }
            //         
            //








            //GradingService gradingService = new GradingService();
            // string email = "rrrgmia@gmail.com";
            //  string board = "one";
            //Console.WriteLine(gradingService.Register(email, "Aka123k123"));
            // gradingService.Login(email, "Aka123k123");
            // string invalid = "jgiosejiooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
            // Console.WriteLine(gradingService.AddBoard(email, "one"));
            // Console.WriteLine(gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DateTime.Now));
            // Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
            // Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0));
            // Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 1));
            // Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 0));
            // Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 1));
            // Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
            // Console.WriteLine(gradingService.AdvanceTask(email, "one", 2, 0));
            // Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0)); // no such task in column 0
            // Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 2));
            // Console.WriteLine(gradingService.InProgressTasks(email));
            // Console.WriteLine(gradingService.LimitColumn(email, board, 1, 5));
            // Console.WriteLine(gradingService.LimitColumn(email, board, 1, 4));
            // Console.WriteLine(gradingService.LimitColumn(email, board, 1, 10));
            // Console.WriteLine(gradingService.GetColumnLimit(email, board, 1));
            // Console.WriteLine(gradingService.GetColumnName(email, board, 5)); // INVALID NUMBER
            // Console.WriteLine(gradingService.AddTask(email, "three", "new", "HELLOW WORLD", DateTime.Now)); // no such board three
            // Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 1, 0, DateTime.Now));// not good , changes to task that not in true coloumn number
            // Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 9, 2, DateTime.Now));// not good , changes to invalid coloumn number
            // Console.WriteLine(gradingService.RemoveBoard(email, "one"));
            // Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
            //
            // Console.WriteLine(gradingService.AddBoard(email, "two"));
            // Console.WriteLine(gradingService.AddTask(email, "two", "new", "HELLOW WORLD", DateTime.Now));
            // Console.WriteLine(gradingService.UpdateTaskDueDate(email, "two", 0, 0, DateTime.Now));//
            // Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 0, "new title"));//
            // Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 1, "new title"));//no such task
            // Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 0));
            // Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, "new title"));//
            // Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, invalid));//invalid title
            // Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, "new descp"));
            // Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, invalid));//
            // Console.WriteLine(gradingService.LimitColumn(email, "two", 1, 1));
            // Console.WriteLine(gradingService.AddTask(email, "two", "new task", "HELLOW WORLD", DateTime.Now));
            // Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 1)); // the column in full
        }
    }









