using System;
using Core.Database;
using IoC.DI;

namespace Core.Utils
{
    public enum DatabaseType{
        None,
        MySql,
        Postgres,
        Oracle,
    }

    public class CurrentFrameworkState
    {
        #region instance - ctor
        private static CurrentFrameworkState _instance;

        public static CurrentFrameworkState Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CurrentFrameworkState();

                return _instance;
            }
        }

        private CurrentFrameworkState()
        {
            //do nothing
        }
        #endregion

        public DatabaseType DatabaseType { get; private set; }

        public void ChangeDataBase(
            DatabaseType databaseType,
            string host,
            string dbName,
            string username,
            string password)
        {
            //check and close current database
            if (ServiceLocator.Instance.Get<IDatabase>() != null)
            {
                try
                {
                    ServiceLocator.Instance.Get<IDatabase>().CloseConnection();
                }
                catch(Exception e)
                {
                    //do nothing
                }
            }

            switch(databaseType)
            {
                case DatabaseType.MySql:
                    ServiceLocator.Instance.Register<IDatabase, MySqlDatabase>(host, dbName, username, password);
                    break;

                case DatabaseType.Postgres:
                    ServiceLocator.Instance.Register<IDatabase, PostgreDatabase>(host, dbName, username, password);
                    break;

                case DatabaseType.Oracle:
                    ServiceLocator.Instance.Register<IDatabase, OracleDatabase>(host, dbName, username, password);
                    break;

                case DatabaseType.None:
                default:
                    return;
            }

            this.DatabaseType = databaseType;
        }
    }
}
