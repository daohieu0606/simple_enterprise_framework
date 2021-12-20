using System;
using System.Collections.Generic;

namespace Core.Database
{
    public interface IDatabase
    {
        bool OpenConnection();
        bool CloseConnection();
        IList<string> GetAllTableNames();
    }
}
