using L01_2019MF650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2019MF650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristaController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public motoristaController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto; ;
        }

        //Metodos Motorista
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<motoristas> listadoMotor = (from e in _restauranteContexto.motoristas
                                           select e).ToList();

            if (listadoMotor.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoMotor);

        }

        [HttpGet]
        [Route("GetByNombre/{name}")]

        public IActionResult GetNombre(string name)
        {
            List<motoristas> listadoMotor = (from e in _restauranteContexto.motoristas
                                           where e.nombreMotorista == name
                                           select e).ToList();

            if (listadoMotor.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoMotor);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPedido([FromBody] motoristas motoristas)
        {

            try
            {
                _restauranteContexto.motoristas.Add(motoristas);
                _restauranteContexto.SaveChanges();
                return Ok(motoristas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarMotorista(int id, [FromBody] motoristas motorModificar)
        {
            motoristas? motoristaActual = (from e in _restauranteContexto.motoristas
                                     where e.motoristaId == id
                                     select e).FirstOrDefault();
            if (motoristaActual == null)
            {
                return NotFound(id);
            }

            motoristaActual.nombreMotorista = motorModificar.nombreMotorista;

            _restauranteContexto.Entry(motoristaActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();
            return Ok(motorModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarMotorista(int id)
        {

            motoristas? motorista = (from e in _restauranteContexto.motoristas
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if (motorista == null)
                return NotFound();

            _restauranteContexto.motoristas.Attach(motorista);
            _restauranteContexto.motoristas.Remove(motorista);
            _restauranteContexto.SaveChanges();

            return Ok(motorista);
        }
    }
}
