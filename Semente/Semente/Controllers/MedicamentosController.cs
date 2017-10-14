using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semente.Models;
using Microsoft.AspNetCore.Authorization;

namespace Semente.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/Medicamentos")]
    public class MedicamentosController : Controller
    {
        private readonly SementeContext _context;

        public MedicamentosController(SementeContext context)
        {
            _context = context;
        }

        // GET: api/Medicamentos
        [HttpGet]
        public IEnumerable<Medicamento> GetMedicamento()
        {
            return _context.Medicamento;
        }

        // GET: api/Medicamentos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicamento([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicamento = await _context.Medicamento.SingleOrDefaultAsync(m => m.Id == id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return Ok(medicamento);
        }

        [HttpGet("{Nome}")]
        public async Task<IActionResult> GetMedicamentoByNome(String nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var medicamento = await _context.Medicamento.SingleOrDefaultAsync(m => m.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (medicamento == null)
            {
                return NotFound();
            }

            return Ok(medicamento);
        }

        // PUT: api/Medicamentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento([FromRoute] long id, [FromBody] Medicamento medicamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Medicamentos
        [HttpPost]
        public async Task<IActionResult> PostMedicamento([FromBody] Medicamento medicamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Medicamento.Add(medicamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicamento", new { id = medicamento.Id }, medicamento);
        }

        // DELETE: api/Medicamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamento([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicamento = await _context.Medicamento.SingleOrDefaultAsync(m => m.Id == id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.Medicamento.Remove(medicamento);
            await _context.SaveChangesAsync();

            return Ok(medicamento);
        }

        private bool MedicamentoExists(long id)
        {
            return _context.Medicamento.Any(e => e.Id == id);
        }
    }
}