using ActividadSemana4.Entidades;
using ActividadSemana4.Servicios.Implementación;
using ActividadSemana4.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActividadSemana4.Presentación
{
    //En la bd no estaba el SP para el proximo nro, asi que lo hice:
    //create PROCEDURE [dbo].[SP_PROXIMO_NRO]
//    @next int OUTPUT
//    AS
//   BEGIN

//    SET @next = (SELECT MAX(nro)+1  FROM T_ORDENES_RETIRO);
//    END
    public partial class FrmNuevaOrden : Form
    {
        OrdenRetiro nuevo = null;
        IServicio servicio = null;
        public FrmNuevaOrden()
        {
            InitializeComponent();
            nuevo=new OrdenRetiro();
            servicio = new Servicio();
        }
        private void FrmNuevaOrden_Load(object sender, EventArgs e)
        {
            lblNroOrden.Text = lblNroOrden.Text + " " + servicio.TraerProxNro().ToString();
            CargarMateriales();
            cboMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtResponsable.Text))
            {
                MessageBox.Show("Debe ingresar un responsable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(cboMaterial.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un material", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad valida","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["ColMaterial"].Value.ToString().Equals(cboMaterial.Text))
                {
                    MessageBox.Show("Este material ya esta agregado", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            
            Material m = (Material)cboMaterial.SelectedItem;
            if (Convert.ToInt32(txtCantidad.Text) >m.Stock)
            {
                MessageBox.Show("No hay stock suficiente!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
                int cant = Convert.ToInt32(txtCantidad.Text);
            DetalleOrden detalle=new DetalleOrden(m,cant);
            nuevo.AgregarDetalle(detalle);

            dgvDetalles.Rows.Add(new object[] { m.Nombre,m.Stock, cant, "Quitar" });
        }
        private void CargarMateriales()
        {
            cboMaterial.DataSource=servicio.TraerMateriales();
            cboMaterial.ValueMember = "codigo";
            cboMaterial.DisplayMember = "nombre";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtResponsable.Text))
                {
                MessageBox.Show("Debe ingresar un responsable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(dgvDetalles.Rows.Count==0)
            {
                MessageBox.Show("Debe ingresar al menos un detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            GrabarOrden();
        }
        private void GrabarOrden()
        {
            int aux = servicio.TraerProxNro();
            nuevo.Responsable = txtResponsable.Text;
            if(servicio.GrabarOrden(nuevo))
            {
                MessageBox.Show("Se registró la orden numero "+aux+" con exito!","Hecho",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No se pudo registrar la orden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvDetalles.CurrentCell.ColumnIndex==3)
            {
                nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
            }
        }
    }
}
