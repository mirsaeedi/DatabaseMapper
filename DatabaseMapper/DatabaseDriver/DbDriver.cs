using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.DatabaseDriver
{
    public abstract class DbDriver
    {
        protected DbConnectionString _dbConnectionString;
        protected bool _hasPersistentConnection=false;
        protected DbConnection _dbConnection;
        DbTransaction _transaction;

        public virtual void ExecuteNonQuery(string query)
        {
            DbConnection connection = GetConnection();
            DbCommand command = connection.CreateCommand();
            command.Transaction = _transaction;
            command.CommandText = query;
            command.Connection = connection;

            if(connection.State!=ConnectionState.Open)
                connection.Open();

            command.ExecuteNonQuery();

            CloseConnection(connection);
        }

        public virtual object ExecuteScalar(string query)
        {
            DbConnection connection = GetConnection();
            DbCommand command = connection.CreateCommand();
            command.Transaction = _transaction;
            command.CommandText = query;
            command.Connection = connection;

            if (connection.State != ConnectionState.Open)
                connection.Open();

            object value = command.ExecuteScalar();

            CloseConnection(connection);

            return value;
        }

        public virtual DbDataReader ExecuteQuery(string query)
        {
            DbDataReader dataReader = null;
            DbConnection connection = GetConnection();
            DbCommand command = connection.CreateCommand();
            command.Transaction = _transaction;
            command.CommandText = query;
            command.Connection = connection;

            if (connection.State != ConnectionState.Open)
                connection.Open();

            dataReader = command.ExecuteReader();

            return dataReader;
        }

        public void BeginTransaction()
        {
            if (!_hasPersistentConnection)
                throw new Exception("NO PERSISTENT CONNECTION");
            
            if(_transaction!=null)
                throw new Exception("NO TRANSACTION IN MIDDLE OF ANOTHER TRANSACTION");

            if (_dbConnection == null)
            {
                DbConnection connection = GetConnection();

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        public void EndTransaction()
        {
            _transaction.Commit();
            _transaction = null;
        }

        private DbConnection GetConnection()
        {
            if (_hasPersistentConnection)
            {
                if (_dbConnection == null)
                    _dbConnection = CreateConnection();

                return _dbConnection;
            }

             _dbConnection=CreateConnection();
             return _dbConnection;
        }

        private DbConnection CreateConnection()
        {
            return _dbConnectionString.GetConnection();
        }

        public void ExecuteQueryFromFile(string filePath)
        {
            string query = GetQuery(filePath);
            DbConnection connection = GetConnection();
            DbCommand command = connection.CreateCommand();
            command.Transaction = _transaction;
            command.CommandText = query;
            command.Connection = connection;

            if (connection.State != ConnectionState.Open)
                connection.Open();

            command.ExecuteNonQuery();

            CloseConnection(connection);
        }

        private void CloseConnection(DbConnection connection)
        {
            if (!_hasPersistentConnection)
                connection.Close();
        }

        public void Close()
        {
            _dbConnection.Close();
        }

        private string GetQuery(string filePath)
        {
            StreamReader sr = new StreamReader(filePath, Encoding.UTF8);

            string query = sr.ReadToEnd();
            sr.Close();
            return query;
        }


        public static DbDriver CreateDriver(DbConnectionString dbConnectionString)
        {
            if (dbConnectionString is PostgreSqlConnectionString)
                return new PostgreSqlDriver(dbConnectionString);

            if (dbConnectionString is AccessConnectionString)
                return new AccessDriver(dbConnectionString);

            if (dbConnectionString is FoxProConnectionString)
                return new FoxProDriver(dbConnectionString);

            return null;
        }

        public bool HasPersistentConnection
        {
            get
            {
                return _hasPersistentConnection;
            }
            set
            {
                _hasPersistentConnection = value;
            }
        }
    }
}
