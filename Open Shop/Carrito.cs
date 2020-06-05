using System;
using System.Collections.Generic;

namespace Open_Shop
{
    class Carrito
    {
       public List<ItemCarrito> Productos = new List<ItemCarrito>();

       public void Agregar(Producto producto, int cantidad)
        {
            var productoEnCarrito = new ItemCarrito();
            productoEnCarrito.Producto = producto;
            productoEnCarrito.Cantidad = cantidad;
            Productos.Add(productoEnCarrito);
        }

        public List<ItemCarrito> obtenerProductosEnCarrito()
        {
            return Productos;
        }

       public void MostrarCarrito()
        {
            System.Console.WriteLine("\nTienes en tu carrito: ");

            decimal totalCarrito = 0;
            foreach (var productoEnCarrito in Productos)
            {
                var cantidad = productoEnCarrito.Cantidad;
                var precio = productoEnCarrito.Producto.Precio;
                var nombre = productoEnCarrito.Producto.Nombre;
                System.Console.WriteLine(cantidad + "x " + nombre + " $" + cantidad * precio);

                totalCarrito = totalCarrito + cantidad * precio;
            }

            System.Console.WriteLine("Total: $" + totalCarrito);
        }
    }
}