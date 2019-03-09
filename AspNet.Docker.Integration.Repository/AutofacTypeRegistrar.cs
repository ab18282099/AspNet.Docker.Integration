using AspNet.Docker.Integration.Helper;
using Autofac;
using System.Reflection;

namespace AspNet.Docker.Integration.Repository
{
    /// <summary>
    /// Autofac Type register
    /// </summary>
    public class AutofacTypeRegistrar : ITypeRegistrar
    {
        /// <summary>
        /// register order
        /// </summary>
        public int Order => 3;

        /// <summary>
        /// Register type
        /// </summary>
        /// <param name="builder">Used to build an <see cref="T:Autofac.IContainer" /> from component registrations.</param>
        public void Register(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            builder
               .RegisterAssemblyTypes(assembly)
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }
    }
}