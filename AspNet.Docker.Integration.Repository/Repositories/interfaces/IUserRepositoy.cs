using AspNet.Docker.Integration.Repository.Models;

namespace AspNet.Docker.Integration.Repository
{
    /// <summary>
    /// 使用者資料儲存庫介面
    /// </summary>
    public interface IUserRepositoy : IGenericRepository<User, DockerPostgresDbContext>
    {
    }
}