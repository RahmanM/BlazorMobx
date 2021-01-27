using BlazerCortexSample.Client.Models;
using BlazerCortexSample.Client.Store;
using Blazored.LocalStorage;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazerCortexSample.Client.Data
{
    // This is just a simulation of a service, in reality this should call the End Point API of the server and do the stuff ...

    public class TodoService : ITodoService
    {

        public List<Todo> Todos { get; set; }
        public HttpClient Http { get; }
        public string BaseUrl { get; set; } = "https://jsonplaceholder.typicode.com";

        public TodoService(HttpClient http)
        {
            Todos = new List<Todo>();
            Http = http;
        }

        public void AddTodo(string title)
        {
            var newTodo = new Todo()
            {
                Id = Todos.Count + 1,
                Title = title,
                Completed = false,
            };

            Todos.Add(newTodo);
        }

        public List<Todo> ToggleAll(bool completed)
        {
            foreach (var todo in Todos)
            {
                todo.Completed = completed;
            }

            return Todos.ToList();
        }

        public List<Todo> ClearCompleted()
        {
            foreach (var completedItem in Todos.Where(x => x.Completed).ToList())
            {
                Todos.Remove(completedItem);
            }

            return Todos.ToList();
        }


        // NOTE: This won't work e.g. will loadd todos the computed will work but adding new Todos won't show the list
        public async Task<List<Todo>> LoadTodos()
        {
            var usersTask = Http.GetFromJsonAsync<List<User>>($"{BaseUrl}/users");
            var todosTask = Http.GetFromJsonAsync<List<Todo>>($"{BaseUrl}/todos");

            await Task.WhenAll(usersTask, todosTask);

            var users = usersTask.Result;
            var todos = todosTask.Result;

            var list = todos.Take(10).ToList();

            foreach (var todo in list)
            {
                todo.UserName = users.Where(u => u.Id == todo.Id).FirstOrDefault().Name ;
            }

            return list;
        }

    }

    // NOTE: Graphql doesn't work because of CORS not enabled in server

    //var graphQLClient = new GraphQLHttpClient("https://graphqlzero.almansi.me/api/", new NewtonsoftJsonSerializer());
    //var todosRequest =  new GraphQLRequest
    //{
    //    Query = @"
    //    {
    //     todos {
    //        data {
    //          id
    //          title
    //          completed
    //          user{
    //              id
    //              name
    //          }
    //        }
    //      }
    //    }"
    //};
    //var response = await graphQLClient.SendQueryAsync<IList<Todo>>(todosRequest);
    //return response.Data.ToList();
}
