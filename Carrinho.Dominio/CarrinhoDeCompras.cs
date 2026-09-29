using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrinho.Dominio
{
    public class CarrinhoDeCompras
    {
        private readonly List<Item> _itens = new();
        public decimal DescontoPercentual { get; private set; }

        public void AdicionarItem(string nome, decimal precoUnitario, int quantidade)
        {

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do item é obrigatório.", nameof(nome)); // ArgunmentException é uma exceção padrão do .NET que indica que um argumento fornecido para um método é inválido.

            if (precoUnitario < 0)
                throw new ArgumentException("O preço não pode ser negativo.", nameof(precoUnitario)); // nameof(precoUnitario) retorna o 'precoUnitario' como string, que é o nome do parâmetro que causou a exceção.

            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

            _itens.Add(new Item(nome, precoUnitario, quantidade)); // Se alguma condição falhar nada é adicionado, isso chamamos de 'Atomicidade', ou seja, tudo ou nada, ou funciona ou não funciona.
        }

        public decimal CalcularSubtotal()
        {
            return _itens.Sum(i => i.PrecoUnitario * i.Quantidade); //i é 'Item', compilador sabe que 'i' é um 'Item' porque '_itens' é List<Item>.
        }

        public void AplicarDesconto(decimal percentual)
        {
            if (percentual < 0 || percentual > 100)
                throw new ArgumentException("O desconto deve estar entre 0 e 100.", nameof(percentual));

            DescontoPercentual = percentual;
        }

        public decimal CalcularTotal()
        {
            var subtotal = CalcularSubtotal();
            return subtotal - (subtotal * DescontoPercentual / 100);
        }

        public bool RemoverItem(string nome)
        {
            var item = _itens.FirstOrDefault(i => i.Nome == nome); // FirstOrDefault retorna o primeiro item que satisfaz a condição, ou null se nenhum item for encontrado.

            if (item is null)
                return false;

            _itens.Remove(item);
            return true;
        }
    }
}