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
        Task<SuperMetroidTracker?> GeTrackerAsNoTrackingAsync(Guid trackerId);
        Task<SuperMetroidTracker?> GeTrackerAsNoTrackingAsync(Guid gameId, int trackerIndex);
        Task<SuperMetroidTracker?> GeTrackerAsync(Guid trackerId);
        Task<SuperMetroidTracker?> GeTrackerAsync(Guid gameId, int trackerIndex);
        Task SaveChangesAsyc(SuperMetroidTracker tracker);
    }
}
