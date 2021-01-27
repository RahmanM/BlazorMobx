namespace BlazerCortexSample.Client.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using BlazerCortexSample.Client.Store;
    using Cortex.Net.Api;

    /// <summary>
    /// Represents a model of a Todo.
    /// </summary>
    public class Todo
    {
        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        [JsonIgnore]
        public TodoStore Store { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        [Observable]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Todo item is completed.
        /// </summary>
        [Observable]
        public bool Completed { get; set; }

        [Observable]

        public int UserId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// Toggles this item for completion.
        /// </summary>
        [Action]
        public void Toggle()
        {
            this.Completed = !this.Completed;
        }

        /// <summary>
        /// Destroys this todo by removing it from the Store.
        /// </summary>
        public void Destroy()
        {
            this.Store.Todos.Remove(this);
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }


    }

    /// <summary>
    /// Enumeration for Todo filters.
    /// </summary>
    public enum TodoFilter
    {
        /// <summary>
        /// All todo items are displayed.
        /// </summary>
        All,

        /// <summary>
        /// Only active todo items are displayed.
        /// </summary>
        Active,

        /// <summary>
        /// Only completed Todo items are displayed.
        /// </summary>
        Completed,
    }
}
