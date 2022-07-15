using Npgsql;
using System.Data;

namespace ToDoApp.Shared;

public class NpgsqlConnectionFactory : IDbConnectionFactory {

    private IConfiguration _config;

    public NpgsqlConnectionFactory(IConfiguration config) {
        _config = config;
    }

    public IDbConnection CreateConnection() {
        string connectionString = _config.GetConnectionString("ToDoDatabase");
        return new NpgsqlConnection(connectionString);
    }

}
