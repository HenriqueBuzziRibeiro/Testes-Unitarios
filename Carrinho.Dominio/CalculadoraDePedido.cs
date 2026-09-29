using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrinho.Dominio
{
    public class CalculadoraDePedido
    {
        private readonly IServicoDeFrete _servicoDeFrete;

        public CalculadoraDePedido(IServicoDeFrete servicoDeFrete) // Injeção de dependência, a classe não cria o serviço de frete, ela recebe por parâmetro.
        {
            _servicoDeFrete = servicoDeFrete;
        }

        public decimal CalcularValorFinal(CarrinhoDeCompras carrinho, string cep)
        {
            var total = carrinho.CalcularTotal();

            if (total >= 100)
                return total;

            var frete = _servicoDeFrete.Consultar(cep);
            return total + frete;
        }
    }
}
