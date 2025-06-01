using CapaDatosFasecolda;
using CapaDatosValidacion;
using CapaNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapaPresentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidacionesController : ControllerBase
    {
        private Logica logica = new Logica(new ConexionBD(), new ConexionAPI());
        [HttpPost]
        [Route("{ccCliente}/{placa}")]
        public bool Post(string ccCliente, string placa)
        {
            var resultado = logica.ValidarSolicitud(ccCliente, placa);
            return resultado.Aprobada;
        }
    }
}
