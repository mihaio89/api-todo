/*using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class TodoApiTests : WebApplicationFactory
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        // Use WebApplicationFactory to create the client
        _client = CreateClient();
        SeedDatabase();
    }

    private void SeedDatabase()
    {
        using (var scope = Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<TodoDb>();
            db.Database.EnsureCreated();
            db.TodoItems.RemoveRange(db.TodoItems); // Clear existing data

            db.TodoItems.AddRange(new List<TodoItem>
            {
                new TodoItem { Id = 1, IsCompleted = true, Description = "Completed Task 1" },
                new TodoItem { Id = 2, IsCompleted = false, Description = "Incomplete Task 2" },
                new TodoItem { Id = 3, IsCompleted = true, Description = "Completed Task 3" },
                new TodoItem { Id = 4, IsCompleted = false, Description = "Incomplete Task 4" }
            });
            db.SaveChanges();
        }
    }

    [Test]
    public async Task GetCompletedTodos_ReturnsCompletedTodos()
    {
        // Act
        var response = await _client.GetAsync("/completed");

        // Assert
        response.EnsureSuccessStatusCode();
        var todos = await response.Content.ReadFromJsonAsync<List<TodoItem>>();
        Assert.IsNotNull(todos);
        Assert.AreEqual(2, todos.Count); // Two completed todos in the test data
    }

    [Test]
    public async Task GetNotCompletedTodos_ReturnsNotCompletedTodos()
    {
        // Act
        var response = await _client.GetAsync("/notcompleted");

        // Assert
        response.EnsureSuccessStatusCode();
        var todos = await response.Content.ReadFromJsonAsync<List<TodoItem>>();
        Assert.IsNotNull(todos);
        Assert.AreEqual(2, todos.Count); // Two not completed todos in the test data
    }

    [Test]
    public async Task GetCompletedTodos_ReturnsNoContent_WhenNoCompletedTodos()
    {
        // Act
        var response = await _client.GetAsync("/completed");

        // Assert
        Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }

    [Test]
    public async Task GetNotCompletedTodos_ReturnsNoContent_WhenNoNotCompletedTodos()
    {
        // Act
        var response = await _client.GetAsync("/notcompleted");

        // Assert
        Assert.AreEqual(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }
}
*/