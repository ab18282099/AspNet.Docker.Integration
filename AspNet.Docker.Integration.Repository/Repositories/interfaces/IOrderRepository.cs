using AspNet.Docker.Integration.Repository.Models;

namespace AspNet.Docker.Integration.Repository
{
    /// <summary>
    /// 訂單儲存庫介面
    /// </summary>
    public interface IOrderRepository : IGenericRepository<Order, DockerSqlServerDbContext>
    {
    }
}