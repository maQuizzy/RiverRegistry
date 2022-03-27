using RiverRegistry.Data.Contexts;
using RiverRegistry.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using EntityFrameworkCore.CommonTools;
using Z.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Transactions;
using System.Linq.Expressions;
using System;

namespace RiverRegistry.Data.Repositories
{
    public class EfShipRepository : IShipRepository
    {
        private readonly ShipsDbContext _ctx;

        public EfShipRepository(ShipsDbContext context)
        {
            _ctx = context;
        }

        private ShipsDbContext AddToContext(ShipsDbContext ctx, Ship ship, int count, int commitCount, bool recreateContext)
        {
            ctx.Ships.Add(ship);

            if(count % commitCount == 0)
            {
                ctx.SaveChanges();
                if (recreateContext)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ShipsDbContext>();
                    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RiverShipsDb;Trusted_Connection=True;MultipleActiveResultSets=true");
                    ctx = new ShipsDbContext(optionsBuilder.Options);
                    ctx.ChangeTracker.AutoDetectChangesEnabled = false;
                    ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                }
            }

            return ctx;
        }

        public void ShipBulkInsert(Ship[] ships)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ShipsDbContext context = null;
                try
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ShipsDbContext>();
                    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RiverShipsDb;Trusted_Connection=True;MultipleActiveResultSets=true");
                    context = new ShipsDbContext(optionsBuilder.Options);
                    context.ChangeTracker.AutoDetectChangesEnabled = false;
                    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                    int count = 0;
                    foreach (var ship in ships)
                    {
                        ++count;
                        context = AddToContext(context, ship, count, 100, true);
                    }

                    context.SaveChanges();
                }
                finally
                {
                    if (context != null)
                        context.Dispose();
                }

                scope.Complete();
            }
        }

        public async Task<int> AddShipAsync(Ship ship)
        {
            ship.ShipId = 0;
            ship.ShipCapacity.ShipId = 0;
            ship.ShipDimensions.ShipId = 0;
            ship.ShipEngines.ShipId = 0;

            _ctx.Ships.Add(ship);

            await _ctx.SaveChangesAsync();



            return ship.ShipId;
        }

        public int AddShip(Ship ship)
        {
            ship.ShipId = 0;
            ship.ShipCapacity.ShipId = 0;
            ship.ShipDimensions.ShipId = 0;
            ship.ShipEngines.ShipId = 0;

            _ctx.Ships.Add(ship);

            _ctx.SaveChanges();

            return ship.ShipId;
        }

        public async Task UpdateShipAsync(Ship ship)
        {

            ship.ShipCapacity.ShipId = ship.ShipId;
            ship.ShipDimensions.ShipId = ship.ShipId;
            ship.ShipEngines.ShipId = ship.ShipId;

            _ctx.Ships.Update(ship);

            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteShipAsync(int shipId)
        {
            var ship = _ctx.Ships.FirstOrDefault(s => s.ShipId == shipId);

            _ctx.Remove(ship);

            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<Ship> GetShips(Expression<Func<Ship, bool>> linqExpression)
        {
            return _ctx.Ships
                .Include(s => s.ShipDimensions)
                .Include(s => s.ShipEngines)
                .Include(s => s.ShipCapacity)
                .Where(linqExpression);
        }

        public IEnumerable<Ship> GetAllShips()
        {
            return _ctx.Ships
                .Include(s => s.ShipDimensions)
                .Include(s => s.ShipEngines)
                .Include(s => s.ShipCapacity)
                .AsEnumerable();
        }

        public async Task<Ship> GetShipByIdAsync(int shipId)
        {
            return await _ctx.Ships
                .Include(s => s.ShipDimensions)
                .Include(s => s.ShipEngines)
                .Include(s => s.ShipCapacity)
                .FirstOrDefaultAsync(s => s.ShipId == shipId);
        }
    }
}
