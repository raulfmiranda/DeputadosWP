using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Deputados.Model
{
    public class Deputado
    {
        public string Id { get; set; }
        public string NomeParlamentar { get; set; }
        public string NomeCompleto { get; set; }
        public string Cargo { get; set; }
        public string Partido { get; set; }
        public string Mandato { get; set; }
        public string Sexo { get; set; }
        public string Uf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Nascimento { get; set; }
        public string FotoURL { get; set; }
        public double GastoTotal { get; set; }
        public double GastoPorDia { get; set; }
        public List<Comissao> Comissoes { get; set; }
        public List<Projeto> Projetos { get; set; }
        public List<DeputadoFrenquencia> Frequencias { get; set; }
        public BitmapImage imageFromUrl { get; set; }

        public Deputado()
        {

        }


    }
}
