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
    public class SuppliersController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private readonly IMapper _mapper;

        public SuppliersController(ApplicationDbContext database, IMapper mapper) {
            _database = database;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplierOutDTO>>> GetAsync() {
            try {
                var suppliersTemp = await _database.Suppliers
                    .Include(supplier => supplier.Address)
                    .AsNoTracking().ToListAsync();

                var suppliers = _mapper.Map<List<SupplierOutDTO>>(suppliersTemp);

                return suppliers;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os fornecedores do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetSupplier")]
        public async Task<ActionResult<SupplierOutDTO>> GetByIdAsync(int id) {
            try {
                var supplierTemp = await _database.Suppliers
                    .Include(supplier => supplier.Address)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(supplier => supplier.Id == id);

                if (supplierTemp == null) return NotFound();

                var supplier = _mapper.Map<SupplierOutDTO>(supplierTemp);

                return supplier;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar o fornecedor do banco de dados");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] SupplierCreateDTO supplierDTO) {
            try {
                var supplier = _mapper.Map<Supplier>(supplierDTO);

                _database.Suppliers.Add(supplier);
                _database.SaveChanges();

                var supplierOut = _mapper.Map<SupplierOutDTO>(supplier);

                return new CreatedAtRouteResult("GetSupplier", new { id = supplier.Id }, supplierOut);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar o fornecedor no banco de dados");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] SupplierEditDTO supplierDTO) {
            if (id != supplierDTO.Id) {
                return BadRequest();
            }

            try {
                var supplier = _mapper.Map<Supplier>(supplierDTO);
                var address = _database.SupplierAddresses.Include(address => address.Supplier)
                    .AsNoTracking().FirstOrDefault(address => address.Supplier.Id == supplier.Id);
                
                supplier.Address.Id = address.Id;
                supplier.AddressId = address.Id;

                _database.Entry(supplier).State = EntityState.Modified;
                _database.Entry(supplier.Address).State = EntityState.Modified;
                _database.SaveChanges();
                return Ok();
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar editar o fornecedor no banco de dados");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<SupplierOutDTO> Delete(int id) {
            try {
                var supplier = _database.Suppliers
                    .Include(supplier => supplier.Address)
                    .FirstOrDefault(supplier => supplier.Id == id);

                if (supplier == null) return NotFound();

                var supplierOut = _mapper.Map<SupplierOutDTO>(supplier);

                try {
                    _database.SupplierAddresses.Remove(supplier.Address); // Delete also the supplier by Cascade
                    _database.SaveChanges();
                } catch (Exception) {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Não foi possível deletar pois provavelmente há alguma relação entre esse item e outro");
                }

                return supplierOut;
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar deletar o fornecedor do banco de dados");
            }
        }
    }
}