using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ServiciosDB
    {
        Conexion conexion = new Conexion();

        SqlDataReader leer;
        DataTable table = new DataTable();
        SqlCommand cmd = new SqlCommand();


        public DataTable MostrarProductos()
        {
            cmd.Connection = Conexion.ConectarDB();
            cmd.CommandText = "SP_MostrarProductoss";
            cmd.CommandType = CommandType.StoredProcedure;
            leer = cmd.ExecuteReader();
            table.Load(leer);
            Conexion.ConectarDB().Close();
            return table;
        }
        //Metodo para mostrar las categorias existentes
        public DataTable MostrarCategorias()
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = Conexion.ConectarDB())
            {
                using (SqlCommand cmd = new SqlCommand("SP_MostrarCategorias", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
                return table;
            }
        }

        public void InsertarProductos(string nombre, decimal precio, int stock, int idCategoria)
        {
            cmd.Connection = Conexion.ConectarDB();
            cmd.CommandText = "SP_InsertarProductos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Precio", precio);
            cmd.Parameters.AddWithValue("@Stock", stock);
            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public void ActualizarProductos(int IdProducto, string nombre, decimal precio, int stock, int idCategoria)
        {
            cmd.Connection = Conexion.ConectarDB();
            cmd.CommandText = "SP_ActualizarProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Precio", precio);
            cmd.Parameters.AddWithValue("@Stock", stock);
            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
            cmd.Parameters.AddWithValue("@IdProducto", IdProducto);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public void EliminarProducto(int IdProducto)
        {
            cmd.Connection = Conexion.ConectarDB();
            cmd.CommandText = "SP_EliminarProducto";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdProducto", IdProducto);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        //Metodos de categorias
        public void InsertarCategorias(string categoria)
        {
            using(SqlConnection conn = Conexion.ConectarDB())
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertarCategorias", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Categoria", categoria);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ModificarCategorias(string categoria, int IdCategoria)
        {
            cmd.Connection = Conexion.ConectarDB();
            cmd.CommandText = "SP_ModificarCategorias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Categoria", categoria);
            cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public void EliminarCategorias(int IdCategoria)
        {
            cmd.Connection = Conexion.ConectarDB();
            cmd.CommandText = "SP_EliminarCategoria";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
    }
}
