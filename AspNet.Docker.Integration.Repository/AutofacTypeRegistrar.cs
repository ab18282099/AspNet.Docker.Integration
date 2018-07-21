using AspNet.Docker.Integration.Helper;
using Autofac;
using System.Reflection;

namespace AspNet.Docker.Integration.Repository
{
    /// <summary>
    /// 物件註冊
    /// </summary>
    public class AutofacTypeRegistrar : ITypeRegistrar
    {
        /// <summary>
        /// 註冊排序
        /// </summary>
        public int Order => 3;

        /// <summary>
        /// 註冊物件
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