using FloorPlanApplication.Dtos.OrderItem;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Mappers;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FloorPlanApplication.Controllers
{
    [Route("api/OrderItems")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IPlanRepository _planRepository;

        public OrderItemsController(IOrderItemRepository orderItemRepository, IPlanRepository planRepository)
        {
            _orderItemRepository = orderItemRepository;
            _planRepository = planRepository;
        }

        
    }
}
