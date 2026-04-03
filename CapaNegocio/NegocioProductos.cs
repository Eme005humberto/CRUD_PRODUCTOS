using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NegocioProductos
    {
        private ServiciosDB pr = new ServiciosDB();
        

        public DataTable mostrarProductos()
        {
            return pr.MostrarProductos(); //Invocamos el metodo que nos servira para mostrar la info de los productos
        }

        public void InsertData(string nombre, decimal precio, int stock, int idCategoria)
        {
            pr.InsertarProductos(nombre,precio, stock,idCategoria); //Invocamos el metodo que nos servira para insertar la info de los productos
        }

        public void UpdateData(int IdProducto, string nombre, decimal precio, int stock, int idCategoria)
        {
            pr.ActualizarProductos(IdProducto, nombre, precio, stock, idCategoria); //Invocamos el metodo que nos servira para actualizar la info de los productos
        }

        public void DeleteData(int IdProducto)
        {
            pr.EliminarProducto(IdProducto); //Invocamos el metodo que nos servira para eliminar la info de los productos
        }
    }
}
