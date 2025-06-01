using CapaNegocio;
using System.Net;
using Newtonsoft.Json;


namespace CapaDatosFasecolda
{
    public class ConexionAPI : FuenteDatosFasecolda
    {
        public ConteoAccidenteDTO consultarCantidadAccidentesPorPlaca(string placa)
        {
            string url = "http://localhost:5283/api/Aseguradora/" + placa ;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                string respuesta = reader.ReadToEnd();

                return JsonConvert.DeserializeObject<ConteoAccidenteDTO>(respuesta) ?? new ConteoAccidenteDTO();
            }
            catch
            {
                return new ConteoAccidenteDTO();
            }
        }
    }
}
