using Microsoft.EntityFrameworkCore;
using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using SMWebTracker.Domain.Interfaces;

namespace SMWebTracker.Data.Repositories
{
    public class SuperMetroidGameRepository : ISuperMetroidGameRepository
    {
        private readonly TrackerDB _trackerDB;

        public SuperMetroidGameRepository(TrackerDB trackerDB)
        {
            _trackerDB = trackerDB;
        }

        public async Task<SuperMetroidGame> CreateNewGameAsync(int playerCount, string description, Guid userId)
        {
            var newGame = new SuperMetroidGame
            {
                PlayerCount = playerCount,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow,
                Description = description,
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
                    Description = g.Description,
                    CreatedBy = g.CreatedBy,
                    CreatedAt = g.CreatedAt.ToLocalTime(),
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

        public async Task<SuperMetroidGame?> GetGameAsync(Guid gameId)
        {
            return await _trackerDB.SuperMetroidGames
                .Include(g => g.SuperMetroidTrackers)
                .FirstOrDefaultAsync(g => g.Id.Equals(gameId));
        }

        public async Task UpdageGame(SuperMetroidGame game)
        {
            _trackerDB.SuperMetroidGames.Update(game);
            await _trackerDB.SaveChangesAsync();
        }
    }
}
