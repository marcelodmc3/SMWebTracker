using Microsoft.EntityFrameworkCore;
using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using SMWebTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Data.Repositories
{
    public class SuperMetroidGameRepository : ISuperMetroidGameRepository
    {
        private readonly TrackerDB _trackerDB;

        public SuperMetroidGameRepository(TrackerDB trackerDB)
        {
            _trackerDB = trackerDB;
        }

        public async Task<SuperMetroidGame> CreateNewGameAsync(int playerCount, Guid userId)
        {
            var newGame = new SuperMetroidGame
            {
                PlayerCount = playerCount,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow,
                IsOpen = true
            };

            await _trackerDB.SuperMetroidGames.AddAsync(newGame);
            await _trackerDB.SaveChangesAsync();

            return newGame;
        }

        public async Task<List<ActiveGameModel>> GetActiveGamesAsNoTrackingAsync()
        {
            return await _trackerDB.SuperMetroidGames
                    .Include(g => g.SuperMetroidTrackers)
                .AsNoTracking()
                .Select(g => new ActiveGameModel
                {
                    Id = g.Id,
                    CreatedBy = g.CreatedBy,
                    CreatedAt = g.CreatedAt,
                    Players = g.SuperMetroidTrackers.Select(t => t.PlayerName).ToList(),
                })
                .ToListAsync();
        }

        public async Task<SuperMetroidGame?> GetGameAsNoTrackingAsync(Guid gameId)
        {
            return await _trackerDB.SuperMetroidGames
                .Include(g => g.SuperMetroidTrackers)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id.Equals(gameId));
        }
    }
}
