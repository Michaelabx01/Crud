using Microsoft.AspNetCore.Mvc;
using NetCoreCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreCrud.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        private List<Cliente> clientes = new List<Cliente>
        {
            new Cliente
            {
                id = "1",
                correo = "mavaldiviezo@gmail.com",
                edad = "24",
                nombre = "Michael Valdiviezo"
            },
            new Cliente
            {
                id = "2",
                correo = "bakihanma@gmail.com",
                edad = "20",
                nombre = "Baki Hanma"
            }
        };

        [HttpGet("listar")]
        public ActionResult<IEnumerable<Cliente>> ListarClientes()
        {
            return Ok(clientes);
        }

        [HttpGet("listar/{codigo}")]
        public ActionResult<Cliente> ListarClientePorId(string codigo)
        {
            var cliente = clientes.FirstOrDefault(c => c.id == codigo);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost("guardar")]
        public ActionResult<Cliente> GuardarCliente(Cliente cliente)
        {
            cliente.id = Guid.NewGuid().ToString();
            clientes.Add(cliente);
            return CreatedAtAction(nameof(ListarClientePorId), new { codigo = cliente.id }, cliente);
        }

        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarcliente(Cliente cliente)
        {
            string token = Request.Headers.Where(x => x.Key == "Autorization").FirstOrDefault().Value;
            //elimina el cliente
            if (token != "michael123")
            {
                return new
                {
                    success = false,
                    message = "token incorrecto",
                    result = ""
                };
            }
            return new
            {
                success = true,
                message = "cliente eliminado exitosamente",
                result = cliente
            };
        }
    }
}