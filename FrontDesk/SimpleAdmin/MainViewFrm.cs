using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Sha;
using System.Drawing.Drawing2D;
using System.Threading;

namespace SimpleAdmin
{
    public partial class MainViewFrm : Form
    {

        //private BroculosDrawing customCtrlTest;
        //LaneCard test;
        EditGameFrm EditGame_wnd;
        //public delegate void ShowLanePtr(int i);
        //public ShowLanePtr myDelegate;
        //
        //const int board_width = 5;
        //const int board_height = 5;
        //LaneCard[] Lanes;
        LanesBoard lanesBoard;
        int selected_lane = -1;
        //
        //MultiClientServer BossServer;
        MultiClientServer lanesServer;
        //
        TClock myDeskWatch;
        //
        //BDManager BolCtrl;
        BDManager bdAdmin;
        bool appClosing = false;
        //
        ProgressDialog mainProgDiag;

        TransferForm transferDialog;

        public MainViewFrm()
        {
            InitializeComponent();
            //------------------------------------------
            int board_width = 5;
            Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["cols"], out board_width);
            int board_boxes = 25;
            Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["boxes"], out board_boxes);
            lanesBoard = new LanesBoard(CardsBoard, CardClick, board_width, board_boxes);
            //--------------------------------------
            EditGame_wnd = new EditGameFrm();
            //
            lanesServer = new MultiClientServer(ProccessMSG);
            //
            //BolCtrl = new TLanesManager();
            //
            myDeskWatch = new TClock();
            bdAdmin = new BDManager(myDeskWatch);
            //
            mainProgDiag = new ProgressDialog();

            transferDialog = new TransferForm();

            myDeskWatch.Start();
            //
            serverAddressLabel.Text += MultiClientServer.GetLocalIPAddress();
            //
            myDeskWatch.AttachDateLabel(dateLabel, this);
            myDeskWatch.AttachTimeLabel(timeLabel, this);
            bdAdmin.StartScoreUpdater();
        }

        private void CardClick(object sender, EventArgs e)
        {
            int idx;
            int.TryParse(((MaterialSkin.Controls.MaterialCard)sender).Name, out idx);
            if (idx - 1 != selected_lane)
            {
                lanesBoard[idx - 1].Select();
                if (selected_lane != -1)
                {

                    lanesBoard[selected_lane].DeSelect();
                }
                selected_lane = idx - 1;
                LaneDetailsLabel.Text = "Pista " + ((MaterialSkin.Controls.MaterialCard)sender).Name;
                //
                BttnsRules();
            }//if (idx - 1 != selected_lane)
        }//end of CardClick

        //Este método será llamado por cualquiera de los hilos que atienden a los clientes
        //Se tiene precaución al llamar a BttnsRules() que trabaja sobre varios controles de este formulario pues pertenecen al hilo principal 
        void ProccessMSG(int ID, string msg)
        {
            if (!appClosing)
            {
                if (ID != -1)
                {
                    if (msg == "conected")
                    {
                        //Lanes[ID].Connected = true;
                        lanesBoard[ID].Connected = true;
                        if (selected_lane == ID)
                        {
                            if (this != null)
                                if (!this.IsDisposed)
                                {
                                    if (this.InvokeRequired)
                                        this.Invoke(new MethodInvoker(BttnsRules));
                                    else
                                        BttnsRules();
                                }
                        }
                    }
                    else if (msg == "disconected")
                    {
                        //Lanes[ID].Connected = false;
                        lanesBoard[ID].Connected = false;
                        if (selected_lane == ID)
                        {
                            if (this != null)
                                if (!this.IsDisposed)
                                {
                                    if (this.InvokeRequired)
                                        this.Invoke(new MethodInvoker(BttnsRules));
                                    else
                                        BttnsRules();
                                }
                        }
                    }
                    /*
                    else if(msg == "K")
                    {

                    }
                    */
                    else if(msg != "K") //Sino se trata del msg de Keep Alive
                    {
                        int f = msg.IndexOf(':');
                        string aux = msg.Substring(0, f);
                        string aux2 = msg.Substring(f + 1);
                        int plyidBD;
                        int cas; //casilla
                        int points; //puntos
                        string plyidBDstr, casStr, pointsStr;

                        if(aux == "s") //Están enviando el valor de un tiro (s: shoot) realizado
                        {
                            f = aux2.IndexOf(':');
                            aux = aux2.Substring(0, f);
                            aux2 = aux2.Substring(f + 1);
                            //plyidBD = 
                            plyidBDstr = aux;
                            int.TryParse(aux, out plyidBD);
                            f = aux2.IndexOf(':');
                            aux = aux2.Substring(0, f);
                            aux2 = aux2.Substring(f + 1);
                            pointsStr = aux;
                            int.TryParse(aux, out points);
                            //
                            casStr = aux2;
                            int.TryParse(aux2, out cas);
                            //BolCtrl.SaveShoot(points, cas, plyidBD, Lanes[ID].LaneTeam.BDCurrGameID);
                            bdAdmin.SaveShoot(points, cas, plyidBD, lanesBoard[ID].LaneTeam.BDCurrGameID);
                        }
                    }
                }
            }//appClosing
        }


        //
        private void BttnsRules()
        {
            //if (Lanes[selected_lane].Connected)
            if (lanesBoard[selected_lane].Connected)
            {
                //if (Lanes[selected_lane].LaneState == LaneStates.Free)
                if (lanesBoard[selected_lane].LaneState == LaneStates.Free)
                {
                    OpenGameBttn.Text = "Abrir Juego";
                    OpenGameBttn.Enabled = true;
                    TransferGameBttn.Enabled = false;
                    DeActBttn.Enabled = true;
                    DeActBttn.Text = "Desactivar Pista";
                    CancelGameBttn.Enabled = false;
                }
                //else if (Lanes[selected_lane].LaneState == LaneStates.ByGame || Lanes[selected_lane].LaneState == LaneStates.ByTime)
                else if (lanesBoard[selected_lane].LaneState == LaneStates.ByGame || lanesBoard[selected_lane].LaneState == LaneStates.ByTime || lanesBoard[selected_lane].LaneState == LaneStates.Unlimited)
                {
                    OpenGameBttn.Text = "Modificar Juego";
                    OpenGameBttn.Enabled = true;
                    TransferGameBttn.Enabled = true; //added
                    DeActBttn.Enabled = false;
                    CancelGameBttn.Enabled = true;
                }
                else
                {
                    if (lanesBoard[selected_lane].LaneState == LaneStates.Disabled)
                    {
                        DeActBttn.Enabled = true;
                        DeActBttn.Text = "Activar Pista";
                    }
                    OpenGameBttn.Enabled = false;
                    TransferGameBttn.Enabled = false;
                    CancelGameBttn.Enabled = false;
                }
            }
            else //la pista no está conectada
            {
                //if (Lanes[selected_lane].LaneState == LaneStates.ByGame || Lanes[selected_lane].LaneState == LaneStates.ByTime)
                if (lanesBoard[selected_lane].LaneState == LaneStates.ByGame || lanesBoard[selected_lane].LaneState == LaneStates.ByTime || lanesBoard[selected_lane].LaneState == LaneStates.Unlimited)
                {
                    //OpenGameBttn.Text = "Modificar Juego";  //Pensándolo mejor, no quiero modificar un juego en una pista que no etsa conectada, pues no tendra efecto inmediato y para que complicarse...
                    //OpenGameBttn.Enabled = true;
                    TransferGameBttn.Enabled = true; //added
                    DeActBttn.Enabled = false;
                }
                else
                {
                    if(lanesBoard[selected_lane].LaneState == LaneStates.Disabled)
                    {
                        DeActBttn.Enabled = true;
                        DeActBttn.Text = "Activar Pista";
                    } 
                    else
                    {
                        DeActBttn.Enabled = true;
                        DeActBttn.Text = "Desactivar Pista";
                    }

                    //OpenGameBttn.Enabled = false;
                    TransferGameBttn.Enabled = false;
                }
                OpenGameBttn.Enabled = false; //Boton de abir y modificar juego, prefiri que simpre q no este conectado, deshabilitarlo
                CancelGameBttn.Enabled = false; //mas o menos las misma filosofia anterior
            } //else (no está conectada)

        }
        //
        private void OpenGameBttn_Click(object sender, EventArgs e)
        {
            EditGame_wnd.Text = "Pista " + (selected_lane + 1) + " - " + OpenGameBttn.Text + "...";   //Abrir / Modificar ...
            EditGame_wnd.Clear();
            EditGame_wnd.LoadTeam(lanesBoard[selected_lane].LaneTeam);

            if (OpenGameBttn.Text == "Modificar Juego")
            {
                //lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "EditGame");  //mejor hacerlo en el background

                Thread doWork = new Thread(ReadGameBckg);
                doWork.Start();
                mainProgDiag.SetInfo("Accediendo a juego en Pista " + (selected_lane + 1) + " ...");
                mainProgDiag.SetProgress(25);
                mainProgDiag.ShowDialog();  //Barra de progreso...
                EditGame_wnd.modifying = true;
                DialogResult choose1 = EditGame_wnd.ShowDialog();
                if (choose1 == DialogResult.OK)
                {
                    Thread doWork2 = new Thread(ModifyGameBckg);
                    doWork2.Start();
                    mainProgDiag.SetInfo("Modificando juego en Pista " + (selected_lane + 1) + " ...");
                    mainProgDiag.SetProgress(25);
                    mainProgDiag.ShowDialog();
                    lanesBoard[selected_lane].UpdateUnlimitedInfo();
                }
                else
                    lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "ContinueBowling");
            }
            else  //-- Abrir juego ---
            {
                EditGame_wnd.modifying = false;
                DialogResult choose = EditGame_wnd.ShowDialog();

                if (choose == DialogResult.OK)
                {
                    //Salvar la copia, pues LoadTeam hace que el form trabaje con una copia
                    lanesBoard[selected_lane].LaneTeam.Copy(EditGame_wnd.TheTeam);
                    //Abrir Juego entonces ya el Team de la LaneCard, está con la información correcta.
                    if (lanesBoard[selected_lane].LaneTeam.Qty > 0)
                    {

                        Thread doWork = new Thread(OpenGameBckg);
                        doWork.Start();
                        mainProgDiag.SetInfo("Abriendo juego en Pista " + (selected_lane + 1) + " ...");
                        mainProgDiag.SetProgress(25);
                        mainProgDiag.ShowDialog();
                        //lanesBoard[selected_lane].LaneState = LaneStates.ByGame;
                        lanesBoard[selected_lane].LaneState = LaneStates.Unlimited;
                        BttnsRules(); //Abrir -> Modificar
                    }
                }
            }//-- Abrir Juego ---
        }// private void OpenGameBttn_Click(object sender, EventArgs e)

        private void OpenGameBckg()
        {
            bdAdmin.OpenGame(selected_lane + 1, lanesBoard[selected_lane].LaneTeam);
            lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "OpenGame"); //Sending OpenGame comand to the target lane
            CloseProgressForm();
        }

        private void ModifyGameBckg()
        {
            bdAdmin.ModifyGame(EditGame_wnd.TheTeam);
            lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "RetrieveGame"); //Sending RetrieveGame comand to the target lane
            lanesBoard[selected_lane].LaneTeam.Copy(EditGame_wnd.TheTeam);
            CloseProgressForm();
        }

        //Se encargará de leer el juego en la BD, realmente lo único que hará es acceder para leer 
        //y guardar la ctd de tiros realizados por cada jugador
        private void ReadGameBckg()
        {
            lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "EditGame");
            //Thread.Sleep(500);  //Esperando a que la pista reciva el cmnd, lo mejor sería esperar hasta obtener un acuse de recibo
            //Comentareado para ganar en rapidez, pero valorar descomentarear luego, a lo mejor con un menor delay
            //o tal vez hacerlo antes de mostrar la barra de progreso, para que la percepcion de demora no  sea tanta


            while (bdAdmin.CheckPendingShoots(lanesBoard[selected_lane].LaneTeam.BDCurrGameID)) //Esperando porq no hayan tiros pendientes de la pista en cuestión
            {
                Thread.Sleep(500);
            }
            mainProgDiag.SetRelativeProgress_async(25);
            //bdAdmin.ReadGame(lanesBoard[selected_lane].LaneTeam);
            bdAdmin.ReadGame(EditGame_wnd.TheTeam);
            //Buscar jugador en turno:
            EditGame_wnd.TheTeam.Sync(); //Este método seterará la la variable enTurno a true del jugador que corresponda
            CloseProgressForm();  //Muestra el 100% completado y 100 ms despues cierra el dialogo de progreso
        }

        //...async... (Implementado arcaicamente tal vez..., but it works)
        void CloseProgressForm()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(CloseProgressForm));
            else
            {
                this.mainProgDiag.SetProgress(100);
                Thread.Sleep(100); //Dando tiempo a que se vea la barra llena
                this.mainProgDiag.Hide();
            }
        }

        private void MainViewFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            appClosing = true;
            //Mandando a parar los distintos hilos de la aplicación.
            lanesServer.Stop();
            bdAdmin.StopScoreUpdater();
            myDeskWatch.Stop();
            bdAdmin.StopPopulateBD(); //En este caso no manda a parar, espera a que pare
        }

        public void PopulateBD()
        {
            bdAdmin.RequestFillBD(new BDManager.AvailablesDiscount()); //No uso este parámetro esta vez, pero en las subsiguientes llamadas si será necesario.
        }

        public void AssignInitialProgressBar(SetProgressCBack pBar)
        {
            bdAdmin.AssignProgressBar(pBar);
        }

        //Creo que el transfer game es lo suficientemente rápido como para no necesitar un barra de progreso
        //Aunque algunas veces se pudiera demorar un tilin, por lo que la voy a implementar
        private void TransferBttn_Click(object sender, EventArgs e)
        {
            string[] lanesList = lanesBoard.GetLanesAvailablesList();
            if (lanesList != null)
            {
                lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "EditGame");
                transferDialog.SetLanes(lanesList);
                DialogResult choose = transferDialog.ShowDialog(); 
                if(choose == DialogResult.OK)
                {
                    Thread doWork = new Thread(TransferGameBckg);
                    doWork.Start();
                    mainProgDiag.SetInfo("Transfiriendo juego a la Pista " + transferDialog.laneIDresult + " ...");
                    mainProgDiag.SetProgress(25);  //progreso absoluto
                    mainProgDiag.ShowDialog();

                    LaneStates prevState = lanesBoard[selected_lane].LaneState;

                    if (transferDialog.LeftDesactivate())
                        lanesBoard[selected_lane].LaneState = LaneStates.Disabled;
                    else
                        lanesBoard[selected_lane].LaneState = LaneStates.Free;

                    lanesBoard[transferDialog.laneIDresult - 1].LaneTeam.Copy(lanesBoard[selected_lane].LaneTeam);
                    lanesBoard[selected_lane].LaneTeam.Clear();  //limpiando info del equipo
                    lanesBoard[transferDialog.laneIDresult - 1].LaneState = prevState;

                    BttnsRules();
                }
                else
                    lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "ContinueBowling");
            }
            else
                MessageBox.Show("No hay  pistas disponibles para transferir...","Ninguna pista libre...",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void TransferGameBckg()
        {
            lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "TransferTo:" + lanesBoard[transferDialog.laneIDresult - 1].LaneID);  //Indicar que el juego ha sido tansferido a la pista mas cual...

            while (bdAdmin.CheckPendingShoots(lanesBoard[selected_lane].LaneTeam.BDCurrGameID)) //Esperando porq no hayan tiros pendientes de la pista en cuestión
            {
                Thread.Sleep(500);
            }

            mainProgDiag.SetRelativeProgress_async(25);  

            bdAdmin.TransferGameTo(lanesBoard[transferDialog.laneIDresult - 1].LaneID, lanesBoard[selected_lane].LaneTeam);
            lanesServer.sendCmnd(lanesBoard[transferDialog.laneIDresult - 1].LaneIDstr, "RetrieveGame");
            CloseProgressForm();
        }


        private void DeActBttn_Click(object sender, EventArgs e)
        {
            if(DeActBttn.Text == "Desactivar Pista")
            {
                lanesBoard[selected_lane].LaneState = LaneStates.Disabled;
            }
            else
            {
                lanesBoard[selected_lane].LaneState = LaneStates.Free;
            }
            BttnsRules();
        }

        private void CancelGameBttn_Click(object sender, EventArgs e)
        {
            Thread doWork = new Thread(CancelGameBckg);
            doWork.Start();
            mainProgDiag.SetInfo("Cancelando juego en la Pista " + lanesBoard[selected_lane].LaneID + " ...");
            mainProgDiag.SetProgress(25);
            mainProgDiag.ShowDialog();
            lanesBoard[selected_lane].LaneState = LaneStates.Free;  //No se debe hacer en otro hilo, porq tiene q ver con el paint... (se llama a invalidate)
            BttnsRules();
        }

        private void CancelGameBckg()
        {
            bdAdmin.CancelGame(selected_lane + 1, lanesBoard[selected_lane].LaneTeam);
            lanesServer.sendCmnd(lanesBoard[selected_lane].LaneIDstr, "CancelGame");
            lanesBoard[selected_lane].LaneTeam.Clear();  //limpiando info del equipo
            CloseProgressForm();
        }
    }  //Form1

    /// <span class="code-SummaryComment"><summary></span>
    /// Use this for drawing custom graphics and text with transparency.
    /// Inherit from DrawingArea and override the OnDraw method.
    /// <span class="code-SummaryComment"></summary></span>
    abstract public class DrawingArea : Panel
    {
        /// <span class="code-SummaryComment"><summary></span>
        /// Drawing surface where graphics should be drawn.
        /// Use this member in the OnDraw method.
        /// <span class="code-SummaryComment"></summary></span>
        protected Graphics graphics;

        /// <span class="code-SummaryComment"><summary></span>
        /// Override this method in subclasses for drawing purposes.
        /// <span class="code-SummaryComment"></summary></span>
        abstract protected void OnDraw();

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                cp.Style &= ~0x04000000; //WS_CLIPSIBLINGS
                cp.Style &= ~0x02000000; //WS_CLIPCHILDREN

                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Don't paint background
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Update the private member so we can use it in the OnDraw method
            this.graphics = e.Graphics;

            // Set the best settings possible (quality-wise)
            this.graphics.TextRenderingHint =
                System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.graphics.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.graphics.PixelOffsetMode =
                System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // Calls the OnDraw subclass method
            OnDraw();
        }
    }//class DrawingArea

    class BroculosDrawing : DrawingArea
    {
        protected override void OnDraw()
        {
            // Gets the image from the global resources
            //Image broculoImage = global::SimpleAdmin.Properties.Resources.icons8_change_96;
            Image broculoImage = global::SimpleAdmin.Properties.Resources.juegocruzado3;

            // Sets the images' sizes and positions
            int width = broculoImage.Size.Width;
            int height = broculoImage.Size.Height;
            Rectangle big = new Rectangle(0, 0, (int)(width * 0.55 + 0.5), (int)(height * 0.55 + 0.5));
            //Rectangle small = new Rectangle(50, 50, (int)(0.75 * width),(int)(0.75 * height));
            this.Size = new System.Drawing.Size((int)(width * 0.55 + 0.5), (int)(height * 0.55 + 0.5));
            // Draws the two images
            this.graphics.DrawImage(broculoImage, big);
            //this.graphics.DrawImage(broculoImage, small);

            //Sets the text's font and style and draws it
            float fontSize = 8.25f;
            Point textPosition = new Point(50, 100);
            //TextRenderer.DrawText("http://www.broculos.net", "Microsoft Sans Serif", fontSize
            // , FontStyle.Underline, Brushes.Blue, textPosition);
        }
    }// BroculosDrawing
}
