using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturación1w3.Entidades
{
    internal class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }

        public string Cliente { get; set; }

        public List<DetalleProducto> ListaProducto { get; set; }

        public Factura()
        {
            ListaProducto = new List<DetalleProducto>();
        }

        public void AgregarLista(DetalleProducto detalle)
        {
            ListaProducto.Add(detalle);
        }

        public void QuitarLista(int posicion)
        {
            ListaProducto.RemoveAt(posicion);
        }

        public double CalcularTotal()
        {
            double total = 0;

            foreach (DetalleProducto i in ListaProducto)
            {
                total += i.calcularsubtotal();
            }
            return total;
        }
    }
}
