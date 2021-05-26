using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.Data;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public StocksController(ApplicationDbContext database) {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<List<Stock>>> GetAsync() {
            try {
                var stocks = await _database.Stocks
                    .AsNoTracking().ToListAsync();

                return stocks;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os estoques do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetStock")]
        public async Task<ActionResult<Stock>> GetByIdAsync(int id) {
            try {
                var stock = await _database.Stocks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(stock => stock.Id == id);

                if (stock == null) return NotFound();

                return stock;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar o estoque do banco de dados");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Stock stockTemp) {
            if (id != stockTemp.Id) {
                return BadRequest();
            }
            try {
                var stock = _database.Stocks
                    .FirstOrDefault(stock => stock.Id == id);

                if (stock == null) return NotFound();
                
                stock.SellValue = stockTemp.SellValue;

                _database.SaveChanges();
                return Ok();
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar editar o estoque do banco de dados");
            }
        }
    }
}