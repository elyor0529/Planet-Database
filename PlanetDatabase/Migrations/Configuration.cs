using System.Data.Entity.Infrastructure.Interception;
using PlanetDatabase.Models;

namespace PlanetDatabase.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.PDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            CommandTimeout = 3600;
            ContextKey = "PlanetDatabase.Models.PDbContext";

            //logging
            DbInterception.Add(new EFInterceptor());
        }

        protected override void Seed(PDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var planetes = new Planet[]
            {
                new Planet {Id = 1, Name = "Mercury", KmFromSun = 57.9m*(10 ^ 6)},
                new Planet {Id = 2, Name = "Venus", KmFromSun = 102.8m*(10 ^ 6)},
                new Planet {Id = 3, Name = "Earth", KmFromSun = 149.6m*(10 ^ 6)},
                new Planet {Id = 4, Name = "Mars", KmFromSun = 227.9m*(10 ^ 6)},
                new Planet {Id = 5, Name = "Jupiter", KmFromSun = 778.3m*(10 ^ 6)},
                new Planet {Id = 6, Name = "Saturn", KmFromSun = 1427.0m*(10 ^ 6)},
                new Planet {Id = 7, Name = "Uranus", KmFromSun = 2871.0m*(10 ^ 6)},
                new Planet {Id = 8, Name = "Neptune", KmFromSun = 4497.1m*(10 ^ 6)},
                new Planet {Id = 9, Name = "Pluto", KmFromSun = 5913.0m*(10 ^ 6)}
            };

            context.Planets.AddOrUpdate(a => a.Id, planetes);
        }
    }
}
