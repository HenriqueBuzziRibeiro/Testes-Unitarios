using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrinho.Dominio.Testes
{
    public class CalculadoraDePedidoTestes
    {
        [Fact]
        public void CalcularValorFinal_CompraAbaixoDe100_SomaOFrete()
        {
            // Arrange
            var mockFrete = new Mock<IServicoDeFrete>(); // Cria um objeto que finge ser o serviço de frete
            mockFrete.Setup(f => f.Consultar("01001-000")).Returns(25.00m); // Sem o 'Returns(25.00m)' o consultar retornaria 0.

            var carrinho = new CarrinhoDeCompras();
            carrinho.AdicionarItem("Caneta", 10.00m, 2);   // subtotal 20,00

            var calculadora = new CalculadoraDePedido(mockFrete.Object); //  o mockFrete.Object é passado para o construtor da CalculadoraDePedido, o '.Object' nesse caso carrega os valores de retorno que foram configurados no mock, ou seja, quando o método Consultar for chamado com o CEP "01001-000", ele retornará 25.00m.

            // Act
            var valorFinal = calculadora.CalcularValorFinal(carrinho, "01001-000");

            // Assert
            Assert.Equal(45.00m, valorFinal);   // 20,00 + 25,00
        }

        [Fact]
        public void CalcularValorFinal_CompraAcimaDe100_NaoConsultaOFrete()
        {
            // Arrange
            var mockFrete = new Mock<IServicoDeFrete>();

            var carrinho = new CarrinhoDeCompras();
            carrinho.AdicionarItem("Monitor", 150.00m, 1);   // subtotal 150,00

            var calculadora = new CalculadoraDePedido(mockFrete.Object);

            // Act
            var valorFinal = calculadora.CalcularValorFinal(carrinho, "01001-000");

            // Assert
            Assert.Equal(150.00m, valorFinal);
            mockFrete.Verify(f => f.Consultar(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void CalcularValorFinal_ConsultaFrete_UsaOCepInformado()
        {
            // Arrange
            var mockFrete = new Mock<IServicoDeFrete>();
            mockFrete.Setup(f => f.Consultar(It.IsAny<string>())).Returns(10.00m);

            var carrinho = new CarrinhoDeCompras();
            carrinho.AdicionarItem("Caneta", 10.00m, 1);

            var calculadora = new CalculadoraDePedido(mockFrete.Object);

            // Act
            calculadora.CalcularValorFinal(carrinho, "88010-000");

            // Assert
            mockFrete.Verify(f => f.Consultar("88010-000"), Times.Once);
        }
    }
}
