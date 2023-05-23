using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserMapper : DataMapper
    {
        private const string _UserTableName = "Users";
        

        public UserMapper() : base(_UserTableName)
        {
        }
        public List<UserDTO> SelectAllUsers()
        {
            List<UserDTO> result = Select().Cast<UserDTO>().ToList();
            return result;
        }
        public UserDTO GetUser(string email)
        {
            UserDTO result = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {_UserTableName} WHERE {UserDTO.EmailColumnName} = @mailVal ;";
                SQLiteDataReader dataReader = null;
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"mailVal", email));
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    dataReader.Read();
                    result = ((UserDTO)ConvertReaderToObject(dataReader));
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return result;
        }

        public string GetEmail(int userId)
        {
            UserDTO user = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_UserTableName} where {UserDTO.UserIdColomnName} = {userId};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                        user = (UserDTO)ConvertReaderToObject(dataReader);
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return user.Email;
        }


        public int GetUserId(string email)
        {
            UserDTO user = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_UserTableName} where {UserDTO.EmailColumnName} = {email};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                        user = (UserDTO)ConvertReaderToObject(dataReader);
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return user.Id;
        }


        public string GetPassword(string email)
        {
            UserDTO user = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_UserTableName} where {UserDTO.EmailColumnName} = @mailVal;";
                SQLiteDataReader dataReader = null;
                try
                {
                    command.Parameters.Add(new SQLiteParameter(@"mailVal", email));
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                        user = (UserDTO)ConvertReaderToObject(dataReader);
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return user.Password;
        }





        public bool Insert(UserDTO user)
        {

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_UserTableName} ({UserDTO.UserIdColomnName} ,{UserDTO.EmailColumnName},{UserDTO.PasswordColumnName}) " +
                        $"VALUES (@idVal,@emailVal,@passwordVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", user.Id);
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"passwordVal", user.Password);
                   

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    log.Info("User added to dataBase");
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                
                return res>0;
            }
        }
        public void DeleteData()
        {
            List<UserDTO> users = SelectAllUsers();
            foreach (UserDTO us in users)
                Delete(us);
        }
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            int userId = reader.GetInt32(0);
            string email = reader.GetString(1);
            string password = reader.GetString(2);
            UserDTO UserDTO = new UserDTO(email , password, userId);
            return UserDTO;
          
        }


    }
}
