using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2019MF650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2019MF650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class restauranteController : ControllerBase
    {
        private readonly restauranteContext _restauranteContexto;

        public restauranteController(restauranteContext restauranteContexto)
        {
            _restauranteContexto = restauranteContexto; ;
        }
        //Metodos Pedidos
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<pedidos> listadoPedido = (from e in _restauranteContexto.pedidos
                                           select e).ToList();

            if (listadoPedido.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPedido);

        }

        [HttpGet]
        [Route("GetByCliente/{id}")]

        public IActionResult Get(int id)
        {
            List<pedidos> listadoPedido = (from e in _restauranteContexto.pedidos
                               where e.clienteId == id
                               select e).ToList();

            if (listadoPedido.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPedido);

        }

        [HttpGet]
        [Route("GetByMotorista/{id}")]

        public IActionResult Get(int idM)
        {
            List<pedidos> listadoPedidoM = (from e in _restauranteContexto.pedidos
                                           where e.motoristaId== idM
                                           select e).ToList();

            if (listadoPedidoM.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPedidoM);

        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPedido([FromBody] pedidos pedido)
        {

            try
            {
                _restauranteContexto.pedidos.Add(pedido);
                _restauranteContexto.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoActual = (from e in _restauranteContexto.pedidos
                                      where e.pedidoId == id
                                      select e).FirstOrDefault();
            if (pedidoActual == null)
            {
                return NotFound(id);
            }

            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContexto.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContexto.SaveChanges();
            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarPedido(int id)
        {

            pedidos? pedido = (from e in _restauranteContexto.pedidos
                               where e.pedidoId == id
                               select e).FirstOrDefault();

            if (pedido == null)
                return NotFound();

            _restauranteContexto.pedidos.Attach(pedido);
            _restauranteContexto.pedidos.Remove(pedido);
            _restauranteContexto.SaveChanges();

            return Ok(pedido);
        }

    }
}
