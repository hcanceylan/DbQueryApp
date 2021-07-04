using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace DbQueryApp.DbServicesCore
{
    public class SqlAccess : DbContext, ISqlAccess
    {

        private DbConnection _connection;
        public SqlAccess(DbContextOptions<SqlAccess> options) : base(options)
        {
            //SqlAccess Class is inherited from DbContext, DbContext is configured in Startup.cs as transient with options.
            //Options exist in appsettings.json. For now, there is only one option (Connection String)
            _connection = Database.GetDbConnection();
            //Connection field gets the connection reference from DbContext when the dependency of SqlAccess(DbContext) injected.
        }

        public int ManipulateData<T, U>(string sql, U parameters) where T : class
        {
            return _connection.Execute(sql, parameters); //This is a dapper generic method for execute insert-delete-update operations with parameters
        }

        public List<T> LoadData<T, U>(string sql, U parameters) where T : class
        {
            return _connection.Query<T>(sql, parameters).ToList(); //This is a special dapper generic method for execute select queries with parameters
        }

    }
}
