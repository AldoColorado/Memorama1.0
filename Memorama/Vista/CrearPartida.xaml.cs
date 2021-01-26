using Modelo.Modelo;
using System;
using System.Collections.Generic;
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

namespace Memorama.Vista
{
    /// <summary>
    /// Lógica de interacción para CrearPartida.xaml
    /// </summary>
    public partial class CrearPartida : ProxyPartida.IPartidaServiceCallback
    {
        Partida partida;
        InstanceContext contexto;
        ProxyPartida.PartidaServiceClient servidor;
        Jugador jugador;
        string codigoPartida;

        public CrearPartida(Jugador jugador)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.jugador = jugador;

            partida = new Partida();

            contexto = new InstanceContext(this);
            servidor = new ProxyPartida.PartidaServiceClient(contexto);

        }

        private void BotonCrearPartida(object sender, RoutedEventArgs e)
        {
            string codigoConTexto = "CODIGO: ";
            
            codigoPartida= servidor.GenerarCodigo();
            TxtCodigo.Text = codigoConTexto + codigoPartida;

            partida.codigo = codigoPartida;   
        }

        private void BotonIniciarPartida(object sender, RoutedEventArgs e)
        {
            bool creada = false;
            if(partida.codigo != null )
            {
                creada = servidor.CrearPartida(partida, jugador);
            }
            else
            {
                MessageBox.Show("Debes generar el codigo de partida para continuar");
                return;
            }

            if(!creada)
            {
                MessageBox.Show("ERROR: El servidor no esta disponible, intente de nuevo más tarde");
            }

            PrePartida ventanaPrePartida = new PrePartida(jugador, codigoPartida);
            ventanaPrePartida.Show();
            Window.GetWindow(this).Close();
        }

        public void JugadoresEnPartida(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void OrdenCartas(int[] numeros)
        {
            throw new NotImplementedException();
        }
    }
}
