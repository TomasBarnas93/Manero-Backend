using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;

namespace Manero_Backend.Helpers.Services
{
    public class OrderStatusTypeService : BaseService<OrderStatusTypeEntity>, IOrderStatusTypeService
    {
        private readonly IOrderStatusTypeRepository _orderStatusTypeRepository;

        public OrderStatusTypeService(IOrderStatusTypeRepository orderStatusTypeRepository) : base(orderStatusTypeRepository) 
        {
            _orderStatusTypeRepository = orderStatusTypeRepository;
        }


    }
}
