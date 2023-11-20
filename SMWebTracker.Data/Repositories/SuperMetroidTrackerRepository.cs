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
    }
}
