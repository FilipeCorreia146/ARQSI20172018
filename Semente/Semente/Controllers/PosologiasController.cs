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
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/Posologias")]
    public class PosologiasController : Controller
    {
        private readonly SementeContext _context;

        public PosologiasController(SementeContext context)
        {
            _context = context;
        }

        // GET: api/Posologias
        [HttpGet]
        public IEnumerable<Posologia> GetPosologia()
        {
            return _context.Posologia.Include("Apresentacao");
        }

        // GET: api/Posologias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosologia([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posologia = await _context.Posologia.Include("Apresentacao").SingleOrDefaultAsync(m => m.Id == id);

            if (posologia == null)
            {
                return NotFound();
            }

            return Ok(posologia);
        }

        // PUT: api/Posologias/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> PutPosologia([FromRoute] long id, [FromBody] Posologia posologia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != posologia.Id)
            {
                return BadRequest();
            }

            _context.Entry(posologia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosologiaExists(id))
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

        // POST: api/Posologias
        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> PostPosologia([FromBody] Posologia posologia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Posologia.Add(posologia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosologia", new { id = posologia.Id }, posologia);
        }

        // DELETE: api/Posologias/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeletePosologia([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posologia = await _context.Posologia.SingleOrDefaultAsync(m => m.Id == id);
            if (posologia == null)
            {
                return NotFound();
            }

            _context.Posologia.Remove(posologia);
            await _context.SaveChangesAsync();

            return Ok(posologia);
        }

        private bool PosologiaExists(long id)
        {
            return _context.Posologia.Any(e => e.Id == id);
        }
    }
}