using Autofac;
using System;

namespace AspNet.Docker.Integration.Helper
{
    /// <summary>
    /// Dependency Resolver
    /// </summary>
    public class DependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Resolver's Lazy instance
        /// </summary>
        private static Lazy<DependencyResolver> ResolverInstance;

        /// <summary>
        /// SyncRoot
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// DI Container
        /// </summary>
        private IContainer ContainerInstance;

        /// <summary>
        /// Get single instance of Dependency Resolver
        /// </summary>
        public static IDependencyResolver Current
        {
            get
            {
                if (ResolverInstance != null && ResolverInstance.IsValueCreated)
                {
                    return ResolverInstance.Value;
                }

                lock (SyncRoot)
                {
                    ResolverInstance = new Lazy<DependencyResolver>(() => new DependencyResolver());
                }

                return ResolverInstance.Value;
            }
        }

        /// <summary>
        /// private constructor
        /// </summary>
        private DependencyResolver()
        {
        }

        /// <summary>
        /// set up container
        /// </summary>
        /// <param name="container">di container</param>
        public void SetContainer(IContainer container)
        {
            this.ContainerInstance = container;
        }

        /// <summary>
        /// Resolve specified service
        /// </summary>
        /// <typeparam name="TService">specified service type</typeparam>
        /// <returns>specified service object</returns>
        public TService Resolve<TService>()
        {
            return this.ContainerInstance.Resolve<TService>();
        }

        /// <summary>
        /// Resolve specified service by specified key
        /// </summary>
        /// <typeparam name="TService">specified service type</typeparam>
        /// <param name="key">specified key</param>
        /// <returns>specified service object</returns>
        public TService ResolveKeyed<TService>(object key)
        {
            return this.ContainerInstance.ResolveKeyed<TService>(key);
        }
    }
}