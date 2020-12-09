using Modelo;
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMemorama
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculatorService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public partial class ServicioMemorama : ILoginService
    {
        private ILoginServiceCallback callBack = null;
        private ObservableCollection<Jugador> jugadores;
        private readonly Dictionary<string, ILoginServiceCallback> clientes;

        public ServicioMemorama()
        {
            jugadores = new ObservableCollection<Jugador>();
            clientes = new Dictionary<string, ILoginServiceCallback>();

        }


        public bool Login(string nombre, string contrasenia)
        {
            bool seLogeo = false;
            bool usuarioLogueado = false;
            JugadorDAO usuarioALoguear = new JugadorDAO();

            string contraseniaEncriptada = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(contrasenia);
            contraseniaEncriptada = Convert.ToBase64String(encryted);

            usuarioLogueado = usuarioALoguear.ValidarUsuario(nombre, contraseniaEncriptada);

            if(usuarioLogueado)
            {
                Console.WriteLine("Usuario Logeado");
                Console.WriteLine("Conectando");
                //var conexion = OperationContext.Current.GetCallbackChannel<IClient>();
                //conexion.VerificarUsuarioLogeado(usuarioLogueado);
                //OperationContext.Current.GetCallbackChannel<IClient>().VerificarUsuarioLogeado(usuarioLogueado);
                seLogeo = true;
                Console.WriteLine("Paso de conectando lol");
            }
            else
            {
                Console.WriteLine("No paso Logeado");
            }

            return seLogeo;
        }

        public void Conectarse(Jugador jugador)
        {
            callBack = OperationContext.Current.GetCallbackChannel<ILoginServiceCallback>();

            if(callBack != null)
            {
                clientes.Add((jugador.idJugador).ToString(), callBack);
                jugadores.Add(jugador);
                clientes?.ToList().ForEach(c => c.Value.UsuariosConectados(jugadores));
                Console.WriteLine($"{jugador.nickName} se ha conectado");
            }
        }
    }

    public partial class ServicioMemorama : IRegistroService
    {

        public void CrearJugador(Jugador jugador)
        {

            bool creado = false;
            JugadorDAO jugadorDAO = new JugadorDAO();
            creado = jugadorDAO.Crear(jugador);

            if(creado)
            {
                var conexion = OperationContext.Current.GetCallbackChannel<IRegistroServiceCallback>();
                conexion.VerificarCreacionJugador(true);
                Console.WriteLine("Jugador creado");
            }
            else
            {
                Console.WriteLine("No se creo");
            }
        }

        public void EnviarCorreoRegistro(string correo, string codigoDeRegistro)
        {
            bool correoEnviado;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.To.Add(correo);
            mail.Subject = "Confirmación de registro en Memorama";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Tu codigo de confirmación es:" + codigoDeRegistro;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.From = new System.Net.Mail.MailAddress("aldocoloradocd@gmail.com");

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            cliente.Credentials = new System.Net.NetworkCredential("aldocoloradocd@gmail.com", "LesCactus27");

            cliente.Port = 587;
            cliente.EnableSsl = true;

            cliente.Host = "smtp.gmail.com"; //"mail.dominio.com"

            try
            {
                cliente.Send(mail);
                correoEnviado = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
                correoEnviado = false;
            }

            if(correoEnviado)
            {
                var conexion = OperationContext.Current.GetCallbackChannel<IRegistroServiceCallback>();
                conexion.VerificarEnvioDeCorreo(true);

            }
            else
            {
                Console.WriteLine("No se pudo enviar el correo");
            }
        }
    }
}
