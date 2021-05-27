using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.Data;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
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

        [HttpPost]
        public ActionResult Create([FromBody] Selling selling) {
            try {
                selling.Confirmed = false;
                _database.Sellings.Add(selling);

                foreach (var sellingItem in selling.SellingItems) {
                    var stock = _database.Stocks
                        .FirstOrDefault(stock => stock.ProductId == sellingItem.ProductId);
                    
                    if (stock == null) return NotFound();
                    if (stock.Quantity < sellingItem.Quantity) return BadRequest();

                    stock.Quantity -= sellingItem.Quantity;
                }

                _database.SaveChanges();

                return new CreatedAtRouteResult("GetSelling", new { id = selling.Id }, selling);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar a venda no banco de dados");
            }
        }

        [HttpPut("{id}/confirmation")]
        public ActionResult Put(int id) {
            try {
                var selling = _database.Sellings
                    .FirstOrDefault(sell => sell.Id == id);

                if (selling == null) return NotFound();
                
                selling.Confirmed = true;

                _database.SaveChanges();
                return Ok();
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar confirmar a venda no banco de dados");
            }
        }
    }
}