using AspNet.Docker.Integration.Repository.Models;

namespace AspNet.Docker.Integration.Repository
{
    /// <summary>
    /// 訂單儲存庫
    /// </summary>
    public class OrderRepository : GenericRepository<Order, DockerSqlServerDbContext>, IOrderRepository
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="dbContext">db context</param>
        public OrderRepository(DockerSqlServerDbContext dbContext) : base(dbContext)
        {
        }
    }
}