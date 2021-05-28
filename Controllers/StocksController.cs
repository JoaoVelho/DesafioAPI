using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.Data;
using DesafioAPI.DTOs;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    public class StocksController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public StocksController(ApplicationDbContext database) {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<List<StockOutDTO>>> GetAsync() {
            try {
                var stocksTemp = await _database.Stocks
                    .AsNoTracking().ToListAsync();

                var stocks = new List<StockOutDTO>();
                stocksTemp.ForEach(stockTemp => {
                    var stock = new StockOutDTO {
                        Id = stockTemp.Id,
                        ProductId = stockTemp.ProductId,
                        Quantity = stockTemp.Quantity,
                        SellValue = stockTemp.SellValue
                    };
                    stocks.Add(stock);
                });

                return stocks;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os estoques do banco de dados");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockOutDTO>> GetByIdAsync(int id) {
            try {
                var stockTemp = await _database.Stocks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(stock => stock.Id == id);

                if (stockTemp == null) return NotFound();

                var stock = new StockOutDTO {
                    Id = stockTemp.Id,
                    ProductId = stockTemp.ProductId,
                    Quantity = stockTemp.Quantity,
                    SellValue = stockTemp.SellValue
                };

                return stock;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar o estoque do banco de dados");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] StockEditDTO stockDTOTemp) {
            if (id != stockDTOTemp.Id) {
                return BadRequest();
            }
            
            try {
                var stock = _database.Stocks
                    .FirstOrDefault(stock => stock.Id == id);

                if (stock == null) return NotFound();
                
                stock.SellValue = stockDTOTemp.SellValue;

                _database.SaveChanges();
                return Ok();
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar editar o estoque do banco de dados");
            }
        }
    }
}