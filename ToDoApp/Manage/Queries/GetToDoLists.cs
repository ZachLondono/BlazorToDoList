using MediatR;
using Dapper;
using ToDoApp.Manage.Data;
using ToDoApp.Shared;

namespace ToDoApp.Manage.Queries;

public class GetToDoLists {

    public record Query() : IRequest<IEnumerable<ToDoListData>>;

    public class Handler : IRequestHandler<Query, IEnumerable<ToDoListData>> {

        private readonly IDbConnectionFactory _factory;

        public Handler(IDbConnectionFactory factory) {
            _factory = factory;
        }

        public async Task<IEnumerable<ToDoListData>> Handle(Query request, CancellationToken cancellationToken) {

            const string query = @"SELECT ""Id"", ""Name"" FROM ""ToDoLists"";";

            var connection = _factory.CreateConnection();

            return await connection.QueryAsync<ToDoListData>(query);

        }

    }


}
