using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RiverRegistry.Controllers;
using RiverRegistry.Data.Contexts;
using RiverRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverRegistry.Tests
{
    public static class TestHelper
    {
        public static DbContextOptions<TContext> GetInMemoryContextOptions<TContext> (string guid = null) where TContext : DbContext
        {
            DbContextOptions<TContext> options;

            var builder = new DbContextOptionsBuilder<TContext>(); 

            if (guid == null)
                guid = Guid.NewGuid().ToString();

            builder.UseInMemoryDatabase(guid);

            options = builder.Options;

            return options;
        }

        public static Ship GetNewShip()
        {
            Ship ship = new Ship()
            {
                ShipDimensions = new ShipDimensions
                {

                },

                ShipEngines = new ShipEngines
                {

                },

                ShipCapacity = new ShipCapacity
                {

                },
            };

            return ship;
        }
    }
}
