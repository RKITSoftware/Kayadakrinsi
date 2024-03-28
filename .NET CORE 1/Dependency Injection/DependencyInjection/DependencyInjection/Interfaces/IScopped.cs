namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Interface for implementing Scopped service lifetime
    /// </summary>
    public interface IScopped
    {
        /// <summary>
        /// Retrives guid of service
        /// </summary>
        /// <returns>Guid of service</returns>
        public Guid Get();
    }
}
