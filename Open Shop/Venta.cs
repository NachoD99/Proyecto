using System;
using System.Collections.Generic;

namespace Open_Shop
{
    class Venta
    {
    private List<ItemCarrito> ProductosVendidos = new List<ItemCarrito>();
     public Carrito Carrito { get; set; }

     public Venta(List<ItemCarrito> productos)
        {
            Carrito = new Carrito(); 
            foreach (var item in productos)
            {
                Carrito.Agregar(item.Producto, item.Cantidad);
            }
        }
    }
}