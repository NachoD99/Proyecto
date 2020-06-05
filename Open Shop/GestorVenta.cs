using System;
using System.Collections.Generic;

namespace Open_Shop
{
   
    class GestorVenta
    {
        static Carrito Carrito = new Carrito();
       
         static List<Venta> Ventas = new List<Venta>();

        static void Main(string[] args)
        {
            System.Console.WriteLine("\nBIENVENIDO A OPEN SHOP");
            while (true)
            {
                var fin = Operando();

                if (fin) break;
                
            }
        }
        static public bool Operando()
        {
            System.Console.WriteLine("Menú:");
            System.Console.WriteLine("1.Comprar");
            System.Console.WriteLine("2.Preparar pedido");

            var MenuInicial = int.Parse(Console.ReadLine());
            
            if(MenuInicial==1)
                {
                    while (true)
                    {
                        var finalizado = Comprar();
                        if (finalizado) break;
                    }
                    var pago = MetodoPago();
                    var productos = Carrito.obtenerProductosEnCarrito();
                    var venta = new Venta(productos);
                    Ventas.Add(venta);  
                }

            else if(MenuInicial==2)
            {
                System.Console.WriteLine("Pedido de compra:");

                if (Ventas.Count > 0)
                {
                    foreach (var item in Ventas)
                    {
                        System.Console.WriteLine("\n -------------------------------- \n");
                        item.Carrito.MostrarCarrito();
                        System.Console.WriteLine("\n -------------------------------- \n");
                    }
                }
                else System.Console.WriteLine("No se registraron ventas");
            }
            else System.Console.WriteLine("La opción ingresada no es válida");
                        
            
            System.Console.WriteLine("\n¿Desea continuar operando?");
            System.Console.WriteLine("1.Sí");
            System.Console.WriteLine("2.No");
            var opcionOperar = int.Parse(System.Console.ReadLine());
            
            if (opcionOperar == 1) return false;
            else return true;
        }
    
        static public bool Comprar()
        {
            RegistroProductos.MostrarProductos();

            System.Console.WriteLine("\nSeleccione un producto");
            var opcionProducto = System.Console.ReadLine();
            var producto = RegistroProductos.Productos[int.Parse(opcionProducto) - 1];

            System.Console.WriteLine("\nIntroduzca la cantidad de productos que desea comprar:");
            var opcionCantidad = System.Console.ReadLine();
            int cantidadElegida = (int.Parse(opcionCantidad));

            Carrito.Agregar(producto, cantidadElegida);
            Carrito.MostrarCarrito();

            System.Console.WriteLine("\nDigite 1 para seguir comprando, 2 para seleccionar metodo de pago");
            var opcionSeguir = System.Console.ReadLine();

            if (int.Parse(opcionSeguir) == 1) return false;
            
            else return true;
        }
    
    static public int MetodoPago()
     {
        System.Console.WriteLine("Seleccione medio de pago:");
        System.Console.WriteLine("1. Tarjeta crédito a 6 cuotas");
        System.Console.WriteLine("2. Tarjeta debito");
        var medioPago = System.Console.ReadLine();

        if (string.IsNullOrEmpty(medioPago))
        {
            System.Console.WriteLine("Por favor seleccione un medio de pago");
            MetodoPago();
        }

        if(int.Parse(medioPago) == 1)
        {
            System.Console.WriteLine("Usted seleccionó pagar con tarjeta de crédito");
            GestorVenta.Confirmacion();
        }
        if(int.Parse(medioPago) == 2)
        {
            System.Console.WriteLine("Usted seleccionó pagar con tarjeta de débito");
            GestorVenta.Confirmacion();
        }
        return int.Parse(medioPago);
     }

    static public void Confirmacion()
    {
        System.Console.WriteLine("¿Desea confirmar su compra?");
        System.Console.WriteLine("1- SI");
        System.Console.WriteLine("2- NO");
        var opcion = System.Console.ReadLine();

        if (string.IsNullOrEmpty(opcion))
        {
            System.Console.WriteLine("Por favor confirme");
            Confirmacion();
        }
        if(int.Parse(opcion)==1) System.Console.WriteLine("Gracias por comprar en Open Shop");
        if(int.Parse(opcion)==2) MetodoPago();
    }
    }
}