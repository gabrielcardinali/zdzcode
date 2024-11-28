using doacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doacoes.Data.Context;

namespace doacoes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstituicaoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public InstituicaoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Instituicao>> Post([FromBody] Instituicao request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instituicao = new Instituicao(
                request.Id,
                request.Nome,
                request.Responsavel,
                request.Email,
                request.Telefone,
                request.Endereco
            );

            _dbContext.Instituicoes.Add(instituicao);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = instituicao.Id }, instituicao);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Instituicao>> Get(int id)
        {
            var instituicao = await _dbContext.Instituicoes.FirstOrDefaultAsync(i => i.Id == id);

            if (instituicao == null)
            {
                return NotFound();
            }

            return instituicao;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Instituicao request)
        {
            var instituicao = await _dbContext.Instituicoes.FindAsync(id);
            if (instituicao == null)
            {
                return NotFound();
            }

            instituicao = new Instituicao(
                id,
                request.Nome,
                request.Responsavel,
                request.Email,
                request.Telefone,
                request.Endereco
            );

            _dbContext.Entry(instituicao).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstituicaoExists(id))
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
        public async Task<ActionResult<IEnumerable<Instituicao>>> Get()
        {
            return await _dbContext.Instituicoes.ToListAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var instituicao = await _dbContext.Instituicoes.FindAsync(id);
            if (instituicao == null)
            {
                return NotFound();
            }

            _dbContext.Instituicoes.Remove(instituicao);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool InstituicaoExists(int id)
        {
            return _dbContext.Instituicoes.Any(i => i.Id == id);
        }
    }
}