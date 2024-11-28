using doacoes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doacoes.Data.Context;

namespace doacoes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoacaoTipoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DoacaoTipoController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<DoacaoTipo>> Post([FromBody] DoacaoTipo request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doacaoTipo = new DoacaoTipo(
                request.Id,
                request.Descricao
            );

            _dbContext.DoacaoTipos.Add(doacaoTipo);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = doacaoTipo.Id }, doacaoTipo);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DoacaoTipo>> Get(int id)
        {
            var doacaoTipo = await _dbContext.DoacaoTipos.FirstOrDefaultAsync(dt => dt.Id == id);

            if (doacaoTipo == null)
            {
                return NotFound();
            }

            return doacaoTipo;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] DoacaoTipo request)
        {
            var doacaoTipo = await _dbContext.DoacaoTipos.FindAsync(id);
            if (doacaoTipo == null)
            {
                return NotFound();
            }

            doacaoTipo = new DoacaoTipo(
                id,
                request.Descricao
            );

            _dbContext.Entry(doacaoTipo).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoacaoTipoExists(id))
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
        public async Task<ActionResult<IEnumerable<DoacaoTipo>>> Get()
        {
            return await _dbContext.DoacaoTipos.ToListAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doacaoTipo = await _dbContext.DoacaoTipos.FindAsync(id);
            if (doacaoTipo == null)
            {
                return NotFound();
            }

            _dbContext.DoacaoTipos.Remove(doacaoTipo);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool DoacaoTipoExists(int id)
        {
            return _dbContext.DoacaoTipos.Any(dt => dt.Id == id);
        }
    }
}