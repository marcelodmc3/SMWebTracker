using SMWebTracker.Domain.Dtos;
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
    }
}
