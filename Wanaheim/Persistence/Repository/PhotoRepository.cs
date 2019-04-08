using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;

namespace Wanaheim.Persistence.Repository
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        private ApplicationDbContext _context;

        public PhotoRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        private ApplicationDbContext _marketContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<IEnumerable<Photo>> GetPhotosByItem(int itemId)
        {
            return await _context.Photos
                .Where(p => p.ItemId == itemId)
                .ToListAsync();
        }
    }
}
