using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DesafioAPI.Data;
using DesafioAPI.DTOs;
using DesafioAPI.Models;
using DesafioAPI.Services;
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
    public class SellingsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public SellingsController(ApplicationDbContext database, IMapper mapper, 
            IMailService mailService) {
            _database = database;
            _mapper = mapper;
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SellingOutDTO>>> GetAsync() {
            try {
                var isAdmin = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals(ClaimTypes.Role));
                string userId = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals("userId")).Value;

                var sellings = await _database.Sellings
                    .Include(sell => sell.SellingItems)
                    .Where(sell => isAdmin == null ? sell.ClientId == userId : true)
                    .AsNoTracking().ToListAsync();

                var sellingsDTO = _mapper.Map<List<SellingOutDTO>>(sellings);

                return sellingsDTO;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar as vendas do banco de dados");
            }
        }

        [HttpGet("client/{clientId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<SellingOutDTO>>> GetByClientIdAsync(string clientId) {
            try {
                var sellings = await _database.Sellings
                    .Include(sell => sell.SellingItems)
                    .Where(sell => sell.ClientId == clientId)
                    .AsNoTracking().ToListAsync();

                var sellingsDTO = _mapper.Map<List<SellingOutDTO>>(sellings);

                return sellingsDTO;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar as vendas do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetSelling")]
        public async Task<ActionResult<SellingOutDTO>> GetByIdAsync(int id) {
            try {
                var isAdmin = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals(ClaimTypes.Role));
                string userId = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals("userId")).Value;

                var selling = await _database.Sellings
                    .Include(sell => sell.SellingItems)
                    .Where(sell => isAdmin == null ? sell.ClientId == userId : true)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(sell => sell.Id == id);

                if (selling == null) return NotFound();

                var sellingDTO = _mapper.Map<SellingOutDTO>(selling);

                return sellingDTO;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar a venda do banco de dados");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SellingOutDTO>> Create([FromBody] SellingCreateDTO sellingDTO) {
            try {
                var selling = _mapper.Map<Selling>(sellingDTO);

                selling.ClientId = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals("userId")).Value;
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

                var email = _database.Clients.AsNoTracking()
                    .FirstOrDefault(client => client.Id == selling.ClientId).Email;

                MailRequest request = new MailRequest {
                    ToEmail = email,
                    Subject = "Compra Realizada",
                    Body = "Compra realizada com sucesso, obrigado por comprar conosco!"
                };
                await _mailService.SendEmailAsync(request);

                var sellingOut = _mapper.Map<SellingOutDTO>(selling);

                return new CreatedAtRouteResult("GetSelling", new { id = selling.Id }, sellingOut);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar a venda no banco de dados");
            }
        }

        [HttpPut("{id}/confirmation")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Put))]
        public ActionResult Put(int id) {
            try {
                string userId = HttpContext.User.Claims
                    .FirstOrDefault(claim => claim.Type.ToString().Equals("userId")).Value;

                var selling = _database.Sellings
                    .FirstOrDefault(sell => sell.Id == id);

                if (selling == null) return NotFound();
                if (selling.ClientId != userId) return NotFound();
                
                selling.Confirmed = true;

                _database.SaveChanges();
                return NoContent();
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar confirmar a venda no banco de dados");
            }
        }
    }
}