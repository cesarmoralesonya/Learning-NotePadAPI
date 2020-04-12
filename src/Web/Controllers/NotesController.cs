using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        //GET: api/Notes
        [HttpGet]
        public async Task<IEnumerable<Note>> GetNotes()
        {
            return await _noteRepository.ListAllAsync();
        }

        //GET: api/Notes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        //PUT: api/Notes/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote([FromRoute] int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.Id)
            {
                return BadRequest();
            }

            try
            {
                await _noteRepository.UpdateAsync(note);
            }
            catch(DbUpdateConcurrencyException)            
            {
                return NotFound();
            }

            return NoContent();
        }

        //POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _noteRepository.AddAsync(note);

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        //DELETE: api/Notes/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if(note == null)
            {
                return NotFound();
            }

            await _noteRepository.DeleteAsync(note);

            return Ok(note);
        }
    }
}
