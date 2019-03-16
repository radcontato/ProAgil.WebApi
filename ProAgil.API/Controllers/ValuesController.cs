﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.Data;
using ProAgil.API.Model;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly DataContext _Context;

        public ValuesController(DataContext context)
        {
            this._Context = context;
        }



        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                  var results = await _Context.Eventos.ToListAsync();
                  return Ok(results);
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }         
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {   
            try{
                var results = await _Context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id);
                return Ok(results);

            }
            catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }


          
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
