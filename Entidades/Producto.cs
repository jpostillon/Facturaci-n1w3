using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturación1w3.Entidades
{
    internal class Producto
    {

        public int numero { get; set; }
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }






        public Producto()
        {
            Nombre = string.Empty;
            PrecioUnitario = 0;
        }

        public Producto(int numero, string Nombre, double PrecioUnitario)
        {
            this.Nombre = Nombre;
            this.PrecioUnitario = PrecioUnitario;
            this.numero = numero;
        }


        public override string ToString()
        {
            return Nombre + " " + PrecioUnitario;
        }
    }
}
