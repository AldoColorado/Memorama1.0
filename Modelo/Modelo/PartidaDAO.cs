using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class PartidaDAO : AbstractCRUD<Partida>
    {
        public override bool Crear(Partida entity)
        {
            bool creado = false;

            try
            {
                db.Partida.Add(entity);
                db.SaveChanges();
                creado = true;
            }
            catch(System.InvalidOperationException ex)
            {
                creado = false;
                Console.WriteLine(ex.GetBaseException());
            }

            return creado;

        }

        public bool BuscarPartida(string codigo)
        {
            bool partidaEncontrada = false;

            try
            {

                Partida buscar = db.Partida.Where(q => q.codigo.Equals(codigo)).FirstOrDefault();
                if(buscar != null)
                {
                    partidaEncontrada = true;
                }
                else
                {
                    partidaEncontrada = false;
                }


            }
            catch(System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.GetBaseException());
                partidaEncontrada = false;
            }


            return partidaEncontrada;

        }

        public override void Eliminar(object pk)
        {
            throw new NotImplementedException();
        }

        public override void Modificar(object entity)
        {
            throw new NotImplementedException();
        }

        public override List<Partida> Obtener()
        {
            throw new NotImplementedException();
        }

        public override Partida ObtenerEntidad(object pk)
        {
            throw new NotImplementedException();
        }
    }
}
