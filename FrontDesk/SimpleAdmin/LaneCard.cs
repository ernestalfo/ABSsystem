using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdmin
{

    enum LaneStates {ByTime, ByGame, Free, Unlimited, Disabled, None}  //Unlimited surge en la versión HomeEdition y me estoy refiriendo a líneas ilimitadas

    class LaneCard
    {
        //Miembros Privados, Controles gráficos:
        private MaterialSkin.Controls.MaterialCard Card;
        private System.Windows.Forms.Control Father;
        private System.Windows.Forms.Label QtyPlayersLabel;
        private System.Windows.Forms.Label InfoGameLabel;
        private MaterialSkin.Controls.MaterialLabel LaneIDLabel;
        private MaterialSkin.Controls.MaterialLabel LaneStateLabel;

        //para la lógica:
        private int laneID;
        //private bool conectado = true;  //testing
        private bool conectado = false; 
        LaneStates laneState = LaneStates.None;  //Para la img de fondo entre otras cosas
        bool selected = false;
        Team MyTeam;

        //Referencias (de posición y tamaño de los distintos elementos):
        public const int sizeCard_X = 181;
        public const int sizeCard_Y = 123;
        private const int locStateImg_X = 0;
        private const int locStateImg_Y = 13;
        private const int sizeStateImg_X = 181;
        private const int sizeStateImg_Y = 102;
        private const int locConnImg_X = 4;
        private const int locConnImg_Y = 89;
        private const int sizeConnImg_X = 28;
        private const int sizeConnImg_Y = 26;
        private const int sizeQtyPlayersLabel_X = 28;
        private const int sizeQtyPlayersLabel_Y = 15;
        private const int locQtyPlayersLabel_X = 143;
        private const int locQtyPlayersLabel_Y = 22;
        private const int sizeInfoGameLabel_X = 51;
        private const int sizeInfoGameLabel_Y = 15;
        private const int locInfoGameLabel_X = 121;
        private const int locInfoGameLabel_Y = 88;
        //private const int sizeLaneIDLabel_X = 24;
        private const int sizeLaneIDLabel_X = 28;
        private const int sizeLaneIDLabel_Y = 24;
        private const int locLaneIDLabel_X = 12;
        private const int locLaneIDLabel_Y = 5;
        private const int sizeLaneStateLabel_X = 106;
        private const int sizeLaneStateLabel_Y = 19;
        private const int locLaneStateLabel_X = 66;
        private const int locLaneStateLabel_Y = 98;

        //Propiedades:
        public int LaneID
        {
            get { return laneID; }
            set
            {
                laneID = value;
                LaneIDLabel.Text = laneID.ToString();
                Card.Name = LaneIDLabel.Text;
            }
        }
        //
        public string LaneIDstr
        {
            get { return "lane" + laneID; }
        }
        //
        public bool Connected
        {
            get { return conectado; }
            set
            {
                conectado = value; //Mi hilo ppal no debe modificar esta variable, al menos no ahora, lo hace al ppo si acaso, por ahora no debe existir colision con ningún hilo...
                Card.Invalidate();  //Debo consultar this.InvokeRequired???
            }
            //from StackOverFlow (User: Borja):
           /*
           Invalidate doesn't need an invoke.
           The invalidate only includes a paint message to be processed by the main thread with the rest of the pending messages. 
           But the paint is not done when you call to invalidate and the control is not changed by this thread,
           so you don't need to use an invoke for it.
           If you need to ensure that the control is refreshed,
           maybe the invalidate is not enough and you need to call to the update too.
            */

        }
        //
        public LaneStates LaneState
        {
            get { return laneState; }
            set
            {
                setState(value); //Aprovechando un metodo que habia escrito ya
                Card.Invalidate(); //added in v0.001b
            }
        }
        //
        public Team LaneTeam
        {
            get { return MyTeam; }
            set { MyTeam = value; }
        }
        //Constructor:
        public LaneCard(System.Windows.Forms.Control p)
        {
            Card = new MaterialSkin.Controls.MaterialCard();
            Card.Size = new System.Drawing.Size(sizeCard_X, sizeCard_Y);
            Card.MouseEnter += card_MouseEnter;
            Card.MouseLeave += card_MouseLeave;
            Father = p;
            Father.Controls.Add(Card);
            //
            InitializeCard();
            //
            MyTeam = new Team();
        }
        //Otro contructor:
        public LaneCard()
        {
            Card = new MaterialSkin.Controls.MaterialCard();
            Card.Size = new System.Drawing.Size(sizeCard_X, sizeCard_Y);
            Card.MouseEnter += card_MouseEnter;
            Card.MouseLeave += card_MouseLeave;
            //
            InitializeCard();
            //
            MyTeam = new Team();
        }
        //Metodos:
        public void PlaceIn(System.Drawing.Point P)
        {
            Card.Location = P;
        }
        //
        private void CardPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (Card.Visible)
            {
                System.Drawing.Image fondo;
                switch (laneState)
                {
                    case LaneStates.ByGame:
                        //fondo = global::SimpleAdmin.Properties.Resources.ByGame_Color_Players;
                        fondo = global::SimpleAdmin.Properties.Resources.ByGame_Color_Players_nSize;
                        break;
                    case LaneStates.Unlimited:
                        //fondo = global::SimpleAdmin.Properties.Resources.ByGame_Color_Players;
                        fondo = global::SimpleAdmin.Properties.Resources.ByGame_Color_Players_nSize;
                        break;
                    case LaneStates.ByTime:
                        //fondo = global::SimpleAdmin.Properties.Resources.ByTime_Colo_Players;
                        fondo = global::SimpleAdmin.Properties.Resources.ByTime_Colo_Players_nSize;
                        break;
                    case LaneStates.Free:
                        //fondo = global::SimpleAdmin.Properties.Resources.Free_Colors3;
                        fondo = global::SimpleAdmin.Properties.Resources.Free_Colors3_nSize;
                        break;
                    case LaneStates.Disabled:
                        //fondo = global::SimpleAdmin.Properties.Resources.desactivada;
                        fondo = global::SimpleAdmin.Properties.Resources.desactivada_nSize;
                        break;
                    default:
                        //fondo = global::SimpleAdmin.Properties.Resources.desactivada;
                        fondo = global::SimpleAdmin.Properties.Resources.desactivada_nSize;
                        break;
                }
                System.Drawing.Rectangle DimLoc = new System.Drawing.Rectangle(locStateImg_X, locStateImg_Y, sizeStateImg_X, sizeStateImg_Y);
                e.Graphics.DrawImage(fondo, DimLoc);
                if (!conectado)
                {
                    //System.Drawing.Image OfflineIMG = global::SimpleAdmin.Properties.Resources._128px_Warning_icon_svg;
                    System.Drawing.Image OfflineIMG = global::SimpleAdmin.Properties.Resources.warning_nSize;
                    System.Drawing.Rectangle DimLoc2 = new System.Drawing.Rectangle(locConnImg_X, locConnImg_Y, sizeConnImg_X, sizeConnImg_Y);
                    e.Graphics.DrawImage(OfflineIMG, DimLoc2);
                }
                if (selected)
                {
                    // Create a new pen.
                    System.Drawing.Pen newPen = new System.Drawing.Pen(System.Drawing.Brushes.YellowGreen);
                    // Set the pen's width.
                    newPen.Width = 6.0F;
                    // Set the LineJoin property.
                    newPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    //Draw a rectangle.
                    //e.Graphics.DrawRectangle(newPen, new System.Drawing.Rectangle(0, 0, sizeCard_X, sizeCard_Y));
                    DrawRoundedRectangle(e.Graphics, newPen, 0, 1, sizeCard_X - 1, sizeCard_Y - 2, 8, 8);
                    //Dispose of the pen.
                    newPen.Dispose();
                }

                //e.Graphics.DrawRectangle()
            }
        }
        //
        private void DrawRoundedRectangle(System.Drawing.Graphics g, System.Drawing.Pen pen, int x, int y, int w, int h, int rx, int ry)
        {
            System.Drawing.Drawing2D.GraphicsPath path  = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(x, y, rx + rx, ry + ry, 180, 90);
            path.AddLine(x + rx, y, x + w - rx, y);
            path.AddArc(x + w - 2 * rx, y, 2 * rx, 2 * ry, 270, 90);
            path.AddLine(x + w, y + ry, x + w, y + h - ry);
            path.AddArc(x + w - 2 * rx, y + h - 2 * ry, rx + rx, ry + ry, 0, 91);
            path.AddLine(x + rx, y + h, x + w - rx, y + h);
            path.AddArc(x, y + h - 2 * ry, 2 * rx, 2 * ry, 90, 91);
            path.CloseFigure();
            g.DrawPath(pen, path);
        }

        //
        public void setParent(System.Windows.Forms.Control p)
        {
            Father = p;
            Father.Controls.Add(Card);
        }
        //
        /*
        public void setLaneID(int id)
        {
            LaneID = id;
            LaneIDLabel.Text = id.ToString();
        }
        */
        //
        public void setOnClick(System.EventHandler callback)
        {
            Card.Click += callback;
        }
        //
        public void Select()
        {
            //Card.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            selected = true;
            //LaneIDLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            //LaneIDLabel.HighEmphasis = true;
           // LaneIDLabel.UseAccent = true;
            LaneIDLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            //LaneIDLabel.Invalidate(); //Actualizame papi!!!
            Card.Invalidate();
        }
        //
        public void DeSelect()
        {
            //Card.BorderStyle = System.Windows.Forms.BorderStyle.None;
            selected = false;
            //LaneIDLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            //LaneIDLabel.HighEmphasis = false;
            //LaneIDLabel.UseAccent = false;
            LaneIDLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            //LaneIDLabel.Invalidate(); //Actualizame papi!!!
            Card.Invalidate();
        }
        //
        private void setState(LaneStates newstate)
        {
            if(newstate != laneState)
            {
                laneState = newstate;
                switch (laneState)
                {
                    case LaneStates.ByGame:
                        QtyPlayersLabel.Visible = true;
                        InfoGameLabel.Visible = true;
                        LaneStateLabel.Visible = false;
                        UpdateByGameInfo();
                        break;
                    case LaneStates.ByTime:
                        QtyPlayersLabel.Visible = true;
                        InfoGameLabel.Visible = true;
                        LaneStateLabel.Visible = false;
                        break;
                    case LaneStates.Free:
                        QtyPlayersLabel.Visible = false;
                        InfoGameLabel.Visible = false;
                        LaneStateLabel.Visible = true;
                        LaneStateLabel.Text = "LIBRE";
                        break;
                    case LaneStates.Disabled:
                        QtyPlayersLabel.Visible = false;
                        InfoGameLabel.Visible = false;
                        LaneStateLabel.Visible = true;
                        LaneStateLabel.Text = "DESACTIVADA";
                        break;
                    case LaneStates.Unlimited:
                        QtyPlayersLabel.Visible = true;
                        InfoGameLabel.Visible = true;
                        //InfoGameLabel.Text = "1 / ∞";
                        LaneStateLabel.Visible = false;
                        UpdateUnlimitedInfo();
                        break;
                    default:
                        QtyPlayersLabel.Visible = false;
                        InfoGameLabel.Visible = false;
                        LaneStateLabel.Visible = true;
                        break;
                }//switch

            }//if
        }//end of ... void setState(LaneStates newstate)
        //
        private void UpdateByGameInfo()
        {
            QtyPlayersLabel.Text = MyTeam.Qty.ToString(); 
            InfoGameLabel.Text = "1 / 1"; 
        }

        //private void UpdateUnlimitedInfo()
        public  void UpdateUnlimitedInfo()
        {
            QtyPlayersLabel.Text = MyTeam.Qty.ToString();
            InfoGameLabel.Text = "1 / ∞";
        }
        public void Hide()
        {
            Card.Visible = false;
        }
        //
        public void Show()
        {
            Card.Visible = true;
        }
        //
        private void card_MouseEnter(object sender, System.EventArgs e)
        {
            if(!selected)
                LaneIDLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
        }
        //
        private void card_MouseLeave(object sender, System.EventArgs e)
        {
            if (!selected)
                LaneIDLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
        }
        //
        private void InitializeCard()
        {
            QtyPlayersLabel = new System.Windows.Forms.Label();
            InfoGameLabel = new System.Windows.Forms.Label();
            LaneIDLabel = new MaterialSkin.Controls.MaterialLabel();
            LaneStateLabel = new MaterialSkin.Controls.MaterialLabel();
            //
            QtyPlayersLabel.Size = new System.Drawing.Size(sizeQtyPlayersLabel_X, sizeQtyPlayersLabel_Y);
            QtyPlayersLabel.Location = new System.Drawing.Point(locQtyPlayersLabel_X, locQtyPlayersLabel_Y);
            //QtyPlayersLabel.AutoSize = false;  //Parece que viene en false por defecto
            QtyPlayersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            QtyPlayersLabel.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            QtyPlayersLabel.ForeColor = System.Drawing.Color.White;
            QtyPlayersLabel.BackColor = System.Drawing.Color.Black;
            QtyPlayersLabel.Text = "88"; //testing...
            //
            InfoGameLabel.Size = new System.Drawing.Size(sizeInfoGameLabel_X, sizeInfoGameLabel_Y);
            InfoGameLabel.Location = new System.Drawing.Point(locInfoGameLabel_X, locInfoGameLabel_Y);
            //InfoGameLabel.AutoSize = false;
            InfoGameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            InfoGameLabel.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            InfoGameLabel.ForeColor = System.Drawing.Color.White;
            InfoGameLabel.BackColor = System.Drawing.Color.Black;
            //InfoGameLabel.Text = "1 / 5";
            //
            LaneIDLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //LaneIDLabel.Depth = 0;
            LaneIDLabel.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            LaneIDLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            LaneIDLabel.Location = new System.Drawing.Point(locLaneIDLabel_X, locLaneIDLabel_Y);
            //LaneIDLabel.MouseState = MaterialSkin.MouseState.HOVER;
            LaneIDLabel.Size = new System.Drawing.Size(sizeLaneIDLabel_X, sizeLaneIDLabel_Y);
            //LaneIDLabel.AutoSize = false;
            //LaneIDLabel.Text = "5";
            LaneIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            LaneStateLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            LaneStateLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            LaneStateLabel.Location = new System.Drawing.Point(locLaneStateLabel_X, locLaneStateLabel_Y);
            LaneStateLabel.Size = new System.Drawing.Size(sizeLaneStateLabel_X, sizeLaneStateLabel_Y);
            //LaneStateLabel.Text = "LIBRE";
            //LaneStateLabel.Text = "DESACTIVADA";
            //LaneIDLabel.AutoSize = false;
            LaneStateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //LaneStateLabel.Enabled = false; //Vamos a empezar con esto desactivado
            //LaneStateLabel.Visible = false;
            //
            Card.Controls.Add(QtyPlayersLabel);
            Card.Controls.Add(InfoGameLabel);
            Card.Controls.Add(LaneIDLabel);
            Card.Controls.Add(LaneStateLabel);
            //
            Card.Paint += new System.Windows.Forms.PaintEventHandler(this.CardPaint);
            //Card.Click += += new System.EventHandler
        }
    }//end class LaneCard

}
