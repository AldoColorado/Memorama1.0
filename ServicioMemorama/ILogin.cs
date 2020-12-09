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
    [ServiceContract(CallbackContract = typeof(ILoginServiceCallback))]
    public interface ILoginService
    {
        [OperationContract]
        bool Login(String nombre, string contrasenia);

        [OperationContract(IsOneWay = true)]
        void Conectarse(Jugador jugador);
    }

    [ServiceContract]
    public interface ILoginServiceCallback
    {
        [OperationContract]
        void VerificarUsuarioLogeado(bool logeado);

        [OperationContract(IsOneWay = true)]
        void UsuariosConectados(ObservableCollection<Jugador> jugadores);
    }
}
