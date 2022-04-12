using RiverRegistry.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Gridify;

namespace RiverRegistry.Data.Repositories
{
    public interface IShipRepository
    {
        void ShipBulkInsert(Ship[] ships);
        Task<int> AddShipAsync(Ship ship);
        int AddShip(Ship ship);
        Paging<Ship> GetShips(GridifyQuery query);
        Task<Ship> GetShipByIdAsync(int shipId);
        IEnumerable<Ship> GetAllShips();
        Task UpdateShipAsync(Ship ship);
        Task DeleteShipAsync(int shipId);
    }
}
