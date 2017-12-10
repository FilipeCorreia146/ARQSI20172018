using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semente.Models;

namespace Semente
{
    [Produces("application/json")]
    [Route("api/Reacoes")]
    public class ReacoesController : Controller
    {
        private readonly SementeContext _context;

        public ReacoesController(SementeContext context)
        {
            _context = context;
        }

        // GET: api/Reacoes
        [HttpGet]
        public IEnumerable<Reacao> GetReacao()
        {
            return _context.Reacao.Include(r => r.Farmaco);
        }

        // GET: api/Reacoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReacao([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reacao = await _context.Reacao.Include(r => r.Farmaco).SingleOrDefaultAsync(m => m.Id == id);

            if (reacao == null)
            {
                return NotFound();
            }

            return Ok(reacao);
        }

        // PUT: api/Reacoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReacao([FromRoute] int id, [FromBody] Reacao reacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(reacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReacaoExists(id))
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

        // POST: api/Reacoes
        [HttpPost]
        public async Task<IActionResult> PostReacao([FromBody] Reacao reacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Reacao.Add(reacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReacao", new { id = reacao.Id }, reacao);
        }

        // DELETE: api/Reacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReacao([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reacao = await _context.Reacao.SingleOrDefaultAsync(m => m.Id == id);
            if (reacao == null)
            {
                return NotFound();
            }

            _context.Reacao.Remove(reacao);
            await _context.SaveChangesAsync();

            return Ok(reacao);
        }

        private bool ReacaoExists(int id)
        {
            return _context.Reacao.Any(e => e.Id == id);
        }
    }
}