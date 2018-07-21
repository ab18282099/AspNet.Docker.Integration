using Autofac;

namespace AspNet.Docker.Integration.Helper
{
    /// <summary>
    /// Autofac 物件註冊介面
    /// </summary>
    public interface ITypeRegistrar
    {
        /// <summary>
        /// 註冊排序
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 註冊物件
        /// </summary>
        /// <param name="builder">Used to build an <see cref="T:Autofac.IContainer" /> from component registrations.</param>
        void Register(ContainerBuilder builder);
    }
}