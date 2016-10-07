using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PlanetDatabase.Models;

namespace PlanetDatabase.Controllers
{
    public class PlanetController : ApiController
    {
        private readonly PDbContext _dbContext = new PDbContext();

        // GET api/planet
        public async Task<Planet[]> Get()
        {
            return await _dbContext.Planets.ToArrayAsync();
        }

        // GET api/planet/5
        public async Task<Planet> Get(int id)
        {
            return await _dbContext.Planets.FindAsync(id);
        }

        // POST api/planet
        public async Task Post([FromBody]Planet planet)
        {
            _dbContext.Planets.Add(planet);
            _dbContext.Entry(planet).State = EntityState.Added;

            await _dbContext.SaveChangesAsync();
        }

        // PUT api/planet/5
        public async Task Put(int id, [FromBody]Planet planet)
        {
            _dbContext.Planets.Attach(planet);
            _dbContext.Entry(planet).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        // DELETE api/planet/5
        public async Task Delete(int id)
        {
            var planet = await _dbContext.Planets.FindAsync(id);

            planet.IsDeleted = true;
            _dbContext.Planets.Attach(planet);
            _dbContext.Entry(planet).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
