using MediatR;
using Dapper;
using ToDoApp.Editor.Data;
using ToDoApp.Shared;

namespace ToDoApp.Editor.Commands;

public class RenameItem {

    public record Command(ToDoListItemData Item) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly IDbConnectionFactory _factory;

        public Handler(IDbConnectionFactory factory) {
            _factory = factory;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            const string query = @"UPDATE toDoListItems
                                    SET name = @Name
                                    WHERE id = @Id;";

            var connection = _factory.CreateConnection();

            await connection.ExecuteAsync(query, request.Item);

        }
    }

}