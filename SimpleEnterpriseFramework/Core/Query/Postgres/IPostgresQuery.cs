using Npgsql;

namespace Core.Query
{
    interface IPostgresQuery
    {
        NpgsqlCommand GetQuery();
    }
}

