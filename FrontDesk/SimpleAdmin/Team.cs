using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdmin
{
    //class Team
    public class Team
    {
        public const int MAXPLYS = 12;
        //string Nombre;
        string Nombre  = "";  //Parece que sino se inicializa se queda en null
        Player[] players;
        public int[] erasedPlys; //Jugadores eliminados //público para que pueda ser accedido desde BDManager, arreglar más adelante
        //int delPlayers = 0;
        int delIDX = 0;

        int QtyPlayers = 0;
        private HashSet<string> stringSet = new HashSet<string>();  //solo para comprobar nombres duplicados
        public int Qty { get { return QtyPlayers; } }
        //
        //la tenia como una propiedad auto, pero tuve que crearla sino me daba error de TeamName null, parece que no instancia hasta que no se usa ??? ya no estoy seguro
        public string TeamName
        {
            get
            {
                return Nombre;
            }
            set
            {
                Nombre = value;
            }
        }
        //
        public int BDCurrGameID
        {
            get;
            set;
        }
        public int BDCurrTeamID
        {
            get;
            set;
        }
        //Métodos:
        //Constructor:
        public Team()
        {
            players = new Player[MAXPLYS];
            stringSet = new HashSet<string>();
            //
            erasedPlys = new int[MAXPLYS];
        }
        //Otros
        public int AddPlayer(Player newPlayer)
        {
            if (QtyPlayers < MAXPLYS)
            {
                if (stringSet.Contains(newPlayer.Name))
                    return -1;  //No se puede añadir un jugador con el mismo nombre de uno que ya existe en el equipo
                else
                {//entonces añadir jugador
                    players[QtyPlayers] = newPlayer;
                    //int debug_BDid = players[QtyPlayers].BDplyID;
                    stringSet.Add(newPlayer.Name);
                    QtyPlayers++;
                    return 1;
                }
            }
            else
                return 0;  //Se alcanzó el máximo número de jugadores
        }
        //
        public Player this[int index]
        {
            get
            {
                if (index < QtyPlayers && index >= 0)
                    return players[index];
                else
                    return null;
            }
            set
            {
                if (index < QtyPlayers && index >= 0)
                    players[index] = value;
            }
        }
        
        //
        /*
        public void Insert(int idxPlyEdit, Player miPlayer)
        {
            this[idxPlyEdit] = miPlayer;
        }
        */
        //Como utiliza [] de la clase solo inserta dentro de idx:0 <-> idx:Qty-1
        //No sirve para añadir nuevos jugadores,o sea aumentar la cantidad de ellos
        public bool Insert(int idxPlyEdit, Player miPlayer)
        {
            //Remuevo la palabara primero, porque a lo mejor se trata de una actualizacion del juagdor
            //y se mantiene el mismo nombre, pero cambia el bumper, el hcp, etc
            stringSet.Remove(this[idxPlyEdit].Name);

            if (stringSet.Contains(miPlayer.Name))
            {
                stringSet.Add(this[idxPlyEdit].Name);
                return false;  //Ya existe un jugador con ese nombre en el equipo
            }

            this[idxPlyEdit] = miPlayer;
            stringSet.Add(miPlayer.Name);

            return true;
        }
        //
        public void Copy(Team ORG)
        {
            this.stringSet.Clear();

            //this.TeamName = string.Copy(ORG.TeamName);
            Nombre = string.Copy(ORG.TeamName);
            //this.QtyPlayers = ORG.QtyPlayers; //Noo!!! porque AddPlayer aumenta QtyPlayer...
            this.QtyPlayers = 0; //si AddPlayer lo aumenta lo ponemos en cero entonces
            for (int i = 0; i < ORG.QtyPlayers; i++)
            {
                Player P = new Player();
                P.Copy(ORG[i]);
                AddPlayer(P);  //uso AddPlayer porque también trabaja sobre stringSet
            }
            BDCurrGameID = ORG.BDCurrGameID;
            BDCurrTeamID = ORG.BDCurrTeamID;
        }
        //
        public void Clear()
        {
            stringSet.Clear();
            QtyPlayers = 0;
            Nombre = "";
        }

        //Se encargará de retornar la ctd de jugadores nuevos, o sea a los que no se le ha asignado un PlayerID correspondiente a la BD
        public int GetQnewPlayers()
        {
            int result = 0;
            for(int i = 0; i < QtyPlayers; i++)
            {
                if (players[i].BDplyID == -1)
                    result++;
            }
            return result;
        }

        public void DeletePlayer(int idx)
        {
            if(idx >= 0 && idx < QtyPlayers && QtyPlayers > 0)
            {
                stringSet.Remove(players[idx].Name);
                if(players[idx].BDplyID != -1)  //Si ya era un jugador registrado en la BD
                {
                    erasedPlys[delIDX++] = players[idx].BDplyID;
                    //delPlayers++;
                }
                for(int i = idx; i < QtyPlayers - 1; i++)
                {
                    players[i] = players[i + 1];
                }
                QtyPlayers--;
            }
        }

        //Que cantidad de jugadores fueron eliminados?
        public int GetQdelPlayers()
        {
            return delIDX;
        }

        //Metodos Traidos desde el proyecto de C++

        //Se parece al Sync() de Tteam del proyecto en C++, pero aqui no se le quita la pausa a nadie
        //Aquí voy a agregar que mande a calcular a los jugadores su máxima posición permitida para el cursor
        public void Sync()
        {
            int player_active = Qty;
            int frame = 10;  //frame: 0 <-> 9
            bool scndBall = false;
            for (int i = 0; i < Qty; i++)
            {
                //if (players[i].currentFrame < frame)
                if (players[i].currentFrame < frame && !players[i].paused)
                { //El primer jugador en el menor inning (frame) será el jugador activo, pudiera ser que más jugadores estén en ese mismo inning
                    //if (!players[i].paused)  //Ahora tambien tendrá que cumplir con que no este  pausado
                    //{
                        //Este codigo lo puse despues, a lo mejor como muchas cosas necesitaráa de refactorizarse un poco
                        //Por ahora asumo que solo hay un jugador en segunda bola, si es que hay alguno, y si ese es el caso, pues ese sera el jugador en curso, nadie compite con el, 
                        //aunque esten en innings menores que el.
                        if (!scndBall)
                        {
                            player_active = i;
                            frame = players[i].currentFrame;  //y ese será el frame (inning) donde está el equipo en general, o el juego como tal, esto del equipo fue un inventico mas
                            scndBall = !players[i].fstBall;
                        }
                    //}
                }//if (players[i].currentFrame < frame)
            }//for

            //Es que en el décimo inning el valor del frame puede ir de 9 a 11, y 12 cuando ya terminó, pero puede terminar sin llegar a 12, llega a 12 en el caso de tres chuzas seguidas X X X
            if (frame >= 9) //Si todos están en el décimo inning, el primero encontrado en tener el juego activo todavía será el jugador en curso
            {   //En el décimo inning pudiera llegar hasta 9, 10 y 11.
                for (int i = 0; i < Qty; i++)
                    if (players[i].gActive)
                    {
                        if (!players[i].paused)
                        {
                            if (!scndBall)
                            {
                                player_active = i;
                                frame = 9; //moved here from that line below...
                                scndBall = !players[i].fstBall;
                                break;
                            }
                        }
                    } // */
            }

            //players[i].TurnoON();
            if (player_active != Qty)
                players[player_active].enTurno = true;

            //Esto no estaba en el Sync() original, a lo mejor tengo que sacralo de aqui en algun momento.. IDK..
            for (int i = 0; i < Qty; i++)
                players[i].getMAXPOS();

        }//void Sync()

    }//end of class Team
}
