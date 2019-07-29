using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
            System.Console.WriteLine("\t\tEmail: {0}", usr.EMail);
            System.Console.WriteLine("\t\tHabilitado: {0}", usr.Habilitado);
            System.Console.WriteLine();
        }

        private void Borrar()
        {
            try
            {
                System.Console.Clear();
                System.Console.Write("Ingrese un ID del usuario: ");
                int ID = int.Parse(System.Console.ReadLine());
                Usuario usr = UsuarioNegocio.GetOne(ID);
                this.MostrarDatos(UsuarioNegocio.GetOne(ID));
                System.Console.WriteLine();

                System.Console.WriteLine("Presione ENTER para confimar borrado, o cualquier tecla para cancelar");
                System.Console.WriteLine();
                ConsoleKeyInfo confirm;
                confirm = System.Console.ReadKey();

                if (confirm.Key == ConsoleKey.Enter)
                {
                    usr.State = BusinessEntity.States.Deleted;
                    System.Console.WriteLine("Usurario {0} borrado", usr.ID);
                    UsuarioNegocio.Delete(ID);
                }
            }
            catch (FormatException fe)
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

        private void Modificar()
        {
            try
            {
                System.Console.Clear();
                System.Console.Write("Ingrese un ID del usuario: ");
                int ID = int.Parse(System.Console.ReadLine());
                Usuario usr = UsuarioNegocio.GetOne(ID);
                System.Console.WriteLine();
                System.Console.WriteLine("\t\tUsuario: {0}", usr.ID);
                System.Console.WriteLine("\t\tIngrese el nuevo nombre: ");
                SendKeys.SendWait(usr.Nombre);
                usr.Nombre = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese el nuevo apellido: ");
                SendKeys.SendWait(usr.Apellido);
                usr.Apellido = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese el nuevo nombre de usuario: ");
                SendKeys.SendWait(usr.NombreUsuario);
                usr.NombreUsuario = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese el nuevo clave: ");
                SendKeys.SendWait(usr.Clave);
                usr.Clave = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese el nuevo email: ");
                SendKeys.SendWait(usr.EMail);
                usr.EMail = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese el nuevo estado (1- Habilitado / 0- No habilitado): ");
                if (usr.Habilitado)
                { SendKeys.SendWait("1"); }
                else
                { SendKeys.SendWait("0"); }
                usr.Habilitado = (Console.ReadLine() == "1");
                System.Console.WriteLine();
                System.Console.WriteLine("Presione una tecla para confirmar modificaciones");
                System.Console.ReadKey();
                usr.State = BusinessEntity.States.Modified;
                System.Console.WriteLine("Usurario {0} modificado", usr.ID);

                UsuarioNegocio.Save(usr);
            }
            catch (FormatException fe)
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

        private void Agregar()
        {
            try
            {
                Usuario usr = new Usuario();

                System.Console.Clear();
                System.Console.WriteLine("\t\tIngrese Nombre: ");
                usr.Nombre = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese Apellido: ");
                usr.Apellido = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese Usuario: ");
                usr.NombreUsuario = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese Clave: ");
                usr.Clave = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese Email: ");
                usr.EMail = Console.ReadLine();
                System.Console.WriteLine("\t\tIngrese Estado (1- Habilitado / 0- No habilitado): ");
                usr.Habilitado = (Console.ReadLine() == "1");
                System.Console.WriteLine();
                usr.State = BusinessEntity.States.New;
                UsuarioNegocio.Save(usr);
                System.Console.WriteLine("Usurario {0} agregado", usr.ID);

;
            }
            catch (FormatException fe)
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

        private void Consultar()
        {
            try
            {
                System.Console.Clear();
                System.Console.Write("Ingrese un ID del usuario: ");
                int ID = int.Parse(System.Console.ReadLine());
                this.MostrarDatos(UsuarioNegocio.GetOne(ID));
                System.Console.WriteLine();

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
