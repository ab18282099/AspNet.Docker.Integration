using Autofac;

namespace AspNet.Docker.Integration.Helper
{
    /// <summary>
    /// Dependency Resolver interface
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// set container
        /// </summary>
        /// <param name="container">di container</param>
        void SetContainer(IContainer container);

        /// <summary>
        /// Resolve specified service
        /// </summary>
        /// <typeparam name="TService">specified service type</typeparam>
        /// <returns>specified service object</returns>
        TService Resolve<TService>();

        /// <summary>
        /// Resolve specified service by specified key
        /// </summary>
        /// <typeparam name="TService">specified service type</typeparam>
        /// <param name="key">specified key</param>
        /// <returns>specified service object</returns>
        TService ResolveKeyed<TService>(object key);
    }
}