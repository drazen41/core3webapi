using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrzavaNaselje.Models;

namespace DrzavaNaselje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NaseljeController : ControllerBase
    {
        private readonly DrzavaContext _context;

        public NaseljeController(DrzavaContext context)
        {
            _context = context;
        }

        // GET: api/Naselje
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Naselje>>> GetNaselja()
        {
            var model = from n in _context.Naselja
                        join d in _context.Drzave on n.DrzavaId equals d.Id
                        select new Naselje { Id = n.Id, Naziv = n.Naziv, Drzava = d, DrzavaId = d.Id, PostanskiBroj = n.PostanskiBroj  };
            //return await _context.Naselja.Include("Drzava").ToListAsync();
            return await model.ToListAsync();
        }

        // GET: api/Naselje/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Naselje>> GetNaselje(int id)
        {
            var naselje = await _context.Naselja.FindAsync(id);

            if (naselje == null)
            {
                return NotFound();
            }

            return naselje;
        }

        // PUT: api/Naselje/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNaselje(int id, Naselje naselje)
        {
            if (id != naselje.Id)
            {
                return BadRequest();
            }

            _context.Entry(naselje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NaseljeExists(id))
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

        // POST: api/Naselje
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Naselje>> PostNaselje(Naselje naselje)
        {
            _context.Naselja.Add(naselje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNaselje", new { id = naselje.Id }, naselje);
        }

        // DELETE: api/Naselje/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Naselje>> DeleteNaselje(int id)
        {
            var naselje = await _context.Naselja.FindAsync(id);
            if (naselje == null)
            {
                return NotFound();
            }

            _context.Naselja.Remove(naselje);
            await _context.SaveChangesAsync();

            return naselje;
        }

        private bool NaseljeExists(int id)
        {
            return _context.Naselja.Any(e => e.Id == id);
        }
    }
}
