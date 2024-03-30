using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UMFG.Venda.Aprensetacao.Classes;
using UMFG.Venda.Aprensetacao.ViewModels;

namespace UMFG.Venda.Aprensetacao.Comandos
{
    internal class RemoverPedidoCommand : AbstractCommand
    {
        public override void Execute(object? parameter)
        {
            
            if (!(parameter is ListarProdutosViewModel vm))
            {
                MessageBox.Show("O parâmetro fornecido não é válido.");
                return;
            }


            if (vm.ProdutoSelecionado == null)
            {
                MessageBox.Show("Selecione um produto para remover.");
                return;
            }

            if (!vm.Pedido.Produtos.Contains(vm.ProdutoSelecionado))
            {
                MessageBox.Show("O produto selecionado não existe na lista.");
                return;
            }

            vm.Pedido.Produtos.Remove(vm.ProdutoSelecionado);

            AtualizarTotalPedido(vm);
        }

        private void AtualizarTotalPedido(ListarProdutosViewModel vm)
        {
            decimal novoTotal = vm.Pedido.Produtos.Sum(x => x.Preco);
            vm.Pedido.Total = novoTotal;
        }
    }
}

