using Deputados.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace Deputados
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class DetalheDeputado : Page
    {
        private Deputado deputado;

        public DetalheDeputado()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                deputado = (Deputado)e.Parameter;
                tbNomeParlamentar.Text = deputado.NomeParlamentar;
                tbNomeCompleto.Text = deputado.NomeCompleto;
                tbCargo.Text = deputado.Cargo;
                tbPartido.Text = deputado.Partido;
                tbMandato.Text = deputado.Mandato;
                tbSexo.Text = deputado.Sexo;
                tbUf.Text = deputado.Uf;
                tbTelefone.Text = deputado.Telefone;
                tbEmail.Text = deputado.Email;
                tbNascimento.Text = deputado.Nascimento;
                tbGastoTotal.Text = deputado.GastoTotal.ToString();
                tbGastoPorDia.Text = deputado.GastoPorDia.ToString();
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            this.splitView.IsPaneOpen = !this.splitView.IsPaneOpen;
        }

        private void Global_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            mbt1.BorderBrush = new SolidColorBrush(Colors.Transparent);
            mbt2.BorderBrush = new SolidColorBrush(Colors.Transparent);
            mbt3.BorderBrush = new SolidColorBrush(Colors.Transparent);
            btn.BorderBrush = new SolidColorBrush(Colors.White);
            btn.BorderThickness = new Thickness(5, 0, 0, 0);

            if (btn.Name.Equals("mbt1"))
            {
                //tbConteudo.Text = "1";
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
    }
}
