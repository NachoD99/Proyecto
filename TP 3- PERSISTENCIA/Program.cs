using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TP_3
{
    class GestorCursos
    {
        
        static void Main(string[] args)
        {
            
            Persona.registrarDocente();

            System.Console.WriteLine("\nBienvenido a la secretaría de extensión y cultura de la Facultad");
            Administrador();
        }
        public static void Administrador ()
        {
                Curso curso = new Curso();
                System.Console.WriteLine("\nMenú: \n1- Cargar cursos\n2- Registrarse a curso\n3- Mostrar personas en curso\n4- Salir");
                var seleccion = System.Console.ReadLine();
            
            if(int.Parse(seleccion)==1)
            {
                System.Console.WriteLine("\nA continuación registre los cursos: ");
                curso.registrarCursos();
            }
            
            if(int.Parse(seleccion)==2)
            {
                System.Console.WriteLine("\nINSCRIPCION A CURSO");
                Inscripcion.registrarInscripcion();
            }
            
            if(int.Parse(seleccion)==3)
            {
                Inscripcion.mostrarPersonasEnCurso();
            }
        }
    }

    class Curso
    {
        public Guid id { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }
        public string nombreCurso { get; set; }
        public string dias { get; set; }
        public string horarios { get; set; }
        public int cupoDisponible { get; set; }
        public int cupoMinimo { get; set; }
        public bool estadoCurso { get; set; }
        public Persona docente { get; set; }
        public string descripcion { get; set; }
        public static List<Curso> cursos = new List<Curso>();
        public static int cantidadCursos;

        public Curso(){}
        public Curso(string nombreCurso, string dias, string horarios, int cupoDisponible, int cupoMinimo, bool estadoCurso, 
                string descripcion, Persona docente, DateTime fechaInicio, DateTime fechaFinalizacion)
        {
            this.nombreCurso = nombreCurso;
            this.dias = dias;
            this.horarios = horarios;
            this.cupoDisponible = cupoDisponible;
            this.cupoMinimo = cupoMinimo;
            this.estadoCurso = estadoCurso;
            this.descripcion = descripcion;
            this.docente = docente;
            this.fechaInicio = fechaInicio;
            this.fechaFinalizacion = fechaFinalizacion;
        }
        public void registrarCursos ()
        {
            //Pedir datos de los cursos
            System.Console.WriteLine("Nombre del curso: ");
            nombreCurso = System.Console.ReadLine();

            System.Console.WriteLine("Días que se dictará: ");
            dias = System.Console.ReadLine();

            System.Console.WriteLine("Horario: ");
            horarios = System.Console.ReadLine();

            System.Console.WriteLine("Cupo disponible: ");
            cupoDisponible = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Cupo mínimo: ");
            cupoMinimo = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Descripcion: ");
            descripcion = System.Console.ReadLine();

            fechaInicio = DateTime.Today;
            System.Console.WriteLine("Duración del curso (en días): ");
            var duracion = int.Parse(System.Console.ReadLine());

            fechaFinalizacion = fechaInicio.AddDays(duracion);

            int docenteElegido= asignarDocente(); 
            docente = Persona.docentes.ElementAt(docenteElegido-1);
            
            cursos.Add(new Curso (nombreCurso, dias, horarios, cupoDisponible, cupoMinimo, true, descripcion, docente, fechaInicio, fechaFinalizacion));
            
            //Persistir los datos de los cursos 
            var cursoEnArchivoJson = JsonConvert.SerializeObject(cursos, Formatting.Indented);
            System.IO.File.WriteAllText("cursos.json", cursoEnArchivoJson);

            cantidadCursos=cursos.Count;
            System.Console.WriteLine("¿Desea registrar otro curso?\n1-SI\n2-NO");
            var opcion = int.Parse(System.Console.ReadLine());

            if (opcion == 1) registrarCursos();
            else GestorCursos.Administrador();
        }
        
        public int asignarDocente()
        {   
            System.Console.WriteLine("Los docentes disponibles para asignar a los cursos son: ");
            Persona.mostrarDocentes();

            System.Console.WriteLine("Seleccione el docente que quiera dictar el curso: ");
            int opcionDocente = int.Parse(System.Console.ReadLine());
            return opcionDocente;
        }
        public static void mostrarCursos ()
        {   
            //deserializar los cursos
            if(System.IO.File.Exists("cursos.json"))
            {
                string jsonCursos = System.IO.File.ReadAllText("cursos.json");
                List<Curso> cursosJson = JsonConvert.DeserializeObject<List<Curso>>(jsonCursos);

                int pos = 1;
            foreach (var i in cursosJson)
                {   
               
                System.Console.WriteLine("\nCURSO N°:"+pos);
                System.Console.WriteLine("Nombre: "+i.nombreCurso);
                System.Console.WriteLine("Día: "+i.dias);
                System.Console.WriteLine("Horario: "+i.horarios);
                System.Console.WriteLine("Fecha inicio: "+i.fechaInicio);
                System.Console.WriteLine("Fecha finalización: "+i.fechaFinalizacion);

                pos++;
                }    
            }
        }
    }

    class Persona
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string domicilio { get; set; }
        public string telefono { get; set; }  
        public string email { get; set; }
        public string dni { get; set; }
        public string especialidad { get; set; }
        public static List<Persona> docentes = new List<Persona>();
        public string nombreCursoInscripto { get; set; }
        public DateTime fechaInscripcion { get; set; }
        
        public Persona (){}
        
        public Persona(string nombre, string dni, string email, string telefono,
        string nombreCursoInscripto, DateTime fechaInscripcion)
        {
            this.nombre = nombre;
            this.dni = dni;
            this.email = email;
            this.telefono = telefono;
            this.nombreCursoInscripto = nombreCursoInscripto;
            this.fechaInscripcion = fechaInscripcion;
        }
        public Persona(int id,string nombre, string domicilio, string telefono,
                      string mail, string dni, string especialidad)
        {
            this.id = id;
            this.nombre = nombre;
            this.domicilio = domicilio;
            this.telefono = telefono;
            this.email = email;
            this.dni = dni;
            this.especialidad = especialidad;
        }  
        
        public static void registrarDocente ()
        {
            docentes.Add(new Persona(1,"Docente 1", "Domicilio 1","Telefono 1","Mail 1", "DNI 1", "Especialidad 1"));
            docentes.Add(new Persona(2,"Docente 2", "Domicilio 2","Telefono 2","Mail 2", "DNI 2", "Especialidad 2"));
            docentes.Add(new Persona(3,"Docente 3", "Domicilio 3","Telefono 3","Mail 3", "DNI 3", "Especialidad 3"));
            docentes.Add(new Persona(4,"Docente 4", "Domicilio 4","Telefono 4","Mail 4", "DNI 4", "Especialidad 4"));
            docentes.Add(new Persona(5,"Docente 5", "Domicilio 5","Telefono 5","Mail 5", "DNI 5", "Especialidad 5"));

            var docenteJson = JsonConvert.SerializeObject(docentes, Formatting.Indented);
            System.IO.File.WriteAllText("docentes.Json", docenteJson);
        }

        public static void mostrarDocentes ()
        {
            if (System.IO.File.Exists("docentes.json"))
            {
                string docentesJson = System.IO.File.ReadAllText("docentes.json");
                List<Persona> JsonDocentes = JsonConvert.DeserializeObject<List<Persona>>(docentesJson);

                int pos=1;
                foreach (var i in JsonDocentes)
                {   
                    System.Console.WriteLine(pos+"- Docente: "+i.nombre+"\tEspecialidad: "+i.especialidad+"\n");
                    pos++;
                }
            }
        }
    }

    class Inscripcion
    {
        public Guid id { get; set; }
        public static Curso cursoInscripto { get; set; }
        public static DateTime fechaInscripcion { get; set; }
        public static List<Persona> personasEnCurso = new List<Persona>();
        public static List<Curso> cursosInscripcion = new List<Curso>();

        public static void registrarInscripcion ()
        {   
            //Pedir datos
            System.Console.WriteLine("Ingrese su nombre y apellido: ");
            var nombre1 = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese su DNI: ");
            var dni1= System.Console.ReadLine();
            System.Console.WriteLine("Ingrese su E-mail: ");
            var email1 = System.Console.ReadLine();
            System.Console.WriteLine("Ingrese su teléfono: ");
            var tel1 = System.Console.ReadLine();
            
            //Seleccionar cursos
            System.Console.WriteLine("Seleccione que curso desea tomar: ");
            Curso.mostrarCursos(); 
            System.Console.WriteLine("Ingrese su opción: ");
            var opcionCurso = int.Parse(System.Console.ReadLine());

            if (opcionCurso <= Curso.cantidadCursos)
                {
                    cursoInscripto = cursosInscripcion.ElementAt(opcionCurso-1);
                    fechaInscripcion = DateTime.Today;
                    personasEnCurso.Add(new Persona(nombre1, dni1, email1, tel1, cursoInscripto.nombreCurso, fechaInscripcion));
                
                    //Serializar personas en curso
                    var personasEnCursoJson = JsonConvert.SerializeObject(personasEnCurso, Formatting.Indented);
                    System.IO.File.WriteAllText("inscripciones.Json", personasEnCursoJson);

                    GestorCursos.Administrador();
                } else
                 {
                    System.Console.WriteLine("Curso no válido");
                    registrarInscripcion();
                 }
        }
        public static void mostrarPersonasEnCurso()
        {
            if (System.IO.File.Exists("inscripciones.json"))
            {
                string personasJson = System.IO.File.ReadAllText("inscripciones.json");
                List<Persona> JsonPersonas = JsonConvert.DeserializeObject<List<Persona>>(personasJson);

                foreach (var i in JsonPersonas)
                {
                System.Console.WriteLine("Nombre: "+i.nombre);
                System.Console.WriteLine("Asiste al curso: "+i.nombreCursoInscripto);
                System.Console.WriteLine("Fecha de inscripción: "+i.fechaInscripcion+"\n");
                } 
            } 
            GestorCursos.Administrador();
        }
    }
}
