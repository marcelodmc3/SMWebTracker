using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Interfaces
{
    public interface ISuperMetroidGameService
    {
        Task<NewSuperMetroidGameModel> CreateNewGameAsync(NewSuperMetroidGameParameters parameters, string userEmail);
        Task<SuperMetroidGame?> GetGameAsNoTrackingAsync(Guid gameId);
        Task<SuperMetroidTracker?> GeTrackerAsNoTrackingAsync(Guid trackerId);
        Task<SuperMetroidTracker?> GeTrackerAsNoTrackingAsync(Guid gameId, int trackerIndex);
        Task<SuperMetroidTracker?> Track(Guid trackerId, SuperMetroidTrackerModel trackerChanges);
        Task<SuperMetroidTracker?> Track(Guid gameId, int trackerIndex, SuperMetroidTrackerModel trackerChanges);
        Task<List<ActiveGameModel>> GetActiveGamesAsNoTrackingAsync();
        Task<NewSuperMetroidGameModel> UpdateGameAsync(Guid gameId, NewSuperMetroidGameParameters parameters);
    }
}
