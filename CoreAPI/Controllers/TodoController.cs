using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            this._context=context;
            if(this._context.TodoItems.Count() == 0)
            {
                //Create a new TodoItem if collection is empty,
                //which means you can't delete all TodoItems.
                this._context.TodoItems.Add(new TodoItem{Name = "Item1"});
                this._context.SaveChanges();
            }
        }

        //GET : api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() 
                    => await this._context.TodoItems.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await this._context.TodoItems.FindAsync(id);

            if(todoItem == null) return NotFound();

            return todoItem;

        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            this._context.TodoItems.Add(todoItem);
            await this._context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItem", new TodoItem{Id = todoItem.Id}, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if(id != todoItem.Id) return BadRequest();

            this._context.Entry(todoItem).State= EntityState.Modified;
            await this._context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            TodoItem item = await this._context.TodoItems.FindAsync(id);
            if (item == null) return NotFound();
            
            this._context.Entry(item).State = EntityState.Deleted;
            //this._context.TodoItems.Remove(todoItem)
            await this._context.SaveChangesAsync();
            
            return item;
        }
    }
}