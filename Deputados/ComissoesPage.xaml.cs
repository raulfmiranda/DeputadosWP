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
    public sealed partial class ComissoesPage : Page
    {
        private ObservableCollection<Comissao> comissoes;
        private Deputado deputado;

        public ComissoesPage()
        {
            this.InitializeComponent();
            GerarListaComissoes();
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

        private void GerarListaComissoes()
        {
            comissoes = new ObservableCollection<Comissao>();
            Comissao comi = new Comissao();

            comi.SiglaComissao = "CEXRACIS";
            comi.Condicao = "Titular";
            comi.NomeComissao = "Comissão Externa da Câmara dos Deputados, com ônus para esta Casa, para propor ações legislativas e políticas capazes de combater os recentes casos de Racismo, bem como investigar as providências adotadas pelos setores públicos e privados.";
            comi.EntradaTxt = "24/04/2014";
            comi.SaidaTxt = "07/05/2014";


            for (int i = 0; i < 7; i++)
            {
                comissoes.Add(comi);
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
