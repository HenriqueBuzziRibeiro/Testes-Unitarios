using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrinho.Dominio.Testes
{
    public class CarrinhoDeComprasTestes
    {
        [Fact] // O 'Fact' indica que este é um método de teste
        public void CalcularSubtotal_CarrinhoVazio_DevolveZero() //Interessante estruturar o nome do teste dessa forma, pois ele descreve o cenário e o resultado esperado
        {
            // Arrange
            var carrinho = new CarrinhoDeCompras();

            // Act
            var subtotal = carrinho.CalcularSubtotal();

            // Assert
            Assert.Equal(0, subtotal); // Assert.Equal(esperado, obtido)
        }

        [Fact]
        public void CalcularSubtotal_ComDoisItens_SomaCorretamente()
        {
            // Arrange
            var carrinho = new CarrinhoDeCompras();
            carrinho.AdicionarItem("Caneta", 10.00m, 2); // o 'm' indica que o número é decimal
            carrinho.AdicionarItem("Caderno", 5.50m, 1);

            // Act
            var subtotal = carrinho.CalcularSubtotal();

            // Assert
            Assert.Equal(25.50m, subtotal); // nunca repita a fórmula de cálculo no Assert, sempre use o valor esperado diretamente
        }

        [Theory]
        [InlineData("", 10, 1)] // cada inline cobre uma validação diferente, todas devem lançar exceção
        [InlineData("Caneta", -5, 1)]
        [InlineData("Caneta", 10, 0)]
        public void AdicionarItem_DadosInvalidos_LancaExcecao(string nome, decimal preco, int quantidade)
        {
            // Arrange
            var carrinho = new CarrinhoDeCompras();

            // Act + Assert
            Assert.Throws<ArgumentException>(() => carrinho.AdicionarItem(nome, preco, quantidade)); //Até então o Assert verificara um valor (Equal), esse aqui confere um comportamento, ou seja, se é lançado uma exceção.
        }

        [Theory] // Testa descontos válidos, perceba que usamos 'Assert.Equal'
        [InlineData(0, 25.50)] //repare que o inline é o quanto vai de desconto, seguido do resultado esperado ao final
        [InlineData(50, 12.75)]
        [InlineData(100, 0)]
        public void CalcularTotal_ComDescontoValido_AplicaCorretamente(decimal desconto, decimal totalEsperado)
        {
            // Arrange
            var carrinho = new CarrinhoDeCompras();
            carrinho.AdicionarItem("Caneta", 10.00m, 2);
            carrinho.AdicionarItem("Caderno", 5.50m, 1);
            carrinho.AplicarDesconto(desconto);

            // Act
            var total = carrinho.CalcularTotal();

            // Assert
            Assert.Equal(totalEsperado, total);
        }

        [Theory] // Testa descontos inválidos, perceba que usamos 'Assert.Throws' para verificar se a exceção é lançada
        [InlineData(-1)]
        [InlineData(101)]
        public void AplicarDesconto_ForaDaFaixa_LancaExcecao(decimal desconto)
        {
            // Arrange
            var carrinho = new CarrinhoDeCompras();

            // Act + Assert
            Assert.Throws<ArgumentException>(() => carrinho.AplicarDesconto(desconto));
        }

        [Fact]
        public void RemoverItem_ItemExistente_RemoveEDevolveTrue()
        {
            // Arrange
            var carrinho = new CarrinhoDeCompras();
            carrinho.AdicionarItem("Caneta", 10.00m, 2);

            // Act
            var removeu = carrinho.RemoverItem("Caneta");

            // Assert
            Assert.True(removeu); // o Assert.True verifica se o valor é verdadeiro
            Assert.Equal(0, carrinho.CalcularSubtotal());
        }

        [Fact]
        public void RemoverItem_ItemInexistente_DevolveFalse()
        {
            // Arrange
            var carrinho = new CarrinhoDeCompras();
            carrinho.AdicionarItem("Caneta", 10.00m, 2);

            // Act
            var removeu = carrinho.RemoverItem("Caderno");

            // Assert
            Assert.False(removeu); // o Assert.False verifica se o valor é falso
            Assert.Equal(20.00m, carrinho.CalcularSubtotal());
        }
    }
}