using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardMemberMapper : DataMapper
    {
        private const string _boardMembers = "BoardMembers";

        public BoardMemberMapper() : base(_boardMembers)
        {
        }
        public List<string> GetMembers(int id)
        {
            List<string> members = new List<string>();
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select MemberEmail from {_boardMembers} where @idVal=id;";
                SQLiteDataReader dataReader = null;
                try
                {
                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", id);
                    command.Parameters.Add(idParam);
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        members.Add(dataReader.GetString(0));
                    }
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
            return members;
        }
        public List<int> GetBoards(string email)
        {
            List<int> boards = new List<int>();
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select id from {_boardMembers} where @emailVal=MemberEmail;";
                SQLiteDataReader dataReader = null;
                try
                {
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", email);
                    command.Parameters.Add(emailParam);
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    int index = 0;
                    while (dataReader.Read())
                    {
                        boards.Add(dataReader.GetInt32(index));
                        index++;
                    }
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
            return boards;
        }
        public bool Insert(BoardMembersDTO boardMember)
        {

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_boardMembers} ({DTO.IDCollumnName} ,{BoardMembersDTO.userEmail}) " +
                        $"VALUES (@idVal,@nameVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", boardMember.Id);
                    SQLiteParameter titleParam = new SQLiteParameter(@"nameVal", boardMember.UserEmail);
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(titleParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    log.Info("member has added in data base");
                }
                catch(Exception ex)
                {
                    log.Error(ex);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }
        public bool DeleteMember(BoardMembersDTO obj)
        {
            return Delete(obj);
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            BoardMembersDTO result = null;
            if (!reader.IsDBNull(0) & !reader.IsDBNull(1))
            {
                int id = reader.GetInt32(0);
                string userEmail = reader.GetString(1);
                result = new BoardMembersDTO(id, userEmail);
            }
            return result;
        }
        public List<BoardMembersDTO> SelectAllBoardsMembers()
        {
            List<BoardMembersDTO> result = Select().Cast<BoardMembersDTO>().ToList();
            return result;
        }
        public void DeleteData()
        {
            List<BoardMembersDTO> boardMembersDTOs=SelectAllBoardsMembers();
            foreach (BoardMembersDTO boardMemberDTO in boardMembersDTOs)
                if(boardMemberDTO!=null)
                Delete(boardMemberDTO);
        }


    }
    
}
