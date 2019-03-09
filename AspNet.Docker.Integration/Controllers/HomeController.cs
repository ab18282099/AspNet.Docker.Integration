using AspNet.Docker.Integration.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNet.Docker.Integration.Controllers
{
    /// <summary>
    /// Home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Home
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// About
        /// </summary>
        /// <returns>view</returns>
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// Contact
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        /// <summary>
        /// Privacy
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns>view</returns>
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