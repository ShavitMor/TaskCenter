using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskMapper : DataMapper
    {
        private const string _TaskTableName = "Tasks";

        public TaskMapper() : base(_TaskTableName) { }

        public List<TaskDTO> SelectAllTasks()
        {
            List<TaskDTO> result = Select().Cast<TaskDTO>().ToList();
            return result;
        }

        public List<TaskDTO> GetAllTasksOfBoard(int BoardID)
        {
            List<TaskDTO> result = new();
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_TaskTableName} where Related_Board_Id = {BoardID}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        result.Add((TaskDTO)ConvertReaderToObject(dataReader));

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
            return result;
        }

        public List<TaskDTO> GetAllTasksOfColumn(int BoardID, int TaskState)
        {
            List<TaskDTO> result = new();
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_TaskTableName} where Related_Board_Id = {BoardID} and Task_State = {TaskState}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        result.Add((TaskDTO)ConvertReaderToObject(dataReader));

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
            return result;
        }

        public TaskDTO GetTask(int BoardID, int Id)
        {
            TaskDTO result = null;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_TaskTableName} where Related_Board_Id = {BoardID} and Id = {Id}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    if(dataReader.Read())
                        result = ((TaskDTO)ConvertReaderToObject(dataReader));
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

        public int GetTaskState(int BoardID, int Id)
        {
            return GetTask(BoardID, Id).TaskState;
        }
        public DateTime GetCreationDate(int BoardID, int Id)
        {
            return GetTask(BoardID, Id).CreationDate;
        }
        public DateTime GetDueDate(int BoardID, int Id)
        {
            return GetTask(BoardID, Id).DueDate;
        }
        public string GetTitle(int BoardID, int Id)
        {
            return GetTask(BoardID, Id).Title;
        }
        public string GetDescription(int BoardID, int Id)
        {
            return GetTask(BoardID, Id).Description;
        }
        public string GetAssignee(int BoardID, int Id)
        {
            return GetTask(BoardID, Id).Assignee;
        }

        public bool Insert(TaskDTO task)
        {

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_TaskTableName} ({TaskDTO.IDCollumnName},{TaskDTO.BoardIdCol},{TaskDTO.TitleCol},{TaskDTO.DescCol},{TaskDTO.TaskStatCol},{TaskDTO.DueDateCol},{TaskDTO.CreatDatCol},{TaskDTO.AssigneCol}) " +
                        $"VALUES (@idVal,@boardVal,@titleVal,@desVal,@stateVal,@duedateVal,@creationVal,@asigneeVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", task.TaskID);
                    SQLiteParameter boardParam = new SQLiteParameter(@"boardVal", task.BoardID);
                    SQLiteParameter titleParam = new SQLiteParameter(@"titleVal", task.Title);
                    SQLiteParameter descParam = new SQLiteParameter(@"desVal", task.Description);
                    SQLiteParameter stateParam = new SQLiteParameter(@"stateVal", task.TaskState);
                    SQLiteParameter dueDParam = new SQLiteParameter(@"duedateVal", task.DueDate);
                    SQLiteParameter createDParam = new SQLiteParameter(@"creationVal", task.CreationDate);
                    SQLiteParameter assignParam = new SQLiteParameter(@"asigneeVal", task.Assignee);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(boardParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descParam);
                    command.Parameters.Add(stateParam);
                    command.Parameters.Add(dueDParam);
                    command.Parameters.Add(createDParam);
                    command.Parameters.Add(assignParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                    log.Info("new task added to Task dataBase.");
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

        public bool ChangetTitle(int boardID, int taskId, string newTitle)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_TaskTableName} set [{TaskDTO.TitleCol}]=@{newTitle} where {TaskDTO.BoardIdCol}={boardID} and Id={taskId}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(newTitle,newTitle));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
                    log.Info($"updated title of {taskId} from board {boardID}.");
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
        public bool ChangetDescription(int boardID, int taskId, string newDescription)
        {
            int res = -1;
            newDescription= newDescription.Replace(' ', '_');
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_TaskTableName} set [{TaskDTO.DescCol}]=@{newDescription} where {TaskDTO.BoardIdCol}={boardID} and Id={taskId}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(newDescription,newDescription));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
                    log.Info($"Description of {taskId} from board {boardID} was changed.");
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

        public bool ChangetState(int boardID, int taskId, int state)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_TaskTableName} set [{TaskDTO.TaskStatCol}]=@state where {TaskDTO.BoardIdCol}={boardID} and Id={taskId}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter("@state",state));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
                    log.Info($" State of {taskId} from board {boardID} was changed.");
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
        public bool ChangetDueDate(int boardID, int taskId, DateTime dueDate)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_TaskTableName} set [{TaskDTO.DueDateCol}]=@{dueDate.ToString()} where {TaskDTO.BoardIdCol}={boardID} and Id={taskId}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(dueDate.ToString(),dueDate.ToString()));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
                    log.Info($" Due date of {taskId} from board {boardID} was changed.");
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

        public bool ChangeAssignee(int boardID, int taskId, string userMail = null)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_TaskTableName} set [{TaskDTO.AssigneCol}]=@assigneeVal where {TaskDTO.BoardIdCol}={boardID} and Id={taskId}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(@"assigneeVal", userMail));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
                    log.Info($" Assignee of {taskId} from board {boardID} was changed to {userMail} in DB.");
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

        public bool Delete(TaskDTO task)
        {

            int res = -1;

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_TaskTableName} where id={task.TaskID} and {TaskDTO.BoardIdCol} = {task.BoardID}"

                };
                try
                {
                    log.Info($"Deleting from DB: Task {task.TaskID} from board {task.BoardID}...");
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"Task deleted succesfully.");

                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool DeleteAllTaskOfBoard(int boardID)
        {

            int res = -1;

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_TaskTableName} where {TaskDTO.BoardIdCol} = {boardID}"

                };
                try
                {
                    log.Info($"Deleting all Tasks from board {boardID}...");
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"Tasks of board {boardID} deleted succesfully.");

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
            List<TaskDTO> tasks = SelectAllTasks();
            foreach (TaskDTO task in tasks)
                Delete(task);
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            int taskID = reader.GetInt32(0);
            int boardID = reader.GetInt32(1);
            string dueDate = reader.GetString(2);
            string creationDate = reader.GetString(3);
            string title = reader.GetString(4);
            string description = reader.GetString(5);
            description = description.Replace('_', ' ');
            int state = reader.GetInt16(6);
            string assigneeMail = null;
            if (!reader.IsDBNull(7))
                 assigneeMail = reader.GetString(7);
            return new TaskDTO (boardID,taskID, dueDate, creationDate, title, description, state, assigneeMail);
        }
    }
}
