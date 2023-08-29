using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Facturación1w3.Entidades;

namespace Facturacion1w3.Presentacion
{
    public partial class FormFactura : Form
    {
        Factura nueva;
        public FormFactura()
        {
            InitializeComponent();
            nueva = new Factura();
        }

        private void FormFactura_Load(object sender, EventArgs e)
        {
            proximaFactura();
            CargarProductos();
            CargarFormaPago();
            txtFecha.Text = DateTime.Today.ToShortDateString();
            txtCliente.Text = "Consumidor Final";
            txtDescuento.Text = "0";
            txtCantidad.Text = "1";
        }

        private void CargarFormaPago()
        {
            SqlConnection Conexion = new SqlConnection();
            Conexion.ConnectionString = @"Data Source=DESKTOP-M7J5IEU\SQLEXPRESS;Initial Catalog=Facturacion;Integrated Security=True";
            Conexion.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CONSULTAR_FORMAPAGO";
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            Conexion.Close();

            cboFormaPago.DataSource = dt;
            cboFormaPago.DisplayMember = dt.Columns[1].ColumnName;
            cboFormaPago.ValueMember = dt.Columns[0].ColumnName;

        }

        private void CargarProductos()
        {
            SqlConnection Conexion = new SqlConnection();
            Conexion.ConnectionString = @"Data Source=DESKTOP-M7J5IEU\SQLEXPRESS;Initial Catalog=Facturacion;Integrated Security=True";
            Conexion.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CONSULTAR_ARTICULO";
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            Conexion.Close();

            cboArticulo.DataSource = dt;
            cboArticulo.ValueMember = dt.Columns[0].ColumnName;
            cboArticulo.DisplayMember = dt.Columns[1].ColumnName;
        }

        private void proximaFactura()
        {
            SqlConnection Conexion = new SqlConnection();
            Conexion.ConnectionString = @"Data Source=DESKTOP-M7J5IEU\SQLEXPRESS;Initial Catalog=Facturacion;Integrated Security=True";
            Conexion.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_PROXIMO_ID";
            SqlParameter Parametro = new SqlParameter();
            Parametro.ParameterName = "@NEXT";
            Parametro.SqlDbType = SqlDbType.Int;
            Parametro.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(Parametro);

            cmd.ExecuteNonQuery();

            Conexion.Close();


            lblFactura.Text = lblFactura.Text + " " + Parametro.Value.ToString();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (cboArticulo.SelectedIndex == -1)
            {
                MessageBox.Show("DEBE SELECCIONAR UN ARTICULO", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("DEBE INGRESAR UNA CANTIDAD VALIDA", "CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dgvDetalle.Rows)
            {
                if (fila.Cells["Detalle"].Value.ToString().Equals(cboArticulo.Text))  // para validar si el producto que tengo en el combo esta en la grilla
                {
                    MessageBox.Show("ESTE PRODUCTO YA SE ENCUENTRA PRESUPUESTADO", "CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            DataRowView item = (DataRowView)cboArticulo.SelectedItem;

            int nro = Convert.ToInt32(item.Row.ItemArray[0]);  //FUNCIONA IGUAL QUE EL VALUEMEMBER Y DISPLAYMEMBER
            string nom = item.Row.ItemArray[1].ToString();

            double pre = Convert.ToDouble(item.Row.ItemArray[2]);
            Producto p = new Producto(nro, nom, pre);

            int cant = Convert.ToInt32(txtCantidad.Text);
            DetalleProducto detalle = new DetalleProducto(p, cant);

            nueva.AgregarLista(detalle);
            dgvDetalle.Rows.Add(detalle.Producto.numero,
                detalle.Producto.Nombre,
                detalle.Producto.PrecioUnitario,
                detalle.Cantidad,
                "quitar");


            calcularTotales();

        }

        private void calcularTotales()
        {
            txtSubTotal.Text = nueva.CalcularTotal().ToString();
            if (!string.IsNullOrEmpty(txtDescuento.Text) && int.TryParse(txtDescuento.Text, out _))
            {
                double descuento = nueva.CalcularTotal() * Convert.ToDouble(txtDescuento.Text) / 100;
                txtTotal.Text = (nueva.CalcularTotal() - descuento).ToString();
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalle.CurrentCell.ColumnIndex == 4)
            {
                nueva.QuitarLista(dgvDetalle.CurrentRow.Index);
                dgvDetalle.Rows.RemoveAt(dgvDetalle.CurrentRow.Index);
                calcularTotales();
            }
        }

        private void FormFactura_Load_1(object sender, EventArgs e)
        {

        }
    }
}