using System.Threading.Tasks;

namespace Rebus.Workers.ThreadPoolBased
{
    /// <summary>
    /// Implements a strategy with which workers will back off in idle periods. Please note that the <see cref="IBackoffStrategy"/>
    /// implementations must be reentrant!
    /// </summary>
    public interface IBackoffStrategy
    {
        /// <summary>
        /// Executes the next wait operation by blocking the thread, possibly advancing the wait cursor to a different wait time for the next time.
        /// This function is called each time a worker thread cannot continue because no more parallel operations are allowed to happen.
        /// </summary>
        void Wait();

	    /// <summary>
	    /// Executes the next wait operation by blocking the thread, possibly advancing the wait cursor to a different wait time for the next time.
	    /// This function is called each time a worker thread cannot continue because no more parallel operations are allowed to happen.
	    /// </summary>
	    Task WaitAsync();

		/// <summary>
		/// Executes the next wait operation by blocking the thread, possibly advancing the wait cursor to a different wait time for the next time.
		/// This function is called each time no message was received.
		/// </summary>
		void WaitNoMessage();

	    /// <summary>
	    /// Executes the next wait operation by blocking the thread, possibly advancing the wait cursor to a different wait time for the next time.
	    /// This function is called each time no message was received.
	    /// </summary>
	    Task WaitNoMessageAsync();

		/// <summary>
		/// Blocks the thread for a (most likely longer) while, when an error has occurred
		/// </summary>
		void WaitError();

	    /// <summary>
	    /// Blocks the thread for a (most likely longer) while, when an error has occurred
	    /// </summary>
	    Task WaitErrorAsync();

		/// <summary>
		/// Resets the strategy. Is called whenever a message was received.
		/// </summary>
		void Reset();
    }
}