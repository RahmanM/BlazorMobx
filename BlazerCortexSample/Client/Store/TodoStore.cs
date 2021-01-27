namespace BlazerCortexSample.Client.Store
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlazerCortexSample.Client.Data;
    using BlazerCortexSample.Client.Models;
    using Cortex.Net.Api;

    /// <summary>
    /// Store of Todo items.
    /// </summary>
    [Observable]
    public class TodoStore
    {

        public TodoStore(ITodoService todoService)
        {
            TodoService = todoService;
        }

        /// <summary>
        /// Gets the Todo items.
        /// </summary>
        /// 
        public IList<Todo> Todos { get; private set; }

        /// <summary>
        /// Gets the number of active Todos.
        /// </summary>
        [Computed]
        public int ActiveCount => this.Todos.Count(x => !x.Completed);

        /// <summary>
        /// Gets the number of active Todos.
        /// </summary>
        [Computed]
        public int CompletedCount => this.Todos.Count - this.ActiveCount;

        [Computed]
        public Dictionary<string, int> UsersTodosCount => 
            Todos.GroupBy(g => g.UserName)
            .Select(t => new { User = t.Key, TodoCount = t.Count() })
            .ToDictionary(pair => pair.User ?? "Unknown User", pair => pair.TodoCount);

        public ITodoService TodoService { get; }

        /// <summary>
        /// Adds a todo item to the Store.
        /// </summary>
        /// <param name="title">The title of the new Todo item.</param>
        [Action]
        public void AddTodo(string title, string userId)
        {
            TodoService.AddTodo(title);

            var user = Todos.Where(u => u.UserName == userId).FirstOrDefault();

            var newTodo = new Todo()
            {
                Id = Todos.Count + 1,
                Title = title,
                Completed = false,
                Store = this,
                UserId = user.UserId,
                UserName = user.UserName
            };

            this.Todos.Insert(0, newTodo);
        }

          /// <summary>
        /// Toggles all items to the new completed state.
        /// </summary>
        /// <param name="completed">Whether the todo item is completed.</param>
        [Action]
        public void ToggleAll(bool completed)
        {
            TodoService.ToggleAll(completed);

            foreach (var todo in this.Todos)
            {
                todo.Completed = completed;
            }
        }

        /// <summary>
        /// Clears the store of the completed items.
        /// </summary>
        [Action]
        public void ClearCompleted()
        {
            TodoService.ClearCompleted();

            foreach (var completedItem in this.Todos.Where(x => x.Completed).ToList())
            {
                this.Todos.Remove(completedItem);
            }
        }


        [Action]
        public async Task LoadTodos()
        {
            var todos = await TodoService.LoadTodos();

            foreach (var todo in todos)
            {
                var newTodo = new Todo()
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Completed = todo.Completed,
                    Store = this,
                    UserId = todo.UserId,
                    UserName = todo.UserName
                };

                this.Todos.Add(newTodo);
            }
            
        }
    }
}
