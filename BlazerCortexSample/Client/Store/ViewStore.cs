namespace BlazerCortexSample.Client.Store
{
    using BlazerCortexSample.Client.Models;
    using Cortex.Net.Api;

    /// <summary>
    /// Stores main todo view state.
    /// </summary>
    [Observable]
    public class ViewStore
    {
        /// <summary>
        /// Gets or sets the Todo being edited.
        /// </summary>
        public Todo TodoBeingEdited { get; set; }

        /// <summary>
        /// Gets or sets the Todo filter.
        /// </summary>
        public TodoFilter TodoFilter { get; set; }
    }
}
