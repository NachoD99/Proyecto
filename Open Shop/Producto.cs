using System;
using System.Collections.Generic;

namespace Open_Shop
{
    class Producto
    {
       public string Nombre { get; set; }
       public decimal Precio { get; set; }

       public Producto(string nombre, decimal precio)
       {
           Nombre = nombre;
           Precio = precio;
       }
    }
}