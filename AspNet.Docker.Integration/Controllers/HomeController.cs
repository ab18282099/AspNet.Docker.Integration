using AspNet.Docker.Integration.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNet.Docker.Integration.Controllers
{
    /// <summary>
    /// 範例的首頁控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns>檢視</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 關於
        /// </summary>
        /// <returns>檢視</returns>
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// 聯絡
        /// </summary>
        /// <returns>檢視</returns>
        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        /// <summary>
        /// 隱私政策
        /// </summary>
        /// <returns>檢視</returns>
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// 錯誤頁面
        /// </summary>
        /// <returns>檢視</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            });
        }
    }
}