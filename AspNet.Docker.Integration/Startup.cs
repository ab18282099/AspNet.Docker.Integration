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
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// constructor
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
        /// Autofac DI container
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
            builder.Populate(services);

            // Get ordered TypeRegistrar
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

            foreach (ITypeRegistrar registrar in registrars)
            {
                registrar.Register(builder);
            }

            // register Proxy for Interceptors(AOP)
            builder.RegisterDynamicProxy(configure =>
            {
                configure.Interceptors.AddTyped<MethodInterceptorAttribute>();
            });

            IContainer container = builder.Build();
            this.ApplicationContainer = container;

            // Set solution packages' dependency resolver
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
                // build database and sync table if db not created.
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