using doacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doacoes.Data.Context;
using doacoes.Dtos;

namespace doacoes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoacaoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DoacaoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Doacao>> Post([FromBody] AdicionarDoacaoCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doacao = new Doacao(
                request.Id,
                request.DoadorId,
                request.InstituicaoId,
                request.DoacaoTipoId,
                request.Valor,
                request.Quantidade,
                request.Descricao
            );

            _dbContext.Doacoes.Add(doacao);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = doacao.Id }, doacao);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Doacao>> Get(int id)
        {
            var doacao = await _dbContext.Doacoes.FirstOrDefaultAsync(d => d.Id == id);

            if (doacao == null)
            {
                return NotFound();
            }

            return doacao;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditarDoacaoCommand request)
        {
            var doacao = await _dbContext.Doacoes.FindAsync(id);
            if (doacao == null)
            {
                return NotFound();
            }

            doacao = new Doacao(
                id,
                request.DoadorId,
                request.InstituicaoId,
                request.DoacaoTipoId,
                request.Valor,
                request.Quantidade,
                request.Descricao
            );

            _dbContext.Entry(doacao).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoacaoExists(id))
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doacao>>> Get()
        {
            return await _dbContext.Doacoes.ToListAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doacao = await _dbContext.Doacoes.FindAsync(id);
            if (doacao == null)
            {
                return NotFound();
            }

            _dbContext.Doacoes.Remove(doacao);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool DoacaoExists(int id)
        {
            return _dbContext.Doacoes.Any(e => e.Id == id);
        }
    }
}