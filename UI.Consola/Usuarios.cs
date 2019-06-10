using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Logic;
using Business.Entities;

namespace UI.Consola
{
    public class Usuarios
    {
        private Business.Logic.UsuarioLogic _usuarioNegocio = new UsuarioLogic();

        public Business.Logic.UsuarioLogic UsuarioNegocio
        {
            get { return _usuarioNegocio; }
            set { _usuarioNegocio = value; }
        }

        public void Menu()
        {
            string option;
            do
            {
                System.Console.Clear();
                System.Console.WriteLine("Bienvenido");
                System.Console.WriteLine("1– Listado General");
                System.Console.WriteLine("2– Consulta");
                System.Console.WriteLine("3– Agregar");
                System.Console.WriteLine("4- Modificar");
                System.Console.WriteLine("5- Eliminar");
                System.Console.WriteLine("6- Salir");
                System.Console.WriteLine();
                option = System.Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ListadoGeneral();
                        break;
                    case "2":
                        Consultar();
                        break;
                    case "3":
                        Agregar();
                        break;
                    case "4":
                        Modificar();
                        break;
                    case "5":
                        Borrar();
                        break;
                    default:
                        System.Console.Clear();
                        System.Console.WriteLine("Opción incorrecta");
                        break;
                }
            } while (option != "6");

        }
        private void ListadoGeneral()
        {
            Console.Clear();
            foreach (Usuario usr in UsuarioNegocio.GetAll())
            {
                MostrarDatos(usr);
            }
            System.Console.WriteLine();
            System.Console.WriteLine("Presione una tecla para continuar");
            System.Console.ReadKey();
        }

        private void MostrarDatos(Usuario usr)
        {
            System.Console.WriteLine("Usuario: {0}", usr.ID);
            System.Console.WriteLine("\t\tNombre: {0}", usr.Nombre);
            System.Console.WriteLine("\t\tApellido: {0}", usr.Apellido);
            System.Console.WriteLine("\t\tNombre de usuario: {0}", usr.NombreUsuario);
            System.Console.WriteLine("\t\tClave: {0}", usr.Clave);
            System.Console.WriteLine("\t\tEmail: {0}", usr.Clave);
            System.Console.WriteLine("\t\tHabilitado: {0}", usr.Habilitado);
            System.Console.WriteLine();
        }

        private void Borrar()
        {
            throw new NotImplementedException();
        }

        private void Modificar()
        {
            throw new NotImplementedException();
        }

        private void Agregar()
        {
            throw new NotImplementedException();
        }

        private void Consultar()
        {
            try
            {
                System.Console.Clear();
                System.Console.Write("Ingrese un ID del usuario: ");
                int ID = int.Parse(System.Console.ReadLine());
                this.MostrarDatos(UsuarioNegocio.GetOne(ID));
                System.Console.WriteLine();
                System.Console.WriteLine("Presione una tecla para continuar");
                System.Console.ReadKey();
            }
            catch(FormatException fe)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("La ID ingresada debe ser un numero entero");
            }
            catch (Exception e)
            {
                System.Console.WriteLine();
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Presione una tecla para continuar");
                System.Console.ReadKey();
            }
        }
    }
}
