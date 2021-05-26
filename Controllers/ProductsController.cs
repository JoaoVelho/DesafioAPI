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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public ProductsController(ApplicationDbContext database) {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAsync() {
            try {
                var products = await _database.Products
                    .AsNoTracking().ToListAsync();

                return products;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os produtos do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id) {
            try {
                var product = await _database.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(prod => prod.Id == id);

                if (product == null) return NotFound();

                return product;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar o produto do banco de dados");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Product product) {
            try {
                _database.Products.Add(product);
                _database.SaveChanges();

                return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar o produto no banco de dados");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product productTemp) {
            if (id != productTemp.Id) {
                return BadRequest();
            }

            try {
                var product = _database.Products
                    .FirstOrDefault(prod => prod.Id == id);

                if (product == null) return NotFound();
                
                product.Name = productTemp.Name;
                product.Description = productTemp.Description;
                product.Unit = productTemp.Unit;

                _database.SaveChanges();
                return Ok();
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar editar o produto no banco de dados");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id) {
            try {
                var product = _database.Products
                    .FirstOrDefault(prod => prod.Id == id);

                if (product == null) return NotFound();

                try {
                    _database.Products.Remove(product);
                    _database.SaveChanges();
                } catch (Exception) {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Não foi possível deletar pois provavelmente há alguma relação entre esse item e outro");
                }

                return product;
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar deletar o produto do banco de dados");
            }
        }
    }
}