using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using UMFG.Venda.Aprensetacao.Interfaces;
using UMFG.Venda.Aprensetacao.Models;
using UMFG.Venda.Aprensetacao.ViewModels;

namespace UMFG.Venda.Aprensetacao.UserControls
{
    public partial class ucReceber : UserControl
    {
        private IObserver observer;

        private ucReceber(IObserver observer, PedidoModel pedido)
        {
            InitializeComponent();
            this.observer = observer;
            DataContext = new ReceberViewModel(this, observer, pedido);
        }

        internal static PedidoModel Exibir(IObserver observer, PedidoModel pedido)
        {
            var tela = new ucReceber(observer, pedido);
            var vm = tela.DataContext as ReceberViewModel;

            vm.Notify();
            return vm.Pedido;
        }

        private void ValidarDados(object sender, RoutedEventArgs e)
        {
            string data = this.txtData.Text;
            string numeroCartao = this.txtNumeroCartao.Text;
            string cvv = this.txtCVV.Text;
            DateTimeStyles estiloData = DateTimeStyles.None;
            DateTime dataHoje = DateTime.Today;

            bool dataValida = DateTime.TryParseExact(data, "MM-yyyy", CultureInfo.InvariantCulture, estiloData, out DateTime dataConvertida) &&
                               dataConvertida >= DateTime.Today;

            bool cvvValido = int.TryParse(cvv, out _) && cvv.Length == 3;
            bool cartaoValido = long.TryParse(numeroCartao, out _) && numeroCartao.Length == 16;

            if (cartaoValido && dataValida && cvvValido)
            {
                MessageBox.Show("Pagamento realizado", "Sucesso");
                ucListarProdutos.Exibir(observer);
            }
            else
            {
                MessageBox.Show("Verifique seus dados novamente", "Erro");
            }
        }
    }
}
