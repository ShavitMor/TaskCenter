using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class ColumnMapper : DataMapper
    {
        private const string _ColumnTableName = "Columns";
        

        public ColumnMapper() : base(_ColumnTableName) { }
        public List<ColumnDTO> SelectAllColumns()
        {
            List<ColumnDTO> result = Select().Cast<ColumnDTO>().ToList();
            return result;
        }

        public ColumnDTO GetColumn(int BoardID, int ColID)
        {
            ColumnDTO column = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_ColumnTableName} where Related_Board_Id = {BoardID} and Id = {ColID}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                        column = (ColumnDTO)ConvertReaderToObject(dataReader);
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
            return column;
        }
        public ColumnDTO GetBacklogColumn(int BoardID) { return GetColumn(BoardID, ((short)TaskState.Backlog)); }
        public ColumnDTO GetInProgressColumn(int BoardID) { return GetColumn(BoardID, ((short)TaskState.InProgress)); }
        public ColumnDTO GetDoneColumn(int BoardID) { return GetColumn(BoardID, ((short)TaskState.Done)); }
        public int GetTaskLimit(int BoardID, int ColID) { return GetColumn(BoardID, ColID).TaskLimit; }
        public List<ColumnDTO> GetColumnsOfBoard(int BoardID)
        {
            List<ColumnDTO> columns = new();
            columns.Add(GetColumn(BoardID, 0));
            columns.Add(GetColumn(BoardID, 1));
            columns.Add(GetColumn(BoardID, 2));
            return columns;

        }

        public bool Insert(ColumnDTO Column)
        {

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_ColumnTableName} ({ColumnDTO.IDCollumnName} ,{ColumnDTO.BoardIdCol},{ColumnDTO.TaskLimCol}) " +
                        $"VALUES (@idVal,@relatedBoardVal,@limitVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", Column.ColIndex);
                    SQLiteParameter boardParam = new SQLiteParameter(@"relatedBoardVal", Column.BoardID);
                    SQLiteParameter taskLimParam = new SQLiteParameter(@"limitVal", Column.TaskLimit);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(boardParam);
                    command.Parameters.Add(taskLimParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    log.Info("new column added to dataBase");
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
                return res > 0;
            }
        }

        public bool ChangeTaskLimit(int boardID, int colId, int newLim)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_ColumnTableName} set [{ColumnDTO.TaskLimCol}]=@limitVal where {ColumnDTO.BoardIdCol}={boardID} and Id={colId}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(@"limitVal",newLim));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
                    log.Info($"updated task limit of column{colId} from board {boardID} to {newLim}");
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        public bool Delete(ColumnDTO column)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_ColumnTableName} where Id={column.ColIndex} and {ColumnDTO.BoardIdCol} = {column.BoardID}"

                };
                try
                {
                    log.Info($"Deleting Column {column.ColIndex} from board {column.BoardID}...");
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"column deleted succesfully.");

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public bool DeleteAllColumnsOFBoard(int boardID)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_ColumnTableName} where {ColumnDTO.BoardIdCol} = {boardID}"

                };
                try
                {
                    log.Info($"Deleting all Columns from board {boardID}...");
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"All columns from board{boardID} deleted succesfully.");

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public void DeleteData()
        {
            List<ColumnDTO> columns = SelectAllColumns();
            foreach (ColumnDTO column in columns)
                Delete(column);
        }
        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            int id = reader.GetInt32(0);
            int boardID = reader.GetInt32(1);
            int taskLim = reader.GetInt32(2);
            return new ColumnDTO (boardID, id, taskLim );
        }
    }
}
