using AspNet.Docker.Integration.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspNet.Docker.Integration.Controllers
{
    /// <summary>
    /// api 測試控制器
    /// </summary>
    public class TestController : Controller
    {
        /// <summary>
        /// DockPostgresDbContext
        /// </summary>
        private readonly DockPostgresDbContext DbContext;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="dockPostgresDbContext">DockPostgresDbContext</param>
        public TestController(DockPostgresDbContext dockPostgresDbContext)
        {
            this.DbContext = dockPostgresDbContext;
        }

        /// <summary>
        /// 取得所有使用者資料
        /// </summary>
        /// <returns> json response </returns>
        public IActionResult GetUsers()
        {
            return this.Json(this.DbContext.Users.ToList());
        }
    }
}