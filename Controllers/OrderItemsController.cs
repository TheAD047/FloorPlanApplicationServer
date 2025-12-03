using FloorPlanApplication.Data.Enums;
using FloorPlanApplication.Dtos.OrderItem;
using FloorPlanApplication.Extensions;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Mappers;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanApplication.Controllers
{
    [Route("api/OrderItems")]
    [Authorize]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IPlanRepository _planRepository;
        private readonly UserManager<User> _userManager;

        public OrderItemsController(IOrderItemRepository orderItemRepository,IPlanRepository planRepository, UserManager<User> userManager)
        {
            _orderItemRepository = orderItemRepository;
            _planRepository = planRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Cart")]
        public async Task<IActionResult> GetOrderItemsForUser(int? index)
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized();

            var items = await _orderItemRepository.GetItemsByClientID(user.Id, index ?? 0, 10);

            var orderitems = items.Select(o => o.ToOrderItemDTO());

            return Ok(orderitems);
        }

        [HttpPut]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] CreateOrderItemDTO DTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized();

            var isAdmin = await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString());

            if (DTO.ClientID != user.Id && !isAdmin)
                return BadRequest("User mismatch");

            var plan = await _planRepository.GetPlanByID(DTO.PlanID);

            if(plan == null)
                return BadRequest();

            DTO.ClientID = user.Id;
            OrderItem item = DTO.ToOrderItemFromCreatDTO();

            item.Price = plan.Price;

            _orderItemRepository.AddOrderItem(item);

            return CreatedAtAction(nameof(GetOrderItemsForUser), new {index = 0}, item.ToOrderItemDTO());
        }

        [HttpPost]
        [Route("EditItem")]
        public async Task<IActionResult> EditItem([FromBody] EditItemDTO DTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized();

            var isAdmin = await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString());

            var item = await _orderItemRepository.GetOrderItemByID(DTO.OrderItemID);

            if (item == null)
                return NotFound();

            if(item.ClientID != user.Id && !isAdmin)
                return BadRequest("User mismatch");
                        
            if(DTO.Code == null && !isAdmin)
                return BadRequest("Unauthorized");

            //Code check

            item.Price = DTO.Price;

            _orderItemRepository.UpdateOrderItem(item);

            return Ok(item.ToOrderItemDTO);
        }

        [HttpPost]
        [Route("DeleteItem")]
        public async Task<IActionResult> DeleteItem([FromBody] DeleteItemDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();

            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return Unauthorized();

            var isAdmin = await _userManager.IsInRoleAsync(user, UserRole.ADMIN.ToString());

            var item = await _orderItemRepository.GetOrderItemByID(DTO.OrderItemID);

            if (item == null)
                return NotFound();

            if (item.ClientID != user.Id && !isAdmin)
                return BadRequest("User mismatch");

            _orderItemRepository.DeleteOrderItem(item);

            return NoContent();
        }
    }
}
