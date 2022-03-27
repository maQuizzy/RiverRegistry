using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiverRegistry.Controllers;
using RiverRegistry.Data.Contexts;
using RiverRegistry.Data.Repositories;
using RiverRegistry.Models;
using RiverRegistry.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RiverRegistry.Tests
{
    public class ShipControllerTests
    {
        [Fact]
        public async void Can_ReturnShipById()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);

            var ship = TestHelper.GetNewShip();
            ship.Project = "SuperProject";
            await repository.AddShipAsync(ship);

            ShipController controller = new ShipController(repository);

            OkObjectResult result = controller.Get(new ShipParameters { ShipId = ship.ShipId }) as OkObjectResult;
            var returnedShips = result.Value as IEnumerable<Ship>;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal("SuperProject", returnedShips.ElementAt(0).Project);

        }

        [Fact]
        public async void Can_ReturnNotFoundWhenGetShipById()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);

            var ship = TestHelper.GetNewShip();
            ship.Project = "SuperProject";
            await repository.AddShipAsync(ship);

            ShipController controller = new ShipController(repository);

            NotFoundResult result = controller.Get(new ShipParameters { ShipId = 2 }) as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void Can_PostShip()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);
            ShipController controller = new ShipController(repository);

            var newShip = TestHelper.GetNewShip();
            newShip.Project = "SuperProject";
            OkResult result = await controller.Post(newShip) as OkResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Single(context.Ships);
            Assert.Equal("SuperProject", context.Ships.FirstOrDefault().Project);
        }

        [Fact]
        public async void Can_ReturnBadRequestWhenPostShip()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);
            ShipController controller = new ShipController(repository);

            var newShip = TestHelper.GetNewShip();
            newShip.ShipDimensions = null;
            var result = await controller.Post(newShip) as BadRequestResult;

            Assert.Empty(context.Ships);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async void Can_PutShip()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);

            var newShip = TestHelper.GetNewShip();
            newShip.Project = "SuperProject";
            await repository.AddShipAsync(newShip);

            context.ChangeTracker.Clear();

            ShipController controller = new ShipController(repository);

            var putShip = TestHelper.GetNewShip();
            putShip.ShipId = newShip.ShipId;
            putShip.Project = "QwertyProject";

            var result = await controller.Put(putShip) as OkResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Single(context.Ships);
            Assert.Equal("QwertyProject", context.Ships.FirstOrDefault().Project);
        }

        [Fact]
        public async void Can_ReturnBadRequestWhenPutShip()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);

            var newShip = TestHelper.GetNewShip();
            newShip.Project = "SuperProject";
            await repository.AddShipAsync(newShip);

            context.ChangeTracker.Clear();

            ShipController controller = new ShipController(repository);

            var putShip = TestHelper.GetNewShip();
            putShip.Project = "QwertyProject";
            putShip.ShipDimensions = null;

            var result = await controller.Put(putShip) as BadRequestObjectResult;

            Assert.Equal(400, result.StatusCode);
            Assert.Single(context.Ships);
            Assert.Equal("SuperProject", context.Ships.FirstOrDefault().Project);
        }

        [Fact]
        public async void Can_DeleteShipById()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);

            var ship = TestHelper.GetNewShip();
            await repository.AddShipAsync(ship);

            ShipController controller = new ShipController(repository);

            OkResult result = await controller.Delete(ship.ShipId) as OkResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Empty(context.Ships);

        }

        [Fact]
        public async void Can_ReturnNotFoundWhenDeleteShipById()
        {
            var contextOptions = TestHelper.GetInMemoryContextOptions<ShipsDbContext>();
            ShipsDbContext context = new ShipsDbContext(contextOptions);
            IShipRepository repository = new EfShipRepository(context);

            var ship = TestHelper.GetNewShip();
            await repository.AddShipAsync(ship);

            ShipController controller = new ShipController(repository);

            NotFoundResult result = await controller.Delete(2) as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
            Assert.Single(context.Ships);

        }

    }
}
