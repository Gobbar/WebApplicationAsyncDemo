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
        private PostgresDemoHelper _postgresDemoHelper;

        public PostgresDemoController(PostgresDemoDbContext postgresDemos)
        {
            _postgresDemoHelper = new PostgresDemoHelper(postgresDemos);
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            try
            {
                var postgresDemos = await _postgresDemoHelper.GetAllPostgresDemos();
                return Results.Ok(postgresDemos);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        // GET api/<PostgresDemosController>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                var demo = await _postgresDemoHelper.GetPostgresDemosById(id);

                if (demo == null)
                {
                    return Results.BadRequest("Demo Id not found");
                }

                return Results.Ok(demo);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        
        // POST api/<PostgresDemosController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] PostgresDemo model)
        {
            try
            {
                var isDemoCreated = await _postgresDemoHelper.InsertPostgresDemo(model);
                var message = isDemoCreated ? "Demo Created" : "Demo Already Created";
                return Results.Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        
        // PUT api/<PostgresDemosController>/5
        [HttpPut]
        public async Task<IResult> Put([FromBody] PostgresDemo model)
        {
            try
            {
                var isDemoUpdated = await _postgresDemoHelper.UpdatePostgresDemo(model);
                var message = isDemoUpdated ? "Demo Updated" : "Demo not found";
                return Results.Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        
        // DELETE api/<PostgresDemosController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var isDemoDeleted = await _postgresDemoHelper.DeletePostgresDemo(id);
                var message = isDemoDeleted ? "Demo Deleted" : "Demo not exists";
                return Results.Ok(new { Message =  message});
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
