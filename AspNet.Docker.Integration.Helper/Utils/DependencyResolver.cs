using Autofac;
using System;

namespace AspNet.Docker.Integration.Helper
{
    /// <summary>
    /// 相依性解析器
    /// </summary>
    public class DependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// 解析器之 Lazy instance
        /// </summary>
        private static Lazy<DependencyResolver> ResolverInstance;

        /// <summary>
        /// SyncRoot
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// 相依性注入容器
        /// </summary>
        private IContainer ContainerInstance;

        /// <summary>
        /// 取得應用程式 scope 中唯一的相依性解析器實例
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
        /// 使用私有建構子
        /// </summary>
        private DependencyResolver()
        {
        }

        /// <summary>
        /// 設定相依性注入容器
        /// </summary>
        /// <param name="container">相依性注入容器</param>
        public void SetContainer(IContainer container)
        {
            this.ContainerInstance = container;
        }

        /// <summary>
        /// 解析指定型別的服務
        /// </summary>
        /// <typeparam name="TService">指定服務型別</typeparam>
        /// <returns>指定服務物件</returns>
        public TService Resolve<TService>()
        {
            return this.ContainerInstance.Resolve<TService>();
        }

        /// <summary>
        /// 由註冊時帶入之鍵值解析指定服務
        /// </summary>
        /// <typeparam name="TService">指定服務型別</typeparam>
        /// <param name="key">註冊鍵值</param>
        /// <returns>指定服務物件</returns>
        public TService ResolveKeyed<TService>(object key)
        {
            return this.ContainerInstance.ResolveKeyed<TService>(key);
        }
    }
}