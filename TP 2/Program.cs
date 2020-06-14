using System;
using System.Collections.Generic;

namespace TP_2
{
    class GestorCotizacion
    {   
        static Cotizacion cotizacion = new Cotizacion();
        
        static void Main(string[] args)
        {
            while (true)
            {
                var fin = Administrador();

                if (fin) 
                {
                    Cotizacion.MostrarCotizaciones();
                    break;
                }
            }
        }

        static public bool Administrador ()
        {
            System.Console.WriteLine("Menú: \n1-REGISTRAR CLIENTES \n2-CALCULAR COTIZACIÓN");
            var opcionMenu = System.Console.ReadLine();

            if (int.Parse(opcionMenu)==1)
            {
                while (true)
                {
                    var fin = Cliente.RegistroClientes();
                    if (fin) break;
                }   
            }
            else if (int.Parse(opcionMenu)==2) PedirDatos();
            
            System.Console.WriteLine("\n");
            System.Console.WriteLine("1.VOLVER AL MENU");
            System.Console.WriteLine("2.FINALIZAR");
            var opcionOperar = int.Parse(System.Console.ReadLine());
            
            if (opcionOperar == 1) return false;
            else return true;
        }
        static public void PedirDatos()
        {   
            //Escojo tipo de material
            System.Console.WriteLine("Elija el tipo de material: ");
            RegistroMateriales.MostrarMateriales();
            var Opcionmaterial= System.Console.ReadLine();
            var material = RegistroMateriales.materiales[int.Parse(Opcionmaterial)-1];
            
            //Selecciono cantidad de metros cuadrados
            System.Console.WriteLine("Indicar los metros cuadrados de pared a cubrir: ");
            double metros=double.Parse(System.Console.ReadLine());
            
            //Escojo espesor de material
            System.Console.WriteLine("Elija el espesor: ");
            RegistroEspesores.MostrarEspesores();
            var OpcionEspesor=int.Parse(System.Console.ReadLine());  
            var espesor = RegistroEspesores.espesores[OpcionEspesor-1];
            System.Console.Clear();
            
            //Escojo el cliente
            System.Console.WriteLine("¿Para que cliente es esta cotización?");
            Cliente.MostrarClientes();
            System.Console.WriteLine("Elija la opcion: ");
            var opcionCliente = int.Parse(System.Console.ReadLine()); 
            var opcionClienteElegido = Cliente.clientes[opcionCliente-1];

            cotizacion.CalcularCotizacion(material, metros, espesor, opcionClienteElegido);                 
        }
    }
    class ItemCotizacion
        {
            public Material Material { get; set; }
            public double Cantidad { get; set; }
            public Espesor Espesor { get; set; }
            public Cliente Cliente { get; set; }
        }

    class Cotizacion
    {
         public DateTime FechaInicial { get; set; }
         public DateTime FechaFinal { get; set; }
         double presupuesto=0;
         double rendimientoBolsas;
         public Cliente cliente = new Cliente();

         public static List<Cotizacion> cotizaciones = new List<Cotizacion>();
         public Cotizacion(){}

        public Cotizacion (DateTime FechaInicial, double presupuesto, double rendimientoBolsas, Cliente cliente, DateTime FechaFinal)
        {
            this.FechaInicial = FechaInicial;
            this.presupuesto = presupuesto;
            this.rendimientoBolsas = rendimientoBolsas;
            this.cliente = cliente;
            this.FechaFinal = FechaFinal;
        }
        public void CalcularCotizacion(Material material, double metros, Espesor espesor, Cliente cliente)
        {      
            var Cotizacion = new ItemCotizacion();
            Cotizacion.Cantidad = metros;
            Cotizacion.Material = material;
            Cotizacion.Espesor = espesor;
            Cotizacion.Cliente = cliente;
            
            double rendimientoBolsas= CalculoBolsas(metros, espesor);

            presupuesto= Cotizacion.Material.precio * Cotizacion.Espesor.precio * rendimientoBolsas;
            System.Console.WriteLine("\n--------------------------\n"+"Pedido de presupuesto: ");
            System.Console.WriteLine(FechaInicial = DateTime.Now);

            System.Console.WriteLine("Su presupuesto es: $"+presupuesto);
                
            System.Console.WriteLine("Necesitará: "+rendimientoBolsas+" bolsas");
            
            System.Console.WriteLine("A nombre de: "+Cotizacion.Cliente.nombre+" "+ Cotizacion.Cliente.apellido);

            System.Console.WriteLine("Su presupuesto tendrá validez hasta el: ");
            System.Console.WriteLine(FechaFinal = FechaInicial.AddDays(30));
            System.Console.WriteLine("--------------------------\n");

            Cotizacion cotizacion1 = new Cotizacion(FechaInicial, presupuesto, rendimientoBolsas, cliente, FechaFinal);
            cotizaciones.Add(cotizacion1);
            }

        public double CalculoBolsas(double metros, Espesor espesor)
        {
            double bolsa=0;
            bolsa= Math.Ceiling(metros / espesor.rendimientoBolsas);
            return bolsa;
        }

        public static void MostrarCotizaciones ()
        {
            foreach (var i in cotizaciones)
            {
                System.Console.WriteLine("\nFecha de cotizacion: "+i.FechaInicial);
                System.Console.WriteLine("A nombre de: "+i.cliente.nombre+" "+i.cliente.apellido);
                System.Console.WriteLine("Tiene un presupuesto de: $"+i.presupuesto);
                System.Console.WriteLine("Se necesitarán: "+i.rendimientoBolsas+" bolsas");
                System.Console.WriteLine("Validez hasta el: "+i.FechaFinal);
            }
        }
    }
    class Material
    {
        public string tipo { get; set; }
        public double precio { get; set; }
        
        public Material(double precio, string tipo)
        {
            this.tipo=tipo;
            this.precio=precio;
        }
    }
    class RegistroMateriales
    {
        public static List<Material> materiales = new List<Material>();

        static RegistroMateriales()
        {
            RegistroMateriales.materiales.Add(new Material(100,"Material aislante 1"));
            RegistroMateriales.materiales.Add(new Material(200,"Material aislante 2"));
            RegistroMateriales.materiales.Add(new Material(300,"Material aislante 3"));
        }

        static public void MostrarMateriales()
        {
            int posicion=1;
            foreach (var i in materiales)
            {
                System.Console.WriteLine(posicion+"-"+" Material: "+ i.tipo+" Precio: "+i.precio);
                posicion++;
            }
        }
    }
    class Espesor
    {
        public int espesor { get; set; }
        public double precio { get; set; }
        public double rendimientoBolsas { get; set; }

        public Espesor(double precio, int espesor,  double rendimientoBolsas)
        {
            this.espesor=espesor;
            this.precio=precio;
            this.rendimientoBolsas = rendimientoBolsas;
        }
    }
    class RegistroEspesores
    {
        public static List<Espesor> espesores = new List<Espesor>();

        static RegistroEspesores()
        {
            RegistroEspesores.espesores.Add(new Espesor(53.6,   50, 9));
            RegistroEspesores.espesores.Add(new Espesor(87,     70, 7.65));
            RegistroEspesores.espesores.Add(new Espesor(117.49, 100, 4.5));
            RegistroEspesores.espesores.Add(new Espesor(128.48, 120, 3.6));
            RegistroEspesores.espesores.Add(new Espesor(143.05, 160, 2.7));
            RegistroEspesores.espesores.Add(new Espesor(180.79, 200, 2.25));
        }

        static public void MostrarEspesores()
        {
            int posicion=1;
            foreach (var i in espesores)
            {
                System.Console.WriteLine(posicion+"-"+" Espesor: "+ i.espesor+" Precio: "+i.precio);
                posicion++;
            }
        }
    }

    
    class Cliente
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string empresa { get; set; }
        public string domicilio { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public static List<Cliente> clientes= new List<Cliente>();

        public Cliente() {}
        public Cliente(string nombre,string apellido, string empresa, string domicilio, string email, string telefono)
        {
            this.nombre=nombre;
            this.apellido=apellido;
            this.empresa=empresa;
            this.domicilio=domicilio;
            this.email=email;
            this.telefono=telefono;
        }
        public static bool RegistroClientes()
        {
            System.Console.WriteLine("REGISTRO DE CLIENTES: ");
            System.Console.WriteLine("Ingrese su nombre: ");
            var nombre1 = System.Console.ReadLine();

            System.Console.WriteLine("Ingrese su apellido: ");
            var apellido1 = System.Console.ReadLine();

            System.Console.WriteLine("Ingrese el nombre de la empresa donde trabaja: ");
            var empresa1= System.Console.ReadLine();

            System.Console.WriteLine("Ingrese su domicilio: ");
            var domicilio1 = System.Console.ReadLine();

            System.Console.WriteLine("Ingrese su e-mail: ");
            var email1 = System.Console.ReadLine();

            System.Console.WriteLine("Ingrese su teléfono: ");
            var telefono1 = System.Console.ReadLine();
            
            Cliente cliente = new Cliente(nombre1, apellido1, empresa1, domicilio1, email1,telefono1);
            clientes.Add(cliente);
            
            System.Console.WriteLine("¿Desea agregar un nuevo cliente? \n1-SI \n2-NO");
            var opcionSeguir = System.Console.ReadLine();

            if (int.Parse(opcionSeguir)==1) return false;
            else  return true;
            
        }

        public static void MostrarClientes()
        {
            int posicion = 0;
            foreach (var i in clientes)
            {   
                posicion++;
                System.Console.WriteLine("\nCliente "+posicion+": \n");
                System.Console.WriteLine("Nombre: "+i.nombre+"\nApellido: "+i.apellido);
                System.Console.WriteLine("Empresa: "+i.empresa+"\nDomicilio: "+i.domicilio);
                System.Console.WriteLine("Email: "+i.email+"\nTelefono: "+i.telefono);
            }
        }
    }
}