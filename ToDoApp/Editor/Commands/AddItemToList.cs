using MediatR;
using Dapper;
using ToDoApp.Editor.Data;
using ToDoApp.Shared;

namespace ToDoApp.Editor.Commands;

public class AddItemToList {

    public record Command(int ListId, string ItemName) : IRequest<ToDoListItemData>;

    public class Handler : IRequestHandler<Command, ToDoListItemData> {

        private readonly IDbConnectionFactory _factory;

        public Handler(IDbConnectionFactory factory) {
            _factory = factory;
        }

        public async Task<ToDoListItemData> Handle(Command request, CancellationToken cancellationToken) {

            const string query = @"INSERT INTO toDoListItems (name, toDoListId) VALUES (@ItemName, @ListId) RETURNING id;";

            var connection = _factory.CreateConnection();

            var newId = await connection.QuerySingleAsync<int>(query, request);

            return new() {
                Id = newId,
                IsDone = false,
                Name = request.ItemName
            };

        }

    }

}
