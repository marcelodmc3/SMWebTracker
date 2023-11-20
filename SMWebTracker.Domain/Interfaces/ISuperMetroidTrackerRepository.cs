using SMWebTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Interfaces
{
    public interface ISuperMetroidTrackerRepository
    {
        Task<SuperMetroidTracker> CreateNewTrackerAsync(string playerName, int playerIndex, Guid gameId);
    }
}
