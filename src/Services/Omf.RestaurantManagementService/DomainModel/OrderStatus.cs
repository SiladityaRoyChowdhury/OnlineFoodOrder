using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omf.RestaurantManagementService.DomainModel
{
    public enum OrderStatus
    {
        InProgress = 0,
        OrderAccepted = 1,
        PreparingOrder = 2,
        OrderReady = 3,
        OutForDelivery = 4,
        Delivered = 5,
        FailToOrder = 6,
        OrderCancelled = 7
    }
}