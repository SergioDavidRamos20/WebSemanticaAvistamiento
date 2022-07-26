using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entidades;

namespace LogicaNegocio
{
  public class TodoLogicaNegocio
    {
        private TodoDAL t = new TodoDAL();
        private ClaseDAL c = new ClaseDAL();
        private SubClaseDAL s = new SubClaseDAL();
        private GeneralDal g = new GeneralDal();
        public List<TodoEntidad> Listar(string buscar)
        {
            return t.Listar(buscar);
        }
        public List<TodoEntidad> ListarClase(string buscar)
        {
            return c.Listar(buscar);
        }
        public List<TodoEntidad> ListarSubClase(string buscar)
        {
            return s.Listar(buscar);
        }
        public List<GeneralEntidad> ListarGeneral(string buscar)
        {
            return g.Listar(buscar);
        }
    }
}
