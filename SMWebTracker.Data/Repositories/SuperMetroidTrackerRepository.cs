using Microsoft.EntityFrameworkCore;
using SMWebTracker.Domain.Entities;
using SMWebTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Data.Repositories
{
    public class SuperMetroidTrackerRepository : ISuperMetroidTrackerRepository
    {
        private readonly TrackerDB _trackerDB;

        public SuperMetroidTrackerRepository(TrackerDB trackerDB)
        {
            _trackerDB = trackerDB;
        }

        public async Task<SuperMetroidTracker> CreateNewTrackerAsync(string playerName, int playerIndex, Guid gameId)
        {
            var newTracker = new SuperMetroidTracker
            {
                SuperMetroidGameId = gameId,
                PlayerName = playerName,
                PlayerIndex = playerIndex
            };

            await _trackerDB.SuperMetroidTrackers.AddAsync(newTracker);
            await _trackerDB.SaveChangesAsync();

            return newTracker;
        }

        public async Task<SuperMetroidTracker?> GeTrackerAsNoTrackingAsync(Guid trackerId)
        {
            return await _trackerDB.SuperMetroidTrackers
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id.Equals(trackerId));
        }

        public async Task<SuperMetroidTracker?> GeTrackerAsync(Guid trackerId)
        {
            return await _trackerDB.SuperMetroidTrackers                
                .FirstOrDefaultAsync(g => g.Id.Equals(trackerId));
        }

        public async Task<SuperMetroidTracker?> GeTrackerAsNoTrackingAsync(Guid gameId, int trackerIndex)
        {
            return await _trackerDB.SuperMetroidTrackers
                .Where(t => t.SuperMetroidGameId.Equals(gameId))
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.PlayerIndex.Equals(trackerIndex));
        }

        public async Task<SuperMetroidTracker?> GeTrackerAsync(Guid gameId, int trackerIndex)
        {
            return await _trackerDB.SuperMetroidTrackers
                .Where(t => t.SuperMetroidGameId.Equals(gameId))
                .FirstOrDefaultAsync(g => g.PlayerIndex.Equals(trackerIndex));
        }

        public async Task SaveChangesAsyc(SuperMetroidTracker tracker)
        {
            _trackerDB.SuperMetroidTrackers.Update(tracker);
            await _trackerDB.SaveChangesAsync();
        }
    }
}
