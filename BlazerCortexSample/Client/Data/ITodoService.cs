using BlazerCortexSample.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazerCortexSample.Client.Data
{
    public interface ITodoService
    {
        void AddTodo(string title);
        List<Todo> ClearCompleted();
       // List<Todo> LoadTodos();
        List<Todo> ToggleAll(bool completed);

        Task<List<Todo>> LoadTodos();
    }
}