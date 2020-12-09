using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para Lobby.xaml
    /// </summary>
    public partial class Lobby //Proxy.IServiceCallback
    {
        private ObservableCollection<Jugador> jugadoresConectados;

        public Lobby()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            jugadoresConectados = new ObservableCollection<Jugador>();
            InstanceContext contexto = new InstanceContext(this);
            //Proxy.ServiceClient servidor = new Proxy.ServiceClient(contexto);

            //jugadoresConectados = servidor.GetUsuariosConectados();

            listaJugadores.ItemsSource = jugadoresConectados;
        }

        public void RecibirMensaje(string mensaje)
        {
            throw new NotImplementedException();  
        }

        public void UsuariosConectados(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void VerificarCreacionJugador(bool creado)
        {
            throw new NotImplementedException();
        }

        public void VerificarEnvioDeCorreo(bool enviado)
        {
            throw new NotImplementedException();
        }

        public void VerificarEnvioDeCorreoRecuperarContrasenia(bool enviado)
        {
            throw new NotImplementedException();
        }

        public void VerificarUsuarioLogeado(bool logeado)
        {
            throw new NotImplementedException();
        }
    }
}
