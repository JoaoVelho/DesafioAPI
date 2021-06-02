using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DesafioAPI.Data;
using DesafioAPI.DTOs;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDbContext database, IMapper mapper) {
            _database = database;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAsync() {
            try {
                var isAdmin = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals(ClaimTypes.Role));
                var products = new List<Product>();

                if (isAdmin != null && isAdmin.Value.Equals("Admin")) {
                    products = await _database.Products
                        .AsNoTracking().ToListAsync();
                } else {
                    var stocks = await _database.Stocks.Include(stock => stock.Product)
                        .AsNoTracking().ToListAsync();
                    
                    stocks.ForEach(stock => {
                        if (stock.Quantity > 0 && stock.SellValue > 0) {
                            products.Add(stock.Product);
                        }
                    });
                }

                return products;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os produtos do banco de dados");
            }
        }

        [HttpGet("asc")]
        public async Task<ActionResult<List<Product>>> GetAscAsync() {
            try {
                var isAdmin = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals(ClaimTypes.Role));
                var products = new List<Product>();

                if (isAdmin != null && isAdmin.Value.Equals("Admin")) {
                    products = await _database.Products
                        .AsNoTracking().ToListAsync();
                } else {
                    var stocks = await _database.Stocks.Include(stock => stock.Product)
                        .AsNoTracking().ToListAsync();
                    
                    stocks.ForEach(stock => {
                        if (stock.Quantity > 0 && stock.SellValue > 0) {
                            products.Add(stock.Product);
                        }
                    });
                }

                products = products.OrderBy(prod => prod.Name).ToList();

                return products;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os produtos do banco de dados");
            }
        }

        [HttpGet("desc")]
        public async Task<ActionResult<List<Product>>> GetDescAsync() {
            try {
                var isAdmin = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals(ClaimTypes.Role));
                var products = new List<Product>();

                if (isAdmin != null && isAdmin.Value.Equals("Admin")) {
                    products = await _database.Products
                        .AsNoTracking().ToListAsync();
                } else {
                    var stocks = await _database.Stocks.Include(stock => stock.Product)
                        .AsNoTracking().ToListAsync();
                    
                    stocks.ForEach(stock => {
                        if (stock.Quantity > 0 && stock.SellValue > 0) {
                            products.Add(stock.Product);
                        }
                    });
                }
                
                products = products.OrderByDescending(prod => prod.Name).ToList();

                return products;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os produtos do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id) {
            try {
                var isAdmin = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals(ClaimTypes.Role));

                var product = await _database.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(prod => prod.Id == id);

                if (product == null) return NotFound();

                if (isAdmin == null) {
                    var stock = _database.Stocks.AsNoTracking()
                        .FirstOrDefault(stock => stock.ProductId == product.Id);
                    
                    if (stock == null) return NotFound();
                }

                return product;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar o produto do banco de dados");
            }
        }

        [HttpGet("name/{name}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<Product>> GetByNameAsync(string name) {
            try {
                var isAdmin = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals(ClaimTypes.Role));

                var product = await _database.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(prod => prod.Name == name);

                if (product == null) return NotFound();

                if (isAdmin == null) {
                    var stock = _database.Stocks.AsNoTracking()
                        .FirstOrDefault(stock => stock.ProductId == product.Id);
                    
                    if (stock == null) return NotFound();
                }

                return product;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar o produto do banco de dados");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Product> Create([FromBody] ProductCreateDTO productDTO) {
            try {
                var product = _mapper.Map<Product>(productDTO);

                _database.Products.Add(product);
                _database.SaveChanges();

                return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar o produto no banco de dados");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Put(int id, [FromBody] ProductEditDTO productDTO) {
            if (id != productDTO.Id) {
                return BadRequest("Id da url diferente do id do corpo da requisição");
            }

            try {
                var productTemp = _database.Products.AsNoTracking()
                    .FirstOrDefault(prod => prod.Id == productDTO.Id);

                if (productTemp == null) return NotFound();

                var product = _mapper.Map<Product>(productDTO);

                _database.Entry(product).State = EntityState.Modified;
                _database.SaveChanges();
                return NoContent();
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar editar o produto no banco de dados");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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