using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using MySql.Data.MySqlClient; 

namespace API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [HttpGet("getall")]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            TodoContext? context = HttpContext.RequestServices.GetService<TodoContext>();
            Response.ContentType = "application/json";
            return Ok(context!.GetAll().ToArray()); 
        } 

        [HttpGet("get/{id}")]
        public ActionResult Get(int id)
        {
            Response.ContentType = "application/json";
            TodoContext? context = HttpContext.RequestServices.GetService<TodoContext>();
            return Ok(context!.Get(id));
        }

        [HttpPost("update")]
        public ActionResult<Todo> Update(Todo todo)
        {
            Response.ContentType = "application/json";
            TodoContext? context = HttpContext.RequestServices.GetService<TodoContext>();

             Todo newTodo = context!.Update(todo); 
             
            return CreatedAtAction(nameof(Update), newTodo );
        }


        [HttpPost("create")]
        public ActionResult<Todo> Create(Todo todo)
        {
            Response.ContentType = "application/json";
            TodoContext? context = HttpContext.RequestServices.GetService<TodoContext>();

            Todo newTodo = context!.Create(todo);

            return CreatedAtAction(nameof(Create), newTodo);
        }

        [HttpGet("delete/{id}")]
        public ActionResult Delete(int id)
        {
            Response.ContentType = "application/json";
            TodoContext? context = HttpContext.RequestServices.GetService<TodoContext>();
            Dictionary<String, bool> result = new Dictionary<string, bool>() ; 
            bool response = context!.Delete(id);
            result.Add("success", response);
            return Ok(result);
        }
    }
}
