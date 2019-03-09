using AspNet.Docker.Integration.Repository.Models;

namespace AspNet.Docker.Integration.Repository
{
    /// <summary>
    /// 使用者資料儲存庫
    /// </summary>
    public class UserRepository : GenericRepository<User, DockerPostgresDbContext>, IUserRepositoy
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="dbContext">本站台使用之 Docker PostgreSql DbContext</param>
        public UserRepository(DockerPostgresDbContext dbContext) : base(dbContext)
        {
        }
    }
}