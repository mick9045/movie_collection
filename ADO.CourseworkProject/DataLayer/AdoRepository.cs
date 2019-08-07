namespace FilmRent.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.SqlClient;
    using FilmRent.DataLayer.Abstract;
    using FilmRent.DataLayer.Exceptions;

    public abstract class AdoRepository<T> where T : IEntity
    {
        #region Fields
        private static SqlConnection _connection;
        private bool _openedConnection;
        #endregion //Fields

        #region Constructors
        public AdoRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _openedConnection = false;
        }
        #endregion //Constructors

        #region Methods
        abstract public T PopulateRecord(SqlDataReader reader);

        protected void OpenConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
            {
                return;
            }
            _connection.Open();
        }

        protected void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                return;
            }
            _connection.Close();
        }

        protected IEnumerable<T> GetRecords(SqlCommand command)
        {
            List<T> list = new List<T>();
            command.Connection = _connection;
            if (_openedConnection == false)
            {
                _connection.Open();
            }
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (SqlException e)
            {
                if (isPermissionError(e))
                {
                    throw new NoPermissionException("No permission", e);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (_openedConnection == false)
                {
                    _connection.Close();
                }
            }
            return list;
        }

        protected T GetRecord(SqlCommand command)
        {
            T record = default(T);
            command.Connection = _connection;
            if (_openedConnection == false)
            {
                _connection.Open();
            }
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    record = PopulateRecord(reader);
                    break;
                }
            }
            catch (SqlException e)
            {
                if (isPermissionError(e))
                {
                    throw new NoPermissionException("No permission", e);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (_openedConnection == false)
                {
                    _connection.Close();
                }
            }

            return record;
        }

        protected IEnumerable<T> ExecuteStoredProcedure(SqlCommand command)
        {
            List<T> list =  new List<T>();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = _connection;
            if (_openedConnection == false)
            {
                _connection.Open();
            }
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader);
                        if (record != null) list.Add(record);
                    }
                }
                catch
                {
                    reader.Close();
                }
            }
            catch (SqlException e)
            {
                if (isPermissionError(e))
                {
                    throw new NoPermissionException("No permission", e);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (_openedConnection == false)
                {
                    _connection.Close();
                }
            }
            return list;
        }

        private bool isPermissionError(SqlException e)
        {
            switch (e.Number)
            {
                case 219:
                case 229:
                case 320:
                case 297:
                case 300:
                case 262:
                    return true;
            }
            return false;
        }
        #endregion //Methods
    }
}
