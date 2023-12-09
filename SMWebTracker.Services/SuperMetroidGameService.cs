using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using SMWebTracker.Domain.Interfaces;

namespace SMWebTracker.Services
{
    public class SuperMetroidGameService : ISuperMetroidGameService
    {
        private readonly ISuperMetroidTrackerRepository _superMetroidTrackerRepository;
        private readonly ISuperMetroidGameRepository _superMetroidGameRepository;
        private readonly IUserRepository _userRepository;

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

            var newGame = await _superMetroidGameRepository.CreateNewGameAsync(newSuperMetroidGameParameters.PlayerNames.Count, newSuperMetroidGameParameters.Description, user.Id);

            var result = new NewSuperMetroidGameModel
            {
                SuperMetroidGameId = newGame.Id,
                SuperMetroidGameTrackers = new List<GuidIndexModel>()
            };

            for (int i = 0; i < newSuperMetroidGameParameters.PlayerNames.Count; i++)
            {
                var player = newSuperMetroidGameParameters.PlayerNames[i].Trim();

                var newTracker = await _superMetroidTrackerRepository.CreateNewTrackerAsync(player, i, newGame.Id);

                result.SuperMetroidGameTrackers.Add(new GuidIndexModel { Id = newTracker.Id, Index = i });
            }

            return result;
        }

        public async Task<List<ActiveGameModel>> GetActiveGamesAsNoTrackingAsync()        
            => await _superMetroidGameRepository.GetActiveGamesAsNoTrackingAsync();        

        public async Task<SuperMetroidGame?> GetGameAsNoTrackingAsync(Guid gameId)
            => await _superMetroidGameRepository.GetGameAsNoTrackingAsync(gameId);

        public async Task<SuperMetroidTracker?> GeTrackerAsNoTrackingAsync(Guid trackerId)
            => await _superMetroidTrackerRepository.GeTrackerAsNoTrackingAsync(trackerId);

        public async Task<SuperMetroidTracker?> GeTrackerAsNoTrackingAsync(Guid gameId, int trackerIndex)
            => await _superMetroidTrackerRepository.GeTrackerAsNoTrackingAsync(gameId, trackerIndex);

        public async Task<SuperMetroidTracker?> Track(Guid trackerId, SuperMetroidTrackerModel trackerChanges)
        {
            var tracker = await _superMetroidTrackerRepository.GeTrackerAsync(trackerId);

            if (tracker != null)
            {
                return await Track(tracker, trackerChanges);
            }

            return null;
        }

        public async Task<SuperMetroidTracker?> Track(Guid gameId, int trackerIndex, SuperMetroidTrackerModel trackerChanges)
        {
            var tracker = await _superMetroidTrackerRepository.GeTrackerAsync(gameId, trackerIndex);

            if (tracker != null)
            {
                return await Track(tracker, trackerChanges);
            }

            return null;
        }

        public async Task<ActiveGameModel> UpdateGameAsync(Guid gameId, NewSuperMetroidGameParameters newSuperMetroidGameParameters)
        {
            var game = await _superMetroidGameRepository.GetGameAsync(gameId);
            if (game != null)
            {
                game.Description = newSuperMetroidGameParameters.Description;

                var toAdd = new List<SuperMetroidTracker>();
                var toRemove = new List<SuperMetroidTracker>();

                foreach (var trakcer in game.SuperMetroidTrackers)
                {
                    if (!newSuperMetroidGameParameters.PlayerNames.Contains(trakcer.PlayerName))
                        toRemove.Add(trakcer);
                }

                foreach (var playerName in newSuperMetroidGameParameters.PlayerNames)
                {
                    if (!game.SuperMetroidTrackers.Select(s => s.PlayerName).Contains(playerName))
                        toAdd.Add(new SuperMetroidTracker
                        {
                            SuperMetroidGameId = gameId,
                            PlayerName = playerName,
                        });
                }

                foreach (var remove in toRemove)
                    game.SuperMetroidTrackers.Remove(remove);

                foreach (var add in toAdd)
                    game.SuperMetroidTrackers.Add(add);

                for (int i = 0; i < game.SuperMetroidTrackers.Count; i++)
                    game.SuperMetroidTrackers[i].PlayerIndex = i;

                await _superMetroidGameRepository.UpdageGame(game);

                return new ActiveGameModel
                {
                    Id = gameId,
                    CreatedBy = game.CreatedBy,
                    CreatedAt = game.CreatedAt,
                    Description = game.Description,
                    Players = game.SuperMetroidTrackers.Select(g => g.PlayerName).ToList()
                };
            }

            return null;
        }

        private async Task<SuperMetroidTracker> Track(SuperMetroidTracker tracker, SuperMetroidTrackerModel trackerChanges)
        {
            bool changed = false;

            if (trackerChanges.VariaSuit.HasValue) { changed = true; tracker.VariaSuit = !tracker.VariaSuit; }
            if (trackerChanges.GravitySuit.HasValue) { changed = true; tracker.GravitySuit = !tracker.GravitySuit; }
            if (trackerChanges.ChargeBeam.HasValue) { changed = true; tracker.ChargeBeam = !tracker.ChargeBeam; }
            if (trackerChanges.IceBeam.HasValue) { changed = true; tracker.IceBeam = !tracker.IceBeam; }
            if (trackerChanges.WaveBeam.HasValue) { changed = true; tracker.WaveBeam = !tracker.WaveBeam; }
            if (trackerChanges.SpazerBeam.HasValue) { changed = true; tracker.SpazerBeam = !tracker.SpazerBeam; }
            if (trackerChanges.PlasmaBeam.HasValue) { changed = true; tracker.PlasmaBeam = !tracker.PlasmaBeam; }
            if (trackerChanges.MorphBall.HasValue) { changed = true; tracker.MorphBall = !tracker.MorphBall; }
            if (trackerChanges.Bombs.HasValue) { changed = true; tracker.Bombs = !tracker.Bombs; }
            if (trackerChanges.HighJumpBoots.HasValue) { changed = true; tracker.HighJumpBoots = !tracker.HighJumpBoots; }
            if (trackerChanges.SpeedBooster.HasValue) { changed = true; tracker.SpeedBooster = !tracker.SpeedBooster; }
            if (trackerChanges.SpaceJump.HasValue) { changed = true; tracker.SpaceJump = !tracker.SpaceJump; }
            if (trackerChanges.SpringBall.HasValue) { changed = true; tracker.SpringBall = !tracker.SpringBall; }
            if (trackerChanges.Kraid.HasValue) { changed = true; tracker.Kraid = !tracker.Kraid; }
            if (trackerChanges.Phantoon.HasValue) { changed = true; tracker.Phantoon = !tracker.Phantoon; }
            if (trackerChanges.Draygon.HasValue) { changed = true; tracker.Draygon = !tracker.Draygon; }
            if (trackerChanges.Ridley.HasValue) { changed = true; tracker.Ridley = !tracker.Ridley; }
            if (trackerChanges.ScrewAttack.HasValue) { changed = true; tracker.ScrewAttack = !tracker.ScrewAttack; }
            if (trackerChanges.SporeSpawn.HasValue) { changed = true; tracker.SporeSpawn = !tracker.SporeSpawn; }
            if (trackerChanges.Crocomire.HasValue) { changed = true; tracker.Crocomire = !tracker.Crocomire; }
            if (trackerChanges.Botwoon.HasValue) { changed = true; tracker.Botwoon = !tracker.Botwoon; }
            if (trackerChanges.GoldenTorizo.HasValue) { changed = true; tracker.GoldenTorizo = !tracker.GoldenTorizo; }
            if (trackerChanges.Grapple.HasValue) { changed = true; tracker.Grapple = !tracker.Grapple; }
            if (trackerChanges.Xray.HasValue) { changed = true; tracker.Xray = !tracker.Xray; }
            if (trackerChanges.Position.HasValue) { changed = true; tracker.Position = trackerChanges.Position.Value; }

            if (changed)
            {
                await _superMetroidTrackerRepository.SaveChangesAsyc(tracker);
            }

            return tracker;
        }
    }
}
