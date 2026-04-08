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
        //Metodo para mostrar los productos existentes
        public DataTable MostrarProductos()
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = Conexion.ConectarDB())
            {
                using (SqlCommand cmd = new SqlCommand("SP_MostrarProductos", conn))
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
        //Metodos de productos para ingresar los nuevos registros
        public void InsertarProductos(string nombre, decimal precio, int stock, int idCategoria)
        {
            using(SqlConnection conn = Conexion.ConectarDB())
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertarProductos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Metodo para actualizar los registros existentes
        public void ActualizarProductos(int IdProducto, string nombre, decimal precio, int stock, int idCategoria)
        {
            using(SqlConnection conn = Conexion.ConectarDB())
            {
                using (SqlCommand cmd = new SqlCommand("SP_ActualizarProducto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", IdProducto);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Metodo para eliminar los registros existentes
        public void EliminarProducto(int IdProducto)
        {
            using (SqlConnection conn = Conexion.ConectarDB())
            {
                using (SqlCommand cmd = new SqlCommand("SP_EliminarProducto", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", IdProducto);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Metodos de categorias
        //Metodo para Insertar categorias
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
        //Metodo para Insertar categorias
        public void ModificarCategorias(string categoria, int IdCategoria)
        {
            using(SqlConnection conn = Conexion.ConectarDB())
            {
                using (SqlCommand cmd = new SqlCommand("SP_ModificarCategorias", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Categoria", categoria);
                    cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Metodo para eliminar los registros existentes
        public void EliminarCategorias(int IdCategoria)
        {
            using (SqlConnection conn = Conexion.ConectarDB())
            {
                   
                using (SqlCommand cmd = new SqlCommand("SP_EliminarCategoria", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
