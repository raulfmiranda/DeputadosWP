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
        // Lista de Deputados que está fazendo Bind com a MainPage.xaml
        private ObservableCollection<Deputado> deputados;

        // Lista para guardar todos os deputados
        private List<Deputado> listaDeputados;

        public MainPage()
        {
            this.InitializeComponent();
            deputados = new ObservableCollection<Deputado>();
            InitComboEstados();
            GerarListaDeputados();
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

        private void InitComboEstados()
        {
            string[] ufs = 
                {"TODOS", "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "AL", "AM", "AP", "BA", "CE",
                "DF", "ES", "GO", "MA", "MG", "MS", "MT", "PA", "PB", "PE", "PI", "PR", "RJ", "RN",
                "RO", "RR", "RS", "SC", "SE", "SP", "TO", "GO", "MA", "MG", "MS", "MT", "PA", "PB",
                "PE", "PI", "PR", "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO" };

            foreach(string uf in ufs)
            {
                cbEstados.Items.Add(uf);
            }
        }

        private void GerarListaDeputados()
        {
            
            listaDeputados = new List<Deputado>();

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
            deputado.ImageFromUrl = new BitmapImage(new Uri(deputado.FotoURL, UriKind.Absolute));

            for (int i = 0; i < 10; i++)
            {
                listaDeputados.Add(deputado);
                deputados.Add(deputado);
            }
        }

        private void FiltrarDeputados(string uf)
        {
            deputados.Clear();

            if(uf.Equals("TODOS"))
            {
                foreach(Deputado dep in listaDeputados)
                {
                    deputados.Add(dep);
                }
            }
            else
            {
                foreach (Deputado dep in listaDeputados)
                {
                    if(dep.Uf.Equals(uf))
                    {
                        deputados.Add(dep);
                    }
                }
            }
        }

        private void LinkNomeParlamentar_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton linkBtn = sender as HyperlinkButton;
    
            this.Frame.Navigate(typeof(DetalheDeputado), BuscaDeputado(linkBtn.Content.ToString()));
        }

        private Deputado BuscaDeputado(string nome)
        {
            foreach(Deputado dep in listaDeputados)
            {
                if(dep.NomeParlamentar.Equals(nome))
                {
                    return dep;
                }
            }
            return null;
        }

        private void cbEstados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string uf = cbEstados.SelectedValue.ToString();
            FiltrarDeputados(uf);
        }
    }
}
