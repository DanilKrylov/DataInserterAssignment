using Dapper;
using DataInserter.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter.Database
{
    internal class DbDataSender : IDbDataSender
    {
        private readonly IDbConnection _connection;
        private bool _disposed = false;

        public DbDataSender(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            _disposed = true;
            _connection.Dispose();
        }

        public async Task<int> InsertDataAsync<T>(string query, IEnumerable<T> data)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (_connection.State != ConnectionState.Open)
                throw new InvalidOperationException("Connection is not open");

            using var transaction = _connection.BeginTransaction();
            try
            {
                var rowsAffected = await _connection.ExecuteAsync(query, data, transaction);
                transaction.Commit();
                return rowsAffected;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
