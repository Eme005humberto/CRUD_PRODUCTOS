using QuestPDF.Fluent;
using System.Data;
using System.Data.SqlClient;

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
            MostrarCategorias();
        }
        //Metodo para mostrar los productos en el datagridview
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                id = dataGridView1.CurrentRow.Cells["IdProducto"].Value.ToString();
                serviciosDB.DeleteData(Convert.ToInt32(id));
                MessageBox.Show("Producto Eliminada!!");
                MostrarProductos();
            }
        }
        //Metodo para mostrar las categorias en el combobox
        public void MostrarCategorias()
        {
            try
            {
                using (SqlConnection conn = CapaDatos.Conexion.ConectarDB())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM CATEGORIA", conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            comboBox1.DataSource = dt;
                            comboBox1.DisplayMember = "Categoria";
                            comboBox1.ValueMember = "IdCategoria";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private List<ReporteCategoria> ObtenerDatosReporte(int idCategoria, string nombreCategoria)
        {
            List<ReporteCategoria> lista = new List<ReporteCategoria>();

            try
            {
                using (SqlConnection conn = CapaDatos.Conexion.ConectarDB())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GenerarReporte", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Categoria", SqlDbType.Int).Value = idCategoria;

                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new ReporteCategoria
                                {
                                    Nombre = dr["Nombre"]?.ToString() ?? "",
                                    Categoria = dr["Categoria"]?.ToString() ?? nombreCategoria
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos del reporte: " + ex.Message);
            }

            return lista;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una categoría.");
                    return;
                }

                int idCategoria = Convert.ToInt32(comboBox1.SelectedValue);
                string nombreCategoria = comboBox1.Text;

                List<ReporteCategoria> datos = ObtenerDatosReporte(idCategoria, nombreCategoria);

                if (datos.Count == 0)
                {
                    MessageBox.Show("No hay datos para la categoría seleccionada.");
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Guardar reporte PDF";
                    saveFileDialog.FileName = $"Reporte_{nombreCategoria}_{DateTime.Now:yyyyMMddHHmmss}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var documento = new ReportePDF(datos, nombreCategoria);
                        documento.GeneratePdf(saveFileDialog.FileName);

                        MessageBox.Show("PDF generado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una categoría.");
                    return;
                }

                int idCategoria = Convert.ToInt32(comboBox1.SelectedValue);
                string Categoria = comboBox1.Text;

                List<ReporteCategoria> datos = ObtenerDatosReporte(idCategoria, Categoria);

                if (datos.Count == 0)
                {
                    MessageBox.Show("No hay datos para la categoría seleccionada.");
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    saveFileDialog.Title = "Guardar reporte PDF";
                    saveFileDialog.FileName = $"Reporte_{Categoria}_{DateTime.Now:yyyyMMddHHmmss}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var documento = new ReportePDF(datos,Categoria);
                        documento.GeneratePdf(saveFileDialog.FileName);

                        MessageBox.Show("PDF generado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message);
            }
        }
    }
}
