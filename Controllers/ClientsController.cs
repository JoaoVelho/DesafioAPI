using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DesafioAPI.Data;
using DesafioAPI.DTOs;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DesafioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;
        private readonly IConfiguration _configuration;

        public ClientsController(ApplicationDbContext database, UserManager<Client> userManager, 
            SignInManager<Client> signInManager, IConfiguration configuration) {
            _database = database;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDTO>>> GetAsync() {
            try {
                var clientsTemp = await _database.Clients.Include(client => client.Address)
                    .AsNoTracking().ToListAsync();

                List<ClientDTO> clients = new List<ClientDTO>();
                clientsTemp.ForEach(clientTemp => {
                    ClientDTO client = new ClientDTO {
                        Id = clientTemp.Id,
                        CPF = clientTemp.CPF,
                        Name = clientTemp.Name,
                        Phone = clientTemp.PhoneNumber,
                        Email = clientTemp.Email,
                        Address = clientTemp.Address
                    };
                    clients.Add(client);
                });

                return clients;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os clientes do banco de dados");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO model) {
            if (model.Password == model.ConfirmPassword) {
                var client = new Client {
                    Name = model.Name,
                    CPF = model.CPF,
                    PhoneNumber = model.Phone,
                    Address = model.Address,
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(client, model.Password);

                if (!result.Succeeded) {
                    return BadRequest(result.Errors);
                }

                await _signInManager.SignInAsync(client, false);
                return Ok(GenerateToken(model));
            } else {
                ModelState.AddModelError("Erro", "Senhas devem ser iguais");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO model) {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, 
                isPersistent: false, lockoutOnFailure: false);
            
            if (result.Succeeded) {
                return Ok(GenerateToken(model));
            } else {
                ModelState.AddModelError("Erro", "Login inválido...");
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ClientDTO clientDTO) {
            if (id != clientDTO.Id) {
                return BadRequest();
            }

            var client = _database.Clients.Include(client => client.Address)
                .FirstOrDefault(client => client.Id == id);
            
            client.Name = clientDTO.Name;
            client.CPF = clientDTO.CPF;
            client.PhoneNumber = clientDTO.Phone;
            client.UserName = clientDTO.Email;
            client.Email = clientDTO.Email;

            client.Address.Street = clientDTO.Address.Street;
            client.Address.Number = clientDTO.Address.Number;
            client.Address.Complement = clientDTO.Address.Complement;
            client.Address.CEP = clientDTO.Address.CEP;
            client.Address.District = clientDTO.Address.District;
            client.Address.City = clientDTO.Address.City;
            client.Address.State = clientDTO.Address.State;

            var result = await _userManager.UpdateAsync(client);

            if (result.Succeeded) {
                _database.SaveChanges();
                return Ok();
            } else {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Client> Delete(string id) {
            try {
                var client = _database.Clients
                    .Include(client => client.Address)
                    .FirstOrDefault(client => client.Id == id);

                if (client == null) return NotFound();

                _database.ClientAddresses.Remove(client.Address); // Delete also the client by Cascade
                _database.SaveChanges();

                return client;
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar deletar o cliente do banco de dados");
            }
        }

        private UserToken GenerateToken(UserLoginDTO model) {
            // define user claims
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // generate key using symmetric algorithm
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            // generate token digital signature using private key and Sha256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Expiration time
            var expirationTime = _configuration["TokenConfigurations:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expirationTime));

            // generate JWT token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfigurations:Issuer"],
                audience: _configuration["TokenConfigurations:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new UserToken() {
                Authenticated = true,
                Expiration = expiration,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}