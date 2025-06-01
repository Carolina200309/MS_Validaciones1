using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{


    public class ConteoAccidenteDTO
    {
        public int soloLatas { get; set; }
        public int heridos { get; set; }
        public int muertos { get; set; }
    }

    public class ResultadoValidacion
    {
        public int id { get; set; }
        public string ccCliente { get; set; }
        public string placa { get; set; }
        public string resultado { get; set; }
      
    }


    public class RespuestaValidacion
    {
        public bool Aprobada { get; set; }
        public string Resultado { get; set; }
        public int PuntosCalculados { get; set; }

    }

}
