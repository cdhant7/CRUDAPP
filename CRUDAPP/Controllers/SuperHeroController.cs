using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CRUDAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperHeroController : Controller
    {
        private readonly IConfiguration _config;

        public SuperHeroController(IConfiguration config) {
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllSuperheroes()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default Connection"));
            IEnumerable<SuperHero> heroes = await SelectAllSuperHero(connection);
            return Ok(heroes);
        }


        [HttpGet("{heroId}")]
        public async Task<ActionResult<SuperHero>> GetheroesbyId(int heroId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default Connection"));
            var heroes = await connection.QueryFirstAsync<SuperHero>("select * from SuperHeroes where id= @Id",
                new { Id = heroId });
            return Ok(heroes);
        }


        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddSuperHeroes(SuperHero hero)
        {

            using var connection = new SqlConnection(_config.GetConnectionString("Default Connection"));
            await connection.ExecuteAsync("insert into superheroes(name,firstname,lastname,place)values(@Name,@FirstName,@LastName,@Place)", hero);//id is added automatically as it is primary key
            return Ok(await SelectAllSuperHero(connection));
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHeroes(SuperHero hero)
        {

            using var connection = new SqlConnection(_config.GetConnectionString("Default Connection"));
            await connection.ExecuteAsync("update superheroes set name=@Name,firstname=@FirstName,lastname=@LastName, place=@Place where id=@Id", hero);
            return Ok(await SelectAllSuperHero(connection));
        }

        [HttpDelete("{heroId}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHeroes(int heroId)
        {

            using var connection = new SqlConnection(_config.GetConnectionString("Default Connection"));
            await connection.ExecuteAsync("delete from SuperHeroes where id=@Id",
                new {Id=heroId});
            return Ok(await SelectAllSuperHero(connection));
        }



        private static async Task<IEnumerable<SuperHero>> SelectAllSuperHero(SqlConnection connection)
        {
            return await connection.QueryAsync<SuperHero>("select * from SuperHeroes");
        }
    }
}
