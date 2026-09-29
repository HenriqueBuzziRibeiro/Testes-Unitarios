using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrinho.Dominio
{
    public interface IServicoDeFrete // Interface é s´´o um contrato, diz quais métodos a classe que implementar essa interface deve ter, mas não diz como eles devem ser implementados.
    {
        decimal Consultar(string cep);
    }
}
