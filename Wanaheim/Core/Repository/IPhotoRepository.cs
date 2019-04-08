using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;

namespace Wanaheim.Core.Repository
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        Task<IEnumerable<Photo>> GetPhotosByItem(int itemId);
    }
}
