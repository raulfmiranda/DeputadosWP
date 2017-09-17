using Deputados.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace Deputados
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Deputado> deputados;

        public MainPage()
        {
            this.InitializeComponent();
            gerarListaDeputados();
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

        private void gerarListaDeputados()
        {
            deputados = new ObservableCollection<Deputado>();

            Deputado deputado = new Deputado();
            deputado.Id = "160976";
            deputado.NomeParlamentar = "TIRIRICA";
            deputado.NomeCompleto = "FRANCISCO EVERARDO OLIVEIRA SILVA";
            deputado.Cargo = "DEPUTADO FEDERAL";
            deputado.Partido = "PR";
            deputado.Mandato = "2011-2014";
            deputado.Sexo = "M";
            deputado.Uf = "SP";
            deputado.Telefone = "3215-5637";
            deputado.Email = "dep.tiririca@camara.gov.br";
            deputado.Nascimento = "6/4/65";
            deputado.FotoURL = "http://www.camara.gov.br/internet/deputado/bandep/160976.jpg";
            deputado.GastoTotal = 767049.9806302488;
            deputado.GastoPorDia = 525.0170983095475;
            deputado.imageFromUrl = new BitmapImage(new Uri(deputado.FotoURL, UriKind.Absolute));

            deputados.Add(deputado);
            deputados.Add(deputado);
            deputados.Add(deputado);
        }
    }
}
