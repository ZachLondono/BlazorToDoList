using MediatR;
using Dapper;
using ToDoApp.Editor.Data;
using ToDoApp.Shared;

namespace ToDoApp.Editor.Commands;

public class ToggleItem {

    public record Command(ToDoListItemData Item) : IRequest<bool>;

    public class Handler : IRequestHandler<Command,bool> {

        private readonly IDbConnectionFactory _factory;

        public Handler(IDbConnectionFactory factory) {
            _factory = factory;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken) {
            
            const string query = @"UPDATE toDoListItems
                                    SET isDone = NOT isDone
                                    WHERE id = @Id
                                    RETURNING isDone;";

            var connection = _factory.CreateConnection();

            var newState = await connection.QuerySingleAsync<bool>(query, new { request.Item.Id });

            return newState;

        }

    }

}
