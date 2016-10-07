using System.Data.Entity;
using PlanetDatabase.Migrations;
using PlanetDatabase.Models;

namespace PlanetDatabase
{
    public static class DbConfig
    {
        public static void ConfigureDb()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PDbContext, Configuration>());
        }
    }
}