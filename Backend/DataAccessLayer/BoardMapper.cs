﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardMapper:DataMapper
    {
        private const string _BoardTableName = "Board";

        public BoardMapper() : base(_BoardTableName)
        {
        }
        public List<BoardDTO> SelectAllBoards()
        {
            List<BoardDTO> result = Select().Cast<BoardDTO>().ToList();
            return result;
        }
        public BoardDTO GetBoard(int id)
        {
             BoardDTO result = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {_BoardTableName} WHERE id = {id};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    dataReader.Read();
                    result =((BoardDTO)ConvertReaderToObject(dataReader));               
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
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
        public string GetName(int id)
        {
            BoardDTO board = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_BoardTableName} where {id}=id;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    dataReader.Read();
                   board = (BoardDTO)ConvertReaderToObject(dataReader);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
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
            return board.BoardName;
        }
       
        public string GetOwner(int id)
        {
            BoardDTO board = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_BoardTableName} where {id}=id;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    dataReader.Read();
                    board = (BoardDTO)ConvertReaderToObject(dataReader);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
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
            return board.Owner;
        }
        
        public bool Insert(BoardDTO board)
        {

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_BoardTableName} ({BoardDTO.IDCollumnName} ,{BoardDTO.BoardNameColumnName},{BoardDTO.FreeTaskIdColumnName},{BoardDTO.OwnerColumnName}) " +
                        $"VALUES (@idVal,@nameVal,@freeidval,@ownerval);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", board.Id);
                    SQLiteParameter nameParam = new SQLiteParameter(@"nameVal", board.BoardName);
                    SQLiteParameter freeIdParam = new SQLiteParameter(@"freeidval", board.FreeTaskId);
                    SQLiteParameter ownerParam = new SQLiteParameter(@"ownerval", board.Owner);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(freeIdParam);
                    command.Parameters.Add(ownerParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    log.Info("board added to dataBase");
                }
                catch(Exception ex)
                {
                    log.Error(ex.ToString());
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }
        public void DeleteData()
        {
            List<BoardDTO> boards = SelectAllBoards();
            foreach (BoardDTO board in boards)
                Delete(board);
        }
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            int boardId = reader.GetInt32(0);
            string boardName = reader.GetString(1);
            int freeTaskId = reader.GetInt32(2);
            string owner = reader.GetString(3);
            BoardDTO boardDTO=new BoardDTO(boardName,boardId,freeTaskId,owner);
            return boardDTO;
        }
    }
}
