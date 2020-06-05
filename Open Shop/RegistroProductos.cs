using System;
using System.Collections.Generic;

namespace Open_Shop
{
    class RegistroProductos
    {
        public static List<Producto> Productos = new List<Producto>();

        static RegistroProductos()
        {
            Productos.Add(new Producto("Cafetera", 3000));
            Productos.Add(new Producto("Celular", 249999.99m));
            Productos.Add(new Producto("Televisor", 22000));
            Productos.Add(new Producto("Heladera", 29999.99m));
            Productos.Add(new Producto("Mouse",500));
            Productos.Add(new Producto("Teclado",900));
            Productos.Add(new Producto("Computadora",50000));
        }
        static public void MostrarProductos()
        {
            System.Console.WriteLine("Listado de productos:");
            int posicion = 1;
            foreach (var producto in Productos)
            {
                System.Console.WriteLine(posicion + "-" + producto.Nombre +" $" + producto.Precio);
                posicion++;
            }
        }
    }
}