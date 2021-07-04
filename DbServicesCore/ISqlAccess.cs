using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbQueryApp.DbServicesCore
{
    public interface ISqlAccess
    {
        List<T> LoadData<T, U>(string sql, U parameters) where T : class;
        int ManipulateData<T, U>(string sql, U parameters) where T : class;
    }
}
