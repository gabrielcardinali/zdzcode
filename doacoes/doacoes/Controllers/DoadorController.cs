using doacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doacoes.Data.Context;

namespace doacoes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoadorController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DoadorController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Doador>> Post([FromBody] Doador request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doador = new Doador(
                request.Id,
                request.Nome,
                request.Email,
                request.Telefone,
                request.Endereco
            );

            _dbContext.Doadores.Add(doador);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = doador.Id }, doador);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Doador>> Get(int id)
        {
            var doador = await _dbContext.Doadores.FirstOrDefaultAsync(d => d.Id == id);

            if (doador == null)
            {
                return NotFound();
            }

            return doador;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Doador request)
        {
            var doador = await _dbContext.Doadores.FindAsync(id);
            if (doador == null)
            {
                return NotFound();
            }

            doador.Nome = request.Nome;
            doador.Email = request.Email;
            doador.Telefone = request.Telefone;
            doador.Endereco = request.Endereco;

            _dbContext.Entry(doador).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoadorExists(id))
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
        public async Task<ActionResult<IEnumerable<Doador>>> Get()
        {
            return await _dbContext.Doadores.ToListAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doador = await _dbContext.Doadores.FindAsync(id);
            if (doador == null)
            {
                return NotFound();
            }

            _dbContext.Doadores.Remove(doador);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool DoadorExists(int id)
        {
            return _dbContext.Doadores.Any(e => e.Id == id);
        }
    }
}