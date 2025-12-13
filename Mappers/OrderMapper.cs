using FloorPlanApplication.Dtos.Order;
using FloorPlanApplication.Models;

namespace FloorPlanApplication.Mappers
{
    public static class OrderMapper
    {
        public static OrderDTO ToOrderDTO(this Order order)
        {
            return new OrderDTO { 
                ID = order.ID,
                OrderCreationDate = order.OrderCreationDate,
                OrderFulfillmentDate = order.OrderFulfillmentDate,
                OrderPlacementDate = order.OrderPlacementDate,
                OrderVerficationDate = order.OrderVerficationDate,
                OrderPaymentDate = order.OrderPaymentDate,
                IsFulfilled = order.IsFulfilled,
                IsPaid = order.IsPaid,
                IsVerififed = order.IsVerififed,
                IsPlaced = order.IsPlaced,
                IsCommercialOrder = order.IsCommercialOrder,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                CompanyID = order.CompanyID,
                Discount = order.Discount,
            };
        }

        public static Order ToOrderFromEditDTO(this EditOrderDTO DTO, Order order)
        {
            var newOrder = new Order { 
                ID = order.ID,
                IsTaxExempted = DTO.IsTaxExempted ?? order.IsTaxExempted,
                Discount = DTO.Discount ?? order.Discount,
                CompanyID = order.CompanyID,
                ClientID = order.ClientID,
            };

            if(!order.IsFulfilled && newOrder.IsFulfilled)
            {
                newOrder.IsFulfilled = true;
                newOrder.OrderVerficationDate = DateTime.Now;
            }

            if(!order.IsPaid && newOrder.IsPaid)
            {
                newOrder.IsPaid = true;
                newOrder.OrderPaymentDate = DateTime.Now;
            }

            if(!order.IsVerififed && newOrder.IsVerififed)
            {
                newOrder.IsVerififed = true;
                newOrder.OrderVerficationDate = DateTime.Now;
            }

            if(!order.IsCancelled && newOrder.IsCancelled)
            {
                newOrder.IsCancelled = true;
                newOrder.OrderCancellationDate = DateTime.Now;
            }

            if(!order.IsPlaced && newOrder.IsPlaced)
            {
                newOrder.IsPlaced = true;
                newOrder.OrderPlacementDate = DateTime.Now;
            }

            return newOrder;

        }

        public static OrderDetailsDTO ToOrderDetailsDTOFromOrder(this Order order)
        {
            return new OrderDetailsDTO { 
                ID = order.ID,
                OrderCancellationDate = order.OrderCancellationDate,
                OrderFulfillmentDate = order.OrderFulfillmentDate,
                OrderCreationDate = order.OrderCreationDate,
                OrderPaymentDate = order.OrderPaymentDate,
                OrderPlacementDate = order.OrderPlacementDate,
                OrderVerficationDate= order.OrderVerficationDate,
                IsFulfilled = order.IsFulfilled,
                IsPaid = order.IsPaid,
                IsVerififed = order.IsVerififed,
                IsCancelled = order.IsCancelled,
                IsPlaced = order.IsPlaced,
                IsTaxExempted = order.IsTaxExempted,
                IsCommercialOrder = order.IsCommercialOrder,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Total = order.Total
            };
        }
    }
}
