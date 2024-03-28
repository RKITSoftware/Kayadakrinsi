namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Interface for implementing Transient service lifetime
    /// </summary>
    public interface ITransient
    {
        /// <summary>
        /// Retrives guid of service
        /// </summary>
        /// <returns>Guid of service</returns>
        public Guid Get();
    }
}
