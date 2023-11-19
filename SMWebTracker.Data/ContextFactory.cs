using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<TrackerDB>
    {
        public TrackerDB CreateDbContext(string[] args)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory().Replace(".Data", ".Api"));

            var configuration = new ConfigurationBuilder()
                 .SetBasePath(directory)
                 .AddJsonFile("appsettings.Development.json")
                 .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

                //"Server=tcp:sm-tracker-dev-server.database.windows.net,1433;Initial Catalog=sm-tracker-dev-database;Persist Security Info=False;User ID=sm-tracker-dev;Password=\"89a;qnuBnf3K)R-\";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionBuilder = new DbContextOptionsBuilder<TrackerDB>();

            optionBuilder.UseSqlServer(connectionString, (opt) => { });

            return new TrackerDB(optionBuilder.Options);
        }
    }
}
