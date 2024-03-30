using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UMFG.Venda.Aprensetacao.Classes;
using UMFG.Venda.Aprensetacao.UserControls;
using UMFG.Venda.Aprensetacao.ViewModels;

namespace UMFG.Venda.Aprensetacao.Comandos
{
    internal sealed class ReceberPedidoCommand : AbstractCommand
    {
        public override void Execute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            try
            {   
                if (vm.Pedido.Produtos.Count == 0)
                {
                    MessageBox.Show("Adicione itens ao pedido antes de prosseguir para o recebimento");
                    return;
                }

                vm.Pedido = ucReceber.Exibir(vm.MainUserControl, vm.Pedido);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
