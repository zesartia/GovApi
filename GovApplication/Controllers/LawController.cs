using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebApplication1.Core;
using WebApplication1.Core.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LawController : ControllerBase
    {

        private readonly ILogger<LawController> _logger;

        public LawController(ILogger<LawController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public async Task<IEnumerable<Law>> Get()
        {
            var law = new List<Law>();
            await using var connectionString = new MySqlConnection(Environment.GetEnvironmentVariable("ConnStr"));

            await connectionString.OpenAsync();

            await using var command = new MySqlCommand("SELECT * FROM Law;", connectionString);
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                law.Add(new Law()
                {
                    Name = reader.GetString(0)
                    
                });
            }
            return law;
        }
    }
}
