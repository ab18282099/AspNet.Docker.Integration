using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AspNet.Docker.Integration
{
    /// <summary>
    /// dotnet core 應用程式
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 建構子
        /// </summary>
        protected Program()
        {
        }

        /// <summary>
        /// 應用程式進入點
        /// </summary>
        /// <param name="args">參數</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 建立 <see cref="WebHost"/> 並設定使用 <see cref="Startup"/> 為啟動類別
        /// </summary>
        /// <param name="args">應用程式參數</param>
        /// <returns><see cref="IWebHostBuilder"/></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}