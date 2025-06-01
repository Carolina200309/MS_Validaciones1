using CapaNegocio;
using Microsoft.Data.SqlClient;

namespace CapaDatosValidacion
{
    public class ConexionBD : FuenteDatosValidacion
    {
        private SqlConnection conexion;

        public ConexionBD()
        {
            conexion = new SqlConnection(@"TrustServerCertificate=True;Password=2003;Persist Security Info=True;User ID=sa;Initial Catalog=MS_Validaciones;Data Source=DESKTOP-VJ6Q10E\SQLEXPRESS");

        }
        public bool guardarInformacionValidacion(ResultadoValidacion resultado)
        {
            try
            {
                    conexion.Open();

                    string insert = @"
                        INSERT INTO Validaciones (ccCliente, placa, resultado)
                        VALUES (@ccCliente, @placa, @resultado)";

                SqlCommand command = new SqlCommand(insert, conexion);


                int rows = command.ExecuteNonQuery();

                conexion.Close();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Error al guardar resultado: {ex.Message}");
                return false;
            }
        }
    }
}

