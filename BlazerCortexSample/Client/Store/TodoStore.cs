namespace BlazerCortexSample.Client.Store
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BlazerCortexSample.Client.Models;
    using Cortex.Net.Api;

    /// <summary>
    /// Store of Todo items.
    /// </summary>
    [Observable]
    public class TodoStore
    {

        public TodoStore()
        {
            //if(this.Todos == null || this.Todos.Count == 0)
            //{
            //    this.LoadTodos();
            //}
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

        /// <summary>
        /// Adds a todo item to the Store.
        /// </summary>
        /// <param name="title">The title of the new Todo item.</param>
        [Action]
        public void AddTodo(string title)
        {
            var newTodo = new Todo()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Completed = false,
                Store = this,
            };
            this.Todos.Add(newTodo);
        }

        [Action]
        public void AddTodo(Todo todo)
        {
            this.Todos.Add(todo);
        }

        /// <summary>
        /// Toggles all items to the new completed state.
        /// </summary>
        /// <param name="completed">Whether the todo item is completed.</param>
        [Action]
        public void ToggleAll(bool completed)
        {
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
            foreach (var completedItem in this.Todos.Where(x => x.Completed).ToList())
            {
                this.Todos.Remove(completedItem);
            }
        }


        // NOTE: This won't work e.g. will loadd todos the computed will work but adding new Todos won't show the list
        [Action]
        public void LoadTodos()
        {
            var todos = new List<Todo>();
            todos.Add(new Todo() { Id = Guid.NewGuid(), Completed = false, Title = "Todo 1", Store = this });
            todos.Add(new Todo() { Id = Guid.NewGuid(), Completed = true, Title = "Todo 2", Store = this });
            todos.Add(new Todo() { Id = Guid.NewGuid(), Completed = false, Title = "Todo 3", Store = this });
            todos.Add(new Todo() { Id = Guid.NewGuid(), Completed = false, Title = "Todo 4", Store = this });
            todos.Add(new Todo() { Id = Guid.NewGuid(), Completed = false, Title = "Todo 5", Store = this });


            this.Todos = todos;
        }
    }
}
