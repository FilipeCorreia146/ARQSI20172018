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

namespace Semente.Controllers
{
    //  [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    //[Route("api/Medicamentos")]
    public class MedicamentosController : Controller
    {
        private readonly SementeContext _context;

        public MedicamentosController(SementeContext context)
        {
            _context = context;
        }

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
                Apresentacao = x.Apresentacao
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
                Medicamento = x.Medicamento,
                FarmacoId = x.FarmacoId,
                Farmaco = x.Farmaco
            };

        //// GET: api/Medicamentos
        //[HttpGet]
        //public IEnumerable<Medicamento> GetMedicamento()
        //{
        //    return _context.Medicamento;
        //}

        // GET: api/Medicamentos
        // GET: api/Medicamentos/?nome={nome}
        [Route("api/Medicamentos")]
        [HttpGet]
        public async Task<IActionResult> GetMedicamento(String nome)
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

        // GET: api/Medicamentos/5
        //[HttpGet("{id}")]
        [Route("api/Medicamentos/{id}")]
        [HttpGet]
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

        // GET: Medicamentos/{id}/Apresentacoes
        [Route("api/Medicamentos/{id}/Apresentacoes")]
        [HttpGet]
        public IEnumerable<long> GetApresentacoes(int id)
        {
            IEnumerable<ApresentacaoDto> apresentacoes = _context.Apresentacao
                .Where(a => a.MedicamentoId == id)
                .Select(AsApresentacaoDto);

            List<long> ids = new List<long>();

            foreach (ApresentacaoDto a in apresentacoes)
            {
                ids.Add(a.Id);
            }

            return ids;
        }

        // GET: Medicamentos/{id}/Posologias
        [Route("api/Medicamentos/{id}/Posologias")]
        [HttpGet]
        public IEnumerable<long> GetPosologias(int id)
        {
            IEnumerable<ApresentacaoDto> apresentacoes = _context.Apresentacao
                .Where(a => a.MedicamentoId == id)
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

        // PUT: api/Medicamentos/5
        [Route("api/Medicamentos")]
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
        [Route("api/Medicamentos")]
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
        [Route("api/Medicamentos")]
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