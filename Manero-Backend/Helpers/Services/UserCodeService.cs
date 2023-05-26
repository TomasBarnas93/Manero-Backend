using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;
using Manero_Backend.Models.Interfaces.Services;

namespace Manero_Backend.Helpers.Services
{
    public class UserCodeService : BaseService<UserCodeEntity>, IUserCodeService
    {
        private readonly IUserCodeRepository _userCodeRepository;

        public UserCodeService(IUserCodeRepository userCodeRepository) : base (userCodeRepository) 
        {
            _userCodeRepository = userCodeRepository;
        }

        public async Task<UserCodeEntity> CreateAsync(UserCodeEntity userCode)
        {
            return await _userCodeRepository.CreateAsync(userCode);
        }

        public async Task<UserCodeEntity> GetAsync(string code)
        {
            return await _userCodeRepository.GetAsync(x => x.Code == code);
        }
    }
}
