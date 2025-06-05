namespace CapaNegocio
{
    public class Logica
    {
        private FuenteDatosValidacion datosValidacion;
        private FuenteDatosFasecolda datosFasecolda;

        public Logica(FuenteDatosValidacion datosValidacion, FuenteDatosFasecolda datosFasecolda)
        {
            this.datosFasecolda = datosFasecolda;
            this.datosValidacion = datosValidacion;
        }

        public RespuestaValidacion ValidarSolicitud(string ccCliente, string placa)
        {
            try
            {
     
                if (string.IsNullOrWhiteSpace(ccCliente) || string.IsNullOrWhiteSpace(placa))
                {
                    return new RespuestaValidacion
                    {
                        Aprobada = false,
                        Resultado = "error",
                        PuntosCalculados = 0,
                    };
                }

       
                var conteoAccidentes = datosFasecolda.consultarCantidadAccidentesPorPlaca(placa);

                int puntos = CalcularPuntos(conteoAccidentes);

                bool aprobada = puntos < 400;
                string resultado = aprobada ? "aprobada" : "rechazada";

    
                var resultadoValidacion = new ResultadoValidacion
                {
                    ccCliente = ccCliente,
                    placa = placa,
                    resultado = resultado,
                    
                };

                bool guardado = datosValidacion.guardarInformacionValidacion(resultadoValidacion);

                if (!guardado)
                {
                    return new RespuestaValidacion
                    {
                        Aprobada = false,
                        Resultado = "error",
                        PuntosCalculados = puntos,
                    };
                }

                return new RespuestaValidacion
                {
                    Aprobada = aprobada,
                    Resultado = resultado,
                    PuntosCalculados = puntos,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ValidarSolicitud: {ex.Message}");
                return new RespuestaValidacion
                {
                    Aprobada = false,
                    Resultado = "error",
                    PuntosCalculados = 0,
                  
                };
            }
        }

        private int CalcularPuntos(ConteoAccidenteDTO conteo)
        {
            // Solo latas: 100 puntos por accidente
            // Heridos: 200 puntos por accidente  
            // Muertos: 300 puntos por accidente
            return (conteo.soloLatas * 100) + (conteo.heridos * 200) + (conteo.muertos * 300);
        }
    }
}
