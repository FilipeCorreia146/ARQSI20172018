﻿using System;
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
    [Route("api/Farmacos")]
    public class FarmacosController : Controller
    {
        private readonly SementeContext _context;

        public FarmacosController(SementeContext context)
        {
            _context = context;
        }

        // GET: api/Farmacos
        [HttpGet]
        public IEnumerable<Farmaco> GetFarmaco()
        {
            return _context.Farmaco;
        }

        // GET: api/Farmacos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFarmaco([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var farmaco = await _context.Farmaco.SingleOrDefaultAsync(m => m.Id == id);

            if (farmaco == null)
            {
                return NotFound();
            }

            return Ok(farmaco);
        }

        // PUT: api/Farmacos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFarmaco([FromRoute] int id, [FromBody] Farmaco farmaco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != farmaco.Id)
            {
                return BadRequest();
            }

            _context.Entry(farmaco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FarmacoExists(id))
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

        // POST: api/Farmacos
        [HttpPost]
        public async Task<IActionResult> PostFarmaco([FromBody] Farmaco farmaco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Farmaco.Add(farmaco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFarmaco", new { id = farmaco.Id }, farmaco);
        }

        // DELETE: api/Farmacos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarmaco([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var farmaco = await _context.Farmaco.SingleOrDefaultAsync(m => m.Id == id);
            if (farmaco == null)
            {
                return NotFound();
            }

            _context.Farmaco.Remove(farmaco);
            await _context.SaveChangesAsync();

            return Ok(farmaco);
        }

        private bool FarmacoExists(int id)
        {
            return _context.Farmaco.Any(e => e.Id == id);
        }
    }
}