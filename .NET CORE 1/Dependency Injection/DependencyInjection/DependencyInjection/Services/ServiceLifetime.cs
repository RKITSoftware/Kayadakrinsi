using DependencyInjection.Interfaces;

namespace DependencyInjection.Services
{
    /// <summary>
    /// Implements interfaces for testing various kinds of service lifetimes
    /// </summary>
    public class ServiceLifetime : ISingletone, ITransient, IScopped
    {
        /// <summary>
        /// Declares Guid for tracking service lifetime
        /// </summary>
        private readonly Guid _id;

        /// <summary>
        /// Initializes Guid each time service created/recreated
        /// </summary>
        public ServiceLifetime()
        {
            _id = Guid.NewGuid();
        }

        /// <summary>
        /// Retrives guid of service
        /// </summary>
        /// <returns>Guid of service</returns>
        public Guid Get()
        {
            return _id;
        }
    }
}
