using Autofac;

namespace AspNet.Docker.Integration.Helper
{
    /// <summary>
    /// 相依解析器介面
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// 設定相依性注入容器
        /// </summary>
        /// <param name="container">相依性注入容器</param>
        void SetContainer(IContainer container);

        /// <summary>
        /// 解析指定型別的服務
        /// </summary>
        /// <typeparam name="TService">指定服務型別</typeparam>
        /// <returns>指定服務物件</returns>
        TService Resolve<TService>();

        /// <summary>
        /// 由註冊時帶入之鍵值解析指定服務
        /// </summary>
        /// <typeparam name="TService">指定服務型別</typeparam>
        /// <param name="key">註冊鍵值</param>
        /// <returns>指定服務物件</returns>
        TService ResolveKeyed<TService>(object key);
    }
}