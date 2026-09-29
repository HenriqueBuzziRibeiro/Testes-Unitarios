# Carrinho de Compras — Testes Unitários com xUnit

Biblioteca de domínio de um carrinho de compras, com suíte de testes unitários em xUnit e Moq.

## Stack

- .NET 9
- xUnit
- Moq

## Estrutura

| Projeto | Papel |
|---|---|
| `Carrinho.Dominio` | biblioteca de classes — a lógica de negócio |
| `Carrinho.Dominio.Testes` | suíte de testes, referencia o domínio |

## Domínio

**`Item`** — linha do carrinho (nome, preço unitário, quantidade). Imutável.

**`CarrinhoDeCompras`**

| Método | Comportamento |
|---|---|
| `AdicionarItem` | valida nome, preço e quantidade; lança `ArgumentException` se inválido |
| `CalcularSubtotal` | soma preço × quantidade de todos os itens |
| `AplicarDesconto` | aceita de 0 a 100; fora da faixa lança `ArgumentException` |
| `CalcularTotal` | subtotal menos o desconto aplicado |
| `RemoverItem` | remove por nome; devolve `bool` indicando sucesso |

**`CalculadoraDePedido`** — soma o total do carrinho com o frete consultado em `IServicoDeFrete`. Compras a partir de R$ 100 têm frete grátis e não consultam o serviço.

## Cobertura de testes

15 testes cobrindo:

- soma de subtotal (carrinho vazio e com itens)
- validações de entrada (`[Theory]` + `Assert.Throws`)
- desconto em valores de fronteira (0, 50, 100 válidos; -1 e 101 inválidos)
- remoção de item existente e inexistente
- cálculo com frete e regra de frete grátis, usando mock de `IServicoDeFrete`

## Como rodar

```bash
dotnet test
```
