using Manero_Backend.Helpers.Services;
using Manero_Backend.Models.Entities;

namespace Manero_Backend.Models.Interfaces.Services
{
    public interface IUserCodeService : IBaseService<UserCodeEntity>
    {
        public Task<UserCodeEntity> GetAsync(string code);
        public Task<UserCodeEntity> CreateAsync(UserCodeEntity userCode);
    }
}
