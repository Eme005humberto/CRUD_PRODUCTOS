using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NegocioCategoria
    {
        private ServiciosDB pr = new ServiciosDB();

        public DataTable mostrarCategorias()
        {
            return pr.MostrarCategorias(); //Invocamos el metodo que nos servira para mostrar la info de las categorias
        }
        public void InsertData(string nombre)
        {
            pr.InsertarCategorias(nombre); //Invocamos el metodo que nos servira para insertar la info de las categorias
        }

        public void UpdateData(string categoria, int IdCategoria)
        {
            pr.ModificarCategorias(categoria,IdCategoria); //Invocamos el metodo que nos servira para actualizar la info de las categorias
        }

        public void DeleteData(int IdCategoria)
        {
            pr.EliminarCategorias(IdCategoria); //Invocamos el metodo que nos servira para eliminar la info de las categorias
        }
    }
}
