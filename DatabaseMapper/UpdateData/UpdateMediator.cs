using DatabaseMapper.DatabaseDriver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMapper.UpdateData
{
    public abstract class UpdateMediator
    {
        public DbConnectionString _dbSourceConnectionString;
        public DbConnectionString _dbDestinationConnectionString;

        private DbDriver _destinationDriver = null;
        private DbDriver _sourceDriver = null;

        private Dictionary<int, string> _dicUniqueId = new Dictionary<int, string>();
        private StringBuilder _batchCommand = new StringBuilder(1000000);
        private int _convertedCount;

        internal  UpdateMediator(DbConnectionString sourceConnectionString, DbConnectionString destinationConnectionString)
        {
            _dbDestinationConnectionString = destinationConnectionString;
            _dbSourceConnectionString = sourceConnectionString;

            _sourceDriver = DbDriver.CreateDriver(_dbSourceConnectionString);
            _sourceDriver.HasPersistentConnection = true;
            
            _destinationDriver= DbDriver.CreateDriver(_dbDestinationConnectionString);
            _destinationDriver.HasPersistentConnection = true;
        }

        internal void Update()
        {
            Dictionary<int, string> dicDestinationUniqueId = GetListOfUniqeIdFromDestination();

            DbDataReader dataReader = GetSourceRecords();
            UpdateDestinationTable(dataReader, dicDestinationUniqueId);
        }

        private void UpdateDestinationTable(DbDataReader dataReader, Dictionary<int, string> dicDestinationUniqueId)
        {
            _convertedCount = 0;
            int id = 0;
            while(dataReader.Read()){


                id = GetSourceUniqueId(dataReader);

                if (dicDestinationUniqueId.ContainsKey(id))
                {
                    try
                    {

                        UpdateApplicantInfoRecord(dataReader);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                    
                }
                else
                {
                    try
                    {

                        InsertApplicantInfoRecord(dataReader);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }

            

            ExecuteBatchCommand();

            _destinationDriver.Close();
            _sourceDriver.Close();
        }

        private void ExecuteBatchCommand()
        {
            _destinationDriver.ExecuteNonQuery(_batchCommand.ToString());
            _batchCommand.Clear();
        }

        private void InsertApplicantInfoRecord(DbDataReader dataReader)
        {
            string query= InsertDestinationRecord(dataReader);

            if (query != null)
                ExecuteCommand(query);
        }

        private void ExecuteCommand(string query)
        {
            AppendCommand(query);

            _convertedCount++;
            if (_convertedCount % 5000 == 0)
            {
                Console.WriteLine(_convertedCount);
                ExecuteBatchCommand();
            }       
        }

        private void UpdateApplicantInfoRecord(DbDataReader dataReader)
        {

            string query= UpdateDestinationRecord(dataReader);
            
            if(query!=null)
                ExecuteCommand(query);
        }

        private DbDataReader GetSourceRecords()
        {
            DbDataReader dataReader = _sourceDriver.ExecuteQuery(SourceReadQuery);
            return dataReader; 
        }

        protected void AppendCommand(string command)
        {
            _batchCommand.Append(command);
        }

        private Dictionary<int, string> GetListOfUniqeIdFromDestination()
        {
            Dictionary<int, string> dicUniqueId = new Dictionary<int, string>();

            if (DestinationReadUniqueQuery != null)
            {
                DbDataReader dataReader = _destinationDriver.ExecuteQuery(DestinationReadUniqueQuery);

                while (dataReader.Read())
                {
                    dicUniqueId.Add(dataReader.GetInt32(0), null);
                }

                dataReader.Close();
            }

            return dicUniqueId;
        }

        protected string SourceReadQuery { get; set; }

        protected string DestinationReadUniqueQuery { get; set; }

        protected abstract int GetSourceUniqueId(DbDataReader dataReader);

        protected abstract string UpdateDestinationRecord(DbDataReader dataReader);

        protected abstract string InsertDestinationRecord(DbDataReader dataReader);

    }
}
