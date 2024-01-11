using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAsyncDemo.Models;

namespace WebApplicationAsyncDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostgresDemoController : ControllerBase
    {
        private PostgresDemoDbContext _postgresDemos;

        public PostgresDemoController(PostgresDemoDbContext postgresDemos)
        {
            _postgresDemos = postgresDemos;
        }

        [HttpGet]
        public IResult Get()
        {
            var postgresDemos = _postgresDemos.PostgresDemos;
            return Results.Ok(postgresDemos);
        }

        // GET api/<PostgresDemosController>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var demo = await _postgresDemos.PostgresDemos.FindAsync(id);
            return Results.Ok(demo);
        }
        
        // POST api/<PostgresDemosController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] PostgresDemo model)
        {
        
            var demoExist = await _postgresDemos.PostgresDemos.AnyAsync(e => e.Name == model.Name);
            if (demoExist == true)
            {
                return Results.Ok(new { Message = "Demo Already Created" });
        
            }
        
            _postgresDemos.Add(model);
            _postgresDemos.SaveChanges();
        
            return Results.Ok(new { Message = "Demo Created" });
        }
        
        // PUT api/<PostgresDemosController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put([FromBody] PostgresDemo model)
        {
        
            _postgresDemos.PostgresDemos.Attach(model);
            _postgresDemos.Entry(model).State = EntityState.Modified;
        
        
            // _postgresDemos.PostgresDemos.Update(model);
            await _postgresDemos.SaveChangesAsync();
        
            return Results.Ok(new { Message = "Demo Updated" });
        }
        
        // DELETE api/<PostgresDemosController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var demo = _postgresDemos.PostgresDemos.Find(id);

            _postgresDemos.PostgresDemos.Remove(demo);
            await _postgresDemos.SaveChangesAsync();
        
            return Results.Ok(new { Message = "Demo Deleted" });
        
        }
    }
}
