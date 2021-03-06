﻿using Memorama.ProxyLogin;
using Memorama.Vista;
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para Lobby.xaml
    /// </summary>
    public partial class Lobby :ProxyLogin.ILoginServiceCallback
    {
        InstanceContext contexto;
        ProxyLogin.LoginServiceClient servidor;

        private ObservableCollection<Jugador> jugadoresConectados;
        Dictionary<Jugador, ILoginServiceCallback> clientes = new Dictionary<Jugador, ILoginServiceCallback>();
        Jugador jugador = new Jugador();

        public Lobby(ObservableCollection<Jugador> jugadores, Jugador jugador)
        {
            this.jugador = jugador;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            //jugadoresConectados = new ObservableCollection<Jugador>(); Creo que no se usa
            TxtJugador.Text = jugador.nickName;

            jugadoresEnLinea.Items.Clear();
            jugadoresEnLinea.ItemsSource = jugadores;

            contexto = new InstanceContext(this);
            servidor = new ProxyLogin.LoginServiceClient(contexto);      
        }

        

        public void UsuariosConectados(Jugador[] jugadores)
        {
            jugadoresConectados.Clear();

            foreach(Jugador c in jugadores)
            {
                jugadoresConectados.Add(c);
            }
        }


        public void VerificarUsuarioLogeado(bool logeado)
        {
            throw new NotImplementedException();
        }

        private void BotonCrearPartida(object sender, System.Windows.RoutedEventArgs e)
        {
            CrearPartida ventanaConfirmarRegistro = new CrearPartida(jugador);
            ventanaConfirmarRegistro.Show();
            Window.GetWindow(this).Close();
        }

        private void BotonUnirseAPartida(object sender, System.Windows.RoutedEventArgs e)
        {
            UnirseAPartida ventanaUnirseAPartida = new UnirseAPartida(jugador);
            ventanaUnirseAPartida.Show();
            Window.GetWindow(this).Close();
        }

        private void BotonSalir(object sender, RoutedEventArgs e)
        {
            servidor.Desconectarse(jugador);
            Window.GetWindow(this).Close();
        }

        
    }
}
