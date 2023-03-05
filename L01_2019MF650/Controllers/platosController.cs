using L01_2019MF650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2019MF650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public platosController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto; ;
        }
        //Metodos Platos
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<platos> listadoPlatos = (from e in _restauranteContexto.platos
                                           select e).ToList();

            if (listadoPlatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPlatos);

        }

        [HttpGet]
        [Route("GetByPrecio/{precio}")]

        public IActionResult GetPrecios(decimal precio)
        {
            List<platos> listadoPlatos = (from e in _restauranteContexto.platos
                                           where e.precio < precio
                                           select e).ToList();

            if (listadoPlatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPlatos);

        }


        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPedido([FromBody] platos platos)
        {

            try
            {
                _restauranteContexto.platos.Add(platos);
                _restauranteContexto.SaveChanges();
                return Ok(platos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPlato(int id, [FromBody] platos platoModificar)
        {
            platos? platoActual = (from e in _restauranteContexto.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();
            if (platoActual == null)
            {
                return NotFound(id);
            }

            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;

            _restauranteContexto.Entry(platoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();
            return Ok(platoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarPlato(int id)
        {

            platos? plato = (from e in _restauranteContexto.platos
                               where e.platoId == id
                               select e).FirstOrDefault();

            if (plato == null)
                return NotFound();

            _restauranteContexto.platos.Attach(plato);
            _restauranteContexto.platos.Remove(plato);
            _restauranteContexto.SaveChanges();

            return Ok(plato);
        }
    }
}
