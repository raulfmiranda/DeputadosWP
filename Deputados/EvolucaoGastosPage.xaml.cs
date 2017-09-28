using Deputados.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace Deputados
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class EvolucaoGastosPage : Page
    {
        private ObservableCollection<GastoAnoTotal> gastos;
        private Deputado deputado;

        public EvolucaoGastosPage()
        {
            this.InitializeComponent();
            GerarListaGastos();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                deputado = (Deputado)e.Parameter;
                imgFromUrl.Source = new BitmapImage(new Uri(deputado.FotoURL, UriKind.Absolute));
                tbNomeParlamentar.Text = deputado.NomeParlamentar;
            }
        }

        private void GerarListaGastos()
        {
            gastos = new ObservableCollection<GastoAnoTotal>();
            GastoAnoTotal gas = new GastoAnoTotal();

            gas.TipoGasto = "ASSINATURA DE PUBLICAÇÕES";
            gas.Ano = 2011;
            gas.Valor = "R$ 340120,25";

            for (int i = 0; i < 12; i++)
            {
                gastos.Add(gas);
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            this.splitView.IsPaneOpen = !this.splitView.IsPaneOpen;
        }

        private void Global_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name.Equals("mbt1"))
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            else if (btn.Name.Equals("mbt2"))
            {
                //tbConteudo.Text = "2";
            }
            else if (btn.Name.Equals("mbt3"))
            {
                //tbConteudo.Text = "3";
            }
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DetalheDeputado), deputado);
        }
    }
}
