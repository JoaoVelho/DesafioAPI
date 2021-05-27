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
    public class SellingsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public SellingsController(ApplicationDbContext database) {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<List<Selling>>> GetAsync() {
            try {
                var sellings = await _database.Sellings
                    .Include(sell => sell.Client)
                    .Include(sell => sell.SellingItems)
                    .AsNoTracking().ToListAsync();

                return sellings;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar as vendas do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetSelling")]
        public async Task<ActionResult<Selling>> GetByIdAsync(int id) {
            try {
                var selling = await _database.Sellings
                    .Include(sell => sell.Client)
                    .Include(sell => sell.SellingItems)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(sell => sell.Id == id);

                if (selling == null) return NotFound();

                return selling;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar a venda do banco de dados");
            }
        }
    }
}