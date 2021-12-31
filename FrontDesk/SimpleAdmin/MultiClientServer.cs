using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Collections;  //Hashtable
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace SimpleAdmin
{
    delegate void ProcDataCBack(int ID, string msg);  //Parecido a los apuntadores de función de C++

    class MultiClientServer
    {
        Hashtable clientsList;  // = new Hashtable();
        TcpListener serverSocket;
        TcpClient clientSocket;
        int counter;  //Ctd de usuarios conectados, lo dejo por ahora aunque actualmente no lo estoy usando
        Thread ListeningTask;
        MainViewFrm MainWindow;
        ProcDataCBack ProcessMsg;
        //
        bool serverAlive;
        object MyLocker;

        public void AddWindows(MainViewFrm W)
        {
            MainWindow = W;
            //MainWindow.Text = "I am listening for connections on " + ((IPEndPoint)serverSocket.LocalEndpoint).Address.ToString() +" on port number " + ((IPEndPoint)serverSocket.LocalEndpoint).Port.ToString();
            MainWindow.Text = GetLocalIPAddress();
        }

        //Constructor:
        public MultiClientServer(ProcDataCBack funcProcData)
        {
            ProcessMsg = funcProcData;
            serverAlive = true;
            MyLocker = new object();
            //
            clientsList = new Hashtable();  //Se podria haber hecho en la misma declaración, no necesariamente en el constructor
            serverSocket = new TcpListener(8888);
            //serverSocket = new TcpListener(8500); //El puerto que usé las pruebas del Score con la interfaz
            clientSocket = default(TcpClient);
            counter = 0;
            //Las siguientes lineas se podrian pasar a un método Start()
            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");
            //Thread ListeningTask = new Thread(Listening);
            ListeningTask = new Thread(Listening);
            ListeningTask.Start();
            //serverSocket.LocalEndpoint
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        /*  En esta variante de Listening, se espera que acto seguido de conectarse, el cliente se identifique,
         *  habria que luego de llevarlo a la practica, ver si se queda asi, o se permite al cliente identificarse 
         *  mas adelante, estoy trabajando con snipets de codigos, metiendo mano por aqui y por alla, por lo que por
         *  ahora lo dejo asi
         *  Creo que es buena idea modificarlo en algun momento...
         */
        void Listening()
        {
            //while (true)
            //bool guardian;

            //Lo estuve probando con lock y funciona bien, sin embargo creo que realmente no es necesario
            //Sirvio como ejercicio, y dejo el codigo para futuras referencias, y por si finalmente tengo que volver a el
            /*
            lock(MyLocker)
            {
                guardian = serverAlive;
            }
            */
            while (serverAlive)
            //while (guardian)
            {
                try
                {
                    counter += 1;
                    clientSocket = serverSocket.AcceptTcpClient();
                    //MainWindow.Text = ((IPEndPoint)serverSocket.LocalEndpoint).Address.ToString() + " on port number " + ((IPEndPoint)serverSocket.LocalEndpoint).Port.ToString();
                    byte[] bytesFrom = new byte[30];
                    string dataFromClient = null;

                    NetworkStream networkStream = clientSocket.GetStream();

                    /*
                    //Dando la bienvenida:
                    Byte[] senderBuffer = Encoding.ASCII.GetBytes("Welcome$");
                    networkStream.Write(senderBuffer, 0, senderBuffer.Length);
                    networkStream.Flush();
                    */
                    
                    //Esperando porque la pista se registre //Esta parte es un punto flaco, porque y sino se registra
                    //...sino se registra se queda bloqueado y no acepta más conexiones (se debe mejorar esto en algún momento)
                    networkStream.Read(bytesFrom, 0, 30);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(dataFromClient + " has been registered ");

                    //handleClient client = new handleClient(clientSocket, dataFromClient, clientsList);
                    handleClient client = new handleClient(clientSocket, dataFromClient, clientsList, ProcessMsg, MyLocker);
                    //Aqui se pudiera comprobar si ya existe la clave, y si es asi borrarla antes de insertarla de nuevo,
                    //porque sino lanza una excepcion, por ahora no me voy a preocupar por eso
                    lock (MyLocker)  //terminé usando el locker aqui y no donde habia pensado al ppo, aqui lo estoy usando para otra cosa
                    {
                        clientsList.Add(dataFromClient, client);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //break;  //No voy a salir del Listening, al menos no por todos los tipos de excepciones 
                }
                /*
                lock (MyLocker)  //Ver acomentarios arriba
                {
                    guardian = serverAlive;
                }
                */
            }
        }//void Listening

        /*Este será el método para enviarle un determinado msj o comando, a una pista dada, por el servidor*/
        //Creo que aqui, al menos por ahora, no hace falta el locker
        public bool sendCmnd(string laneID, string cmnd)
        {
            if (clientsList.Contains(laneID))
            {
                handleClient client = (handleClient)clientsList[laneID];
                client.sendMsg(cmnd);
                return true;
            }
            else
                return false;
        }

        /*  //Por ahora no se esta ejecutando nunca
        ~MyServer()
        {
            serverSocket.Stop();
            ListeningTask.Abort();
        }
        */

        public void Stop()
        {
            //Podria iterar sobre clienList y parar todas las conexiones con los clientes:
            //Fue necesario hacerlo despues de todo, para cerrar los hilos que atienden a los clientes
            //foreach (KeyValuePair<string, handleClient> kvp in clientsList)  //creo que es DictionaryEntry se podria evitar con var...
            lock (MyLocker)
            {
                foreach (DictionaryEntry Item in clientsList)  //Por aqui lanza una excepcion, cdo se cierra el programa, pero solo algunas veces ??????
                {                                              //Creo que es mejor ponerle un lock al diccionario, por si las moscas
                    handleClient clientToFinish;
                    clientToFinish = (handleClient)Item.Value;
                    clientToFinish.Disconect();
                }
            }
            serverSocket.Stop();  //me lanza una excepción, pero la capturo y se muestra en consola, mas nada que hacer por ahora
            //ListeningTask.Abort(); //Al llegar aqui si el socket se paro, el hilo termino y ya ListeningTask es null
            //Bueno si al atrapar la excepcion se salia con el break;

            if(ListeningTask != null)  //No se porque al llegar aqui ya ListeningTask tiene null, al parecer como es llamado desde el evento closing de la forma ppal sucede esto, que ya se libero el recurso, idK
                if (ListeningTask.IsAlive)
                    ListeningTask.Abort();
            /*
            lock (MyLocker)  //Ver comentarios arrriba en  void Listening()
            {
                serverAlive = false;
            }
            */
            serverAlive = false;
            //Thread.Sleep(1000);
            //serverSocket.Stop();
        }


        //--------------------------------------------------------
        public class handleClient
        {
            TcpClient clientSocket;
            string LaneID;
            Hashtable clientsList;
            NetworkStream networkStream;
            //const int BufferSize = 10025;  //Estaba usando 30
            const int BufferSize = 2048;  //Creo q pro ahora con eso es mas que suficeinte
            ProcDataCBack ProcessMsg;
            object clientListLocker;

            /*
            public void startClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
            {
                this.clientSocket = inClientSocket;
                this.clNo = clineNo;
                this.clientsList = cList;
                Thread ctThread = new Thread(doChat);
                ctThread.Start();
            }
            */  //la convertí en el constructor
            //Ahora en el constructor se manda un msj al servidor para que informe que esta linea esta conectada
            public handleClient(TcpClient inClientSocket, string LaneID, Hashtable cList, ProcDataCBack func, object LockerFromServer)
            {
                ProcessMsg = func;
                //
                this.clientSocket = inClientSocket;
                this.LaneID = LaneID;
                this.clientsList = cList;
                //
                clientListLocker = LockerFromServer;
                //
                ProcessMsg(LaneIDtoInt(), "conected");
                //
                networkStream = clientSocket.GetStream();

                //new: //tratando de capturar de unpluged event (desconexion del cable de red)
                //clientSocket.IOControl()  //Estaba buscando KeepAlive o algo asi, pero no lo encuentro
                //networkStream.ReadTimeout = 3000; //3000 ms -> 3 seg 
                networkStream.ReadTimeout = 60000;  //Aumentando el TimeOut al menos hasta que encuentre una forma de que se sincronicen, cdo se pierdan los msjs
                //Ya creo con esto logro que se entere si la conexion sigue viva o no
                //
                Thread ctThread = new Thread(doChat);
                ctThread.Start();
            }

            public void sendMsg(string message)
            {
                //Byte[] senderBuffer = Encoding.ASCII.GetBytes(message + "$");
                Byte[] senderBuffer = Encoding.ASCII.GetBytes(message + "$");
                //networkStream = clientSocket.GetStream();
                networkStream.Write(senderBuffer, 0, senderBuffer.Length);
                networkStream.Flush();
            }

            private void doChat()
            {
                byte[] bytesFrom = new byte[BufferSize];
                string dataFromClient = null;

                while ((true))
                {
                    try
                    {
                        if (clientSocket.Connected)
                        {
                            //NetworkStream networkStream = clientSocket.GetStream(); //Se obtiene en el constructor
                            if (networkStream.CanRead)
                            {
                                networkStream.Read(bytesFrom, 0, BufferSize);
                                //networkStream.Flush();  //Aqui da error
                                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));  //especie de protocolo
                                Console.WriteLine("From Client - " + LaneID + " : " + dataFromClient);         //sino se usa y se utiiza lanza una excepcion
                                //llegarn datos, o un msj...., Please procésalos!!!
                                //ProcessMsg(LaneIDtoInt(), "");
                                ProcessMsg(LaneIDtoInt(), dataFromClient);
                                //--------------------------
                                /* Lo siguiente fue la forma o el mecanismo que encontré para que el servidor 
                                 * reaccione rápidamente cuando la conexión se cierra por parte del cliente.
                                 * Por otro lado cada vez que el cliente recibe un msj que dice "testing"
                                 * simplemente lo subestima y ya.
                                 * (Esto es verdad, y funciona cdo cierro el programa del cliente, pero cdo desconecto el cable de red no sucede)
                                 */
                                //Byte[] senderBuffer = Encoding.ASCII.GetBytes("testing$"); //Cambiar en un futuro por un msj más corto
                                Byte[] senderBuffer = Encoding.ASCII.GetBytes("A$"); //A //Acknowledege
                                //Podria servirle al cliente para saber si la com se ha mantenido viva

                                networkStream.Write(senderBuffer, 0, senderBuffer.Length);
                                networkStream.Flush();
                            }
                        }
                        else
                            break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        //Aqui a diferencia del servidor decidí salir del loop while, por lo que el hilo terminará
                        break;
                    }
                }//end while
                 //finishing:
                clientSocket.Close();
                lock(clientListLocker)
                {
                    clientsList.Remove(LaneID);
                }
                Console.WriteLine("Perdida la conexión con " + LaneID);
                ProcessMsg(LaneIDtoInt(), "disconected");
            }//end doChat
            //
            int LaneIDtoInt()
            {
                //int pos = LaneID.IndexOf("lane");
                int pos = LaneID.IndexOf("e");
                int result = 0;

                //if(int.TryParse(LaneID.Substring(pos), out result))
                //if(int.TryParse(LaneID.Remove(0, pos), out result))
                if (int.TryParse(LaneID.Remove(0, pos + 1), out result))
                    //return result;
                    return result - 1;
                else
                    return - 1;
            }
            //
            public void Disconect()
            {
                if(clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
            //
            /* //Lo escribi yo, pero por ahora no lo voy a usar
            ~handleClient()
            {
                clientSocket.Close();
                clientsList.Remove(LaneID);
            }
            */
        } //end class handleClinet


    }//end of MultiClientServer
}
