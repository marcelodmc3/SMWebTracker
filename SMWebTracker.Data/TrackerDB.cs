using Microsoft.EntityFrameworkCore;
using SMWebTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Data
{
    public class TrackerDB : DbContext
    {
        public TrackerDB(DbContextOptions<TrackerDB> options) : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>();
            builder.Entity<SuperMetroidTracker>();
            builder.Entity<SuperMetroidGame>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SuperMetroidGame> SuperMetroidGames { get; set; }
        public DbSet<SuperMetroidTracker> SuperMetroidTrackers { get; set; }
    }
}
