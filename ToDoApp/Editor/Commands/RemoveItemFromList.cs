using MediatR;
using Dapper;
using ToDoApp.Shared;

namespace ToDoApp.Editor.Commands;

public class RemoveItemFromList {

    public record Command(int ItemId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly IDbConnectionFactory _factory;

        public Handler(IDbConnectionFactory factory) {
            _factory = factory;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            const string query = @"DELETE FROM toDoListItems WHERE id = @ItemId;";

            var connection = _factory.CreateConnection();

            await connection.ExecuteAsync(query, request);

        }

    }

}
