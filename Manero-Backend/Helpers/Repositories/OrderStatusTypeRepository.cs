﻿using Manero_Backend.Contexts;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;

namespace Manero_Backend.Helpers.Repositories
{
    public class OrderStatusTypeRepository : BaseRepository<OrderStatusTypeEntity>, IOrderStatusTypeRepository
    {
        private readonly ManeroDbContext _context;

        public OrderStatusTypeRepository(ManeroDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
