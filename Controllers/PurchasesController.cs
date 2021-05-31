using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
    public class PurchasesController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private readonly IMapper _mapper;

        public PurchasesController(ApplicationDbContext database, IMapper mapper) {
            _database = database;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseOutDTO>>> GetAsync() {
            try {
                var purchases = await _database.Purchases
                    .Include(purch => purch.PurchaseItems)
                    .AsNoTracking().ToListAsync();

                var purchasesDTO = _mapper.Map<List<PurchaseOutDTO>>(purchases);

                return purchasesDTO;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar as compras do banco de dados");
            }
        }

        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult<List<PurchaseOutDTO>>> GetBySupplierIdAsync(int supplierId) {
            try {
                var purchases = await _database.Purchases
                    .Include(purch => purch.PurchaseItems)
                    .Where(purch => purch.SupplierId == supplierId)
                    .AsNoTracking().ToListAsync();

                var purchasesDTO = _mapper.Map<List<PurchaseOutDTO>>(purchases);

                return purchasesDTO;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar as compras do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetPurchase")]
        public async Task<ActionResult<PurchaseOutDTO>> GetByIdAsync(int id) {
            try {
                var purchase = await _database.Purchases
                    .Include(purch => purch.PurchaseItems)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(purch => purch.Id == id);

                if (purchase == null) return NotFound();

                var purchaseDTO = _mapper.Map<PurchaseOutDTO>(purchase);

                return purchaseDTO;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar a compra do banco de dados");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] PurchaseCreateDTO purchaseDTO) {
            try {
                var purchase = _mapper.Map<Purchase>(purchaseDTO);
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

                var purchaseOut = _mapper.Map<PurchaseOutDTO>(purchase);

                return new CreatedAtRouteResult("GetPurchase", new { id = purchase.Id }, purchaseOut);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar a compra no banco de dados");
            }
        }
    }
}