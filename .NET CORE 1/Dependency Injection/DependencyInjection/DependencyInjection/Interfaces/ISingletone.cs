namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Interface for implementing Singletone service lifetime
    /// </summary>
    public interface ISingletone
    {
        /// <summary>
        /// Retrives guid of service
        /// </summary>
        /// <returns>Guid of service</returns>
        public Guid Get();
    }
}
