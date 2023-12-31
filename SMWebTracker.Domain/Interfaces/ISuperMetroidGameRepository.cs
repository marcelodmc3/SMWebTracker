﻿using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Interfaces
{
    public interface ISuperMetroidGameRepository
    {
        Task<SuperMetroidGame> CreateNewGameAsync(int playerCount, string description, Guid userId);
        Task<SuperMetroidGame?> GetGameAsNoTrackingAsync(Guid gameId);
        Task<SuperMetroidGame?> GetGameAsync(Guid gameId);
        Task<List<ActiveGameModel>> GetActiveGamesAsNoTrackingAsync();
        Task UpdageGame(SuperMetroidGame game);
    }
}
