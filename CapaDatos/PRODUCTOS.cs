using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class PRODUCTOS
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public BigInteger Stock { get; set; }
        public int IdCategoria { get; set; }
    }
}
