using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semente.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Semente.DTOs;

namespace Semente.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
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
        //[EnableCors("AllowSpecificOrigin")]
        public IEnumerable<Apresentacao> GetApresentacao()
        {
            return _context.Apresentacao.Include(a => a.Medicamento).Include(a => a.Farmaco);
        }
        /*public IEnumerable<Apresentacao> GetApresentacao()
        {
            IEnumerable<Apresentacao> apresentacoes = _context.Apresentacao.Include(a => a.Medicamento).Include(a => a.Farmaco);
            
            /*foreach(Apresentacao a in apresentacoes)
            {
                a.NomeFarmaco = a.Farmaco.Nome;
                a.NomeMedicamento = a.Medicamento.Nome;
            }

            return apresentacoes;
        }**/

        // GET: api/Apresentacoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApresentacao([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var apresentacao = await _context.Apresentacao.Include(a => a.Medicamento).Include(a => a.Farmaco)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (apresentacao == null)
            {
                return NotFound();
            }

            return Ok(apresentacao);
        }

        [Route("api/Apresentacoes/{id}/Posologias")]
        [HttpGet]
        public IEnumerable<Posologia> GetPosologias(int id)
        {

            IEnumerable<Posologia> posologias = _context.Posologia.
                Where(p => p.ApresentacaoId == id);

            return posologias;
        }

        // PUT: api/Apresentacoes/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        //[Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
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