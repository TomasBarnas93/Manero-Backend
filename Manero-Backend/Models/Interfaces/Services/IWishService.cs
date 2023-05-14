using Manero_Backend.Models.Schemas.Wish;
using Microsoft.AspNetCore.Mvc;

namespace Manero_Backend.Models.Interfaces.Services
{
    public interface IWishService
    {
        public Task<IActionResult> GetAllAsync(string userId);
        public Task<IActionResult> AddAsync(WishSchema schema, string userId);
    }
}
