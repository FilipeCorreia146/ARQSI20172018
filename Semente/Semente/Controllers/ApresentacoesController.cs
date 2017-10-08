using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semente.Models;

namespace Semente.Controllers
{
    [Produces("application/json")]
    [Route("api/Apresentacoes")]
    public class ApresentacoesController : Controller
    {
        private readonly SementeContext _context;

        public ApresentacoesController(SementeContext context)
        {
            _context = context;
        }

        // GET: api/Apresentacoes
        [HttpGet]
        public IEnumerable<Apresentacao> GetApresentacao()
        {
            return _context.Apresentacao;
        }

        // GET: api/Apresentacoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApresentacao([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var apresentacao = await _context.Apresentacao.SingleOrDefaultAsync(m => m.Id == id);

            if (apresentacao == null)
            {
                return NotFound();
            }

            return Ok(apresentacao);
        }

        // PUT: api/Apresentacoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApresentacao([FromRoute] long id, [FromBody] Apresentacao apresentacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != apresentacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(apresentacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApresentacaoExists(id))
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

        // POST: api/Apresentacoes
        [HttpPost]
        public async Task<IActionResult> PostApresentacao([FromBody] Apresentacao apresentacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Apresentacao.Add(apresentacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApresentacao", new { id = apresentacao.Id }, apresentacao);
        }

        // DELETE: api/Apresentacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApresentacao([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var apresentacao = await _context.Apresentacao.SingleOrDefaultAsync(m => m.Id == id);
            if (apresentacao == null)
            {
                return NotFound();
            }

            _context.Apresentacao.Remove(apresentacao);
            await _context.SaveChangesAsync();

            return Ok(apresentacao);
        }

        private bool ApresentacaoExists(long id)
        {
            return _context.Apresentacao.Any(e => e.Id == id);
        }
    }
}