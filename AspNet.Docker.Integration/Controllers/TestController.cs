using AspNet.Docker.Integration.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Docker.Integration.Controllers
{
    /// <summary>
    /// Api test controller
    /// </summary>
    public class TestController : Controller
    {
        /// <summary>
        /// User Repo
        /// </summary>
        private readonly IUserRepositoy UserRepository;

        /// <summary>
        /// Order Repo
        /// </summary>
        private readonly IOrderRepository OrderRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userRepository">User repo</param>
        /// <param name="orderRepository">Order repo</param>
        public TestController(
            IUserRepositoy userRepository,
            IOrderRepository orderRepository)
        {
            this.UserRepository = userRepository;
            this.OrderRepository = orderRepository;
        }

        /// <summary>
        /// Get all user data by GET /Test/GetUsers
        /// </summary>
        /// <returns> json response </returns>
        public IActionResult GetUsers()
        {
            return this.Json(this.UserRepository.GetAll());
        }

        /// <summary>
        /// Get all order data by GET /Test/GetOrders
        /// </summary>
        /// <returns> json response </returns>
        public IActionResult GetOrders()
        {
            return this.Json(this.OrderRepository.GetAll());
        }
    }
}