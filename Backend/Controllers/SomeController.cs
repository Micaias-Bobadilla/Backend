﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Conexion a base d datos terminada");

            Thread.Sleep(1000);
            Console.WriteLine("Envio de mail terminado");

            Console.WriteLine($"Todo ha terminado");

            stopwatch.Stop();
            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            var task1 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexion a base d datos terminada");
                return 8;
            });

            task1.Start();

            Console.WriteLine("Hago otra cosa");

            var resultado = await task1;
            Console.WriteLine("Todo ha terminado");

            return Ok(resultado);

        }
    }
}
