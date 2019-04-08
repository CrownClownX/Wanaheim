using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;

namespace Wanaheim.Persistence.Repository
{
    public class PlayerRepository: Repository<Player>, IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        private ApplicationDbContext _marketContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
