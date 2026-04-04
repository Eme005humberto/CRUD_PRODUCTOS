using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        public static SqlConnection ConectarDB()
        {
            string ConexionDb = "Server=DESKTOP-EMM9D05;Database=CRUD_PRODUCTOS;Trusted_Connection=True;";
            SqlConnection conexion = new SqlConnection(ConexionDb); //Inicializamos la conexion
            try
            {
                if (conexion.State == ConnectionState.Closed) //Verificamos que la conexion este cerrada
                {
                    conexion.Open(); //Abrimos la conexion
                    Console.WriteLine("La conexion ya esta abierta.");
                }
                else
                {
                    conexion.Close(); //Cerramos la conexion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar a la base de datos: " + ex.Message);
            }
            return conexion; //Retornamos la conexion
        }
    }
}