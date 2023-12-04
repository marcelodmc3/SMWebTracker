using SMWebTracker.Data;
using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using SMWebTracker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

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

        private async Task<SuperMetroidTracker> Track(SuperMetroidTracker tracker, SuperMetroidTrackerModel trackerChanges)
        {
            bool changed = false;

            if (trackerChanges.VariaSuit.HasValue) { changed = true; tracker.VariaSuit = trackerChanges.VariaSuit.Value; }
            if (trackerChanges.GravitySuit.HasValue) { changed = true; tracker.GravitySuit = trackerChanges.GravitySuit.Value; }
            if (trackerChanges.ChargeBeam.HasValue) { changed = true; tracker.ChargeBeam = trackerChanges.ChargeBeam.Value; }
            if (trackerChanges.IceBeam.HasValue) { changed = true; tracker.IceBeam = trackerChanges.IceBeam.Value; }
            if (trackerChanges.WaveBeam.HasValue) { changed = true; tracker.WaveBeam = trackerChanges.WaveBeam.Value; }
            if (trackerChanges.SpazerBeam.HasValue) { changed = true; tracker.SpazerBeam = trackerChanges.SpazerBeam.Value; }
            if (trackerChanges.PlasmaBeam.HasValue) { changed = true; tracker.PlasmaBeam = trackerChanges.PlasmaBeam.Value; }
            if (trackerChanges.MorphBall.HasValue) { changed = true; tracker.MorphBall = trackerChanges.MorphBall.Value; }
            if (trackerChanges.Bombs.HasValue) { changed = true; tracker.Bombs = trackerChanges.Bombs.Value; }
            if (trackerChanges.HighJumpBoots.HasValue) { changed = true; tracker.HighJumpBoots = trackerChanges.HighJumpBoots.Value; }
            if (trackerChanges.SpeedBooster.HasValue) { changed = true; tracker.SpeedBooster = trackerChanges.SpeedBooster.Value; }
            if (trackerChanges.SpaceJump.HasValue) { changed = true; tracker.SpaceJump = trackerChanges.SpaceJump.Value; }
            if (trackerChanges.SpringBall.HasValue) { changed = true; tracker.SpringBall = trackerChanges.SpringBall.Value; }
            if (trackerChanges.Kraid.HasValue) { changed = true; tracker.Kraid = trackerChanges.Kraid.Value; }
            if (trackerChanges.Phantoon.HasValue) { changed = true; tracker.Phantoon = trackerChanges.Phantoon.Value; }
            if (trackerChanges.Draygon.HasValue) { changed = true; tracker.Draygon = trackerChanges.Draygon.Value; }
            if (trackerChanges.Ridley.HasValue) { changed = true; tracker.Ridley = trackerChanges.Ridley.Value; }
            if (trackerChanges.ScrewAttack.HasValue) { changed = true; tracker.ScrewAttack = trackerChanges.ScrewAttack.Value; }
            if (trackerChanges.SporeSpawn.HasValue) { changed = true; tracker.SporeSpawn = trackerChanges.SporeSpawn.Value; }
            if (trackerChanges.Crocomire.HasValue) { changed = true; tracker.Crocomire = trackerChanges.Crocomire.Value; }
            if (trackerChanges.Botwoon.HasValue) { changed = true; tracker.Botwoon = trackerChanges.Botwoon.Value; }
            if (trackerChanges.GoldenTorizo.HasValue) { changed = true; tracker.GoldenTorizo = trackerChanges.GoldenTorizo.Value; }
            if (trackerChanges.Grapple.HasValue) { changed = true; tracker.Grapple = trackerChanges.Grapple.Value; }
            if (trackerChanges.Xray.HasValue) { changed = true; tracker.Xray = trackerChanges.Xray.Value; }

            if (changed)
            {
                await _superMetroidTrackerRepository.SaveChangesAsyc(tracker);
            }

            return tracker;
        }
    }
}
