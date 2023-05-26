using Manero_Backend.Contexts;
using Manero_Backend.Models.Entities;
using Manero_Backend.Models.Interfaces.Repositories;

namespace Manero_Backend.Helpers.Repositories
{
    public class UserCodeRepository : BaseRepository<UserCodeEntity>, IUserCodeRepository
    {
        private readonly ManeroDbContext _context;

        public UserCodeRepository(ManeroDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
