using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        private readonly IProAgilRepository _repository;

        public PalestranteController(IProAgilRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("{id}") ]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                  var results = await _repository.GetPalestranteAsyncById(id,true);
                  return Ok(results);
            }

           catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }             
        }

        [HttpGet("getByName/{nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                  var results = await _repository.GetAllPalestranteAsyncByName(nome,true);
                  return Ok(results);
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }         
        }


        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repository.Add(model);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}", model);
                }     
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }    

            return BadRequest();     
        } 

        [HttpPut]
        public async Task<IActionResult> Put(int id,Palestrante model)
        {
            try
            {
                var palestrante = await _repository.GetPalestranteAsyncById(id,false);
                if(palestrante == null) return NotFound();

                _repository.Update(model);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}", model);
                }     
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }    

            return BadRequest();     
        }   

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var palestrante = await _repository.GetPalestranteAsyncById(id,false);
                if(palestrante == null) return NotFound();

                _repository.Delete(palestrante);

                if(await _repository.SaveChangesAsync())
                {
                    return Ok();
                }     
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }    

            return BadRequest();     
        }



        
    }
}