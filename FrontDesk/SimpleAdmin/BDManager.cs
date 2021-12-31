using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace SimpleAdmin
{
    /* Clase encargada de realizar la escrituta y lectura en la BD
     * 
     */
    //21/03/2021:
    // - He ido eliminando el uso de SqlTransaction, tratando de eliminar una excepcion que lanza BDpopulate()
    //   solo me queda en UpdateScore()
    class BDManager
    {
        string connection_string, connection_string2; //connection_string2 será empleada por cnnAuxPopulate para darle un mayor TimeOut
        SqlConnection cnn;
        DataTable PlayerIMG;
        SqlBulkCopy bulkC_Player; //bulk copy  //para insertar varios récords de forma más efectiva en la BD

        DataTable ScoreIMG;
        SqlBulkCopy bulkC_Score;
        //bool connToBD;

        DataTable GameIMG;
        SqlBulkCopy bulkC_Game;

        DataTable TeamIMG;
        SqlBulkCopy bulkC_Team;


        SqlConnection cnnAux;   //UpdatesScore
        SqlConnection cnnAuxPopulate;

        SetProgressCBack showStartProgress;

        object lockBDpopulate;

        struct ShootCmnd
        {
            public int Points;
            public int PlyID;
            public int GameID;
            public int Cas;
        }

        public struct AvailablesDiscount
        {
            public int games;
            public int scores;
            public int teams;
            public int players;
            public AvailablesDiscount(int x = 0)
            {
               games = 1;
               scores = x ;
               teams = 1;
               players = x;
            }
            public void Iniatlize(int x)
            {
                games = 1;
                scores = x;
                teams = 1;
                players = x;
            }
        }

        ConcurrentQueue<ShootCmnd> ShootsToBD;
        ConcurrentQueue<AvailablesDiscount> CosumedBDrows;  //Sera acedido por el hilo ppal y por BDpopulateThread
        Thread ScoreUpdater;
        Thread BDpopulateThread;
        TClock MyClock;
        bool UpdScoreActivated;
        bool populatingBD;
        bool fstTimePopulating;

        int gamesAvailables;
        int currentGameID;
        int playersAvailables;
        int currentPlayerID;
        int scoresAvailables;
        int teamsAvailables;
        int currentTeamID;

        public BDManager(TClock clock)
        {
            /*
            connection_string = @"Data Source = ERNEST\SQLEXPRESS01; Initial Catalog = BOL; User ID = sa; Password = 123";  //On my Laptop
            //connection_string = @"Data Source = MANAGER\SQLEXPRESS; Initial Catalog = BOL; User ID = sa; Password = 123";  //PC Bol TK
            cnn = new SqlConnection(connection_string);
            cnnAux = new SqlConnection(connection_string);
            //cnnAuxPopulate = new SqlConnection(connection_string);
            //connection_string2 = @"Data Source = ERNEST\SQLEXPRESS01; Initial Catalog = BOL; User ID = sa; Password = 123; Connection Timeout = 70";  //Setting un TimeOut de 1 min y 10 seg
            connection_string2 = @"Data Source = ERNEST\SQLEXPRESS01; Initial Catalog = BOL; User ID = sa; Password = 123; Connection Timeout = 30";  //Voy a bajar el timeout para las pruebas
            cnnAuxPopulate = new SqlConnection(connection_string2);
           */
           
            ///*
            connection_string = @"Data Source = " + GetCnnStr() + @"; Initial Catalog = BOL; User ID = sa; Password = 123";   
            cnn = new SqlConnection(connection_string);
            cnnAux = new SqlConnection(connection_string);
            //connection_string2 = @"Data Source = " + GetCnnStr() + @"; Initial Catalog = BOL; User ID = sa; Password = 123; Connection Timeout = 70";  //Setting un TimeOut de 1 min y 10 seg
            connection_string2 = @"Data Source = " + GetCnnStr() + @"; Initial Catalog = BOL; User ID = sa; Password = 123; Connection Timeout = 30"; //Voy a bajar el timeout para las pruebas
            cnnAuxPopulate = new SqlConnection(connection_string2);
            //*/
            //
            lockBDpopulate = new object();

            //Tabla de Jugadores
            PlayerIMG = new DataTable();
            //bulkC_Player = new SqlBulkCopy(cnn);   //probando
            bulkC_Player = new SqlBulkCopy(cnnAuxPopulate);
            bulkC_Player.DestinationTableName = "Player";
            bulkC_Player.BulkCopyTimeout = 0; //No creo que esto elimine las excpeciones
                                              //y las elimina el programa pudiera romperse mas adelante, lo ideal seria elminar cuando se bloquea
            //Conformando Tabla:
            PlayerIMG.Columns.Add(new DataColumn("Nombre", typeof(string)));
            PlayerIMG.Columns.Add(new DataColumn("Handicap", typeof(Int32)));
            PlayerIMG.Columns.Add(new DataColumn("Bumpers", typeof(bool)));
            PlayerIMG.Columns.Add(new DataColumn("Tipo", typeof(string)));
            PlayerIMG.Columns.Add(new DataColumn("TeamID", typeof(Int32)));
            PlayerIMG.Columns.Add(new DataColumn("Estado", typeof(string)));
            PlayerIMG.Columns.Add(new DataColumn("UsaZapatos", typeof(bool)));
            PlayerIMG.Columns.Add(new DataColumn("TallaDeZapatos", typeof(Int32)));
            PlayerIMG.Columns.Add(new DataColumn("PlayerID", typeof(Int32)));
            PlayerIMG.Columns.Add(new DataColumn("Orden", typeof(Int16)));  //tengo puesto smallint en la tabla Player en la BD
            //MAPPING the columns of Datatable Table to Table:
            bulkC_Player.ColumnMappings.Add("Nombre", "Nombre");
            bulkC_Player.ColumnMappings.Add("Handicap", "Handicap");
            bulkC_Player.ColumnMappings.Add("Bumpers", "Bumpers");
            bulkC_Player.ColumnMappings.Add("Tipo", "Tipo");
            bulkC_Player.ColumnMappings.Add("TeamID", "TeamID");
            bulkC_Player.ColumnMappings.Add("Estado", "Estado");
            bulkC_Player.ColumnMappings.Add("UsaZapatos", "UsaZapatos");
            bulkC_Player.ColumnMappings.Add("TallaDeZapatos", "TallaDeZapatos");
            bulkC_Player.ColumnMappings.Add("PlayerID", "PlayerID");
            bulkC_Player.ColumnMappings.Add("Orden", "Orden");

            //Tabla de Score
            ScoreIMG = new DataTable();
            //bulkC_Score = new SqlBulkCopy(cnn);  //probando
            bulkC_Score = new SqlBulkCopy(cnnAuxPopulate);
            bulkC_Score.DestinationTableName = "Score";
            bulkC_Score.BulkCopyTimeout = 0;  //mismo comentario que para el anterior
            //Conformando Tabla:
            ScoreIMG.Columns.Add(new DataColumn("PlayerID", typeof(Int32)));
            ScoreIMG.Columns.Add(new DataColumn("GameID", typeof(Int32)));
            ScoreIMG.Columns.Add(new DataColumn("1_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("1_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("2_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("2_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("3_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("3_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("4_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("4_2", typeof(Int16)));  //tengo puesto smallint en la tabla en la BD
            ScoreIMG.Columns.Add(new DataColumn("5_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("5_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("6_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("6_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("7_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("7_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("8_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("8_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("9_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("9_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("10_1", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("10_2", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("10_3", typeof(Int16)));
            ScoreIMG.Columns.Add(new DataColumn("Shots", typeof(Int16)));
            //MAPPING the columns of Datatable Table to Table:
            bulkC_Score.ColumnMappings.Add("PlayerID", "PlayerID");
            bulkC_Score.ColumnMappings.Add("GameID", "GameID");
            bulkC_Score.ColumnMappings.Add("1_1", "1_1");
            bulkC_Score.ColumnMappings.Add("1_2", "1_2");
            bulkC_Score.ColumnMappings.Add("2_1", "2_1");
            bulkC_Score.ColumnMappings.Add("2_2", "2_2");
            bulkC_Score.ColumnMappings.Add("3_1", "3_1");
            bulkC_Score.ColumnMappings.Add("3_2", "3_2");
            bulkC_Score.ColumnMappings.Add("4_1", "4_1");
            bulkC_Score.ColumnMappings.Add("4_2", "4_2");
            bulkC_Score.ColumnMappings.Add("5_1", "5_1");
            bulkC_Score.ColumnMappings.Add("5_2", "5_2");
            bulkC_Score.ColumnMappings.Add("6_1", "6_1");
            bulkC_Score.ColumnMappings.Add("6_2", "6_2");
            bulkC_Score.ColumnMappings.Add("7_1", "7_1");
            bulkC_Score.ColumnMappings.Add("7_2", "7_2");
            bulkC_Score.ColumnMappings.Add("8_1", "8_1");
            bulkC_Score.ColumnMappings.Add("8_2", "8_2");
            bulkC_Score.ColumnMappings.Add("9_1", "9_1");
            bulkC_Score.ColumnMappings.Add("9_2", "9_2");
            bulkC_Score.ColumnMappings.Add("10_1", "10_1");
            bulkC_Score.ColumnMappings.Add("10_2", "10_2");
            bulkC_Score.ColumnMappings.Add("10_3", "10_3");
            bulkC_Score.ColumnMappings.Add("Shots", "Shots");

            //Tabla Game
            GameIMG = new DataTable();
            //bulkC_Game = new SqlBulkCopy(cnn); //probando
            bulkC_Game = new SqlBulkCopy(cnnAuxPopulate);
            bulkC_Game.DestinationTableName = "Game";
            bulkC_Game.BulkCopyTimeout = 0; //mismo comentario...
            //Conformando Tabla:
            GameIMG.Columns.Add(new DataColumn("GameID", typeof(Int32)));
            GameIMG.Columns.Add(new DataColumn("TeamID", typeof(Int32)));
            GameIMG.Columns.Add(new DataColumn("LaneID", typeof(Int32)));
            GameIMG.Columns.Add(new DataColumn("Estado", typeof(string)));
            GameIMG.Columns.Add(new DataColumn("FechaInicio", typeof(DateTime)));
            GameIMG.Columns.Add(new DataColumn("FechaFinal", typeof(DateTime)));
            GameIMG.Columns.Add(new DataColumn("Variante", typeof(string)));
            //MAPPING the columns of Datatable Table to Table:
            bulkC_Game.ColumnMappings.Add("GameID", "GameID");
            bulkC_Game.ColumnMappings.Add("TeamID", "TeamID");
            bulkC_Game.ColumnMappings.Add("LaneID", "LaneID");
            bulkC_Game.ColumnMappings.Add("Estado", "Estado");
            bulkC_Game.ColumnMappings.Add("FechaInicio", "FechaInicio");
            bulkC_Game.ColumnMappings.Add("FechaFinal", "FechaFinal");
            bulkC_Game.ColumnMappings.Add("Variante", "Variante");

            //Tabla Team
            TeamIMG = new DataTable();
            //bulkC_Team = new SqlBulkCopy(cnn);  //probando
            bulkC_Team = new SqlBulkCopy(cnnAuxPopulate);
            bulkC_Team.DestinationTableName = "Team";
            bulkC_Team.BulkCopyTimeout = 0; //mismo comentario...
            //Conformando Tabla:
            TeamIMG.Columns.Add(new DataColumn("CtdJugadores", typeof(Int32)));
            TeamIMG.Columns.Add(new DataColumn("Total", typeof(Int32)));
            TeamIMG.Columns.Add(new DataColumn("TotalHcp", typeof(Int32)));
            TeamIMG.Columns.Add(new DataColumn("Nombre", typeof(string)));
            TeamIMG.Columns.Add(new DataColumn("TeamID", typeof(Int32)));
            TeamIMG.Columns.Add(new DataColumn("TurnoID", typeof(Int32)));
            TeamIMG.Columns.Add(new DataColumn("Estado", typeof(string)));
            //MAPPING the columns of Datatable Table to Table:
            bulkC_Team.ColumnMappings.Add("CtdJugadores", "CtdJugadores");
            bulkC_Team.ColumnMappings.Add("Total", "Total");
            bulkC_Team.ColumnMappings.Add("TotalHcp", "TotalHcp");
            bulkC_Team.ColumnMappings.Add("Nombre", "Nombre");
            bulkC_Team.ColumnMappings.Add("TeamID", "TeamID");
            bulkC_Team.ColumnMappings.Add("TurnoID", "TurnoID");
            bulkC_Team.ColumnMappings.Add("Estado", "Estado");

            //
            //connToBD = false;
            ShootsToBD = new ConcurrentQueue<ShootCmnd>();
            MyClock = clock;
            UpdScoreActivated = false;
            ScoreUpdater = new Thread(UpdateScore);
            //
            //BDpopulateThread = new Thread(BDpopulate);
            populatingBD = false;
            fstTimePopulating = true;
            CosumedBDrows = new ConcurrentQueue<AvailablesDiscount>();
            //
            gamesAvailables = 0;
            currentGameID = -1;
            playersAvailables = 0;
            currentPlayerID = -1;       
            //
            showStartProgress = null; //Debe llamarse a AssignProgressBar luego
        }

        static string GetCnnStr()
        {
            var partial_connectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
            return partial_connectionString;
        }

        //Nueva forma de abrir juego aprovechando la insercción en adelanto que se ha estado haciendo previamente.
        //Por lo que se basará principalmente en updates.

        //Voy a dejar de usar transaccones aqui, y voy a llamar a RequestFillBD(...) al final simpre que pueda
        //Todo esto buscando que no salga mas la excpecion en BDpopulaye()
        public void OpenGame(int laneIDX, Team playersTeam)
        {
            if (SQLconnect())
            {
                bool requestBDpopulate = true;
                AvailablesDiscount rowsQ;
                rowsQ.games = 1;
                rowsQ.teams = 1;
                rowsQ.players = playersTeam.Qty;
                rowsQ.scores = playersTeam.Qty;
                //RequestFillBD(rowsQ);
                if (!this.CheckBDavailability(rowsQ))
                {
                    RequestFillBD(rowsQ);
                    requestBDpopulate = false;
                    while (!this.CheckBDavailability(rowsQ)) //Esperando a que estén disponibles todos los registros necesarios
                    {
                        Thread.Sleep(500);
                    }
                }
                //----------------------------------------
                SqlCommand SQLcommand = cnn.CreateCommand();
                //SqlTransaction SQLtransaction;

                //Start a local transaction.
                //SQLtransaction = cnn.BeginTransaction("UpdateTables"); //No creo que el nombre sea importante

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                //SQLcommand.Transaction = SQLtransaction;

                int teamID = currentTeamID++;
                playersTeam.BDCurrTeamID = teamID;
                //*** Insertando nuevo equipo: ***
                SQLcommand.CommandText = "Update Team Set Nombre = @Name, CtdJugadores = @QtyPlys, Estado = 'activo' Where TeamID = @teamID";  //activo o assigned, mas adelante se verá cual podria ser mejor
                SQLcommand.Parameters.AddWithValue("@Name", playersTeam.TeamName);
                SQLcommand.Parameters.AddWithValue("@QtyPlys", playersTeam.Qty);
                SQLcommand.Parameters.AddWithValue("@teamID", teamID);
                //SQLcommand.Parameters.AddWithValue("@estado", "activo");
                SQLcommand.ExecuteNonQuery(); //BD access
                SQLcommand.Parameters.Clear();

                //*** Insertando jugadores: ***
                SQLcommand.CommandText = "Update Player Set Nombre = @PlyName, Bumpers = @UseBumpers, TeamID = @teamID, Orden = @orden, Estado = @estado Where PlayerID = @plyID";
                for (int i = 0; i < playersTeam.Qty; i++)
                {
                    SQLcommand.Parameters.AddWithValue("@PlyName", playersTeam[i].Name);
                    SQLcommand.Parameters.AddWithValue("@UseBumpers", playersTeam[i].Bumpers);
                    SQLcommand.Parameters.AddWithValue("@teamID", teamID);
                    SQLcommand.Parameters.AddWithValue("@orden", i);
                    SQLcommand.Parameters.AddWithValue("@plyID", currentPlayerID + i);
                    SQLcommand.Parameters.AddWithValue("@estado", "activo");
                    SQLcommand.ExecuteNonQuery(); //BD access
                    SQLcommand.Parameters.Clear();  //Otras veces no he necesitado hacer esto, no se por que aqui si
                }

                for (int i = 0; i <  playersTeam.Qty; i++)
                    playersTeam[i].BDplyID = currentPlayerID++;

                int gameID = currentGameID++;
                playersTeam.BDCurrGameID = gameID;
                //*** Insertando Juego (Game) ***
                SQLcommand.CommandText = "Update Game Set TeamID = @GteamID, LaneID = @laneID, Estado =  @estado, Variante =  @variante Where GameID = @gameID";
                SQLcommand.Parameters.AddWithValue("@GteamID", teamID);
                //SQLcommand.Parameters.AddWithValue("@laneID", laneIDX + 1);
                SQLcommand.Parameters.AddWithValue("@laneID", laneIDX);
                SQLcommand.Parameters.AddWithValue("@estado", "activo");
                SQLcommand.Parameters.AddWithValue("@variante", "classic");
                SQLcommand.Parameters.AddWithValue("@gameID", gameID);
                SQLcommand.ExecuteNonQuery(); //BD access
                

                //*** Preparando los renglones para el score ***
                SQLcommand.CommandText = "Update Top(1) Score Set PlayerID = @SplyID, GameID = @SgameID, Estado =  @Sestado Where Estado = 'available'";
                for (int i = 0; i < playersTeam.Qty; i++)
                {
                    //SQLcommand.CommandText = "Update Top(1) Score Set PlayerID = @SplyID, GameID = @SgameID, Estado =  @Sestado Where Estado = 'available'";
                    SQLcommand.Parameters.AddWithValue("@SplyID", playersTeam[i].BDplyID);
                    SQLcommand.Parameters.AddWithValue("@SgameID", gameID);
                    SQLcommand.Parameters.AddWithValue("@Sestado", "assigned");
                    SQLcommand.ExecuteNonQuery(); //BD access
                    SQLcommand.Parameters.Clear();  //Otras veces no he necesitado hacer esto, no se por que aqui si
                }
                //Attempt to commit the transaction.  //Debería capturar si hubo alguna excepción y si es asi hacer un rollback
                //SQLtransaction.Commit();

                //Finally
                cnn.Close();
                //Ahora me parece mejor, mover estas líneas arriba
                /*
                AvailablesDiscount rowsQ;
                rowsQ.games = 1;
                rowsQ.teams = 1;
                rowsQ.players = playersTeam.Qty;
                rowsQ.scores = playersTeam.Qty;
                RequestFillBD(rowsQ);
                */
                if(requestBDpopulate)
                    RequestFillBD(rowsQ);
            }
            else
                MessageBox.Show("Data Base connection error", "BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


        //primera versión de modificar juego, basada en OpenGame(...)
        //Todavía no se tiene en cuenta nada de la puntuación...

        //Ver comentarios en OpenGAme, voy a hacer lo miso aqui, para evitar conflictos con BDpopulate()
        public void ModifyGame(Team playersTeam)
        {
            if (SQLconnect())
            {
                int newQplys = playersTeam.GetQnewPlayers();
                int[] BDplyIDnews = null;
                int BDplyIDnewsIDX = 0;

                int delPlys = playersTeam.GetQdelPlayers();

                bool requestBDpopulate = false;
                AvailablesDiscount rowsQ;
                //rowsQ.Iniatlize(0); //Uso de la variable local no asignada...
                rowsQ.games = 0;
                rowsQ.teams = 0;
                rowsQ.players = newQplys;
                rowsQ.scores = newQplys;
                if (newQplys > 0)
                {
                    //AvailablesDiscount rowsQ;
                    //rowsQ.games = 0;
                    //rowsQ.teams = 0;
                    //rowsQ.players = newQplys;
                    //rowsQ.scores = newQplys;
                    //RequestFillBD(rowsQ);
                    BDplyIDnews = new int[newQplys];
                    if (!this.CheckBDavailability(rowsQ))
                    {
                        RequestFillBD(rowsQ);
                        while (!this.CheckBDavailability(rowsQ)) //Esperando a que estén disponibles todos los registros necesarios
                        {
                            //Podria esperar tambien no solo que haya disponiibilidad, sino a que termine completamente... con Stop..., pero lo dejo asi por ahora
                            Thread.Sleep(500);
                        } 
                        //BDplyIDnews = new int[newQplys];
                        //requestBDpopulate = false;
                    }
                    else
                        requestBDpopulate = true;
                }
                //----------------------------------------
                SqlCommand SQLcommand = cnn.CreateCommand();
                //SqlTransaction SQLtransaction;

                //Start a local transaction.
                //SQLtransaction = cnn.BeginTransaction("UpdateTables"); //No creo que el nombre sea importante

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                //SQLcommand.Transaction = SQLtransaction;

                int teamID = playersTeam.BDCurrTeamID;
                // *** Actualizando equipo: ***
                SQLcommand.CommandText = "Update Team Set Nombre = @Name, CtdJugadores = @QtyPlys Where TeamID = @teamID";
                SQLcommand.Parameters.AddWithValue("@Name", playersTeam.TeamName);
                SQLcommand.Parameters.AddWithValue("@QtyPlys", playersTeam.Qty);
                SQLcommand.Parameters.AddWithValue("@teamID", teamID);
                SQLcommand.ExecuteNonQuery(); //BD access
                SQLcommand.Parameters.Clear();

               //*** Actualizando jugadores: ***
               SQLcommand.CommandText = "Update Player Set Nombre = @PlyName, Bumpers = @UseBumpers, TeamID = @teamID, Orden = @orden, Estado = @estado Where PlayerID = @plyID";
                for (int i = 0; i < playersTeam.Qty; i++)
                {
                    //if (playersTeam[i].BDplyID == -1 || playersTeam[i].modify)  //Cuando se elimine uno, habra aque cambiar el orden a todos, asi q siempre por ahora se slavaran todos los jugadores
                    //{
                        SQLcommand.Parameters.AddWithValue("@PlyName", playersTeam[i].Name);
                        SQLcommand.Parameters.AddWithValue("@UseBumpers", playersTeam[i].Bumpers);
                        SQLcommand.Parameters.AddWithValue("@teamID", teamID);
                        SQLcommand.Parameters.AddWithValue("@orden", i);


                        if (playersTeam[i].BDplyID == -1)
                        {
                            playersTeam[i].BDplyID = currentPlayerID++; //Asignando un nuevo PlayerID
                            BDplyIDnews[BDplyIDnewsIDX++] = playersTeam[i].BDplyID;
                        }

                        SQLcommand.Parameters.AddWithValue("@plyID", playersTeam[i].BDplyID);

                        SQLcommand.Parameters.AddWithValue("@estado", "activo");
                        SQLcommand.ExecuteNonQuery(); //BD access
                        SQLcommand.Parameters.Clear();  //Otras veces no he necesitado hacer esto, no se por que aqui si
                    //}
                }

                //*** Actualizar juego... ***
                // - Tal vez cambiar la variante  (classic/notap/3-6-9/lowgame, etc)
                //... //Mas adelante

                //Agregado al aparecer la opcion de corregir la puntuación
                SQLcommand.CommandText = "Update Score Set Shots = @ShootsPly, [1_1] = @cas1, [1_2] = @cas2, [2_1] = @cas3, [2_2] = @cas4, [3_1] = @cas5, [3_2] = @cas6, [4_1] = @cas7, [4_2] = @cas8, [5_1] = @cas9, [5_2] = @cas10, [6_1] = @cas11, [6_2] = @cas12, [7_1] = @cas13, [7_2] = @cas14, [8_1] = @cas15, [8_2] = @cas16, [9_1] = @cas17, [9_2] = @cas18, [10_1] = @cas19, [10_2] = @cas20, [10_3] = @cas21 where PlayerID = @plyID and GameID = @gameID";
                for (int i = 0; i < playersTeam.Qty; i++)
                {
                    //if(playersTeam[i].Shoots != 0)
                    if (playersTeam[i].scoreUpdated && playersTeam[i].BDplyID != -1)  //Si llego aqui es porq BDplyID != -1, pero por si acaso
                    {
                        SQLcommand.Parameters.AddWithValue("@ShootsPly", playersTeam[i].Shoots);
                        SQLcommand.Parameters.AddWithValue("@cas1", playersTeam[i].GetBoxValue(0));
                        SQLcommand.Parameters.AddWithValue("@cas2", playersTeam[i].GetBoxValue(1));
                        SQLcommand.Parameters.AddWithValue("@cas3", playersTeam[i].GetBoxValue(2));
                        SQLcommand.Parameters.AddWithValue("@cas4", playersTeam[i].GetBoxValue(3));
                        SQLcommand.Parameters.AddWithValue("@cas5", playersTeam[i].GetBoxValue(4));
                        SQLcommand.Parameters.AddWithValue("@cas6", playersTeam[i].GetBoxValue(5));
                        SQLcommand.Parameters.AddWithValue("@cas7", playersTeam[i].GetBoxValue(6));
                        SQLcommand.Parameters.AddWithValue("@cas8", playersTeam[i].GetBoxValue(7));
                        SQLcommand.Parameters.AddWithValue("@cas9", playersTeam[i].GetBoxValue(8));
                        SQLcommand.Parameters.AddWithValue("@cas10", playersTeam[i].GetBoxValue(9));
                        SQLcommand.Parameters.AddWithValue("@cas11", playersTeam[i].GetBoxValue(10));
                        SQLcommand.Parameters.AddWithValue("@cas12", playersTeam[i].GetBoxValue(11));
                        SQLcommand.Parameters.AddWithValue("@cas13", playersTeam[i].GetBoxValue(12));
                        SQLcommand.Parameters.AddWithValue("@cas14", playersTeam[i].GetBoxValue(13));
                        SQLcommand.Parameters.AddWithValue("@cas15", playersTeam[i].GetBoxValue(14));
                        SQLcommand.Parameters.AddWithValue("@cas16", playersTeam[i].GetBoxValue(15));
                        SQLcommand.Parameters.AddWithValue("@cas17", playersTeam[i].GetBoxValue(16));
                        SQLcommand.Parameters.AddWithValue("@cas18", playersTeam[i].GetBoxValue(17));
                        SQLcommand.Parameters.AddWithValue("@cas19", playersTeam[i].GetBoxValue(18));
                        SQLcommand.Parameters.AddWithValue("@cas20", playersTeam[i].GetBoxValue(19));
                        SQLcommand.Parameters.AddWithValue("@cas21", playersTeam[i].GetBoxValue(20));
                        SQLcommand.Parameters.AddWithValue("@plyID", playersTeam[i].BDplyID);
                        SQLcommand.Parameters.AddWithValue("@gameID", playersTeam.BDCurrGameID);
                        SQLcommand.ExecuteNonQuery(); //BD access
                        SQLcommand.Parameters.Clear();  
                    }
                }


                //*** Preparando los renglones para el score, en este caso solo de los jugadores nuevos ***
                SQLcommand.CommandText = "Update Top(1) Score Set PlayerID = @SplyID, GameID = @SgameID, Estado =  @Sestado Where Estado = 'available'";
                for (int i = 0; i < newQplys; i++)
                {
                    SQLcommand.Parameters.AddWithValue("@SplyID", BDplyIDnews[i]);
                    SQLcommand.Parameters.AddWithValue("@SgameID", playersTeam.BDCurrGameID);
                    SQLcommand.Parameters.AddWithValue("@Sestado", "assigned");
                    SQLcommand.ExecuteNonQuery(); //BD access
                    SQLcommand.Parameters.Clear();  //Otras veces no he necesitado hacer esto, no se por que aqui si
                }

                //*** Eliminando Jugadores ***
                SQLcommand.CommandText = "Update Player Set Estado = 'eliminado' Where PlayerID = @plyID";
                for (int i = 0; i < delPlys; i++)
                {
                    SQLcommand.Parameters.AddWithValue("@plyID", playersTeam.erasedPlys[i]);
                    SQLcommand.ExecuteNonQuery(); //BD access
                    SQLcommand.Parameters.Clear();  //Otras veces no he necesitado hacer esto, no se por que aqui si
                }

                //Attempt to commit the transaction.  //Debería capturar si hubo alguna excepción y si es asi hacer un rollback
                //SQLtransaction.Commit();

                //Finally
                cnn.Close();

                if (requestBDpopulate)
                    RequestFillBD(rowsQ);
            }
            else
                MessageBox.Show("Data Base connection error", "BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void TransferGameTo(int targetLane, Team playersTeam)
        {
            if (SQLconnect())
            {
                SqlCommand SQLcommand = cnn.CreateCommand();
                SQLcommand.CommandText = "Update Game Set LaneID = @laneID Where GameID = @gameID";
                SQLcommand.Parameters.AddWithValue("@laneID", targetLane);
                SQLcommand.Parameters.AddWithValue("@gameID", playersTeam.BDCurrGameID);
                SQLcommand.ExecuteNonQuery(); //BD access
                //Finally
                cnn.Close();

            }
            else
                MessageBox.Show("Data Base connection error", "BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CancelGame(int laneIDX, Team playersTeam)
        {
            if (SQLconnect())
            {
                SqlCommand SQLcommand = cnn.CreateCommand();
                SQLcommand.CommandText = "Update Game Set Estado = 'cancelado' Where GameID = @gameID";
                SQLcommand.Parameters.AddWithValue("@gameID", playersTeam.BDCurrGameID);
                SQLcommand.ExecuteNonQuery(); //BD access
                //Finally
                cnn.Close();

            }
            else
                MessageBox.Show("Data Base connection error", "BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Tal vez más adelante se cambie el nombre, por ahora solo leerá la ctd de tiros realizados por cada jugador.
        //Podria tambien, a la vez que recibo los tiros ir actualizando la ctd de tiros realizados por cada jugador
        //pero no se, prefiero hacerlo de esta forma por ahora
        //Puede que en un futuro se haya añadido un tiro a la BD desde la bowler consola por ejemplo, y este no haya sido enviado
        //al servidor como son enviados los tiros cada vez que un jugador lanza una bola
        //public void ReadGame(int laneIDX, Team playersTeam)
        /*
        public void ReadGame(Team playersTeam)
        {
            if (SQLconnect())
            {
                SqlCommand SQLcommand = cnn.CreateCommand();
                SqlDataReader SQLreader;
                SQLcommand.CommandText = "Select Shots from Score Where GameID = @gameID and PlayerID = @plyID";
                for(int i = 0; i < playersTeam.Qty; i++)
                {
                    SQLcommand.Parameters.AddWithValue("@gameID", playersTeam.BDCurrGameID);
                    SQLcommand.Parameters.AddWithValue("@plyID", playersTeam[i].BDplyID);
                    //SQLcommand.ExecuteNonQuery(); //BD access
                    SQLreader = SQLcommand.ExecuteReader();
                    if(SQLreader.Read())
                    {
                        playersTeam[i].Shots = Convert.ToInt32(SQLreader.GetValue(0));
                    }
                    SQLreader.Close();
                    SQLcommand.Parameters.Clear();
                }
                //Finally
                cnn.Close();

            }
            else
                MessageBox.Show("Data Base connection error", "BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        */
        //En esta segunda versión voy a intentar leer el Score también para probar cuanto tiempo se tarda
        public void ReadGame(Team playersTeam)
        {
            if (SQLconnect())
            {
                SqlCommand SQLcommand = cnn.CreateCommand();
                SqlDataReader SQLreader;
                //SQLcommand.CommandText = "Select Shots from Score Where GameID = @gameID and PlayerID = @plyID";
                SQLcommand.CommandText = "Select [1_1], [1_2], [2_1], [2_2], [3_1], [3_2], [4_1], [4_2], [5_1], [5_2], [6_1], [6_2], [7_1], [7_2], [8_1], [8_2], [9_1], [9_2], [10_1], [10_2], [10_3], Shots from Score Where GameID = @gameID and PlayerID = @plyID";
                for (int i = 0; i < playersTeam.Qty; i++)
                {
                    SQLcommand.Parameters.AddWithValue("@gameID", playersTeam.BDCurrGameID);
                    SQLcommand.Parameters.AddWithValue("@plyID", playersTeam[i].BDplyID);
                    //SQLcommand.ExecuteNonQuery(); //BD access
                    SQLreader = SQLcommand.ExecuteReader();
                    int[] NewShoots = new int[21];
                    /*
                    if (SQLreader.Read())
                    {
                        //playersTeam[i].Shots = Convert.ToInt32(SQLreader.GetValue(21));
                        int Qshoots = BuildShootsArray(NewShoots, results);
                    }
                    */
                    int Qshoots = BuildShootsArray(NewShoots, SQLreader);
                    playersTeam[i].UpdateScore_classic(NewShoots, Qshoots);
                    SQLreader.Close();
                    SQLcommand.Parameters.Clear();
                }
                //Finally
                cnn.Close();

            }
            else
                MessageBox.Show("Data Base connection error", "BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        int BuildShootsArray(int [] newShoots, SqlDataReader SQLreader)
        {
            if (SQLreader.Read())
            {
                int Qshoots = Convert.ToInt32(SQLreader.GetValue(21));

                for (int i = 0, shoots = 0; i < 21 && shoots < Qshoots; ++i, ++shoots)
                {
                    int points = Convert.ToInt32(SQLreader.GetValue(i));
                    //if (points == 10)
                    //if (points == 10 && i < 18) //casillas 18, 19 y 20 corresponden al 10mo inning y no hay que avanzar...
                    if (i%2 == 0 && points == 10 && i < 18) //Además si es una casilla de primer tiro o de primera bola, sino no
                        i++;
                    newShoots[shoots] = points;
                }
                return Qshoots;
            }
            else
                return 0;
        }


        /*
        public void ReadScore(Team playersTeam, Player Doe)
        {
            if(SQLconnect())
            {
                SqlCommand SQLcommand = cnn.CreateCommand();
                SqlDataReader SQLreader;
                //SQLcommand.CommandText = "Select Shots from Score Where GameID = @gameID and PlayerID = @plyID";
                SQLcommand.CommandText = "Select Shots from Score Where GameID = @gameID and PlayerID = @plyID";
                for (int i = 0; i < playersTeam.Qty; i++)
                {
                    SQLcommand.Parameters.AddWithValue("@gameID", playersTeam.BDCurrGameID);
                    SQLcommand.Parameters.AddWithValue("@plyID", playersTeam[i].BDplyID);
                    //SQLcommand.ExecuteNonQuery(); //BD access
                    SQLreader = SQLcommand.ExecuteReader();
                    if (SQLreader.Read())
                    {
                        playersTeam[i].Shots = Convert.ToInt32(SQLreader.GetValue(0));
                    }
                    SQLreader.Close();
                    SQLcommand.Parameters.Clear();
                }
                //Finally
                cnn.Close();

            }
            else
                MessageBox.Show("Data Base connection error", "BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        */



        bool SQLconnect()
        {
            bool Go = true;
            try
            {
                cnn.Open();
                //connToBD = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Go = false;
            }
            if (Go)
                return true;
            else
                return false;
        }

        bool SQLpopulate_connect()
        {
            bool Go = true;
            try
            {
                cnnAuxPopulate.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Go = false;
            }
            if (Go)
                return true;
            else
                return false;
        }


        

        public void SaveShoot(int shoot, int cas, int plyID, int GameID)
        {
            ShootCmnd P;
            P.Points = shoot;
            P.Cas = cas;
            P.PlyID = plyID;
            P.GameID = GameID;
            ShootsToBD.Enqueue(P);
        }


        //Pensaba que el usar una transacción para agrupar varios tiros iba a ser mejor para la velocidad, pero al parecer no influye mucho
        private void UpdateScore()
        {
            while (UpdScoreActivated)
            {
                int Q = ShootsToBD.Count();
                DateTime now = MyClock.getDateTime();

                if (Q > 0)
                {
                    SqlCommand SQLcommand = cnnAux.CreateCommand();
                    SqlTransaction SQLtransaction;
                    cnnAux.Open();
                    //Start a local transaction.
                    SQLtransaction = cnnAux.BeginTransaction("UpdateScoreTable"); //No creo que el nombre sea importante
                    
                    // Must assign both transaction object and connection
                    // to Command object for a pending local transaction
                    //SQLcommand.Connection = cnnAux;  //creo que no hace falta por se creó a partir de la conexión
                    SQLcommand.Transaction = SQLtransaction;
                    ShootCmnd P;

                    while (Q > 0) //Deberia usar try and catch, pero intentando ganar en performance prescindi de ellos
                    {
                        //while (!ShootsToBD.TryDequeue(out P)) { }
                        ShootsToBD.TryDequeue(out P);
                        //string sql = "UPDATE Score SET " + CasToStr(P.Cas) + " = " + P.Points + " WHERE PlayerID = " + P.PlyID + " and GameID = " + P.GameID;
                        SQLcommand.CommandText = "UPDATE Score SET Shots = Shots + 1, " + CasToStr(P.Cas) + " = " + P.Points + " WHERE PlayerID = " + P.PlyID + " and GameID = " + P.GameID;
                        //SqlCommand SQLcommand = new SqlCommand(sql, cnnAux);
                        SQLcommand.ExecuteNonQuery();
                        Q--;
                    }
                    // Attempt to commit the transaction.  //Dberia capturar si hubo alguna excepcion y si es asi hacer un rollback
                    SQLtransaction.Commit();
                    cnnAux.Close();
                }//if (Q > 0)

                DateTime after = MyClock.getDateTime();
                int ms_elapsed = (int)(after - now).TotalMilliseconds;
                //const int delay_time = 5000;
                const int delay_time = 2500;
                if (ms_elapsed < delay_time)
                    Thread.Sleep(delay_time - ms_elapsed);  //Esperando a acumular algunos tiros para luego actualizar el score
                //else
                    //Thread.Sleep(5000);
            }
        }

        public void StartScoreUpdater()
        {
            if (!UpdScoreActivated)
            {
                UpdScoreActivated = true;
                ScoreUpdater.Start();
            }
        }

        public void StopScoreUpdater()
        {
            UpdScoreActivated = false;
            if (ScoreUpdater != null)
                if (ScoreUpdater.IsAlive)
                    ScoreUpdater.Abort();
        }

        private string CasToStr(int cas)
        {
            switch (cas)
            {
                case 0: return "[1_1]";
                case 1: return "[1_2]";
                case 2: return "[2_1]";
                case 3: return "[2_2]";
                case 4: return "[3_1]";
                case 5: return "[3_2]";
                case 6: return "[4_1]";
                case 7: return "[4_2]";
                case 8: return "[5_1]";
                case 9: return "[5_2]";
                case 10: return "[6_1]";
                case 11: return "[6_2]";
                case 12: return "[7_1]";
                case 13: return "[7_2]";
                case 14: return "[8_1]";
                case 15: return "[8_2]";
                case 16: return "[9_1]";
                case 17: return "[9_2]";
                case 18: return "[10_1]";
                case 19: return "[10_2]";
                case 20: return "[10_3]";
                default:
                    return "";  //Sirve para que lance excepciones
                    //return "[10_3]"; //Esta es mejor, dejo la otra mientras se "ajusta..." jajjajajja
            }
        }

        public void AssignProgressBar(SetProgressCBack pBar)
        {
            showStartProgress = pBar;
        }


        //Metodo a ser llevado acabo por un hilo independiente
        //De vez en cuando lanza una excpecion de TimeOut
        //Ya aumente el tiempo de TimeOut de esta conexion, creo que este hilo debe usar un cliente diferente
        //al de las demas conexiones, a lo mejor dejar a sa con este hilo y preparar otro cliente para las demas conexiones
        //De hecho creo que voy a usar usuarios o logins diferentes para cada conexion
        //Hastahora son tres, una encragada de popular la BD, otra de actualizar el score, y la otra es la que abre/modifica/cancela/tranfriere juego etc.
        //No me ha vuelto a dar el error, desde que arreglé que la tabla team no se actualizara completamente, pero puede no haber sido eso, seguiremos probando y veremos que pasa y como podemos arreglarlo

        //Ojooooo: Puse los TimeOut de los SqlBulkCopy a 0, lo que puede significar que ya no se lance la excepcion y tendria el peligro potencial de que la aplicacion se rompa por otro lado.
        //Existen dos variantes aqui, las mas faciles que veo son: 1 - no usar bulk copy para insertar o 2 -atrapar la excepcion
        //y volver a lanzar el hilo con su tarea de popular la BD.
        //tengo que fijarme si en la maquina del TK sale esta excpepcion

        //((((Ojooooo))) finalmente en produccion sera mejor quitar el timeouy infinot en bulkcopy y poner un valor moderado, y atrapar la excepcion como ya se habia comentado
        private void BDpopulate()
        {
            populatingBD = true;  

            int ctdPistas = 20;
            int Qplayers = 12;

            AvailablesDiscount rows;
            while (CosumedBDrows.TryDequeue(out rows))
            {
                gamesAvailables -= rows.games;
                playersAvailables -= rows.players;
                scoresAvailables -= rows.scores;
                teamsAvailables -= rows.teams;
            }

            if (fstTimePopulating && showStartProgress != null)
                showStartProgress(5); //Terminó de actualizar los números de disponipilidad (games, players, scoresAvailables)

            //Games
            if (gamesAvailables < ctdPistas)
            {
                for (int i = gamesAvailables; i < ctdPistas; i++)
                {
                    GameIMG.Rows.Add(GameIMG.NewRow());
                }
                //gamesAvailables = ctdPistas;
                bulkC_Game.WriteToServer(GameIMG);
                gamesAvailables = ctdPistas;
                GameIMG.Clear();
            }
            if (fstTimePopulating && showStartProgress != null)
                showStartProgress(5);  //progreso del 5% relativo al estado anterior
            
            //Players
            if (playersAvailables < ctdPistas * Qplayers)
            {
                for (int i = playersAvailables; i < ctdPistas * Qplayers; i++)
                {
                    PlayerIMG.Rows.Add(PlayerIMG.NewRow());
                }
                //playersAvailables = ctdPistas * Qplayers;
                bulkC_Player.WriteToServer(PlayerIMG);
                playersAvailables = ctdPistas * Qplayers;
                PlayerIMG.Clear();
            }
            if (fstTimePopulating && showStartProgress != null)
                showStartProgress(20); //progreso del 20% relativo al estado anterior

            //Scores
            if (scoresAvailables < ctdPistas * Qplayers)
            {
                for (int i = scoresAvailables; i < ctdPistas * Qplayers; i++)
                {
                    ScoreIMG.Rows.Add(ScoreIMG.NewRow());
                }
                //scoresAvailables = ctdPistas * Qplayers;
                bulkC_Score.WriteToServer(ScoreIMG);
                scoresAvailables = ctdPistas * Qplayers;
                ScoreIMG.Clear();
            }
            if (fstTimePopulating && showStartProgress != null)
                showStartProgress(20); //progreso del 20% relativo al estado anterior

            //Teams
            if (teamsAvailables < ctdPistas)
            {
                for (int i = teamsAvailables; i < ctdPistas; i++)
                {
                    TeamIMG.Rows.Add(TeamIMG.NewRow());
                }
                //teamsAvailables = ctdPistas;
                bulkC_Team.WriteToServer(TeamIMG);
                teamsAvailables = ctdPistas;
                TeamIMG.Clear();
            }
            if (fstTimePopulating && showStartProgress != null)
                showStartProgress(5);   //progreso del 5% relativo al estado anterior

            //A lo mejor en lo que se hacían estas acciones se invocó de nuevo a CheckBDavailability
            if (CosumedBDrows.Count > 0)
                BDpopulate();

            lock (lockBDpopulate)
            {
                cnnAuxPopulate.Close();
                //cnn.Close();  ///probando
                populatingBD = false;
            }
        }

        //public void CheckBDavailability(AvailablesDiscount rowsQ)
        //Se cambia el nombre por uno más adecuado a lo que hace el método, además pronto seguro se implementará un método encargado de chequear
        //la disponibilidad de la BD no de popularla.
        public void RequestFillBD(AvailablesDiscount rowsQ)
        {
            if (SQLpopulate_connect())
            //if (SQLconnect())  //probando
            //SQLconnect(); //probando
            {
                if (fstTimePopulating) //Si es la primera vez que se ejecuta este metodo,buscar la información en la BD
                {
                    SqlCommand SQLcommand = cnnAuxPopulate.CreateCommand();
                    //SqlCommand SQLcommand = cnn.CreateCommand();  //probando

                    //Game Table Info State , Availability and current ID
                    SQLcommand.CommandText = "Select Count(*) from Game where Estado = 'available'";
                    gamesAvailables = (int)SQLcommand.ExecuteScalar();
                    SQLcommand.CommandText = "Select IDENT_CURRENT('Game')";
                    currentGameID = Convert.ToInt32(SQLcommand.ExecuteScalar()) - gamesAvailables + 1;

                    //Player Table Info State , Availability and current ID
                    SQLcommand.CommandText = "Select Count(*) from Player where Estado = 'available'";
                    playersAvailables = (int)SQLcommand.ExecuteScalar();
                    SQLcommand.CommandText = "Select IDENT_CURRENT('Player')";
                    currentPlayerID = Convert.ToInt32(SQLcommand.ExecuteScalar()) - playersAvailables + 1;

                    //Score Table Info State , Availability 
                    SQLcommand.CommandText = "Select Count(*) from Score where Estado = 'available'";
                    scoresAvailables = (int)SQLcommand.ExecuteScalar();

                    //Team Table Info State , Availability and current ID
                    SQLcommand.CommandText = "Select Count(*) from Game where Estado = 'available'";
                    teamsAvailables = (int)SQLcommand.ExecuteScalar();
                    SQLcommand.CommandText = "Select IDENT_CURRENT('Team')";
                    currentTeamID = Convert.ToInt32(SQLcommand.ExecuteScalar()) - teamsAvailables + 1;
                    //
                    BDpopulate(); 
                    fstTimePopulating = false;
                }
                else
                {
                    //Este bloqueo solo se activa antes de salir de BDpopulate
                    //Lo que se quiere aqui es que si ya BDpopulate esta terminado sea captado en : if(!populating)
                    //y no deje de lanzarse el hilo encargado de popular la BD de datos con las cantidades solicitadas
                    lock (lockBDpopulate)
                    {
                        CosumedBDrows.Enqueue(rowsQ);
                        if (!populatingBD) //Si no se está ejecutando la lanzo.
                        {
                            //CosumedBDrows.Enqueue(rowsQ);
                            if (BDpopulateThread != null)
                                if (BDpopulateThread.IsAlive)
                                    BDpopulateThread.Join();

                            BDpopulateThread = new Thread(BDpopulate);
                            BDpopulateThread.Start();
                        }
                    }
                }
            }
            else
                MessageBox.Show("Data Base connection error", "BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        //Creado medio como tirando piedra, para ver si desaparece la excepcion en BDpopulate()
        //De todas formas no viene mal
        //Le estoy tirando a la teoría de que si se cierra la app, en private void MainViewFrm_FormClosing(object sender, FormClosingEventArgs e)
        //se cierran los recursos o se liberan como las conexiones, en este caso la que usa BDpopulate(): cnnAuxPopulate
        //y entonces cuando se queda el hilo por otro lado tratando de usar la conexion, se queda embarcado.. jjjajaja, crucemos los dedos...
        public void StopPopulateBD()
        {
            while(populatingBD)
            {
                Thread.Sleep(500);
            }
        }


        public bool CheckPendingShoots(int gameID)
        {
            var snapshotQueue = ShootsToBD.GetEnumerator();
            //foreach(var item in snapshotQueue)
            /* The enumerator is positioned before the first element in the collection, 
             * immediately after the enumerator is created. 
             * MoveNext must be called to advance the enumerator to the first element of the collection 
             * before reading the value of Current. */
            while(snapshotQueue.MoveNext())
            {
                ShootCmnd shoot = snapshotQueue.Current;
                if (shoot.GameID == gameID)
                    return true;  //hay tiros de este juego (con este gameID) pendientes de ser procesados
            }
            return false;  //no hay tiros de este juego (con este gameID) pendientes de ser procesados
        }

        public bool CheckBDavailability(AvailablesDiscount rowsQ)
        {
            if (rowsQ.games <= gamesAvailables && rowsQ.players <= playersAvailables && rowsQ.scores <= scoresAvailables && rowsQ.teams <= teamsAvailables)
                return true;
            else
                return false;
        }



    }//class TLanesManager


}//namespace SimpleAdmin



//Old Code: 

/*   //Old Way to OpenGame
        public void OpenGame(int laneIDX, Team playersTeam)
        {
            if (SQLconnect())
            {
                SqlCommand command;
                String sql = "";

                //Insertando nuevo equipo:
                sql = "Insert Team (Nombre, CtdJugadores) Values (@Name, @QtyPlys)";
                command = new SqlCommand(sql, cnn);
   
                command.Parameters.AddWithValue("@Name", playersTeam.TeamName);
                command.Parameters.AddWithValue("@QtyPlys", playersTeam.Qty);
                command.ExecuteNonQuery(); //BD access
                //Obteniendo el TeamID:
                sql = "Select IDENT_CURRENT('Team') from Team";
                command.CommandText = sql;  
                int teamID = Convert.ToInt32(command.ExecuteScalar()); //BD access

                for (int i = 0; i < playersTeam.Qty; i++)
                {
                    DataRow dr = PlayerIMG.NewRow();
                    dr["Nombre"] = playersTeam[i].Name;
                    dr["Bumpers"] = playersTeam[i].Bumpers;
                    dr["TeamID"] = teamID;
                    dr["Orden"] = i;

                    PlayerIMG.Rows.Add(dr);
                }
                bulkC_Player.WriteToServer(PlayerIMG);  //salvando todo de un palo "BULK" 

                //Obteniendo los PlayerID:
                sql = "Select IDENT_CURRENT('Player') from Player";
                command.CommandText = sql;
                int finalPlayerID = Convert.ToInt32(command.ExecuteScalar()); //BD access

                for (int i = finalPlayerID, j = playersTeam.Qty - 1; j >= 0; i--, j--)
                    playersTeam[j].BDplyID = i;

                //Insertando Juego
                sql = "Insert Game (TeamID, LaneID, Estado, Variante) Values (@teamID, @laneID, @estado, @variante)";
                command.CommandText = sql;
                command.Parameters.AddWithValue("@teamID", teamID);
                command.Parameters.AddWithValue("@laneID", laneIDX + 1);
                command.Parameters.AddWithValue("@estado", "activo");
                command.Parameters.AddWithValue("@variante", "classic");
                command.ExecuteNonQuery(); //BD access

                //Obteniendo los GameID:
                sql = "Select IDENT_CURRENT('Game') from Game";
                command.CommandText = sql;
                int GameID = Convert.ToInt32(command.ExecuteScalar()); //BD access
                playersTeam.BDCurrGameID = GameID;

                //Ahora también voy a crear los renglones para el score
                for (int i = 0; i < playersTeam.Qty; i++)
                {
                    DataRow dr = ScoreIMG.NewRow();
                    dr["PlayerID"] = playersTeam[i].BDplyID;
                    dr["GameID"] = GameID;
                    dr["Shots"] = 0;

                    ScoreIMG.Rows.Add(dr);
                }
                bulkC_Score.WriteToServer(ScoreIMG);  //salvando todo de un palo "BULK" 

                //Finally
                cnn.Close();
                //
                PlayerIMG.Clear();
                ScoreIMG.Clear();
            }
            else
                MessageBox.Show("Data Base connection error","BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        */  //End Old Way to OpenGame //el metodo ha sido comentareado y remplazado por otro

//Abriendo juego, usaba adapter y no creo que haga falta
/*
public void OpenGame(int laneIDX, Team playersTeam)
{
if (SQLconnect())
{
SqlCommand command;
SqlDataAdapter adapter = new SqlDataAdapter();
String sql = "";

//Insertando nuevo equipo:
sql = "Insert Team (Nombre, CtdJugadores) Values (@Name, @QtyPlys)";
command = new SqlCommand(sql, cnn);
adapter.InsertCommand = command;
adapter.InsertCommand.Parameters.AddWithValue("@Name", playersTeam.TeamName);
adapter.InsertCommand.Parameters.AddWithValue("@QtyPlys", playersTeam.Qty);
adapter.InsertCommand.ExecuteNonQuery(); //BD access
//command.Dispose();
//Obteniendo el TeamID:
sql = "Select IDENT_CURRENT('Team') from Team";
command = new SqlCommand(sql, cnn);
int teamID = Convert.ToInt32(command.ExecuteScalar()); //BD access
//command.Dispose(); //probando  //trabaja mas rapido me parece cdo comentareo aqui


for (int i = 0; i < playersTeam.Qty; i++)
{
    DataRow dr = PlayerIMG.NewRow();
    dr["Nombre"] = playersTeam[i].Name;
    dr["Bumpers"] = playersTeam[i].Bumpers;
    dr["TeamID"] = teamID;
    dr["Orden"] = i;

    PlayerIMG.Rows.Add(dr);
}
objbulk.WriteToServer(PlayerIMG);  //salvando todo de un palo "BULK" 

//Obteniendo los PlayerID:
sql = "Select IDENT_CURRENT('Player') from Player";
command = new SqlCommand(sql, cnn);
int finalPlayerID = Convert.ToInt32(command.ExecuteScalar()); //BD access
//command.Dispose();  //probando

for (int i = finalPlayerID, j = playersTeam.Qty - 1; j >= 0; i--, j--)
    playersTeam[j].BDplyID = i;

//Insertando Juego
sql = "Insert Game (TeamID, LaneID, Estado, Variante) Values (@teamID, @laneID, @estado, @variante)";
command = new SqlCommand(sql, cnn);
adapter.InsertCommand = command;
adapter.InsertCommand.Parameters.AddWithValue("@teamID", teamID);
//adapter.InsertCommand.Parameters.AddWithValue("@laneID", laneIDX);
adapter.InsertCommand.Parameters.AddWithValue("@laneID", laneIDX + 1);
adapter.InsertCommand.Parameters.AddWithValue("@estado", "activo");
adapter.InsertCommand.Parameters.AddWithValue("@variante", "classic");
adapter.InsertCommand.ExecuteNonQuery(); //BD access
//command.Dispose();

//Finally
cnn.Close();
//
PlayerIMG.Clear();
}
else
MessageBox.Show("Data Base connection error","BD_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

}
*/
//Insertando jugadores: (Ahora lo hago con bulk)
//Se debería comprobar que hay jugadores antes de decirle a alguna pista que han abierto juego pero bueno...(se esta haciendo antes de llamar a este método)
//sql = "Insert Player (Nombre, Bumpers, TeamID, Orden) Values (@Name, @Bumpers, @teamID, @orden)";
/*
for (int i = 0; i < playersTeam.Qty; i++)
{
command = new SqlCommand(sql, cnn);
adapter.InsertCommand = command;
//
adapter.InsertCommand.Parameters.AddWithValue("@Name", playersTeam[i].Name);
adapter.InsertCommand.Parameters.AddWithValue("@Bumpers", playersTeam[i].Bumpers);
adapter.InsertCommand.Parameters.AddWithValue("@teamID", teamID);
adapter.InsertCommand.Parameters.AddWithValue("@orden", i);
adapter.InsertCommand.ExecuteNonQuery();
//
command.Dispose();
}
*/

//Existe una conection pool
/*
void SQLdisconnect()
{
    if (connToBD)
    {
        cnn.Close();
        connToBD = false;
    }
}
*/

/*
public void SaveShoot(int shoot, int cas, int plyID)
{

}
*/
//public void SaveShoot(string shoot, string cas, string plyID)
//public void SaveShoot(string shoot, int cas, string plyID, int GameID)
/*
public void SaveShoot(int shoot, int cas, int plyID, int GameID)
{
    //shoot = "hello";
    if(SQLconnect())
    {
        SqlCommand command;
        String sql = "";

        //sql = "UPDATE Score SET " + CasToStr(cas) + " = @points WHERE PlayerID = @plyid and GameID = @gameid";  //variante 1  --- OK
        sql = "UPDATE Score SET " + CasToStr(cas) + " = " + shoot + " WHERE PlayerID = " + plyID + " and GameID = " + GameID;  //variante2  --- OK
        command = new SqlCommand(sql, cnn);
        /*  //comentareado
        command.Parameters.AddWithValue("@points", shoot);  //viene con la variante 1
        command.Parameters.AddWithValue("@plyid", plyID);
        command.Parameters.AddWithValue("@gameid", GameID);
        */  //comentareado

/*
    command.ExecuteNonQuery();
    //Finally
    cnn.Close();
}
}
*/
