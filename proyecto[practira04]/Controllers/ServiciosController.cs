using Microsoft.AspNetCore.Mvc;
using ServicioDLL.Data.Models;
using ServicioDLL.Data.Repositories;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace proyecto_practira04_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {

        private IServicioRepository _repository;

        public ServiciosController(IServicioRepository repository)
        {
            _repository = repository;
        }



        [HttpGet("con filtros")]
        public IActionResult GetByNameCost(string? nombre = null, int? costo = null)
        {
            var servicios = _repository.GetByNameCost(nombre, costo);

            if (servicios == null || !servicios.Any())
            {
                return NotFound("No se encontraron servicios que coincidan con los criterios.");
            }

            return Ok(servicios);
        }




        [HttpPost]
        public IActionResult Post([FromBody] TServicio value)
        {
            try
            {
                if (IsValid(value))
                {
                    _repository.Create(value);
                    return Ok("Servicio insertado");
                }

                else
                {
                    return BadRequest("Los datos no son correctos");
                }

            }
            catch (Exception)
            {

                return StatusCode(500, "Ha ocurrido un error interno (posible ID ya existente)");
            }
        }



        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TServicio servicioactualizado)
        {
            try
            {
                if (IsValid(servicioactualizado))
                {
                    _repository.Update(servicioactualizado);    
                    return Ok($"Servicio con id [{id}] actualizado con éxito");
                }
                else
                {
                    return BadRequest("Datos no válidos");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Interno: {ex.Message}");
            }
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var servicioDeleted = _repository.Delete(id);
                if (servicioDeleted)
                {
                    return Ok("Servicio eliminado correctamente");
                }
                else
                {
                    return NotFound($"No existe un servicio con el id: [{id}]");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }


        private bool IsValid(TServicio value)
        {
            return !string.IsNullOrEmpty(value.Nombre) && !string.IsNullOrEmpty(value.EnPromocion) &&  value.Id != 0 && value.Costo != 0;
        }
    }
}
