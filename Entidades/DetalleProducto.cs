using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturación1w3.Entidades
{
    internal class DetalleProducto
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public DetalleProducto(Producto Producto, int Cantidad)
        {
            this.Producto = Producto;
            this.Cantidad = Cantidad;
        }

        public double calcularsubtotal()
        {
            return Cantidad * Producto.PrecioUnitario;
        }
    }
}
