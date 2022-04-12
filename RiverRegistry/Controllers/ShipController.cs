using EntityFrameworkCore.CommonTools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiverRegistry.Data.Contexts;
using RiverRegistry.Data.Repositories;
using RiverRegistry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiverRegistry.ETL;
using System.IO;
using Microsoft.Extensions.Configuration;
using Gridify;
using System.Linq.Expressions;

namespace RiverRegistry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        private readonly IShipRepository _repository;

        public ShipController(IShipRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GridifyQuery query)
        {
            try
            {
                var ships = _repository.GetShips(query);

                if (ships.Count == 0)
                    return NotFound();

                return Ok(ships);
            }
            catch(Exception ex)
            {
                return BadRequest("Bad filtering query");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Ship ship)
        {
            try
            {
                await _repository.UpdateShipAsync(ship);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }

            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> Post(Ship ship)
        {
            try
            {
                int shipId = await _repository.AddShipAsync(ship);

                if (shipId > 0)
                    return Ok();

                return BadRequest();
            }
            catch 
            {
                return BadRequest();
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            using (var fileStream = new MemoryStream())
            {
                await file.CopyToAsync(fileStream);
                var etl = new ExcelSource<Ship>(fileStream,
                    row => ShipParser.FromDataRow(row),
                    ships =>
                    {
                        _repository.ShipBulkInsert(ships);
                    });

                etl.HasHeader = true;
                await etl.ExecuteAsync();
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var ship = await _repository.GetShipByIdAsync(id);

            if (ship == null)
            {
                return NotFound();
            }

            await _repository.DeleteShipAsync(id);

            return Ok();
        }
    }
}
