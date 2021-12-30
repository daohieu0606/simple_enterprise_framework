using MySql.Data.MySqlClient;

namespace Core.Query
{
    interface IMySqlQuery
    {
        MySqlCommand GetQuery();
    }
}

