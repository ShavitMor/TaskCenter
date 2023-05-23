using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public abstract class DataMapper
    {
        protected readonly string _connectionstring;
        private readonly string _tableName;
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataMapper(string tableName)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Kanban.db"));
            this._connectionstring = $"Data Source={path};Version=3";
            this._tableName = tableName;
        }
        public string GetConnectionString()
        {
            return this._connectionstring;
        }
        public bool Update(long id, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeName} where id={id}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
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
        public bool Update(long id, string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{attributeName}]=@{attributeName} where id={id}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
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

        public bool Update(long id, string findAtrribute, int findAttributeValue, string updateAttribute, int updateValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{updateAttribute}]=@{updateValue} where id={id} and {findAtrribute}={findAttributeValue}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(updateAttribute, updateValue));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
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

        public bool Update(long id, string findAtrribute, int findAttributeValue, string updateAttribute, string updateValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"update {_tableName} set [{updateAttribute}]=@{updateValue} where id={id} and {findAtrribute}={findAttributeValue}"
                };
                try
                {
                    cmd.Parameters.Add(new SQLiteParameter(updateAttribute, updateValue));
                    connection.Open();
                    res = cmd.ExecuteNonQuery();
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
        public virtual bool Delete(DTO DTOObj)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionstring))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where id={DTOObj.Id}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public virtual int DeleteAll()
        {
            int res = -1;
            log.Debug($"Deleting All data from table {_tableName}...");
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {_tableName};"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res;
        }

        protected List<DTO> Select()
        {
            List<DTO> results = new List<DTO>();
            using (var connection = new SQLiteConnection(_connectionstring))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

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
            return results;
        }

        protected abstract DTO ConvertReaderToObject(SQLiteDataReader reader);
    }
}
