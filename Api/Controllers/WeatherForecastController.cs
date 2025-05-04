using Microsoft.AspNetCore.Mvc;
using Modelos;
using Servicios;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        //Esctura get para recibir mas de un parametro
        [HttpGet("getsaludos2/")]
        public string get()
        {
            Funcion_de_Conexion od=new Funcion_de_Conexion();

            Datos_login op = new Datos_login();
            op.usuario = "admin";
            op.constraseña = "admin";

            Boolean respuesta = od.conexion(op);

            string mensaje = "la conexion fue : " + respuesta;

            return mensaje;

        }
    }
}
