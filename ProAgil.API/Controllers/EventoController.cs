using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {

        private readonly IProAgilRepository _repository;
        public EventoController(IProAgilRepository repository)
        {
            _repository = repository;
        }

         [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                  var results = await _repository.GetAllEventoAsync(true);
                  return Ok(results);
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }         
        }

         [HttpGet("{id}") ]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                  var results = await _repository.GetEventoAsyncById(id,true);
                  return Ok(results);
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }         
        }

          [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                  var results = await _repository.GetAllEventoAsyncByTema(tema,true);
                  return Ok(results);
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }         
        }


        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repository.Add(model);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
                }     
            }

            catch(System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }    

            return BadRequest();     
        }    

        [HttpPut]
        public async Task<IActionResult> Put(int id,Evento model)
        {
            try
            {
                var evento = await _repository.GetEventoAsyncById(id,false);
                if(evento == null) return NotFound();

                _repository.Update(model);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", model);
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
                var evento = await _repository.GetEventoAsyncById(id,false);
                if(evento == null) return NotFound();

                _repository.Delete(evento);

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