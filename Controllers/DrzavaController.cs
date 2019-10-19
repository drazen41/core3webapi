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
    public class DrzavaController : ControllerBase
    {
        private readonly DrzavaContext _context;

        public DrzavaController(DrzavaContext context)
        {
            _context = context;
        }

        // GET: api/Drzava
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drzava>>> GetDrzave()
        {
            return await _context.Drzave.ToListAsync();
        }

        // GET: api/Drzava/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drzava>> GetDrzava(int id)
        {
            var drzava = await _context.Drzave.FindAsync(id);

            if (drzava == null)
            {
                return NotFound();
            }

            return drzava;
        }

        // PUT: api/Drzava/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrzava(int id, Drzava drzava)
        {
            if (id != drzava.Id)
            {
                return BadRequest();
            }

            _context.Entry(drzava).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrzavaExists(id))
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

        // POST: api/Drzava
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Drzava>> PostDrzava(Drzava drzava)
        {
            _context.Drzave.Add(drzava);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetDrzava", new { id = drzava.Id }, drzava);
            return CreatedAtAction(nameof(GetDrzava), new { id = drzava.Id }, drzava);
        }

        // DELETE: api/Drzava/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drzava>> DeleteDrzava(int id)
        {
            var drzava = await _context.Drzave.FindAsync(id);
            if (drzava == null)
            {
                return NotFound();
            }

            _context.Drzave.Remove(drzava);
            await _context.SaveChangesAsync();

            return drzava;
        }

        private bool DrzavaExists(int id)
        {
            return _context.Drzave.Any(e => e.Id == id);
        }
    }
}
