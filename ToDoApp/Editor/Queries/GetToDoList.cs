﻿using MediatR;
using Dapper;
using ToDoApp.Editor.Data;
using ToDoApp.Shared;

namespace ToDoApp.Editor.Queries;

public class GetToDoList {

    public record Query(int ListId) : IRequest<ToDoListData>;

    public class Handler : IRequestHandler<Query, ToDoListData> {

        private readonly IDbConnectionFactory _factory;

        public Handler(IDbConnectionFactory factory) {
            _factory = factory;
        }

        public async Task<ToDoListData> Handle(Query request, CancellationToken cancellationToken) {

            const string listQuery = @"SELECT ""Id"", ""Name"" FROM ""ToDoLists"" WHERE ""Id"" = @ListId;";
            const string itemQuery = @"SELECT ""Id"", ""Name"", ""IsDone"" FROM ""ToDoListItems"" WHERE ""ToDoListId"" = @ListId;";

            var connection = _factory.CreateConnection();

            var list = await connection.QuerySingleAsync<ToDoListData>(listQuery, request);
            var items = await connection.QueryAsync<ToDoListItemData>(itemQuery, request);

            list.Items = items;

            return list;

        }

    }

}
