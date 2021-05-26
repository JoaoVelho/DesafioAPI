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
                    "Erro ao tentar buscar os fornecedores do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetSupplier")]
        public async Task<ActionResult<Supplier>> GetByIdAsync(int id) {
            try {
                var supplier = await _database.Suppliers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(supplier => supplier.Id == id);

                if (supplier == null) return NotFound();

                return supplier;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar o fornecedor do banco de dados");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Supplier supplier) {
            try {
                _database.Suppliers.Add(supplier);
                _database.SaveChanges();

                return new CreatedAtRouteResult("GetSupplier", new { id = supplier.Id }, supplier);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar criar o fornecedor no banco de dados");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Supplier supplier) {
            if (supplier == null) return NotFound();

            if (id != supplier.Id) {
                return BadRequest();
            }

            try {
                _database.Entry(supplier).State = EntityState.Modified;
                _database.SaveChanges();
                return Ok();
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar editar o fornecedor no banco de dados");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Supplier> Delete(int id) {
            try {
                var supplier = _database.Suppliers
                    .Include(supplier => supplier.Address)
                    .FirstOrDefault(supplier => supplier.Id == id);

                if (supplier == null) return NotFound();

                _database.SupplierAddresses.Remove(supplier.Address); // Delete also the supplier by Cascade
                _database.SaveChanges();

                return supplier;
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar deletar o fornecedor do banco de dados");
            }
        }
    }
}