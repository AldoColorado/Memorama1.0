using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class JugadorDAO : AbstractCRUD<Jugador>
    {
       

        public override bool Crear(Jugador entity)
        {
            bool creado = false;

            try
            {
                db.Jugador.Add(entity);
                db.SaveChanges();
                creado = true;
            }
            catch (System.InvalidOperationException ex)
            {
                creado = false;
                Console.WriteLine(ex.GetBaseException());
            }

            return creado;
        }

        public bool ValidarUsuario(string nickName, string contrasenia)
        {
            bool usuarioValido = false;

            try
            {
                
                Jugador buscar = db.Jugador.Where(q => q.nickName.Equals(nickName) && q.contrasenia.Equals(contrasenia)).FirstOrDefault();
                if(buscar != null)
                {
                    usuarioValido = true;
                }
                else
                {
                    usuarioValido = false;
                }
                

            }
            catch(System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.GetBaseException());
                usuarioValido = false;
            }
            

            return usuarioValido;

        }

        public override void Eliminar(object pk)
        {
            Jugador buscar = db.Jugador.Where(q => q.idJugador == (int)pk).First<Jugador>();

            if(buscar == null)
            {
                throw new ArgumentException("Usuario no encontrado en la base de datos");

            }
            else
            {
                db.Jugador.Remove(buscar);
                db.SaveChanges();
            }
        }

        

        public override List<Jugador> Obtener()
        {
            return db.Jugador.ToList<Jugador>();
        }

        public override Jugador ObtenerEntidad(object pk)
        {
            return db.Jugador.Where(q => q.idJugador == (int)pk).First<Jugador>();   
        }

        public override void Modificar(object entity)
        {
            throw new NotImplementedException();
        }

        public bool ValidarJugadorPorCorreo(string correo)
        {
            bool encontrado = false;
            try
            {
                Jugador buscar = db.Jugador.Where(q => q.correoElectronico.Equals(correo)).FirstOrDefault();
                if (buscar != null)
                {
                    encontrado = true;
                }
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }
            return encontrado;
        }

        public Jugador ObtenerJugadorPorCorreo(string correo)
        {
            try
            {
                Jugador buscar = db.Jugador.Where(q => q.correoElectronico.Equals(correo)).FirstOrDefault();
                return buscar;
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }
            return null;
        }

        public bool ActualizarContrasenia(string contrasenia, string nickname)
        {
            Jugador buscar = db.Jugador.Where(q => q.nickName.Equals(nickname)).FirstOrDefault();
            if (buscar != null)
            {
                buscar.contrasenia = contrasenia;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
