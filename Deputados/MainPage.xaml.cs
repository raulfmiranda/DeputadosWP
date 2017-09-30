using Deputados.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;

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
        //{
        //    get { return (ObservableCollection<Deputado>)GetValue(DeputadosProperty); }
        //    set { SetValue(DeputadosProperty, value); }
        //}

        //public static readonly DependencyProperty DeputadosProperty = 
        //    DependencyProperty.Register("deputados", typeof(ObservableCollection<Deputado>), typeof(MainPage), new PropertyMetadata(null));


        public MainPage()
        {
            this.InitializeComponent();
            //deputados = new ObservableCollection<Deputado>();
            InitComboEstados();
            deputados = Deputado.ListarTodosDeputados();
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

        private void FiltrarDeputados(string uf)
        {
            deputados.Clear();
            ObservableCollection<Deputado> deps = new ObservableCollection<Deputado>();

            if (uf.Equals("TODOS"))
            {
                deps = Deputado.ListarTodosDeputados();
                //deputados = Deputado.ListarTodosDeputados();

                foreach(Deputado dep in deps)
                {
                    deputados.Add(dep);
                }
            }
            else
            {
                deps = Deputado.ListarDeputadoPorEstado(uf);
                //deputados = Deputado.ListarDeputadoPorEstado(uf);

                foreach(Deputado dep in deps)
                {
                    deputados.Add(dep);
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
            foreach(Deputado dep in deputados)
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
