using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorDataService.Models;
using BlazorDataService.Services;

namespace BlazorDataService.Controllers
{
    [Route("BlazorDataService/ToDoList")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        readonly ApplicationDbContext _context;
        readonly IToDoListService _service;

        public ToDoController(ApplicationDbContext context,
            IToDoListService service)
        {
            _context = context;
            _service = service;
        }

        // GET: BlazorDataService/ToDoList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDoList()
        {
            return await _context.ToDoList.ToListAsync();
        }

        [HttpGet]
        [Route ("Getv2")]
        public async Task<ActionResult<PaginatedList<ToDo>>> Get (int? pageNumber, string sortField, string sortOrder)
        {
            var list = await _service.GetList(pageNumber, sortField, sortOrder);
            return list;
        }

        // GET: BlazorDataService/ToDoList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDo(int id)
        {
            var toDo = await _context.ToDoList.FindAsync(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;
        }

        // PUT: BlazorDataService/ToDoList/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
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

        // POST: BlazorDataService/ToDoList
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo)
        {
            _context.ToDoList.Add(toDo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }

        // DELETE: BlazorDataService/ToDoList/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDo>> DeleteToDo(int id)
        {
            var toDo = await _context.ToDoList.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDoList.Remove(toDo);
            await _context.SaveChangesAsync();

            return toDo;
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDoList.Any(e => e.Id == id);
        }
    }
}
