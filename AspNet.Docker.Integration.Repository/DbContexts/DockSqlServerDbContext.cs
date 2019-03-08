using AspNet.Docker.Integration.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Docker.Integration.Repository
{
    /// <summary>
    /// 本站台使用之 Docker SqlServer <see cref="DbContext"/>
    /// </summary>
    public class DockSqlServerDbContext : DbContext
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{TContext}"/></param>
        public DockSqlServerDbContext(DbContextOptions<DockSqlServerDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// <see cref="User"/> 資料集
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// <see cref="OnModelCreating"/>
        /// </summary>
        /// <param name="modelBuilder"><see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dockerdemo");

            base.OnModelCreating(modelBuilder);
        }
    }
}