using Manero_Backend.Contexts;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Manero_Backend.Helpers.Repositories
{
    public class OrderRepository : BaseRepository<OrderEntity>, IOrderRepository
    {
        private readonly ManeroDbContext _dbContext;

        public OrderRepository(ManeroDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<OrderEntity>> GetAllIncludeAsync(string userId)
        {
            Console.WriteLine(userId);

            return await _dbContext.Orders
                .Include(x => x.OrderStatuses).ThenInclude(x => x.OrderStatusType)
                .Where(x => x.AppUserId == userId).ToListAsync();
        }

        public async Task<OrderEntity> GetIncludeAsync(string userId, Guid orderId)
        {
            return await _dbContext.Orders.Include(x => x.OrderStatuses).ThenInclude(x => x.OrderStatusType).Where(x => x.AppUserId == userId && x.Id == orderId).FirstOrDefaultAsync();
        }

        public async Task<OrderEntity> GetIncludeAsync( Guid orderId)
        {
            return await _dbContext.Orders.Include(x => x.OrderStatuses).ThenInclude(x => x.OrderStatusType).Where(x =>  x.Id == orderId).FirstOrDefaultAsync();
        }
    }
}
