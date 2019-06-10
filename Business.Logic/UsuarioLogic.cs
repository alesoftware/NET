using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class UsuarioLogic:BusinessLogic
    {
        private Data.Database.UsuarioAdapter _usuarioData = new UsuarioAdapter();
        public Data.Database.UsuarioAdapter UsuarioData
        {
            get { return _usuarioData; }
            set { _usuarioData = value; }
        }

        public Business.Entities.Usuario GetOne(int ID)
        {
            return UsuarioData.GetOne(ID);
        }

        public List<Usuario> GetAll()
        {
            return UsuarioData.GetAll();
        }

        public void Save(Business.Entities.Usuario Usuario)
        {
            UsuarioData.Save(Usuario);
        }

        public void Delete(int ID)
        {
            UsuarioData.Delete(ID);
        }
    }
}
