using AspectCore.Configuration;
using AspectCore.Extensions.Autofac;
using AspNet.Docker.Integration.Helper;
using AspNet.Docker.Integration.Repository;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System;

namespace AspNet.Docker.Integration
{
    /// <summary>
    /// 啟動類別
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Represents a set of key/value application configuration properties.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Autofac DI 容器
        /// </summary>
        private IContainer ApplicationContainer { get; set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns><see cref="IServiceProvider"/>Defines a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // DbContext in application scope
            services.AddDbContext<DockerPostgresDbContext>(options => options.UseNpgsql(this.Configuration.GetConnectionString("DockerPostgres")));
            services.AddDbContext<DockerSqlServerDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("DockerSqlServer")));
            

            ContainerBuilder builder = new ContainerBuilder();

            // 將 services 容器內已有的類型註冊資訊倒入 autofac 容器
            builder.Populate(services);

            // 取得排序後的 ITypeRegistrar
            IOrderedEnumerable<ITypeRegistrar> registrars
                = Assembly.GetExecutingAssembly()
                          .GetReferencedAssemblies()
                          .Select(Assembly.Load)
                          .Concat(new Assembly[]
                           {
                               // Assembly.Load("AspNet.Docker.Integration.Repository"),
                           })
                          .SelectMany(p => p.ExportedTypes.Where(s => s.IsAssignableTo<ITypeRegistrar>() && !s.IsInterface))
                          .Select(p => (ITypeRegistrar)Activator.CreateInstance(p))
                          .Distinct()
                          .OrderBy(p => p.Order);

            // 個別進行註冊
            foreach (ITypeRegistrar registrar in registrars)
            {
                registrar.Register(builder);
            }

            // 註冊 Proxy，用於 interceptor 的綁定(AOP)
            builder.RegisterDynamicProxy(configure =>
            {
                configure.Interceptors.AddTyped<MethodInterceptorAttribute>();
            });

            IContainer container = builder.Build();
            this.ApplicationContainer = container;

            // 設定相依性解析器
            DependencyResolver.Current.SetContainer(container);

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        /// <param name="appLifetime">Allows consumers to perform cleanup during a graceful shutdown.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            using (ILifetimeScope scope = this.ApplicationContainer.BeginLifetimeScope())
            {
                // 若 database 未建立，則根據dbContext 建立 db
                DockerPostgresDbContext postgresDbContext = scope.Resolve<DockerPostgresDbContext>();
                postgresDbContext.Database.EnsureCreated();

                DockerSqlServerDbContext sqlServerDbContext = scope.Resolve<DockerSqlServerDbContext>();
                sqlServerDbContext.Database.EnsureCreated();
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            // You can only do this if you have a direct reference to the container,
            // so it won't work with the above ConfigureContainer mechanism.
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}