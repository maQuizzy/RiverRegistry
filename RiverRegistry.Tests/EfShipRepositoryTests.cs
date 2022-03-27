using Microsoft.EntityFrameworkCore;
using RiverRegistry.Data.Repositories;
using RiverRegistry.Data.Contexts;
using RiverRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RiverRegistry.Tests
{
    public class EfShipRepositoryTests
    {
        [Fact]
        public async void Can_AddShip()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);

            Ship ship = TestHelper.GetNewShip();
            ship.Project = "QwertyProject";
            ship.ShipEngines.MainICEBrand = "SuperBrand";

            int shipId = await repository.AddShipAsync(ship);
            var addedShip = context.Ships.FirstOrDefault(s => s.ShipId == shipId);

            Assert.Single(context.Ships);
            Assert.Equal(1, shipId);
            Assert.Equal("QwertyProject", addedShip.Project);
            Assert.Equal("SuperBrand", addedShip.ShipEngines.MainICEBrand);
        }

        [Fact]
        public async void Can_ReturnShipById()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            Ship ship = TestHelper.GetNewShip();

            context.Ships.Add(ship);
            context.SaveChanges();

            IShipRepository repository = new EfShipRepository(context);

            Ship returnedShip = await repository.GetShipByIdAsync(ship.ShipId);

            Assert.Equal(ship, returnedShip);


        }

        [Fact]
        public async void Can_DeleteShip()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            Ship ship = TestHelper.GetNewShip();
            Ship ship2 = TestHelper.GetNewShip();

            ship2.Project = "SuperProject";

            context.Ships.Add(ship);
            context.Ships.Add(ship2);
            context.SaveChanges();

            IShipRepository repository = new EfShipRepository(context);

            await repository.DeleteShipAsync(ship.ShipId);

            Assert.Single(context.Ships);
            Assert.Equal(context.Ships.First(), ship2);
        }

        [Fact]
        public async void Can_UpdateShip()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            Ship ship = TestHelper.GetNewShip();

            context.Ships.Add(ship);
            context.SaveChanges();

            ship.Project = "SuperProject";

            IShipRepository repository = new EfShipRepository(context);

            await repository.UpdateShipAsync(ship);

            Assert.Equal("SuperProject", context.Ships.First().Project);

        }

        [Fact]
        public void Can_ReturnAllShips()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            Ship ship = TestHelper.GetNewShip();
            Ship ship2 = TestHelper.GetNewShip();
            ship2.Project = "SuperProject";
            Ship ship3 = TestHelper.GetNewShip();

            context.Ships.Add(ship);
            context.Ships.Add(ship2);
            context.Ships.Add(ship3);
            context.SaveChanges();

            IShipRepository repository = new EfShipRepository(context);

            Assert.Equal(3, repository.GetAllShips().Count());
            Assert.Equal("SuperProject", repository.GetAllShips().FirstOrDefault(s => s.ShipId == ship2.ShipId).Project);

        }
    }
}
