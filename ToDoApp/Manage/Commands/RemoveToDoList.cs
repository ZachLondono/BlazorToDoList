using MediatR;
using Dapper;
using ToDoApp.Shared;

namespace ToDoApp.Manage.Commands;

public class RemoveToDoList {

    public record Command(int ListId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly IDbConnectionFactory _factory;

        public Handler(IDbConnectionFactory factory) {
            _factory = factory;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            const string query = @"DELETE FROM ""ToDoLists"" WHERE ""Id"" = @ListId;";

            var connection = _factory.CreateConnection();

            await connection.ExecuteAsync(query, request);

        }

    }

}
