using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Services
{
    public class SuperMetroidGameService : ISuperMetroidGameService
    {
        public readonly ISuperMetroidTrackerRepository _superMetroidTrackerRepository;
        public readonly ISuperMetroidGameRepository _superMetroidGameRepository;
        public readonly IUserRepository _userRepository;

        public SuperMetroidGameService(
            ISuperMetroidTrackerRepository superMetroidTrackerRepository,
            ISuperMetroidGameRepository superMetroidGameRepository,
            IUserRepository userRepository)
        {
            _superMetroidTrackerRepository = superMetroidTrackerRepository;
            _superMetroidGameRepository = superMetroidGameRepository;
            _userRepository = userRepository;
        }

        public async Task<NewSuperMetroidGameModel> CreateNewGameAsync(NewSuperMetroidGameParameters newSuperMetroidGameParameters, string userEmail)
        {
            var user = await _userRepository.FindActiveByEmailAsync(userEmail);

            var newGame = await _superMetroidGameRepository.CreateNewGameAsync(newSuperMetroidGameParameters.PlayerNames.Count, user.Id);

            var result = new NewSuperMetroidGameModel
            {
                SuperMetroidGameId = newGame.Id,
                SuperMetroidGameTrackers = new List<GuidIndexModel>()
            };

            for (int i = 0; i < newSuperMetroidGameParameters.PlayerNames.Count; i++)
            {
                var player = newSuperMetroidGameParameters.PlayerNames[i];

                var newTracker = await _superMetroidTrackerRepository.CreateNewTrackerAsync(player, i, newGame.Id);

                result.SuperMetroidGameTrackers.Add(new GuidIndexModel { Id = newTracker.Id, Index = i });
            }

            return result;
        }
    }
}
