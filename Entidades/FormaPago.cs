using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturación1w3.Entidades
{
    internal class FormaPago
    {
        public string nombre { get; set; }

        public FormaPago()
        {
            nombre = string.Empty;
        }
        public FormaPago(string nombre)
        {
            this.nombre = nombre;
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}
