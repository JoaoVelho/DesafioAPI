using System;
using System.Collections.Generic;
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
    public class SuppliersController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public SuppliersController(ApplicationDbContext database) {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAsync() {
            try {
                var suppliers = await _database.Suppliers
                    .AsNoTracking().ToListAsync();

                return suppliers;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os estoques do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetSupplier")]
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
    }
}