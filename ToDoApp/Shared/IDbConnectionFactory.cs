using System.Data;

namespace ToDoApp.Shared;

public interface IDbConnectionFactory {

    IDbConnection CreateConnection();

}
