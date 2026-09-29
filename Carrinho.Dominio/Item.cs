using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrinho.Dominio
{
    public class Item
    {
        public string Nome { get; }
        public decimal PrecoUnitario { get; }
        public int Quantidade { get; }

        public Item(string nome, decimal precoUnitario, int quantidade)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
        }
    }
}
