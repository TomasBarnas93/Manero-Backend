using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;

namespace Manero_Backend.Helpers.Services
{
    public class OrderStatusService : BaseService<OrderStatusEntity>, IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IOrderStatusTypeService _orderStatusTypeService;

        public OrderStatusService(IOrderStatusRepository orderStatusRepository, IOrderStatusTypeService orderStatusTypeService) : base(orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
            _orderStatusTypeService = orderStatusTypeService;
        }

        public async Task AddCanceledAsync(Guid orderId)
        {
            List<OrderStatusTypeEntity> statusTypes = (List<OrderStatusTypeEntity>)await _orderStatusTypeService.GetAllIEnurableAsync();

            var canceledStatus = new OrderStatusEntity()
            {
                OrderId = orderId,
                OrderStatusTypeId = statusTypes.Last().Id,
                Completed = true,
                CompletedUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                EstimatedTimeUnix = 0
            };
            await _orderStatusRepository.CreateAsync(canceledStatus);
            return;
        }

        public async Task<List<OrderStatusEntity>> CreateStatic(Guid orderId)
        {
            List<OrderStatusEntity> statuses = new List<OrderStatusEntity>();

            List<OrderStatusTypeEntity> statusTypes = (List<OrderStatusTypeEntity>)await _orderStatusTypeService.GetAllIEnurableAsync();

            for (int i = 0; i < statusTypes.Count() - 1; i++)
            {
                if(i < 2)
                {
                    statuses.Add(new OrderStatusEntity() 
                    {
                        OrderId = orderId,
                        OrderStatusTypeId = statusTypes[i].Id,
                        Completed = true,
                        CompletedUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        EstimatedTimeUnix = 0
                    });
                    continue;
                }

                statuses.Add(new OrderStatusEntity()
                {
                    OrderId = orderId,
                    OrderStatusTypeId = statusTypes[i].Id,
                    Completed = false,
                    CompletedUnix = 0,
                    EstimatedTimeUnix = DateTimeOffset.UtcNow.AddDays(1 + i).ToUnixTimeSeconds()
                });
            }


            return statuses;
            /*
            orderStatues.Add(new OrderStatusEntity()
            {
                orderId = orderEntity.Id,
                OrderStatusTypeId = Guid.Parse("ca596a28-4009-4d29-f379-08db5a0e0d5a"),
                Completed = true,
                CompletedUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                EstimatedTimeUnix = 0
            });
            orderStatues.Add(new OrderStatusEntity()
            {
                orderId = orderEntity.Id,
                OrderStatusTypeId = Guid.Parse("087a61ce-318f-436e-f37a-08db5a0e0d5a"),
                Completed = true,
                CompletedUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                EstimatedTimeUnix = 0
            });
            orderStatues.Add(new OrderStatusEntity()
            {
                orderId = orderEntity.Id,
                OrderStatusTypeId = Guid.Parse("547fe825-7621-4b28-f37b-08db5a0e0d5a"),
                Completed = false,
                CompletedUnix = 0,
                EstimatedTimeUnix = DateTimeOffset.UtcNow.AddDays(1 + 3).ToUnixTimeSeconds()
            });
            orderStatues.Add(new OrderStatusEntity()
            {
                orderId = orderEntity.Id,
                OrderStatusTypeId = Guid.Parse("1e915510-4161-4443-f37c-08db5a0e0d5a"),
                Completed = false,
                CompletedUnix = 0,
                EstimatedTimeUnix = DateTimeOffset.UtcNow.AddDays(1 + 4).ToUnixTimeSeconds()
            });
            orderStatues.Add(new OrderStatusEntity()
            {
                orderId = orderEntity.Id,
                OrderStatusTypeId = Guid.Parse("00ad9bdf-55db-4959-f37d-08db5a0e0d5a"),
                Completed = false,
                CompletedUnix = 0,
                EstimatedTimeUnix = DateTimeOffset.UtcNow.AddDays(1 + 5).ToUnixTimeSeconds()
            });
            */

        }
    }
}
