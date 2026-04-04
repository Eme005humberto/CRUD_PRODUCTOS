using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }
    }
}
