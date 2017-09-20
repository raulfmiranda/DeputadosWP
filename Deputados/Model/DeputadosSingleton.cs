using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deputados.Model
{
    public sealed class DeputadosSingleton
    {
        private static DeputadosSingleton instance;
        private static object sync = new Object();

        private DeputadosSingleton() { }

        public static DeputadosSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        if (instance == null)
                        {
                            instance = new DeputadosSingleton();
                        }
                    }
                }
                return instance;
            }

        }

        public List<Deputado> ListaDeputados { get; set; }
    }
}
