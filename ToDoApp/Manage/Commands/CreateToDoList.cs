using MediatR;
using Dapper;
using ToDoApp.Manage.Data;
using ToDoApp.Shared;

namespace ToDoApp.Manage.Commands;

public class CreateToDoList {

    public record Command(string Name) : IRequest<ToDoListData>;

    public class Handler : IRequestHandler<Command, ToDoListData> {

        private readonly IDbConnectionFactory _factory;

        public Handler(IDbConnectionFactory factory) {
            _factory = factory;
        }

        public async Task<ToDoListData> Handle(Command request, CancellationToken cancellationToken) {

            const string query = @"INSERT INTO todolists (name) VALUES (@Name) RETURNING id;";

            var connection = _factory.CreateConnection();

            int newId = await connection.QuerySingleAsync<int>(query, request);

            return new ToDoListData() {
                Id = newId,
                Name = request.Name,
            };

        }

    }

}
