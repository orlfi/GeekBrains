using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using BankCards.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BankCards.DAL
{
    public class ConnectionManager : IDisposable, IConnectionManager
    {
        private readonly string _connectionString;

        private readonly object _lockObject = new object();
        private DbConnection _connection;

        public ConnectionManager(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }

        public DbConnection GetOpenedConnection()
        {
            lock (_lockObject)
            {
                if (_connection is null)
                {
                    _connection = new SqlConnection(_connectionString);
                    _connection.Open();
                }
            }
            return _connection;
        }


        public void Dispose()
        {
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}