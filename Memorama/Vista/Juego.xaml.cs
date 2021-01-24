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

namespace Memorama.Vista
{
    /// <summary>
    /// Lógica de interacción para Juego.xaml
    /// </summary>
    public partial class Juego : ProxyJuego.IJuegoServiceCallback
    {
        JuegoMemorama juego = new JuegoMemorama();

        Canvas canvas2;
        InstanceContext contexto;
        Jugador jugador;
        int puntaje = 0;
        ProxyJuego.JuegoServiceClient servidor;
        ObservableCollection<int> numeros = new ObservableCollection<int>();
        ObservableCollection<int> puntajesJugadores;
        
        private ObservableCollection<Jugador> jugadoresJuego;
        bool llego = false;

        public Juego(JuegoMemorama juego, Jugador jugador )
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            contexto = new InstanceContext(this);
            servidor = new ProxyJuego.JuegoServiceClient(contexto);
            jugadoresJuego = new ObservableCollection<Jugador>();
            puntajesJugadores = new ObservableCollection<int>();
            this.jugador = jugador;
            this.juego = juego;

            TxtNombreJugador.Text = jugador.nickName;
            servidor.ConectarseJuego(jugador);
            servidor.InicializarPuntajes(jugador, 0);

            jugadoresEnJuego.Items.Clear();
            jugadoresEnJuego.ItemsSource = jugadoresJuego;

            puntajes.Items.Clear();
            puntajes.ItemsSource = puntajesJugadores;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = juego;
            NuevoJuego(); 
        }

        private void NuevoJuego()
        {
            try
            {
                juego.NuevoJuego();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void Canvas2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool resultadoCartasVolteadas = false;
            
            try
            {
                resultadoCartasVolteadas = juego.Click(e.GetPosition(canvas2).X, e.GetPosition(canvas2).Y);
                servidor.MovimientoDeJugador(e.GetPosition(canvas2).X, e.GetPosition(canvas2).Y);
                if(resultadoCartasVolteadas)
                {
                    puntaje++;
                    servidor.ModificarPuntajes(jugador, puntaje);
                }    
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());  
                MessageBox.Show(ex.ToString());
                throw; 
            }         
        }

        public void MostrarMovimiento(double x, double y)
        {
            juego.Click(x, y);
        }

        public void OrdenCartas(int[] numeros)
        {
            foreach(int n in numeros)
            {
                this.numeros.Add(n);
            }
        }

        public void JugadoresEnJuego(Jugador[] jugadores)
        {
            jugadoresJuego.Clear();

            foreach(Jugador c in jugadores)
            {
                jugadoresJuego.Add(c);
            }
        }

        public void ActualizarPuntajes(int[] puntajes)
        {
            puntajesJugadores.Clear();

            foreach(var p in puntajes)
            {
                puntajesJugadores.Add(p);
            }
        }
    }
}
