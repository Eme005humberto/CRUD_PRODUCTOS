using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmCategoria : Form
    {
        CapaNegocio.NegocioCategoria serviciosDB = new CapaNegocio.NegocioCategoria();
        private string id = null;
        private bool editar = false;

        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            MostrarCategoria();
        }
        public void MostrarCategoria()
        {
            try
            {
                var dt = serviciosDB.mostrarCategorias();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (editar == false)
            {
                try
                {
                    serviciosDB.InsertData(txtCategoria.Text);
                    MessageBox.Show("Categoria Agregada!!");
                    MostrarCategoria();
                    txtCategoria.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (editar == true)
            {
                try
                {
                    serviciosDB.UpdateData(txtCategoria.Text, Convert.ToInt32(id));
                    MessageBox.Show("Categoria Editada!!");
                    MostrarCategoria();
                    txtCategoria.Clear();
                    editar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                editar = true;
                id = dataGridView1.CurrentRow.Cells["IdCategoria"].Value.ToString();
                txtCategoria.Text = dataGridView1.CurrentRow.Cells["Categoria"].Value.ToString();

            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtCategoria.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    id = dataGridView1.CurrentRow.Cells["IdCategoria"].Value.ToString();
                    serviciosDB.DeleteData(Convert.ToInt32(id));

                    MessageBox.Show("Categoría eliminada correctamente.");
                    MostrarCategoria();
                    txtCategoria.Clear();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show(
                            "No se puede eliminar esta categoría porque tiene productos relacionados.\n\n" +
                            "Primero elimina los productos asociados a esta categoría y luego intenta eliminarla nuevamente.",
                            "Categoría relacionada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "Ocurrió un error en la base de datos:\n" + ex.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Ocurrió un error:\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }
    }
}
