using AspNet.Docker.Integration.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Docker.Integration.Controllers
{
    /// <summary>
    /// api 測試控制器
    /// </summary>
    public class TestController : Controller
    {
        /// <summary>
        /// 使用者資料儲存庫
        /// </summary>
        private readonly IUserRepositoy UserRepository;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="userRepository">使用者資料儲存庫</param>
        public TestController(IUserRepositoy userRepository)
        {
            this.UserRepository = userRepository;
        }

        /// <summary>
        /// 取得所有使用者資料
        /// </summary>
        /// <returns> json response </returns>
        public IActionResult GetUsers()
        {
            return this.Json(this.UserRepository.GetAll());
        }
    }
}