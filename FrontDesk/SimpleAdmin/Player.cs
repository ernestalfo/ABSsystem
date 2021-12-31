using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdmin
{
    public enum PlayerType {Normal, Blind, Pacer };
    /* - activo: esta jugando, lanzara normalmente en su turno
     * - enpausa: esta en pausa, hasta que se reanude, o llegue el final del juego.
     * - saltado: no esta jugando este juego, pero se activara automatiamente al iniciar el proximo juego
     * - noactivo: esta en el equipo pero no juega hasta que se le active manualmente, podria ser util si se estan esperando a algunas personas que estan por llegar...
     */
    public enum PlayerState {activo, enpausa, saltado, noactivo}
    //class Player
    public class Player
    {
        const int maxCH = 25;
        string nombre = "";  //tuve que hacerlo en el constructor sin párametros que le puse
        //Usare un maximo de 25 caracteres para el nombre, esto es pensando en el nombre corto
        //Cuando se vayan a usar nombres completos para registrar usuarios habituales u otras cosas, como cuentas, es otra historia...
        bool bumpers = false;
        PlayerType plyType = PlayerType.Normal;
        int Hcp = -1;
        int NoTap = 10;
        int BlindAVG = -1;
        PlayerState plyState = PlayerState.activo;

        //Properties:
        public bool Bumpers
        {
            get
            { return bumpers; }
            set
            { bumpers = value; }
        }
        //
        public PlayerType PlyType
        {
            get { return plyType; }
            set { plyType = value; }
        }
        //
        public PlayerState PlyState
        {
            get { return plyState; }
            set { plyState = value; }
        }
        //
        public string Name
        {
            get { return nombre; }
            set { nombre = value.Trim().Substring(0, maxCH); }
        }
        //
        public int BDplyID
        {
            get;
            set;
        }

        //Cantidad de tiros registrados en la BD
        public int ShotsBD
        {
            get;
            set;
        }
        public int noTap { get { return NoTap; } }

        bool newScore = false;
        public bool scoreUpdated
        {
            get { return newScore; }
        }

        //int[] boxes = new int[21];  //No confundir las casillas o boxes con EasyDisplay, aunq se guardan números negativos para señalar split y el número 14 para el Foul,
                                       //el valor del noTap por ejemplo sera el mismo numero 9=9 y asi...
                                       //Es mas bien como es salvada la informacion en la BD, valdria le pena, tratarlo de unificar con EasyDisplay en algun momento

        int[] boxes;
        int boxIDX = 0;

        public int Qboxes
        {
            get { return boxIDX; }
        }
        //-------------------------------------------------------------------------------------------------------
        //Campos provenientes del proyecto en C++, para aqui poder validar tambien la puntuación y esas cosas:
        //int[] shootsArray = new int[21];     //21 es el máximo de tiros posibles para un jugador  //Aqui quedan los puntos alcanzados en cada tiro
        int[] shootsArray;
        int shoots = 0;                  //cantidad de tiros realizados
        //int[] TotalFrames = new int[10];    //10 frames o innings, total del inning
        int[] TotalFrames;
        int Total = 0;
        //-----------------
        //relativas a CalculateFrameTotals()
        int index = 0;
        int index_frame = 0;
        bool fst = true;
        int max_reach = 0; //new in V0.3d
                           //-------------------
        //int[] easyDisplay = new int[21]; //Interfaz para luego mostrar X, /, -, y los splis facilmente, en la ultima version los no-tap también
                           //------------------
        int[] easyDisplay;
        //Relativas a  Roll(int x):
        bool ScndBall = false;
        int i_frame = 0;
        bool gameActive = true;  //nos dice si se acabó el juego o no.

        int MAXPOS = -1;

        public bool enTurno = false;
        public bool paused = false;
        public int currentFrame { get { return i_frame; } }
        public bool fstBall { get { return !ScndBall; } }
        public bool gActive { get { return gameActive; } }
        public int maxPOS { get { return MAXPOS; } }
        
        //----------------------------------------------------------------------------------------------------------
        public bool modify = false;
        //public 
        //Métodos:
        //Constructores:
        //Creo que debería lanzar una excepción si el nombre es una cadena vacía
        public Player(string Name)
        {
            //nombre = Name.Trim().Substring(0, maxCH);
            nombre = Name.Trim();
            if (nombre.Length > maxCH)
                nombre.Substring(0, maxCH - 1);

        }
        //
        public Player(string Name, bool use_bumpers)
        {
            nombre = Name.Trim();
            if (nombre.Length > maxCH)
                nombre.Substring(0, maxCH - 1);
            bumpers = use_bumpers;
            //
            BDplyID = -1;  //No ha sido asignado todavía
        }
        //
        public Player()
        {
            nombre = "";
            Hcp = -1;
            NoTap = 10;
            BlindAVG = -1;
            plyState = PlayerState.activo;
            plyType = PlayerType.Normal;
            bumpers = false;
            //
            BDplyID = -1;  //No ha sido asignado todavía
        }
        //-------------------------
        public static string GetStateString(PlayerState p)
        {
            switch (p)
            {
                case PlayerState.activo:
                    return "activo";
                    break;
                case PlayerState.enpausa:
                    return "en pausa";
                    break;
                case PlayerState.saltado:
                    return "saltado";
                    break;
                case PlayerState.noactivo:
                    return "no activo";
                    break;
                default:
                    return "no activo";
            }
        }
        //
        //
        public static bool operator ==(Player P1, Player P2)
        {
            //if ((object)P1 == (object)P2)  //Tratando de manejar los nulls
            //{
            return P1.Name == P2.Name && P1.Bumpers == P2.Bumpers;
            //}
            //else
            //return false;
        }
        //
        public static bool operator !=(Player P1, Player P2)
        {
            return !(P1 == P2);
        }
        //
        //Hay unos cuantos campos que no se copian, lo cual conviene por ahora...
        //porque queremos que permanezcan sin variar en el destino...
        //Como son: (Ahora no recuerdo, tendria que buscar)
        //Deben ser las que s emodifican en el formulario de editar los jugadores , para luego saber que jugadores hay que borrar y cuales hay que agregar
        public void Copy(Player Org)
        {
            this.nombre = string.Copy(Org.nombre);
            this.bumpers = Org.bumpers;
            this.plyType = Org.plyType;
            this.Hcp = Org.Hcp;
            this.NoTap = Org.NoTap;
            this.BlindAVG = Org.BlindAVG;
            this.plyState = Org.plyState;
            //
            this.BDplyID = Org.BDplyID;
        }

        //------------------------------------------------------------------------------------------------
        //Métodos provenientes del proyecto en C++, cualquier duda ir a ese proyecto

        //Algunos metodos tendre que irlos refinando e irlos dejando solo con la parte que se necesita aqui de este lado (en el admn / frontdesk, etc)

        public int UpdateScore_classic(int[] NewShoots, int Q)  //V0.3, ahora retorna un int
        {
            //variables locales, auxilares:
            bool split = false;
            int idxShoots = 0;
            int tiro = 0;
            bool BolaUNO = true;
            int idxFrame = 0;
            int[] Edisplay = new int[21];
            //
            boxIDX = 0;
            //-------------------------------------------------------------------
            bool no_tap = false;

            if(easyDisplay == null) //Solamente pregunto por EasyDisplay, pero si este es null todos los demas deben ser null también
            {
                easyDisplay = new int[21];
                shootsArray = new int[21];
                TotalFrames = new int[10];
                boxes = new int[21];
            }
            while (idxShoots < Q)
            {
                no_tap = false;
                tiro = NewShoots[idxShoots];
                //
                boxes[boxIDX++] = tiro;
                if (tiro < 0)
                {
                    tiro = Math.Abs(tiro);
                    NewShoots[idxShoots] = tiro;
                    if (BolaUNO && tiro > 3 && tiro < 9) //Poniendo una regla para que sea un split si los pinos derribados estan entre 4 y 8 //V0.3
                        split = true;

                }
                if (BolaUNO)
                {//1er tiro
                    if (tiro == 10)
                        BolaUNO = true;
                    //else if (tiro >= NoTap && tiro < 10 || (!((idxFrame + 1) % 3) && AutoStrike))
                    else if (tiro >= NoTap && tiro < 10)
                    {
                        tiro = 10;
                        NewShoots[idxShoots] = tiro;
                        BolaUNO = true;
                        no_tap = true;
                        //
                        boxes[boxIDX] = noTap;  //Para que no sea 10 y se marque subrayado
                    }
                    else
                        BolaUNO = false;

                    if (tiro > 10)
                    {  //Incorporando Foul (antes la función retornaba aqui, pues no era un dato valido, ahora se interpreta como Foul)
                        tiro = 0;
                        Edisplay[idxShoots] = 14;
                        NewShoots[idxShoots] = tiro; //Una vez registrado como Foul lo cambiamos en el arreglo NewShoots también
                        split = false; //buenas prácticas
                    }
                    else
                        Edisplay[idxShoots] = tiro;  //fix v0.2e
                }
                else
                {//2do tiro
                    BolaUNO = true;
                    int tiroAnt = NewShoots[idxShoots - 1];

                    if (tiroAnt + tiro > 10)
                    {
                        if (tiro > 10)
                        { //Incorporando Foul
                            tiro = 0;
                            Edisplay[idxShoots] = 14;
                            NewShoots[idxShoots] = tiro; //Una vez registrado como Foul lo cambiamos en el arreglo NewShoots también
                        }
                        else
                            return -(idxFrame * 2 + 1);
                    }
                    else if (tiroAnt + tiro == 10)  //spare?
                        Edisplay[idxShoots] = 11;  //Marcando el spare, '/'
                    else
                        Edisplay[idxShoots] = tiro;
                }
                //Sobreescribiendo Edisplay en algunos casos
                //if (idxFrame >= 9 && tiro == 10)  //if(i_frame == 10) 10th inning
                if (idxFrame >= 9 && Edisplay[idxShoots] == 10) //fix v0.2e
                {
                    Edisplay[idxShoots] = 12;
                    //boxes[boxIDX++] = 0;  //deberia ir otra cosa, diferente de cero, indicando que debe ir vacia esta casilla
                }
                else if (split)
                {
                    //Edisplay[idxShoots] = tiro;
                    Edisplay[idxShoots] = 20 + tiro;  //fix v0.2e
                    split = false; //fix V0.3
                }

                if (no_tap)
                    Edisplay[idxShoots] = Edisplay[idxShoots] + 30; //Marcando NoTap

                if(Edisplay[idxShoots] == 10 || Edisplay[idxShoots] == 40)
                    boxes[boxIDX++] = 0;

                if (BolaUNO)
                    idxFrame++;
                idxShoots++;
            }//while (idxShoots < Q)
             //Si llega aquí será porque no hubo ningún dato no válido en la actualización
             //... Todos los datos fueron válidos
             //Dejando Roll() preparado:
            ScndBall = !BolaUNO;

            /*
            //pensando en el contador de juegos:
            int tmp_Frame = i_frame;
            if (idxFrame > 10)
                tmp_Frame = 10;
            //int tmp_inc = idxFrame - i_frame;
            int tmp_inc = idxFrame - tmp_Frame;
            if (tmp_inc > 0)
                frame_counter += tmp_inc;
           */

            i_frame = idxFrame;
            shoots = idxShoots; //added //fix v0.2e

            //if (AutoStrike)
               //autoRollStrike();

            //Reseteando algunos valores específicos a CalculateFrameTotals() para luego llamarla
            index = 0;
            index_frame = 0;
            fst = true;
            max_reach = 0; //new V0.3d
                           //Copiando arreglos:
            for (int i = 0; i < 21; i++)
            {
                if (i >= Q)
                { //added //fix v0.2e //no se todavia si es tan necesario
                    easyDisplay[i] = 0;
                    shootsArray[i] = 0;
                }
                else
                {
                    easyDisplay[i] = Edisplay[i];
                    shootsArray[i] = NewShoots[i];
                }
            }
            //Otros reseteos:
            for (int i = 0; i < 10; i++)
                TotalFrames[i] = 0;
            Total = 0;

            //Added V0.3d3_c:
            //if (AutoStrike)      //No es necesraio de este lado (admin / frontdesk)
            //autoRollStrike();
            //Con todo lo anterior llamamos a CalculateFrameTotals() y de paso damos valor a gameActive
            gameActive = !CalculateFrameTotals();

            //Adding in V0.3d
            if (!gameActive && shoots - 1 > max_reach) //shoots es la ctd tiros y max_reach es un index que empieza en 0
                shoots = max_reach + 1;
            //Podria además limpiar los arreglos easyDisplay[] y shootsArray[] de max_reach hacia adelante

            //Limpiando casillas no usadas esta vez:
            //for (int i = boxIDX; i < 21; i++) //No es totalmente necesario, pero cuando mire a la BD no quiero confundirme
                //boxes[i] = 0; //De nuevo otro valor distinto de cero seria mejor aqui, porq cero significa que se fue a la canal, no es lo mismo que la casilla vacía

            return BolaUNO ? idxFrame * 2 : idxFrame * 2 + 1;  //Retornando la casilla donde se quedó el jugador
        }//int UpdateScore_classic(...)

        //bool Player::CalculateFrameTotals()
        /* - Estas variables:
            index, index_frame y bool fst pertenecen a Player, y la idea  de que no sean locales a esta func es porque se queda
            por donde ultima vez se llamó.
            Regresa true si se acabó el juego y false sino...
            Invocado por Roll() y por UpdateScore()(todas sus variantes)
        */
        bool CalculateFrameTotals()
        {
            int tiro = 0;
            while (index < shoots)
            {

                tiro = shootsArray[index];

                if (fst)
                {
                    if (tiro < 10)
                    {
                        index++;
                        fst = !fst;
                    }
                    else
                    {  //Si es una chuza
                        if (shoots - index >= 3)
                        {  //Se han realizado dos tiros mas despues de este?
                           //Entonces es posible actualizar
                            if (index_frame > 0)
                                TotalFrames[index_frame] = TotalFrames[index_frame - 1];
                            TotalFrames[index_frame++] += 10 + shootsArray[index + 1] + shootsArray[index + 2];
                            max_reach = index + 2; //new V0.3d
                            index++;
                        }
                        else
                            break; //Aqui se debe parar de actualizar los totales, no se pudo seguir
                    }
                }//if(fst)
                else
                {
                    int tiroAnt = 0;
                    tiroAnt = shootsArray[index - 1];
                    if (tiroAnt + tiro == 10)
                    { //Si es un spare
                        if (shoots - index >= 2)
                        {//Se ha realizado algun otro tiro después de este?	
                            if (index_frame > 0)
                                TotalFrames[index_frame] = TotalFrames[index_frame - 1];
                            TotalFrames[index_frame++] += 10 + shootsArray[index + 1];
                            max_reach = index + 1; //new V0.3d
                            index++;
                            fst = !fst;
                        }
                        else
                            break;  //Aqui se debe parar de actualizar los totales, no se pudo seguir
                    }
                    else
                    {//Es totalmente posible calcular el total para este inning			
                        if (index_frame > 0)
                            TotalFrames[index_frame] = TotalFrames[index_frame - 1];
                        TotalFrames[index_frame++] += tiro + tiroAnt;
                        max_reach = index; //new V0.3d
                        index++;
                        fst = !fst;
                    }
                }
                if (index_frame == 10) //no seguir aunque se pueda cumplir que (index < shoots)
                    return true;  //Se acabó el juego
            }//while(index < shoots)
            return false;  //no se ha acabado el juego
        }//CalculateFrameTotals()

        //getQtyShoots()
        public int Shoots
        {
            get
            {
                return shoots;
            }
        }

        public int getEasyPoints(int idx)
        {
            return easyDisplay[idx];
        }

        //getQtyFrameTotals()
        //Devuelve la cantidad de frames a los que ya se le ha podido calcular el total
        public int Qframes
        {
            get
            {
                return index_frame;
            }
        }

        //getFTotalString
        public string getFTotalString(int idxFrame)
        {
            int x = TotalFrames[idxFrame];
            if (x == 0 && index_frame < i_frame)
                return "";
            else
                return x.ToString();
        }

        //getTotal()
        public int getTotal()
        {
            if (index_frame == 0)
                return 0;
            else
                //if (hcp != -1 && !LowGame) //hcp no se tiene en cuenta estando en LowGame
                if (Hcp != -1)
                    return TotalFrames[index_frame - 1] + Hcp;  //... + Hcp cdo se implemente
            else
                return TotalFrames[index_frame - 1];
        }

        //Reimplementando int Player::CasIndex() para mayor sencillez:
        //En definitiva donde único se está haciendo uso, o se llama a este método es en Player::getMAXPOS()
        //retornará 21 en algunas ocasiones, eso quiere decir que ya el jugador terminó
        int CasIndex()
        {
            //MyPlayer->getFstBall() ? MyPlayer->getCurrentFrame() * 2 : MyPlayer->getCurrentFrame() * 2 + 1;
            if (i_frame < 10)
                return !ScndBall ? i_frame * 2 : i_frame * 2 + 1;
            else
            {
                if (i_frame == 10)
                {
                    if (shootsArray[shoots - 1] == 10)
                        return 19;
                    else
                        return !ScndBall ? i_frame * 2 : i_frame * 2 + 1;
                }
                return 21;
            }
        }//int Player::CasIndex()
         //---------------------------------------------------------------------------------------------
         //Si MAXPOS es menor q cero es que todavía ese jugador no ha lanzado ni una vez
         //Hasta ahora esta funcion se llama la mayoria de las veces para saber hasta donde se puede desplazar el cursor 
         //en el modo de correcion de la puntuacion, el hecho de que estando en turno se deje escribir en la casilla donde
         //iría el nuevo roll es con toda la intencion de poder poner un tiro que los sensores no hayan detectado por ejemplo
        public int getMAXPOS()
        {
            int x = CasIndex();
            if (enTurno)
                MAXPOS = x;
            else
                MAXPOS = x - 1;

            if (MAXPOS > 20)
                MAXPOS = 20; //Parche a la caretaaaa.....

            return MAXPOS;
        }
        //------------------------------------------------------------------------------------------------
        //Este reset no es general, lo estoy poniendo para la actualización de la puntuación y para el uso con la instancia de Player llamnada backupPly
        //Mas adelante si hiciera falta en otros contextos se podria generalizar
        public void Reset()
        {
            /*
            index = 0;
            index_frame = 0;
            fst = true;
            max_reach = 0;

            shoots = 0;
            */
            //por ahora solo me interesa esto:
            shoots = 0;
        }
        //Nuevo:
        //Igual que el anterios no es general
        public void LoadScoreFromPlayer(Player orgPly)
        {
            //int[] shootsArray = new int[21];     //21 es el máximo de tiros posibles para un jugador  //Aqui quedan los puntos alcanzados en cada tiro
            // int shoots = 0;
            shoots = orgPly.shoots;
            for(int i = 0; i < shoots; i++)
                shootsArray[i] = orgPly.shootsArray[i];

            //Voy a dejar lo de arriba pero lo que necesito por ahora es esto:
            for (int i = 0; i < shoots; i++)
                easyDisplay[i] = orgPly.easyDisplay[i];

            index_frame = orgPly.index_frame;
            for (int i = 0; i < orgPly.Qframes; i++)
                TotalFrames[i] = orgPly.TotalFrames[i];

            boxIDX = orgPly.boxIDX;
            for (int i = 0; i < orgPly.Qboxes; i++)
                boxes[i] = orgPly.boxes[i];

            newScore = true;
        }

        ///*
        public int GetBoxValue(int idxBox)
        {
            if (idxBox < this.boxIDX)
                return boxes[idxBox];
            else
                return 0; //Tambien aqui creo q un numnero dsitinto de 0 seria mejor
        }
        //*/



    }//end of class Player
}
/*
   0 - 9 --> '0' - '9'
   10 --> 'X' | chuza / strike, se debe colocar en la segunda casilla del frame
   11 --> '/' | spare
   12 --> 'X' | chuza, se debe colocar en la primera casilla del frame (este caso solo se da en el 10th inning)
   14 --> 'F' | Foul
   20 - 29 --> '0' - '9' |  splits, en la practica debe ser 24 - 28
   40 --> 'X' (underlined) | chuza, se marca con una X subrayada,
                             indica que fue lograda con un No-Tap o con AutoStrike.
							 Se debe colocar en la segunda casilla del frame
   42 --> 'X' (underlined) | chuza, se marca con una X subrayada,
                             indica que fue lograda con un No-Tap o con AutoStrike.
							 Se debe colocar en la casilla del frame que corresponda
							 , puiera ser la 1ra, la 2da, o la tercera (10th inning)
*/
