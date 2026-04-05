namespace CapaPresentacion
{
    public partial class FrmProductos : Form
    {
        CapaNegocio.NegocioProductos serviciosDB = new CapaNegocio.NegocioProductos();
        private string id = null;
        private bool editar = false;
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            FrmCategoria frmCategoria = new FrmCategoria();
            frmCategoria.ShowDialog();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            MostrarProductos();
        }

        public void MostrarProductos()
        {
            try
            {
                var dt = serviciosDB.mostrarProductos();
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
                    serviciosDB.InsertData(txtNombre.Text, decimal.Parse(txtPrecio.Text), int.Parse(txtStock.Text), int.Parse(txtCategoria.Text));
                    MessageBox.Show("Producto Agregado!!");
                    MostrarProductos();
                    txtCategoria.Clear();
                    txtPrecio.Clear();
                    txtStock.Clear();
                    txtNombre.Clear();

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
                    serviciosDB.UpdateData(Convert.ToInt32(id), txtNombre.Text, decimal.Parse(txtPrecio.Text), int.Parse(txtStock.Text), int.Parse(txtCategoria.Text));
                    MessageBox.Show("Producto Editado!!");
                    MostrarProductos();
                    txtCategoria.Clear();
                    txtPrecio.Clear();
                    txtStock.Clear();
                    txtNombre.Clear();
                    editar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtCategoria.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtPrecio.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtStock.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtCategoria.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                editar = true;
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
             if(dataGridView1.SelectedRows.Count > 0)
            {
                id = dataGridView1.CurrentRow.Cells["IdProducto"].Value.ToString();
                serviciosDB.DeleteData(Convert.ToInt32(id));
                MessageBox.Show("Producto Eliminada!!");
                MostrarProductos();
            }
        }
    }
}
