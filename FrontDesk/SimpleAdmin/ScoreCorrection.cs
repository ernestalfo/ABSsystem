using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAdmin
{
    public partial class ScoreCorrection : Form
    {
        Label[] FrameTots;
        int idxCas;
        Casilla[] casillas;
        int max_Pos_Cur = -1;   //máxima posición permitada para el cursor depende el jugador de que se trate en ese momento
        Player targetPly;
        Player backupPly;
        int wrongIDX = -1;

        Team MyTeam;
        int idxPlayer = -1;

        class Casilla
        {
            Label casLabel;
            bool isSelected;
            int idCas;
            public bool split = false;
            bool wrong = false;
            Color bckColor;

            public int ID
            {
                get
                {
                    return idCas;
                }
            }

            public string Text
            {
                get
                {
                    return casLabel.Text;
                }
                set
                {
                    casLabel.Text = value;
                }
            }

            //Para Paint:isSelected =
            System.Drawing.Pen myPen, myPenCirc, myPenW;
           
            public Casilla(Label targetLabel, int IDcas, System.EventHandler Clickcallback)
            {
                casLabel = targetLabel;
                isSelected = false;
                idCas = IDcas;
                //casLabel.Name = "SCas" + idCas;  //No es necesario, ya vienen con nombre
                casLabel.Click += Clickcallback;
                casLabel.Paint += Cas_Paint;
                //Para Paint:
                myPen = new System.Drawing.Pen(System.Drawing.Color.GreenYellow);
                myPen.Width = 3;
                myPenCirc = new System.Drawing.Pen(System.Drawing.Color.Red);
                myPenW = new System.Drawing.Pen(System.Drawing.Color.Red);
                myPenW.Width = 3;
                //
                bckColor = casLabel.BackColor;
            }

            public void Toogle()
            {
                isSelected = !isSelected;
                casLabel.Invalidate(false);
            }

            private void Cas_Paint(object sender, PaintEventArgs e)
            {
                System.Drawing.Graphics formGraphics;
                formGraphics = e.Graphics;

                // if (wrong)
                //formGraphics.DrawRectangle(myPenW, new Rectangle(1, 1, casLabel.Size.Width - 5, casLabel.Size.Height - 5));
                //else if (isSelected)
                if (isSelected)
                    formGraphics.DrawRectangle(myPen, new Rectangle(1, 1, casLabel.Size.Width - 5, casLabel.Size.Height - 5));

                if(split)
                {
                    formGraphics.DrawEllipse(myPenCirc, new Rectangle(5, 5, casLabel.Size.Width - 13, casLabel.Size.Height - 13));
                }
            }

            public void setUnderlined()
            {
                casLabel.Font = new Font(casLabel.Font.Name, casLabel.Font.Size, FontStyle.Underline);
            }

            public void setRegular()
            {
                casLabel.Font = new Font(casLabel.Font.Name, casLabel.Font.Size, FontStyle.Regular);
            }

            public bool IsUnderlinedStyle()
            {
                return casLabel.Font.Style == FontStyle.Underline;
            }

            public void Redraw()
            {
                casLabel.Invalidate(false);  //En este caso creo que el false esta demas porq el label no tiene hijos...
            }

            public void SetWrong()
            {
                wrong = true;
                casLabel.BackColor = Color.Red;
                casLabel.Invalidate(false);
            }

            public void SetRight()
            {
                wrong = false;
                casLabel.BackColor = bckColor;
                casLabel.Invalidate(false);
            }

        }//end of declaration class Casilla

        public ScoreCorrection()
        {
            InitializeComponent();
  
            casillas = new Casilla[21];
            //Asignado las etiquetas (labels) que fueron creadas con ayuda del diseñador
            casillas[0] = new Casilla(Cas1, 0, Cas_Click);
            casillas[1] = new Casilla(Cas2, 0, Cas_Click);
            casillas[2] = new Casilla(Cas3, 0, Cas_Click); 
            casillas[3] = new Casilla(Cas4, 0, Cas_Click); 
            casillas[4] = new Casilla(Cas5, 0, Cas_Click); 
            casillas[5] = new Casilla(Cas6, 0, Cas_Click); 
            casillas[6] = new Casilla(Cas7, 0, Cas_Click); 
            casillas[7] = new Casilla(Cas8, 0, Cas_Click); 
            casillas[8] = new Casilla(Cas9, 0, Cas_Click); 
            casillas[9] = new Casilla(Cas10, 0, Cas_Click); 
            casillas[10] = new Casilla(Cas11, 0, Cas_Click); 
            casillas[11] = new Casilla(Cas12, 0, Cas_Click); 
            casillas[12] = new Casilla(Cas13, 0, Cas_Click); 
            casillas[13] = new Casilla(Cas14, 0, Cas_Click); 
            casillas[14] = new Casilla(Cas15, 0, Cas_Click); 
            casillas[15] = new Casilla(Cas16, 0, Cas_Click); 
            casillas[16] = new Casilla(Cas17, 0, Cas_Click); 
            casillas[17] = new Casilla(Cas18, 0, Cas_Click); 
            casillas[18] = new Casilla(Cas19, 0, Cas_Click); 
            casillas[19] = new Casilla(Cas20, 0, Cas_Click); 
            casillas[20] = new Casilla(Cas21, 0, Cas_Click); 
            //
            FrameTots = new Label[10];
            //Asignando las etiquetas (labels) que fueron creadas con ayuda del diseñador
            FrameTots[0] = Tot1;
            FrameTots[1] = Tot2;
            FrameTots[2] = Tot3;
            FrameTots[3] = Tot4;
            FrameTots[4] = Tot5;
            FrameTots[5] = Tot6;
            FrameTots[6] = Tot7;
            FrameTots[7] = Tot8;
            FrameTots[8] = Tot9;
            FrameTots[9] = Tot10;
            //
            //Otras inicializaciones:
            idxCas = -1; //Indicará que no hay ninguna seleccionada
            //
            backupPly = new Player();
        }


        private void Cas_Click(object sender, EventArgs e)
        {
            
            int idx;
            int.TryParse(((Label)sender).Name.Substring(3), out idx);

            if (idx - 1 <= max_Pos_Cur) //Si se trata de una casilla válida
            {
                if (idxCas != -1) //Si ya existía una casilla seleccionada
                {
                    casillas[idxCas].Toogle();
                }
                idxCas = --idx;

                casillas[idxCas].Toogle();
            }

        }

        void BttnsRules()
        {
            /*
            if (idxPlayer < MyTeam.Qty - 1)
                NxtPlyBttn.Enabled = true;
            else
                NxtPlyBttn.Enabled = true;

            if (idxPlayer > 0)
                PrevPlyBttn.Enabled = true;
            else
                PrevPlyBttn.Enabled = false;
            */
            if (MyTeam.Qty > 1)
            {
                NxtPlyBttn.Enabled = true;
                PrevPlyBttn.Enabled = true;
            }
            else
            {
                NxtPlyBttn.Enabled = false;
                PrevPlyBttn.Enabled = false;
            }
        }

        //Copiando a PlayerRow::UpdatePLayer()  del proyecto de C+
        //public void LoadPlayerInfo(Player Doe)
        public void LoadPlayerInfo(Team equipo, int idxPly)
        {
            MyTeam = equipo;
            Player Doe = MyTeam[idxPly];  //Ya tenia el codigo funcionando con "Doe"
            idxPlayer = idxPly;

            LoadPlayer(Doe);

            this.Text = "Puntuación de  *** " + Doe.Name + " ***";
            if (Doe.enTurno)
                this.Text += " (JUGADOR EN TURNO)";

            //max_Pos_Cur = Doe.getMAXPOS(); //Esta bien, pero este la vuelve a calcular
            max_Pos_Cur = Doe.maxPOS;

            targetPly = Doe;

            if (idxCas != -1) //Si ya existía una casilla seleccionada
            {
                casillas[idxCas].Toogle();
                idxCas = -1;
            }
            //Creo que me estaba faltando lo siguiente:
            if (wrongIDX != -1)
            {
                casillas[wrongIDX].SetRight();
                wrongIDX = -1;
            }

            backupPly.Reset();
            BttnsRules();
        }

        void LoadPlayer(Player Doe)
        {
            ClearCas();  //añadido cdo comenzaron las pruebas de la puntuación
                         //Setting Texts:
            int j = 0;
            //for (int i = 0; i < ctd_tiros; i++)
            for (int i = 0; i < Doe.Shoots; i++)
            {
                int x = Doe.getEasyPoints(i);
                bool no_tap = false;
                if (x > 30)
                { //Strike hecho con AutoStrike o con NoTap
                    x = x - 30;
                    no_tap = true;
                }
                if (x == 10)
                {
                    j++;
                    casillas[j].Text = "X";
                }
                else if (x == 11)
                    casillas[j].Text = "/";
                else if (x == 12)
                    casillas[j].Text = "X"; //Poniendo la 'X', pero sin hacer j++ (sin mover el cursor)
                else if (x == 14) //added in BSv0.2f
                    casillas[j].Text = "F"; //added in BSv0.2f
                else if (x == 0)
                    casillas[j].Text = "-";
                else if (x > 20)
                {
                    casillas[j].split = true;
                    x = x - 20;
                    casillas[j].Text = x.ToString();
                }
                else
                    casillas[j].Text = x.ToString();

                //if (no_tap && show_notap)
                if (no_tap)
                    casillas[j].setUnderlined();
                else
                    casillas[j].setRegular();

                j++;
            }

            for (int i = 0; i < Doe.Qframes; i++)
                FrameTots[i].Text = Doe.getFTotalString(i);

            int totalsofar = Doe.getTotal(); //Este total incluye HCP en caso de que lo tenga
            totalLabel.Text = totalsofar.ToString();

            //Creo que la linea de abajo va mejor en void LoadPlayerInfo(Team equipo, int idxPly) y no aqui
            //backupPly.Reset(); //Dejando a backupPly en condicciones iniciales
        }

        void ClearCas()
        {
            for(int i = 0; i < 21; i++)
            {
                casillas[i].Text = "";
                casillas[i].split = false;
            }
            /*
            if (idxCas != -1) //Si ya existía una casilla seleccionada
            {
                casillas[idxCas].Toogle();
                idxCas = -1;
            }
            */
            for (int i = 0; i < 10; i++)
                FrameTots[i].Text = "";

        }

        //Copiando a ScoreScreen:: HandlingKeyboard(...) del proyecto de C++
        //Tuve que poner la propiedad KeyPreview d ela forma a true, para que se ejecutara este metodo...
        private void ScoreCorrection_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show($"key Pressed: '{e.KeyChar}' pressed.");
            //Se debe convertir 'x' a 'X' o preguntar por las dos variantes
            //Antender también a '-', '0' <-> '9', '/' y a Retroceso (Q se usará para borrar)
            int cas_result = 21;  // 0 <-> 20
            if (idxCas != -1) //Si existe alguna casilla seleccionada
            {
                //int number = e.KeyChar;
                if (e.KeyChar >= 0x30 && e.KeyChar <= 0x39)//'0' <-> '9'
                {
                    if (e.KeyChar != 0x30)  //Sino se trata de cero
                        setCasValue(e.KeyChar);
                    else
                        setCasValue('-');
                    cas_result = CorrectScore(); //Este metodo usa a backupPly
                }
                else if (e.KeyChar == 'x' || e.KeyChar == 'X')
                {
                    setCasValue('X');
                    cas_result = CorrectScore(); //Este metodo usa a backupPly
                }
                else if (e.KeyChar == '/' || e.KeyChar == '-')
                {
                    setCasValue(e.KeyChar);
                    cas_result = CorrectScore(); //Este metodo usa a backupPly
                }
                else if (e.KeyChar == 8)
                {
                    setCasValue('C');   //Clear casilla, borrar tiro
                    cas_result = CorrectScore(); //Este metodo usa a backupPly
                }

                if(cas_result != 21)  //Se introdujo alguna correción
                {
                    if (cas_result < 0) //Si hubo un error al actualizar
                    {
                        cas_result = Math.Abs(cas_result);
                        casillas[cas_result].SetWrong();

                        if (wrongIDX != -1 && wrongIDX != cas_result)
                            casillas[wrongIDX].SetRight();

                        wrongIDX = cas_result;

                        if (cas_result - 2 >= 0)
                            if (casillas[cas_result - 2].Text == "X")
                                cas_result = cas_result - 2;

                        int wrong_frame = cas_result / 2;
                        for (int i = wrong_frame; i < 10; i++)
                            FrameTots[i].Text = "";

                        totalLabel.Text = "";

                        backupPly.Reset(); //Se resetea por si antes de esta vez se habia realizado alguna actulizacion correctamente. 
                                            //Es que la una ctd diferende de 0 en shoots sera indicativo de una correcta actualizacion, y Reset lo q hace basicamnete es poner a shoots a 0
                    }
                    //else if (wrongIDX != -1)
                    else  //Si todo fue bien con la actualización
                    {
                        if (wrongIDX != -1)
                        {
                            casillas[wrongIDX].SetRight();
                            wrongIDX = -1;
                        }
                        LoadPlayer(backupPly);

                    }

                }

                e.Handled = true;  //dice que ya se procesó, o fue consumido el evento con las teclas que no se lo pase a los demás controles
            }
        }


        //Este viene del proyecto de C++
        void setCasValue(char c)
        {
            //Líneas debajo añadidas al añadir AutoStrike : //finalamente movidas a setCasValue(...)
            //if (!((posCURSOR / 2 + 1) % 3) && variante == GamesTypes::AutoStrike369)
                //return; // Si se trata de los innings 3, 6 o 9 retornar

            if (idxCas < 18)
            { //casillas 18, 19 y 20 del 10th inning

                //Líneas aqui debajo añadidas al agregar Low Game:
                //if (variante == GamesTypes::LowGame && c == '0')
                //if (variante == GamesTypes::LowGame && c == '-')
                // c = 'X';

                //int no_tap = Tteam::MainNoTap(variante);
                int no_tap = targetPly.noTap;

                int points = c - '0';


                if (c == 'X') //Al final tiene un uso casi de '/'|'x' depende sea 1er o 2do tiro (tecla: '/'|'x')
                {
                    if (idxCas % 2 == 0)//Se cumplirá en el primer tiro
                    {
                        //setCasAnotation(idxCas, "");  //limpiando casilla de 1er tiro
                        setCasAnotation(idxCas, '@');  //Usaré la '@' oara indicar que se debe limpiar la casilla
                        setCasAnotation(idxCas + 1, c); //ubicando en 2da casilla
                                                                          //posCURSOR++;
                        //El strike se representa en la casilla de 2do tiro del inning
                    }
                    else
                    { //Se trata del 2do tiro
                        setCasAnotation(idxCas, '/');
                        if (casillas[idxCas - 1].Text.Length == 0)
                            //Vamos a colocar '-' para no dejar la casilla del primer tiro vacía
                            setCasAnotation(idxCas - 1, '@');   //Usaré la '@' oara indicar que se debe limpiar la casilla
                    }
                }
                else if (c == '/')
                {
                    if (idxCas % 2 != 0) //Se cumplirá en el 2do tiro
                    {
                        setCasAnotation(idxCas, c); //ubicando en 2da casilla
                        if (casillas[idxCas - 1].Text.Length == 0)
                            //Vamos a colocar '-' para no dejar la casilla del primer tiro vacía
                            setCasAnotation(idxCas - 1, '-');

                    }
                }
                else if (c == 'S')
                {
                    //Poner o quitar Split
                    if (idxCas % 2 == 0 && casillas[idxCas].Text.Length != 0) //Split solo ocurre en el primer tiro
                    {
                        if (casillas[idxCas].Text.ToCharArray()[0] >= 0x30 && casillas[idxCas].Text.ToCharArray()[0] <= 0x39) //tiene que ser un número
                        {
                            //Splits[posCURSOR] = !Splits[posCURSOR];
                            casillas[idxCas].split = !casillas[idxCas].split;
                            casillas[idxCas].Redraw();
                        }
                    }
                }
                else if (c == 'C') //!!!! -- OJO, revisar ,tratar de mejorar flujo del programa
                { //Erase Ball, solo se permitirá en el último tiro
                  //max_allow_pos nos dirá hasta que casilla es posible insertar un nuevo tiro o corregir un tiro
                  //pero solo se permitirá borrar el último tiro
                  //if(posCURSOR == max_allow_pos)
                  
                    int max_tmp = max_Pos_Cur;
                    
                    //if (variante != GamesTypes::AutoStrike369)
                    //{
                        //if ((posCURSOR == max_allow_pos && !MyPlayer->get_enTurno()) || (posCURSOR == max_allow_pos - 1 && MyPlayer->get_enTurno())) //Aqui se calcula en que casilla esta el ultimo tiro
                        max_tmp = targetPly.enTurno ? max_Pos_Cur - 1 : max_Pos_Cur;
                        if (idxCas >= max_tmp)
                        {
                            setCasAnotation(idxCas, '@');   //Usaré la '@' oara indicar que se debe limpiar la casilla
                            if(idxCas == max_Pos_Cur - 1)
                                setCasAnotation(idxCas + 1, '@'); //Si es el jugador en turno se borra la casilla de l abola e turno ptambien para no dejar espacios en blanco en el score

                        }
                    //}
                    /*
                    else
                    {//variante == GamesTypes::AutoStrike369 //Se esta jugando en modo "3-6-9"...
                     //int max_tmp = max_allow_pos;
                        if (targetPly.enTurno)
                            max_tmp -= 1; //Aqui ya max_tmp se convierte en la pos donde se permitira borrar
                        if (max_tmp == 5 || max_tmp == 11 || max_tmp == 17 && !Player::getThrowBall())
                        {//... y no se lanza la bola(en la mayoria de los sistemas y configuraciones no se lanzará)
                            max_tmp = max_tmp - 2; //Como los strikes en 3-6- fueron automáticos, se le da la oportunidad de borrar dos casillas mas adelante
                            if (posCURSOR == max_tmp)
                            {
                                setCasAnotation(posCURSOR, "", max_allow_pos);
                                setCasAnotation(posCURSOR + 2, "", max_allow_pos);  //Borrando tambien el strike automatico
                            }
                            else if (posCURSOR >= max_tmp)
                                //setCasAnotation(posCURSOR, "", max_tmp);
                                setCasAnotation(posCURSOR, "", max_allow_pos);
                        }
                        //else if (posCURSOR == max_tmp)
                        else if (posCURSOR >= max_tmp)
                            //setCasAnotation(posCURSOR, "", max_tmp);
                            setCasAnotation(posCURSOR, "", max_allow_pos);
                     }
                    */
                }//else if (c == 'C') 
                else
                {//El resto de las posibilidades, miss ('-'), el Foul ('F') y los números (1 - 9)
                 //if (posCURSOR % 2 == 0 && casillas[posCURSOR + 1].getString() == String('X'))
                    if (idxCas % 2 == 0) //1er tiro
                    {
                        if (points >= no_tap && no_tap < 10)
                        {
                            setCasAnotation(idxCas, '@');  //limpiando casilla de 1er tiro
                            setCasAnotation(idxCas + 1, c); //ubicando en 2da casilla
                            //El strike se representa en la casilla de 2do tiro del inning
                            return;
                        }

                        //Se introdujo un lanzamiento en un frame que estaba puesto un strike
                        //casillas[posCURSOR + 1].setString(""); //comentareado, mejor no dejarlo vacío
                        if (casillas[idxCas + 1].Text == "X")
                            setCasAnotation(idxCas + 1, '-'); //mejor con '-'
                    }
                    else
                    {//2do tiro
                        if (casillas[idxCas - 1].Text.Length == 0)//Porq puede ser que donde quiero colocar este roll haya habido un strike y por ende la casilla de alante está vacía
                            setCasAnotation(idxCas - 1, '-');
                    }
                    setCasAnotation(idxCas, c);
                }
            }//if (posCURSOR < 18) 
            else
            { //Otras reglas para el décimo inning
                /* Aqui en el décimo inning no soy tan cuidadoso con lo que se pueda entrar o no por teclado
                   y relego más en la llamada futura a Player:: UpdateScore(...), esta función se encargará de
                   aceptar o no, lo que se le mande.
                */
                if (c == 'X')
                { //Al final tiene un uso casi de '/'|'x' depende sea 1er o 2do tiro

                    if (idxCas != 18)
                    {//Se cumplirá en las casillas 19 y 20
                     //if (casillas[posCURSOR - 1].getString() != "X") //sera tomado como un 2do tiro
                        if (casillas[idxCas - 1].Text != "X" && casillas[idxCas - 1].Text != "/") //fix V0.4
                            setCasAnotation(idxCas, '/');
                        else  //será tomado como un primer tiro
                            setCasAnotation(idxCas, c);
                    }
                    else
                        setCasAnotation(idxCas, c); //En la casilla 18

                }//if (c == 'X')
                else if (c == '/')
                {
                    if (idxCas != 18) //Se cumplirá en las casillas 19 y 20
                        if (casillas[idxCas - 1].Text != "X" && casillas[idxCas - 1].Text != "/") //sera tomado como un 2do tiro
                            setCasAnotation(idxCas, c);
                }//if(c == '/')
                else if (c == 'S')
                {
                    //Poner o quitar Split
                    //if (IsANumber(casillas[posCURSOR])) //Split solo ocurre en el primer tiro
                    if (casillas[idxCas].Text.Length != 0)
                        if (casillas[idxCas].Text.ToCharArray()[0] >= 0x30 && casillas[idxCas].Text.ToCharArray()[0] <= 0x39)
                        {
                            //Splits[posCURSOR] = !Splits[posCURSOR];
                            casillas[idxCas].split = !casillas[idxCas].split;
                            casillas[idxCas].Redraw();
                        }
                }
                else if (c == 'C')
                { //Erase Ball, solo se permitirá en el último tiro
                    if ((idxCas >= max_Pos_Cur && !targetPly.enTurno) || (idxCas >= max_Pos_Cur - 1 && targetPly.enTurno))
                    {
                        setCasAnotation(idxCas, '@');
                        if (idxCas == max_Pos_Cur - 1)
                            setCasAnotation(idxCas + 1, '@'); //Si es el jugador en turno se borra la casilla de l abola e turno ptambien para no dejar espacios en blanco en el score
                    }
                }
                else //El resto de las posibilidades, miss ('-'), el Foul ('F') y los números (1 - 9)
                    setCasAnotation(idxCas, c);
            }//else { //Otras reglas para el décimo inning
        }//void setCasValue(char c)

        //El caso donde se cumple que : (posCURSOR == max_Pos_Cur + 1 && CasCh == 'X') se da por un llamada a setCasAnotation(...) por setCasvalue(...),
        //pero el uusario no entra ningún valor en esa casilla, porq no le está permitido desplzarse o ubicar el cursor más allá de max_Pos_Cur
        void setCasAnotation(int posCURSOR, char CasCh)
        {
            //int no_tap = Tteam::MainNoTap(variante);
            int no_tap = targetPly.noTap;

            int points = CasCh - '0';

            //string aux = CasCh.ToString();
            //Si es una casilla donde se permite escribir
            //O es la casilla sgt a la maxima donde se permite escribir siendo X
            if (posCURSOR <= max_Pos_Cur || (posCURSOR == max_Pos_Cur + 1 && CasCh == 'X') || (posCURSOR == max_Pos_Cur + 1 && points >= no_tap && no_tap < 10)) 
            {
                if (CasCh == '@')
                    casillas[posCURSOR].Text = "";
                else
                {
                    if(targetPly.enTurno && posCURSOR == max_Pos_Cur && posCURSOR > 0)
                    {

                        //if (casillas[posCURSOR - 1].Text.Length != 0) //Para no dejar que escriba dejando un hueco por medio, para que no deje huecos
                        if (casillas[posCURSOR - 1].Text.Length != 0 || (casillas[posCURSOR - 1].Text.Length == 0 &&  CasCh == 'X'))
                            casillas[posCURSOR].Text = CasCh.ToString();
                        return;
                    }
                    casillas[posCURSOR].Text = CasCh.ToString();

                }
            }
            else
                casillas[posCURSOR].Text = "";  //Limpiando casilla

        }


        /*
        private void ScoreCorrection_KeyDown(object sender, KeyEventArgs e)
        {
            if (idxCas != -1)  //Si hay alguna casilla seleccionada
            {
                if (e.KeyCode == Keys.Left)
                {
                    idxCas--;
                    if (idxCas < 0)
                        idxCas = max_Pos_Cur;
                    e.Handled = true;
                }
                else if(e.KeyCode == Keys.Right)
                {
                    idxCas++;
                    if (idxCas > max_Pos_Cur)
                        idxCas = 0;
                    e.Handled = true;
                }
            }

        }
        */

        //Metodo y comentarios traidos de el proyecto en C++
        /*
       - Este método intentará actualizar la puntuación del jugador(player) que tiene asociado (MyPlayer(ptr))
           partiendo de la información que le fue colocada en sus casillas.
       */
        //Este metodo usa a backupPly
        int CorrectScore()
        {
            //int *xrolls = new int[21]; //21 es la máxima cantidad de tiros posibles  //no se porq estaba haciéndolo con new
            int [] xrolls = new int[21];
            int idxrolls = 0;

            //Recorramos las casillas, y no hace falta pasarse de max_allow_pos
            for (int i = 0; i <= max_Pos_Cur; i++)
            {
                string aux = casillas[i].Text;
                if (aux.Length == 0 && i + 1 < 21 && i + 1 <= max_Pos_Cur + 1 && i % 2 == 0)
                {
                    //En algunas ocasiones se permite al cursor estar hasta una determinada casilla
                    //y en esa casilla se introduce un strike ('X'), siendo una casilla de primer tiro
                    //para finalmente ubicar la 'X' en la casilla contigua que no es una casilla permitida
                    aux = casillas[++i].Text;  //Buscando no vaya a ser que sea un strike
                                                      //if (aux != "X") //lo tuve que borrar con el no tap
                                                      //continue;  //tal vez seria mejor un break después de todo...
                }

                //if (!aux.empty())
                if (aux.Length != 0)
                {
                    int number = 0;
                    bool isnumber = true;
                    if(!Int32.TryParse(aux, out number))
                         isnumber = false;

                    if (isnumber)
                    {
                        //if (Splits[i])
                        if (casillas[i].split)
                            number = -number;
                        xrolls[idxrolls++] = number;
                    }
                    else if (aux == "-")
                        xrolls[idxrolls++] = 0;
                    else if (aux == "X")
                    {
                        //if (casillas[i].getStyle() == Text::Underlined)
                        if (casillas[i].IsUnderlinedStyle())
                            xrolls[idxrolls++] = 9; //No-Tap o AutoStrike
                        else
                            xrolls[idxrolls++] = 10;
                    }
                    else if (aux == "/")
                    {
                        //xrolls[idxrolls] = 10 - xrolls[idxrolls++ - 1]; 
                        int x = 10 - Math.Abs(xrolls[idxrolls - 1]);       //debugging...
                        //xrolls[idxrolls++] = (10 - Math.Abs(xrolls[idxrolls - 1])); //V0.4 si esta marcado como split sera negativo  //idxrolls++ no esta funcionando como estoy acostumbrado en c, c++
                        xrolls[idxrolls++] = x;
                    }
                    else if (aux == "F") //Incorporando Foul
                        xrolls[idxrolls++] = 14;
                }
            }//for
            int result = backupPly.UpdateScore_classic(xrolls, idxrolls);

            return result;
        }//int PlayerRow::CorrectScore()


        private void foulBtnn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
                //setCasValue('F');
            EnterValue('F');
        }

        private void kmissBttn_Click(object sender, EventArgs e)
        {
           // if (idxCas != -1)
             //   setCasValue('-');
            EnterValue('-');
        }

        private void k1Bttn_Click(object sender, EventArgs e)
        {
           // if (idxCas != -1)
             //   setCasValue('1');
            EnterValue('1');
        }

        private void k2Bttn_Click(object sender, EventArgs e)
        {
           // if (idxCas != -1)
             //   setCasValue('2');
            EnterValue('2');
        }

        private void k3Bttn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
              //  setCasValue('3');
            EnterValue('3');
        }

        private void k4Bttn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
              //  setCasValue('4');
            EnterValue('4');
        }

        private void k5Bttn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
              //  setCasValue('5');
            EnterValue('5');
        }

        private void k6Btnn_Click(object sender, EventArgs e)
        {
            //setCasValue('6');
            EnterValue('6');
        }

        private void k7Btnn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
            //  setCasValue('7');
            EnterValue('7');
        }

        private void k8Btnn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
            //  setCasValue('8');
            EnterValue('8');
        }

        private void k9Bttn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
            //setCasValue('9');
            EnterValue('9');
        }

        private void kXBttn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
            //  setCasValue('X');
            EnterValue('X');
        }

        private void ksplitBttn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
            //  setCasValue('S');
            EnterValue('S');
        }

        private void kdelBttn_Click(object sender, EventArgs e)
        {
            //if (idxCas != -1)
            //  setCasValue('C');
            EnterValue('C');
        }

        private void noTapBttn_Click(object sender, EventArgs e)
        {
            char notapValue;
            if (targetPly.noTap == 10)
                notapValue = 'X';
            else
                notapValue = (char)targetPly.noTap;

            EnterValue(notapValue);
        }

        void EnterValue(char c)
        {
            if (idxCas != -1)
            {
                setCasValue(c);

               int cas_result = CorrectScore(); //Este metodo usa a backupPly

                if (cas_result < 0) //Si hubo un error al actualizar
                {
                    cas_result = Math.Abs(cas_result);
                    casillas[cas_result].SetWrong();

                    if (wrongIDX != -1 && wrongIDX != cas_result)
                        casillas[wrongIDX].SetRight();

                    wrongIDX = cas_result;

                    if (cas_result - 2 >= 0)
                        if (casillas[cas_result - 2].Text == "X")
                            cas_result = cas_result - 2;

                    int wrong_frame = cas_result / 2;
                    for (int i = wrong_frame; i < 10; i++)
                        FrameTots[i].Text = "";  

                    totalLabel.Text = "";

                    backupPly.Reset();
                }
                else //Si la actulización no tuvo problemas
                {
                    if (wrongIDX != -1)
                    {
                        casillas[wrongIDX].SetRight();
                        wrongIDX = -1;
                    }
                    LoadPlayer(backupPly);  //Actualiza casillas y totales
                }
            }
        }

        private void foulBtnn_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                e.IsInputKey = true;
        }

        private void foulBtnn_KeyDown(object sender, KeyEventArgs e)
        {
            //if (idxCas != -1)  //Si hay alguna casilla seleccionada
            //{
                //casillas[idxCas].Toogle();
                if (e.KeyCode == Keys.Left)
                {
                    if (idxCas != -1)
                        casillas[idxCas].Toogle();
                    idxCas--;
                    if (idxCas < 0)
                        idxCas = max_Pos_Cur;
                    //casillas[idxCas].Redraw();
                    casillas[idxCas].Toogle();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (idxCas != -1)
                        casillas[idxCas].Toogle();
                    idxCas++;
                    if (idxCas > max_Pos_Cur)
                        idxCas = 0;
                    //casillas[idxCas].Redraw();
                    casillas[idxCas].Toogle();
                    e.Handled = true;
                }
           // }
        }

        private void NxtPlyBttn_Click(object sender, EventArgs e)
        {
           
            if (SaveScore())
            {
                idxPlayer = (idxPlayer + 1) % MyTeam.Qty;   //[0, Qty - 1]
                while (MyTeam[idxPlayer].BDplyID == -1)   //Al menos alguno tiene que haber estado con BDplyID asignado para haber llegado hasta aquí
                    idxPlayer = (idxPlayer + 1) % MyTeam.Qty;   //[0, Qty - 1]
                LoadPlayerInfo(MyTeam, idxPlayer);
            }
        }

        private void PrevPlyBttn_Click(object sender, EventArgs e)
        {
            // idxPlayer = (idxPlayer - 1 + MyTeam.Qty) % MyTeam.Qty; //[0, Qty - 1]
            if (SaveScore())
            {
                idxPlayer = (idxPlayer - 1 + MyTeam.Qty) % MyTeam.Qty; //[0, Qty - 1]
                while(MyTeam[idxPlayer].BDplyID == -1)   //Al menos alguno tiene que haber estado con BDplyID asignado para haber llegado hasta aquí
                    idxPlayer = (idxPlayer - 1 + MyTeam.Qty) % MyTeam.Qty; //[0, Qty - 1]
                LoadPlayerInfo(MyTeam, idxPlayer);
            }
        }

        //Devolverá true para indicar que se debe continuar
        //false para no
        bool SaveScore()
        {
            if(backupPly.Shoots > 0)  //Si este tiene algun tiro es que se salvó alguna correción de la puntuación
            {
                DialogResult dR;
                dR = MessageBox.Show("¿Desea actualizar la puntuación de \"" + targetPly.Name + "\" ?", "Actualizar puntuación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dR == DialogResult.Yes)
                {
                    MyTeam[idxPlayer].LoadScoreFromPlayer(backupPly); 
                }
                return true;
            }
            else if(wrongIDX != -1) //Si se cumple esto es que se intentó actualizar y no se pudo por un error
            {
                DialogResult dR;
                dR = MessageBox.Show("No se actualizará la puntuación. ¿Desea continuar?", "Error en la puntuación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dR == DialogResult.Yes)
                    return true;
                else
                    return false;
            }
            return true;
        }

        private void AceptBttn_Click(object sender, EventArgs e)
        {
            if (SaveScore())
                this.DialogResult = DialogResult.OK;
        }
    }
}
