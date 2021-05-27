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
    public class PurchasesController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public PurchasesController(ApplicationDbContext database) {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<List<Purchase>>> GetAsync() {
            try {
                var purchases = await _database.Purchases
                    .Include(purch => purch.Supplier)
                    .Include(purch => purch.PurchaseItems)
                    .AsNoTracking().ToListAsync();

                return purchases;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar as compras do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetPurchase")]
        public async Task<ActionResult<Purchase>> GetByIdAsync(int id) {
            try {
                var purchase = await _database.Purchases
                    .Include(purch => purch.Supplier)
                    .Include(purch => purch.PurchaseItems)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(purch => purch.Id == id);

                if (purchase == null) return NotFound();

                return purchase;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar a compra do banco de dados");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Purchase purchase) {
            try {
                _database.Purchases.Add(purchase);

                foreach (var purchaseItem in purchase.PurchaseItems) {
                    var stock = _database.Stocks
                        .FirstOrDefault(stock => stock.ProductId == purchaseItem.ProductId);
                    
                    if (stock != null) {
                        stock.Quantity += purchaseItem.Quantity;
                        stock.SellValue = purchaseItem.Value;
                    } else {
                        var product = _database.Products
                            .FirstOrDefault(prod => prod.Id == purchaseItem.ProductId);

                        if (product == null) return NotFound();

                        Stock newStock = new Stock {
                            Product = product,
                            Quantity = purchaseItem.Quantity,
                            SellValue = purchaseItem.Value
                        };

                        _database.Stocks.Add(newStock);
                    }
                }

                _database.SaveChanges();

                return new CreatedAtRouteResult("GetPurchase", new { id = purchase.Id }, purchase);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar a compra no banco de dados");
            }
        }
    }
}