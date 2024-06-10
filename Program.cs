using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//adds/registers the TodoDb context to/with the DI container, using an in-memory database
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
//enables display of db related exceptions
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//enables the API Explorer, which is a service that provides metadata about the HTTP API.
//it's used by Swagger to generate the Swagger document.
builder.Services.AddEndpointsApiExplorer();
//Adds the Swagger OpenAPI document generator to the application services and configures it to provide more information about the API,
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

//enables (only in Development environment) the Swagger middleware for serving the generated JSON document and the Swagger UI
//enabling Swagger in a production environment could expose potentially sensitive details about the API's structure and implementation.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapGet("/greeting/en", () => "Hello World!");
app.MapGet("/greeting/de", () => "Hallo Welt!");
app.MapGet("/greeting/es", greeting_es);
string greeting_es()
{
    string msg = "Holla Mundo!";
    return msg;
}

app.MapGet("/todoitems", async (TodoDb db) =>
    await db.TodoItems.ToListAsync()
);

app.MapPost("/todoitems", async (TodoItem item, TodoDb db ) =>
{
    db.TodoItems.Add(item);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{item.Id}", item);
});

app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.TodoItems.FindAsync(id)
        is TodoItem item
            ? Results.Ok(item)
            : Results.NotFound(new { message = "Item not found" })
);

app.MapGet("/v2/todoitems/{id}", async (int id, TodoDb db) =>
{
    var item = await db.TodoItems.FindAsync(id);
    if (item != null)
    {
        return Results.Ok(item);
    }
    else
    {
        return Results.NotFound();
    }
}
);

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.TodoItems.FindAsync(id) is TodoItem item)
    {
        db.TodoItems.Remove(item);
        await db.SaveChangesAsync();
        //return Results.NoContent();
        return Results.Ok(new { message = $"Item with ID {id} deleted" });
    }

    return Results.NotFound(new { message = "Item not found to be deleted" });
});

//Use the MapGroup API
var todos = app.MapGroup("/v2/todos");

todos.MapGet("/", async (TodoDb db) =>
    await db.TodoItems.ToListAsync());

todos.MapGet("/compl", async (TodoDb db) =>
    await db.TodoItems.Where(t => t.IsCompleted).ToListAsync());

todos.MapPut("/{id}", async (int id, TodoItem inputTodo, TodoDb db) =>
{
    var todo = await db.TodoItems.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Description = inputTodo.Description;
    todo.IsCompleted = inputTodo.IsCompleted;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

//Use the TypedResults API
todos.MapGet("/completed", GetCompletedTodos);
static async Task<IResult> GetCompletedTodos(TodoDb db)
{
    return TypedResults.Ok(await db.TodoItems.Where(t => t.IsCompleted).ToListAsync());
}

todos.MapGet("/notcompleted", GetNotCompletedTodos);
static async Task<IResult> GetNotCompletedTodos(TodoDb db)
{
    return TypedResults.Ok(await db.TodoItems.Where(t => !t.IsCompleted).ToListAsync());
}


app.Run();