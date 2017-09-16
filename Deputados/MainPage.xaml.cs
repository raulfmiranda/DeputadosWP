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
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace Deputados
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Deputado deputado = new Deputado();

        public MainPage()
        {
            this.InitializeComponent();
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
                tbConteudo.Text = "1";
            }
            else if (btn.Name.Equals("mbt2"))
            {
                tbConteudo.Text = "2";
            }
            else if (btn.Name.Equals("mbt3"))
            {
                tbConteudo.Text = "3";
            }

        }

        private ObservableCollection<Deputado> gerarListaDeputados()
        {
            ObservableCollection<Deputado> lista = new ObservableCollection<Deputado>();

            return lista;
        }
    }
}
