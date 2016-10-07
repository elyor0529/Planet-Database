using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using EntityFramework.DynamicFilters;
using PlanetDatabase.Helpers;

namespace PlanetDatabase.Models
{
    public class PDbContext : DbContext
    {

        public PDbContext()
            : base("DefaultConnection")
        {
            Database.CommandTimeout = 3600;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            Configuration.UseDatabaseNullSemantics = true;
            Configuration.ValidateOnSaveEnabled = true;
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Planet> Planets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
            modelBuilder.Filter("IsDeleted", (BaseEntity f) => f.IsDeleted, false);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => SaveChanges(), cancellationToken);
        }

        public override int SaveChanges()
        {
            try
            {
                var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.Entity is BaseEntity
                                && (x.State == EntityState.Added || x.State == EntityState.Modified));

                foreach (var entry in modifiedEntries)
                {
                    var entity = (BaseEntity)entry.Entity;
                    var now = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedOn = now;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        Entry(entity).Property(x => x.CreatedOn).IsModified = false;
                    }

                    entity.UpdatedOn = now;
                }

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                LogHelper.WriteError(exceptionMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}