using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;
using Util;


namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        public UsuarioDesktop()
        {
            InitializeComponent();
        }

        public Business.Entities.Usuario UsuarioActual { get; set; }

        public UsuarioDesktop(ModoForm modo) : this()
        {
            Modo = ModoForm.Alta;
        }

        public UsuarioDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            Business.Logic.UsuarioLogic ul = new UsuarioLogic();
            UsuarioActual = ul.GetOne(ID);
            MapearDeDatos();
        }

        public virtual void MapearDeDatos()
        {
            this.txtID.Text = this.UsuarioActual.ID.ToString();
            this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
            this.txtNombre.Text = this.UsuarioActual.Nombre;
            this.txtApellido.Text = this.UsuarioActual.Apellido;
            this.txtEmail.Text = this.UsuarioActual.EMail;
            this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
            switch (Modo)
            {
                case ModoForm.Alta:

                case ModoForm.Modificacion:
                    {
                        this.btnAceptar.Text = "Guardar";
                        break;
                    }
                case ModoForm.Baja:
                    {
                        this.btnAceptar.Text = "Eliminar";
                        this.txtClave.Visible = false;
                        this.txtConfirmarClave.Visible = false;
                        this.lbClave.Visible = false;
                        this.lbConfirmarClave.Visible = false;

                        txtApellido.Enabled = false;
                        txtEmail.Enabled = false;
                        txtNombre.Enabled = false;
                        txtUsuario.Enabled = false;
                        chkHabilitado.Enabled = false;
                        txtID.Enabled = false;

                        break;
                    }
                case ModoForm.Consulta:
                    {
                        this.btnAceptar.Text = "Aceptar";
                        break;
                    }
            }
        }

        public virtual void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Business.Entities.Usuario u = new Usuario();
                UsuarioActual = u;
                this.UsuarioActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Modificacion)
            {
                this.UsuarioActual.ID = int.Parse(this.txtID.Text);
                this.UsuarioActual.State = BusinessEntity.States.Modified;
            }
            this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
            this.UsuarioActual.Nombre = this.txtNombre.Text;
            this.UsuarioActual.Apellido = this.txtApellido.Text;
            this.UsuarioActual.EMail = this.txtEmail.Text;
            this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
            this.UsuarioActual.Clave = this.txtClave.Text;
        }

        public virtual void GuardarCambios()
        {
            Business.Logic.UsuarioLogic ul = new UsuarioLogic();
            switch (btnAceptar.Text)
            {
                case "Aceptar":
                case "Guardar":
                    {
                        MapearADatos();
                        ul.Save(UsuarioActual);
                        break;
                    }
                case "Eliminar":
                    {
                        ul.Delete(UsuarioActual.ID);
                        break;
                    }
            }
        }

        public virtual bool Validar()
        {
            if (String.IsNullOrWhiteSpace(txtNombre.Text) || String.IsNullOrWhiteSpace(txtApellido.Text) || String.IsNullOrWhiteSpace(txtEmail.Text) || String.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                Notificar("Campo vacío", "Debe completar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (txtClave.Text != txtConfirmarClave.Text)
            {
                Notificar("Error en las claves", "Las claves no coinciden", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (txtClave.TextLength < 8)
            {
                Notificar("Error en la clave", "La clave debe poseer como mínimo 8 caracteres", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (Util.Validacion.ValidarEmail(txtEmail.Text) == false)
            {
                Notificar("Error en el Email", "Ingrese un email válido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
                return true;
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (this.btnAceptar.Text == "Eliminar")
            {
                GuardarCambios();
                this.Hide();
                //PersonasLogic pl = new PersonasLogic();
                //DocenteCursoLogic doc = new DocenteCursoLogic();
                //AlumnoInscripcionLogic a = new AlumnoInscripcionLogic();
                //a.Delete2(UsuarioActual.ID);
                //doc.Delete(UsuarioActual.ID);
                //pl.Delete(UsuarioActual.ID);

                MessageBox.Show("Usuario Eliminado", "Información", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                Close();
            }
            if (this.btnAceptar.Text == "Aceptar")
            {
                if (Validar())
                {
                    GuardarCambios();
                    PersonasDesktop formPersona = new PersonasDesktop(UsuarioActual.ID, PersonasDesktop.ModoForm.Alta);
                    this.Hide();
                    formPersona.ShowDialog();
                    MessageBox.Show("Usuario Agregado", "Información", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    Close();
                }
            }
            if (this.btnAceptar.Text == "Guardar")
                if (Validar())
                {
                    GuardarCambios();
                    UI.Desktop.PersonasDesktop formPersona = new PersonasDesktop(UsuarioActual.ID, PersonasDesktop.ModoForm.Modificación);
                    this.Hide();
                    formPersona.ShowDialog();
                    MessageBox.Show("Usuario Modificado", "Información", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    Close();
                }
        }
    }
}
