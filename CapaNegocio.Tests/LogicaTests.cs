using Xunit;
using Moq;
using CapaNegocio;

namespace CapaNegocio.Tests
{
    public class LogicaTests
    {
        [Fact]
        public void ValidarSolicitud_DatosInvalidos_RetornaError()
        {
            var mockFasecolda = new Mock<FuenteDatosFasecolda>();
            var mockValidacion = new Mock<FuenteDatosValidacion>();
            var logica = new Logica(mockValidacion.Object, mockFasecolda.Object);

            var resultado = logica.ValidarSolicitud(null, "");

            Assert.False(resultado.Aprobada);
            Assert.Equal("error", resultado.Resultado);
            Assert.Equal(0, resultado.PuntosCalculados);
        }

        [Fact]
        public void ValidarSolicitud_PuntosMenor400_Aprueba()
        {
            var mockFasecolda = new Mock<FuenteDatosFasecolda>();
            var mockValidacion = new Mock<FuenteDatosValidacion>();
            mockFasecolda.Setup(x => x.consultarCantidadAccidentesPorPlaca("ABC123")).Returns(new ConteoAccidenteDTO { soloLatas = 1, heridos = 1, muertos = 0 });
            mockValidacion.Setup(x => x.guardarInformacionValidacion(It.IsAny<ResultadoValidacion>())).Returns(true);
            var logica = new Logica(mockValidacion.Object, mockFasecolda.Object);

            var resultado = logica.ValidarSolicitud("123", "ABC123");

            Assert.True(resultado.Aprobada);
            Assert.Equal("aprobada", resultado.Resultado);
            Assert.Equal(300, resultado.PuntosCalculados);
        }

        [Fact]
        public void ValidarSolicitud_PuntosMayorIgual400_Rechaza()
        {
            var mockFasecolda = new Mock<FuenteDatosFasecolda>();
            var mockValidacion = new Mock<FuenteDatosValidacion>();
            mockFasecolda.Setup(x => x.consultarCantidadAccidentesPorPlaca("XYZ789")).Returns(new ConteoAccidenteDTO { soloLatas = 1, heridos = 1, muertos = 1 });
            mockValidacion.Setup(x => x.guardarInformacionValidacion(It.IsAny<ResultadoValidacion>())).Returns(true);
            var logica = new Logica(mockValidacion.Object, mockFasecolda.Object);

            var resultado = logica.ValidarSolicitud("456", "XYZ789");

            Assert.False(resultado.Aprobada);
            Assert.Equal("rechazada", resultado.Resultado);
            Assert.Equal(600, resultado.PuntosCalculados);
        }

        [Fact]
        public void ValidarSolicitud_ErrorAlGuardar_RetornaError()
        {
            var mockFasecolda = new Mock<FuenteDatosFasecolda>();
            var mockValidacion = new Mock<FuenteDatosValidacion>();
            mockFasecolda.Setup(x => x.consultarCantidadAccidentesPorPlaca(It.IsAny<string>())).Returns(new ConteoAccidenteDTO { soloLatas = 0, heridos = 0, muertos = 0 });
            mockValidacion.Setup(x => x.guardarInformacionValidacion(It.IsAny<ResultadoValidacion>())).Returns(false);
            var logica = new Logica(mockValidacion.Object, mockFasecolda.Object);

            var resultado = logica.ValidarSolicitud("789", "AAA111");

            Assert.False(resultado.Aprobada);
            Assert.Equal("error", resultado.Resultado);
            Assert.Equal(0, resultado.PuntosCalculados);
        }
    }
}
