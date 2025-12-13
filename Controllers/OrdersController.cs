using FloorPlanApplication.Data.Enums;
using FloorPlanApplication.Dtos.Order;
using FloorPlanApplication.Extensions;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Mappers;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanApplication.Controllers
{
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly UserManager<User> _userManager;

        public OrdersController(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, UserManager<User> userManager)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Orders")]
        public async Task<IActionResult> Orders(int? index)
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if(user == null) 
                return NotFound();

            var orderList = await _orderRepository.GetOrdersByClientID(user.Id, index ?? 0, 10);

            var orders = orderList.Select(o => o.ToOrderDTO()).ToList();

            return Ok(orders);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return NotFound();

            var orderItems = await _orderItemRepository.GetItemsByClientID(user.Id, 0, 50);

            if (!orderItems.Any())
                return NotFound();

            var newOrder = new Order { 
                ClientID = user.Id
            };

            var orderCreated = _orderRepository.AddOrder(newOrder);

            if (orderCreated)
            {
                foreach (var item in orderItems)
                {
                    item.OrderID = newOrder.ID;
                }
            }
            else
            {
                return StatusCode(500);
            }

            return CreatedAtAction(nameof(OrderDetails), new {OrderID = newOrder.ID}, newOrder.ToOrderDetailsDTOFromOrder());
        }

        [HttpPost]
        [Route("EditOrder")]
        public async Task<IActionResult> EditOrder(EditOrderDTO DTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null) 
                return NotFound();
            
            bool isAdmiun = await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString());
            
            var order = await _orderRepository.GetOrderByID(DTO.OrderID);

            if (order == null)
                return NotFound();

            if (order.ClientID != user.Id && !isAdmiun)
                return Unauthorized();

            var newOrder = DTO.ToOrderFromEditDTO(order);

            _orderRepository.UpdateOrder(newOrder);

            return RedirectToAction(nameof(OrderDetails), new {OrderID = newOrder.ID});
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized();

            IEnumerable<Order> orders = await _orderRepository.GetOrdersByClientID(user.Id, 0, 100000);
            bool createOrder = false;
            Order order;

            if (orders == null)
            {
                createOrder = true;
                order = new Order();
            }
            else
            {
                order = orders.Where(o => !o.IsPlaced && !o.IsCancelled).First();
            }

            double subtotal = 0.00;

            IEnumerable<OrderItem> items = await _orderItemRepository.GetItemsByClientID(user.Id, 0, 100);

            if (items == null)
                return BadRequest("Please add items to cart to place eorder");

            items = items.Where(i => i.OrderID == order.ID);

            if (items == null)
                return BadRequest("Please add items to cart to place order");

            foreach (var item in items)
            {
                subtotal += item.Price;
            }

            order.OrderPlacementDate = DateTime.Now;
            order.IsPlaced = true;

            if(createOrder)
            {
                order.CompanyID = user.ComopanyID;
                order.ClientID = user.Id;
                order.OrderCreationDate = DateTime.Now;
                _orderRepository.AddOrder(order);
            }
            else
            {
                _orderRepository.UpdateOrder(order);
            }
            
            return RedirectToAction(nameof(OrderDetails), new {OrderID = order.ID});
        }

        [HttpGet]
        [Route("OrderDetails")]
        public async Task<IActionResult> OrderDetails(int OrderID)
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized();

            bool isAdmin = await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString());

            var order = await _orderRepository.GetOrderByID(OrderID);

            if (order == null) 
                return NotFound();

            if (order.ClientID != user.Id && !isAdmin)
                return Unauthorized();

            return Ok(order.ToOrderDetailsDTOFromOrder());   
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int OrderID)
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized();

            bool isAdmin = await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString());

            if (!isAdmin)
                return Unauthorized();

            var order = await _orderRepository.GetOrderByID(OrderID);

            if (order == null) 
                return NotFound();

            bool deleted =  _orderRepository.DeleteOrder(order);

            if (!deleted)
                return StatusCode(500);

            return NoContent();
        }
    }
}
