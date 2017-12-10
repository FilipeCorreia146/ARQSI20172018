using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semente.Models;
using System.Linq.Expressions;
using Semente.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Semente.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    //[Route("api/Farmacos")]
    public class FarmacosController : Controller
    {
        private readonly SementeContext _context;

        private static readonly Expression<Func<Farmaco, FarmacoDto>> AsFarmacoDto =
            x => new FarmacoDto
            {
                Id = x.Id,
                Nome = x.Nome
            };

        private static readonly Expression<Func<Medicamento, MedicamentoDto>> AsMedicamentoDto =
            x => new MedicamentoDto
            {
                Id = x.Id,
                Nome = x.Nome
            };

        private static readonly Expression<Func<Posologia, PosologiaDto>> AsPosologiaDto =
            x => new PosologiaDto
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Dose = x.Dose,
                ApresentacaoId = x.ApresentacaoId,
                //Apresentacao = x.Apresentacao
            };

        private static readonly Expression<Func<Apresentacao, ApresentacaoDto>> AsApresentacaoDto =
            x => new ApresentacaoDto
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Forma = x.Forma,
                Concentracao = x.Concentracao,
                Qtd = x.Qtd,
                MedicamentoId = x.MedicamentoId,
                //Medicamento = x.Medicamento,
                FarmacoId = x.FarmacoId,
                //Farmaco = x.Farmaco
            };

        public FarmacosController(SementeContext context)
        {
            _context = context;
        }

        //// GET: api/Farmacos
        //[HttpGet]
        //public IEnumerable<Farmaco> GetFarmaco()
        //{
        //    return _context.Farmaco;
        //}

        // GET: api/Farmacos
        // GET: api/Farmacos/?nome={nome}
        [Route("api/Farmacos")]
        [HttpGet]
        public async Task<IActionResult> GetFarmaco(String nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (nome == null)
            {
                return Ok(_context.Medicamento);
            }

            var medicamento = await _context.Medicamento.SingleOrDefaultAsync(m => m.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (medicamento == null)
            {
                return NotFound();
            }

            return Ok(medicamento);
        }

        // GET: api/Farmacos/5
        //[HttpGet("{id}")]
        [Route("api/Farmacos/{id}")]
        [HttpGet]
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

        // GET: Farmacos/{id}/Medicamentos
        [Route("api/Farmacos/{id}/Medicamentos")]
        [HttpGet]
        public IEnumerable<long> GetMedicamentos(int id)
        {
            IEnumerable<ApresentacaoDto> apresentacoes = _context.Apresentacao
                .Where(a => a.FarmacoId == id)
                .Select(AsApresentacaoDto);

            List<long> ids = new List<long>();

            Boolean b = false;

            foreach (ApresentacaoDto a in apresentacoes)
            {
                foreach (int i in ids)
                {
                    if (i == a.MedicamentoId)
                    {
                        b = true;
                    }
                }
                if (b == false)
                {
                    ids.Add(a.MedicamentoId);
                }
            }

            return ids;
        }

        // GET: Farmacos/{id}/Posologias
        [Route("api/Farmacos/{id}/Posologias")]
        [HttpGet]
        public IEnumerable<long> GetPosologias(int id)
        {
            IEnumerable<ApresentacaoDto> apresentacoes = _context.Apresentacao
                .Where(a => a.FarmacoId == id)
                .Select(AsApresentacaoDto);

            IEnumerable<PosologiaDto> posologias = _context.Posologia
                .Select(AsPosologiaDto);

            List<long> ids = new List<long>();

            foreach (ApresentacaoDto a in apresentacoes)
            {
                foreach (PosologiaDto p in posologias)
                {
                    if (p.ApresentacaoId == a.Id)
                    {
                        ids.Add(p.Id);
                    }
                }
            }

            return ids;
        }

        // GET: Farmacos/{id}/Apresentacoes
        [Route("api/Farmacos/{id}/Apresentacoes")]
        [HttpGet]
        public IEnumerable<long> GetApresentacoes(int id)
        {
            IEnumerable<ApresentacaoDto> apresentacoes = _context.Apresentacao
                .Where(a => a.FarmacoId == id)
                .Select(AsApresentacaoDto);

            List<long> ids = new List<long>();

            foreach (ApresentacaoDto a in apresentacoes)
            {
                ids.Add(a.Id);
            }

            return ids;
        }

        [Route("api/Farmacos/{id}/Reacoes")]
        [HttpGet]
        public IEnumerable<Reacao> GetReacoes(int id)
        {

            IEnumerable<Reacao> reacoes = _context.Reacao.Where(r => r.FarmacoId == id);

            return reacoes;
        }

        // PUT: api/Farmacos/5
        [Route("api/Farmacos")]
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Route("api/Farmacos")]
        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Bearer")]
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
        [Route("api/Farmacos")]
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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