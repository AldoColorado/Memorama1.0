
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Login : ProxyLogin.ILoginServiceCallback
    {
        public bool aceptado = false;
        public Login()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonIngresar(object sender, RoutedEventArgs e)
        {
            bool logeado = false;
            Jugador jugador = new Jugador();

            jugador.nickName = TextoNickName.Text;
            jugador.contrasenia = TextoPassword.Password;

            InstanceContext contexto = new InstanceContext(this);
            ProxyLogin.LoginServiceClient servidor = new ProxyLogin.LoginServiceClient(contexto);
            

            logeado = servidor.Login(TextoNickName.Text, TextoPassword.Password);
            System.Threading.Thread.Sleep(new TimeSpan(0, 0, 10));

            if(logeado)
            {
                servidor?.Conectarse(jugador);
                Lobby ventanaLobby = new Lobby();
                Window.GetWindow(this).Close();
                ventanaLobby.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta");
                Application.Current.Shutdown();
            }
        }

        private void BotonRegistrarse(object sender, RoutedEventArgs e)
        {
            Registrarse ventanaRegistrase = new Registrarse();
            Window.GetWindow(this).Close();
            ventanaRegistrase.Show();

        }

        private void BotonRecuperarContrasenia(object sender, RoutedEventArgs e)
        {
            RecuperarContrasenia ventanaRecuperarContrasenia = new RecuperarContrasenia();
            Window.GetWindow(this).Close();
            ventanaRecuperarContrasenia.Show();
        }

        public void UsuariosConectados(Jugador[] jugadores)
        {
            
        }

        public void VerificarUsuarioLogeado(bool logeado)
        {
            this.aceptado = logeado;
        }
    }
}
