using AspNet.Docker.Integration.Repository;
using AspNet.Docker.Integration.Repository.Models;
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
        /// Add new user data
        /// </summary>
        /// <param name="user">User</param>
        /// <returns> json response </returns>
        [HttpPost]
        [Route("api/test/add/user")]
        public IActionResult AddUser([FromBody] User user)
        {
            User newUser = this.UserRepository.Add(user);
            
            return this.Json(newUser);
        }

        /// <summary>
        /// Add new order data
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns> json response </returns>
        [HttpPost]
        [Route("api/test/add/order")]
        public IActionResult AddOrder([FromBody] Order order)
        {
            Order newOrder = this.OrderRepository.Add(order);

            return this.Json(newOrder);
        }

        /// <summary>
        /// Get all user data by GET /Test/GetUsers
        /// </summary>
        /// <returns> json response </returns>
        [Route("api/test/get/users")]
        public IActionResult GetUsers()
        {
            return this.Json(this.UserRepository.GetAll());
        }

        /// <summary>
        /// Get all order data by GET /Test/GetOrders
        /// </summary>
        /// <returns> json response </returns>
        [Route("api/test/get/orders")]
        public IActionResult GetOrders()
        {
            return this.Json(this.OrderRepository.GetAll());
        }
    }
}