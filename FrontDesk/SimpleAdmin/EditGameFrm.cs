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
    public partial class EditGameFrm : Form
    {
        //int state = 0; //unknow
        int state = 1; //se puede "añadir" un nuevo jugador
                       //state = 2; Se trata de NUEVO
                       //state = 3; Se trata de Siguiente
        int idxPlyEdit = 0;
        //Team TheTeam;
        public Team TheTeam { get; set; }
        //ProgressDialog ProgDiag;
        ScoreCorrection correctScoreFrm;

        public bool modifying = false; 

        public EditGameFrm()
        {
            InitializeComponent();
            //Valores por defecto en testing4:
            TypePlyCBox.SelectedIndex = 0;
            NoTapCBox.SelectedIndex = 0;
            ByLinesRB.Select();
            //ByLinesCB.SelectedIndex = 0;
            ByLinesCB.SelectedIndex = 10; //∞  //líneas ilimitadas
            ScoringMethodCB.SelectedIndex = 0;
            TheTeam = new Team();
            //ProgDiag = new ProgressDialog();
            correctScoreFrm = new ScoreCorrection();
        }

        
        private void PlayersList_MouseClick(object sender, MouseEventArgs e)
        {
            //Lo bueno con este evento que al parecer si no hay elementos en la lista, no ocurre aunque se haga click

            //MessageBox.Show("Ha seleccionado el jugador: " + PlayersList.FocusedItem.Index, "Jugador seleccionado",MessageBoxButtons.OK);

            int iT = BttnRulesB();
            if (iT != -1)
            {
                if (idxPlyEdit < TheTeam.Qty)
                {
                    Player miPlayer = new Player(PlyNameTB.Text, BumpersChckB.Checked);
                    miPlayer.BDplyID = TheTeam[idxPlyEdit].BDplyID;
                    //Saving.... / Actualizando:
                    if (TheTeam[idxPlyEdit] != miPlayer)
                    {
                        if (TheTeam.Insert(idxPlyEdit, miPlayer))
                        {
                            UpdatePlayerList(idxPlyEdit, miPlayer);
                        }
                        else
                        {
                            MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                //
                idxPlyEdit = iT;
                LoadPlayerInfo(TheTeam[idxPlyEdit]);
                //Playe
                BttnRulesA();  //botones de arriba, los pegados al text edit del nombre del jugador
            }
        }

        //private void BttnRulesB()
        private int BttnRulesB()
        {
            if (PlayersList.SelectedIndices.Count == 0)
            {
                CorrectScoreBttn.Enabled = false;  
                DelPlyBttn.Enabled = false;
                PausePlyBttn.Enabled = false;
                MovUpPlyBttn.Enabled = false;
                MovDownPlyBttn.Enabled = false;
                return -1;
            }
            else
            {
                int idx = PlayersList.SelectedIndices[0];

                if (idx < PlayersList.Items.Count - 1)
                    MovDownPlyBttn.Enabled = true;
                else
                    MovDownPlyBttn.Enabled = false;

                if (idx > 0)
                    MovUpPlyBttn.Enabled = true;
                else
                    MovUpPlyBttn.Enabled = false;

                DelPlyBttn.Enabled = true;

                if(idx >= 0)
                    if (TheTeam[idx].BDplyID != -1)    
                        CorrectScoreBttn.Enabled = true;
                    else
                        CorrectScoreBttn.Enabled = false;

                //Regla agregada para el boton de borrar, ya en el boton de borrar se le daba una buena manipulacion, pero con las rgelas se mas intuitiva la navegacion y se ahorran clics del usuario
                if(TheTeam.Qty <= 1 && modifying)
                    DelPlyBttn.Enabled = false;
                //else

                return idx;
            }
        }

        private void PlyNameTB_TextChanged(object sender, EventArgs e)
        {
            /*
            if (PlyNameTB.Text.Trim().Length > 0)
                AddPlyBttn.Enabled = true;
            else
                AddPlyBttn.Enabled = false;
            */
            BttnRulesA();
        }

        private void AddPlyBttn_Click(object sender, EventArgs e)
        {
            Player miPlayer = new Player(PlyNameTB.Text, BumpersChckB.Checked);
            if (state == 1) //Modo Añadir
            {
                int addply_result = TheTeam.AddPlayer(miPlayer);

                if (addply_result == -1)
                {
                    MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PlyNameTB.Text = ""; //Clearing
                    //Restablezco el nombre anterior: //Prefiero hacer esto que dejar un nombre que ya exista y traiga confusión
                    //PlyNameTB.Text = TheTeam[idxPlyEdit].Name;
                }
                else if (addply_result == 1)
                {
                    AddToPlayerList(miPlayer);
                    //PlyNameTB.Text = ""; //Clearing
                    ClearPlayerInfo();
                    PlyNameTB.Focus();
                    idxPlyEdit++;
                }
            }
            else if(state == 2) //Modo NUEVO (jugador en blanco para editar)
            {
                if (idxPlyEdit < TheTeam.Qty && TheTeam[idxPlyEdit] != miPlayer)
                {
                    miPlayer.BDplyID = TheTeam[idxPlyEdit].BDplyID;
                    //miPlayer.modify = true;  //Ya no se está usando...
                    //Saving....:
                    if (TheTeam.Insert(idxPlyEdit, miPlayer))
                    {
                        UpdatePlayerList(idxPlyEdit, miPlayer);
                        idxPlyEdit++;
                        ClearPlayerInfo();
                    }
                    else
                    { //No avanzo, ni borro nada, para que se vea el error
                        MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Restablezco el nombre anterior: //Prefiero hacer esto que dejar un nombre que ya exista y traiga confusión
                        PlyNameTB.Text = TheTeam[idxPlyEdit].Name;
                    }
                }
                else
                {//No tiene que salvar nada, pero avanza, para que se pueda entrar un nuevo jugador
                    idxPlyEdit++;
                    ClearPlayerInfo();
                }
            }
            else if(state == 3) //Modo Siguiente
            {
                if (idxPlyEdit < TheTeam.Qty && TheTeam[idxPlyEdit] != miPlayer)
                {
                    miPlayer.BDplyID = TheTeam[idxPlyEdit].BDplyID;
                    miPlayer.modify = true;
                    //Saving....:
                    if (TheTeam.Insert(idxPlyEdit, miPlayer))
                    {
                        UpdatePlayerList(idxPlyEdit, miPlayer);
                        idxPlyEdit++;
                        LoadPlayerInfo(TheTeam[idxPlyEdit]);
                    }
                    else
                    { //No avanzo, ni borro nada, para que se vea el error
                        MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Restablezco el nombre anterior:
                        PlyNameTB.Text = TheTeam[idxPlyEdit].Name;
                    }
                }
                else
                {//No tiene que salvar nada, pero avanza, para mostrar el próximo jugador
                    idxPlyEdit++;
                    LoadPlayerInfo(TheTeam[idxPlyEdit]);
                }

            }
            BttnRulesA();
        }

        private void AddToPlayerList(Player p)
        {
            string[] PlayerItem = new string[4];
            PlayerItem[0] = p.Name;
            PlayerItem[1] = Player.GetStateString(p.PlyState);
            PlayerItem[2] = "-";
            PlayerItem[3] = "-";
            PlayersList.Items.Add(new ListViewItem(PlayerItem));
        }

        private void UpdatePlayerList(int idx, Player p)
        {
            
            string[] PlayerItem = new string[4];
            PlayerItem[0] = p.Name;
            PlayerItem[1] = Player.GetStateString(p.PlyState);
            PlayerItem[2] = "-";
            PlayerItem[3] = "-";
            PlayersList.Items[idx].Remove();
            PlayersList.Items.Insert(idx, new ListViewItem(PlayerItem));  
        }

        private void ClearPlayerInfo()
        {
            PlyNameTB.Text = "";
            BumpersChckB.Checked = false;
        }

        private void LoadPlayerInfo(Player p)
        {
            if ((object)p != null)  //Tuve que poner object porq sino se va a el metodo sobrecargado de != y da error, lo mejor es arreglarlo en el metodo, pero ahora, ahora voy pa alla...
            {
                PlyNameTB.Text = p.Name;
                PlyNameTB.Select(PlyNameTB.Text.Length, 0); //Poniendo el cursor al final del nombre del jugador
                BumpersChckB.Checked = p.Bumpers;
            }
        }

        private void BttnRulesA()
        {

            /*Botones del del primer group box donde se llenan los datos de los jugadores */
            /*Habilitando o Deshabilitando el 2do Btn(Add / New / Nxt)*/
            if (idxPlyEdit == Team.MAXPLYS)
            {
                AddPlyBttn.Enabled = false;
                PlyNameTB.Enabled = false;
            }
            else
            {
                if (PlyNameTB.Text.Trim().Length > 0)
                    AddPlyBttn.Enabled = true;
                else
                    AddPlyBttn.Enabled = false;
                PlyNameTB.Enabled = true;
            }

            /*Habilitando o Deshabilitando el 1er Btn(Prev)*/
            if (idxPlyEdit > 0)
                PrevPlyBttn.Enabled = true;
            else
                PrevPlyBttn.Enabled = false;

            /*Cambiando el texto del 2do Boton, segun la situación, el estado mas bien*/
            if(TheTeam.Qty == 0)
            {
                AddPlyBttn.Text = "Añadir otro";
                state = 1;
            }
            else if (idxPlyEdit < TheTeam.Qty - 1)
            {
                if (idxPlyEdit != -1)
                {
                    AddPlyBttn.Text = "SIGUIENTE";
                    state = 3;
                }
                else
                {
                    AddPlyBttn.Text = "Añadir otro...";
                    state = 1;
                }
            }
            else if (idxPlyEdit == TheTeam.Qty)
            {
                //AddPlyBttn.Text = "Añadir"; //Se convierte a mayusculas auto...(MaterialButton)
                AddPlyBttn.Text = "Añadir otro";
                state = 1;
            }
            else if(idxPlyEdit == TheTeam.Qty - 1)
            {
                //AddPlyBttn.Text = "NUEVO";
                AddPlyBttn.Text = "Añadir otro";
                state = 2;
            }
        }

        private void MovUpPlyBttn_Click(object sender, EventArgs e)
        {
            if (PlayersList.SelectedIndices.Count == 0)
                MessageBox.Show("Ningún jugador seleccionado");
            else
            {
                //int idxPly = PlayersList.SelectedItems[0].Index;
                //MessageBox.Show("jugador: " + idxPly);
                int idx = PlayersList.SelectedIndices[0];
                if (idx > 0)
                {
                    /*
                    ListViewItem Temp = PlayersList.Items[idx - 1];
                    PlayersList.Items[idx - 1] = PlayersList.Items[idx];
                    PlayersList.Items[idx] = Temp;
                    */
                    //ListViewItem Temp = (ListViewItem)PlayersList.Items[idx].Clone();
                    ListViewItem Temp = PlayersList.Items[idx];
                    PlayersList.Items[idx].Remove();
                    PlayersList.Items.Insert(idx - 1, Temp);
                    //swaping players too:
                    Player tempPly = TheTeam[idx - 1];
                    TheTeam[idx - 1] = TheTeam[idx];
                    TheTeam[idx] = tempPly;
                    //Ajustando idxPlyEdit:
                    if (idxPlyEdit == idx)
                        idxPlyEdit = idx - 1;
                    else if (idxPlyEdit == idx - 1)
                        idxPlyEdit = idx;
                    //
                    BttnRulesA();
                }
                else
                    MessageBox.Show("A donde quieres moverlo papooo!!!!");
            }
        }

        private void MovDownPlyBttn_Click(object sender, EventArgs e)
        {
            if (PlayersList.SelectedIndices.Count == 0)
                MessageBox.Show("Ningún jugador seleccionado");
            else
            {
                int idx = PlayersList.SelectedIndices[0];
                if (idx < PlayersList.Items.Count - 1)
                {
                    ListViewItem Temp = PlayersList.Items[idx];
                    PlayersList.Items[idx].Remove();
                    PlayersList.Items.Insert(idx + 1, Temp);
                    //swaping players too:
                    Player tempPly = TheTeam[idx + 1];
                    TheTeam[idx + 1] = TheTeam[idx];
                    TheTeam[idx] = tempPly;
                    //Ajustando idxPlyEdit:
                    if (idxPlyEdit == idx)
                        idxPlyEdit = idx + 1;
                    else if (idxPlyEdit == idx + 1)
                        idxPlyEdit = idx;
                    //
                    BttnRulesA();
                }
                else
                    MessageBox.Show("A dónde quieres moverlo papooo!!!!");
            }
        }

        private void PrevPlyBttn_Click(object sender, EventArgs e)
        {
            if (idxPlyEdit > 0)
            {
                Player miPlayer = new Player(PlyNameTB.Text, BumpersChckB.Checked);
                if (idxPlyEdit < TheTeam.Qty)  //__UPDATE__
                {
                    if (miPlayer != TheTeam[idxPlyEdit])
                    {
                        miPlayer.BDplyID = TheTeam[idxPlyEdit].BDplyID;
                        miPlayer.modify = true;
                        //Saving....:
                        if (TheTeam.Insert(idxPlyEdit, miPlayer))
                            UpdatePlayerList(idxPlyEdit, miPlayer);
                        else
                        { //No retrocedo, ni borro nada, para que se vea el error
                            MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Restablezco el nombre anterior:
                            PlyNameTB.Text = TheTeam[idxPlyEdit].Name;
                            return;
                        }
                    }
                }
                else if(miPlayer.Name != "") //No salvar nada si el nombre esta en blanco
                {//Podría tratarse de un nuevo jugador que se quiere añadir __ANADIR__

                    int addply_result = TheTeam.AddPlayer(miPlayer);
                    
                    if (addply_result == -1)
                    {
                        MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        PlyNameTB.Text = ""; //Clearing
                        return;
                    }
                    else if (addply_result == 1)
                        AddToPlayerList(miPlayer);
                }
                //Retrocediendo, se haya salvado el jugador o no
                idxPlyEdit--;
                LoadPlayerInfo(TheTeam[idxPlyEdit]);
            }
            BttnRulesA();
        }

        /*
        private void EditPlyBttn_Click(object sender, EventArgs e)
        {
            idxPlyEdit = PlayersList.SelectedIndices[0];
            LoadPlayerInfo(TheTeam[idxPlyEdit]);
            BttnRulesA();
        }
        */
        //Voy a unir esta acción con el clic ya

        public void LoadTeam(Team T)
        {
            //TheTeam = T;
            TheTeam.Copy(T);
            if(T != null)
            {
                if(T.Qty > 0)
                {
                    for (int i = 0; i < T.Qty; i++)
                        AddToPlayerList(T[i]);
                    idxPlyEdit = 0;
                    LoadPlayerInfo(T[idxPlyEdit]);

                    //Salvando valores originales, serán usados para modificar el equipo
                    //org_TeamQ = T.Qty;
                }
                TeamNameTB.Text = TheTeam.TeamName;
            }
            BttnRulesA();
            BttnRulesB();
            //
            //PlyNameTB.Focus();
        }

        /* Borra los elementos de la lista pero no borra los encabezados*/
        private void ClearPlayerList()
        {
            int L = PlayersList.Items.Count;
            for(int i = 0; i < L; i++)
                PlayersList.Items[0].Remove();

            PlayersList.SelectedIndices.Clear(); //added 25/marzo/2021
        }

        private void PlayersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            int iT = BttnRulesB();
            if (iT != -1)
            {
                //Ahora en esta versión voy a combinar lo que se hacía aquí con el btn "Editar"
                //idxPlyEdit = PlayersList.SelectedIndices[0];
                idxPlyEdit = iT;
                LoadPlayerInfo(TheTeam[idxPlyEdit]);
                BttnRulesA();  //botones de arriba, los pegados al text edit del nombre del jugador
            }
            */
            int iT = BttnRulesB();
        }

        public void Clear()
        {
            //Clearing the Form:
            idxPlyEdit = 0;
            TeamNameTB.Text = "";
            //Clearing the player info:
            PlyNameTB.Text = "";
            BumpersChckB.Checked = false;
            //PlayersList.Clear();
            ClearPlayerList();   //Mejor hacerlo antes
            //Dejando el foco en el text box de nombre del jugador:
            PlyNameTB.Focus();

            //Ahora en esta versión agregamos:
            TheTeam.Clear();
            //

        }

        private void EditGameFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Estas acciones son mejor hacerlas en otro lugar
            //En una PC rapida no se nota la diferencia, pero en mi laptop si, se ve como se borra la lista
            //Entonces es mejor hacerlo sin que se vea antes de cargar el formulario
            //Pero que se encargue el que lanze el formulario de llamar a un método Clear antes
            /*
            //Clearing the Form:
            idxPlyEdit = 0;
            TeamNameTB.Text = "";
            //Clearing the player info:
            PlyNameTB.Text = "";
            BumpersChckB.Checked = false;
            //PlayersList.Clear();
            ClearPlayerList();   //Mejor hacerlo antes
            //Dejando el foco en el text box de nombre del jugador:
            PlyNameTB.Focus();
            */
        }

        private void PlyNameTB_KeyUp(object sender, KeyEventArgs e)
        {

            ///*
            //return; //debugging...
            if (e.KeyCode == Keys.Enter && AddPlyBttn.Enabled)
            {//Aqui puse lo mismo que en el botón Añadir
                Player miPlayer = new Player(PlyNameTB.Text, BumpersChckB.Checked);
                if (state == 1) //Modo Añadir
                {
                    int addply_result = TheTeam.AddPlayer(miPlayer);

                    if (addply_result == -1)
                    {
                        MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        PlyNameTB.Text = ""; //Clearing
                                             //Restablezco el nombre anterior: //Prefiero hacer esto que dejar un nombre que ya exista y traiga confusión
                                             //PlyNameTB.Text = TheTeam[idxPlyEdit].Name;
                    }
                    else if (addply_result == 1)
                    {
                        AddToPlayerList(miPlayer);
                        //PlyNameTB.Text = ""; //Clearing
                        ClearPlayerInfo();
                        PlyNameTB.Focus();
                        idxPlyEdit++;
                    }
                }//state == 1
                else if (state == 2) //Modo NUEVO (jugador en blanco para editar)
                {
                    if (idxPlyEdit < TheTeam.Qty && TheTeam[idxPlyEdit] != miPlayer)
                    {
                        miPlayer.BDplyID = TheTeam[idxPlyEdit].BDplyID;
                        miPlayer.modify = true;
                        //Saving....:
                        if (TheTeam.Insert(idxPlyEdit, miPlayer))
                        {
                            UpdatePlayerList(idxPlyEdit, miPlayer);
                            idxPlyEdit++;
                            ClearPlayerInfo();
                        }
                        else
                        { //No avanzo, ni borro nada, para que se vea el error
                            MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Restablezco el nombre anterior: //Prefiero hacer esto que dejar un nombre que ya exista y traiga confusión
                            PlyNameTB.Text = TheTeam[idxPlyEdit].Name;
                        }
                    }
                    else
                    {//No tiene que salvar nada, pero avanza, para que se pueda entrar un nuevo jugador
                        idxPlyEdit++;
                        ClearPlayerInfo();
                    }
                }// (state == 2)
                else if (state == 3) //Modo Siguiente
                {
                    if (idxPlyEdit < TheTeam.Qty && TheTeam[idxPlyEdit] != miPlayer)
                    {
                        miPlayer.BDplyID = TheTeam[idxPlyEdit].BDplyID;
                        miPlayer.modify = true;
                        //Saving....:
                        if (TheTeam.Insert(idxPlyEdit, miPlayer))
                        {
                            UpdatePlayerList(idxPlyEdit, miPlayer);
                            idxPlyEdit++;
                            LoadPlayerInfo(TheTeam[idxPlyEdit]);
                        }
                        else
                        { //No avanzo, ni borro nada, para que se vea el error
                            MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //Restablezco el nombre anterior:
                            PlyNameTB.Text = TheTeam[idxPlyEdit].Name;
                        }
                    }
                    else
                    {//No tiene que salvar nada, pero avanza, para mostrar el proximo jugador
                        idxPlyEdit++;
                        LoadPlayerInfo(TheTeam[idxPlyEdit]);
                    }

                }//state == 3
                BttnRulesA();
                //e.Handled = true; //trick for supressing the ding sound
                //e.SuppressKeyPress = true;
            }
            //*/
        }

        private void AceptBttn_Click(object sender, EventArgs e)
        {
            //Mostrar diálogo con información resumen de lo que se va a hacer
            //por ejemplo: se va a abrir las pista por x juegos para tantas personas... (para más adelante)
           
            Player miPlayer = new Player(PlyNameTB.Text, BumpersChckB.Checked);
            

            if (idxPlyEdit == TheTeam.Qty && AddPlyBttn.Enabled) //Añadiendo este jugador antes de cerrar...
            {
                //Player miPlayer = new Player(PlyNameTB.Text, BumpersChckB.Checked);
                //Vamos a salvar a este jugador
                int addply_result = TheTeam.AddPlayer(miPlayer);

                if (addply_result == -1)
                {
                    MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PlyNameTB.Text = ""; //Clearing
                    return;
                }
                else if (addply_result == 1)
                    AddToPlayerList(miPlayer);  //Lo estoy añadiendo, pero pa que? sino debe dar tiempo a que se vea, si actos seguido se va a cerrar el formulario
            }
            else if(idxPlyEdit < TheTeam.Qty && TheTeam[idxPlyEdit] != miPlayer && miPlayer.Name != "") //Actualizando este jugador antes de cerrar...
            {
                miPlayer.BDplyID = TheTeam[idxPlyEdit].BDplyID;
                miPlayer.modify = true;
                //Saving....:
                if (TheTeam.Insert(idxPlyEdit, miPlayer))
                {
                    UpdatePlayerList(idxPlyEdit, miPlayer); //Parecido al comentario anterior, actualizarlo para que?sino se debe poder apreciar
                    //idxPlyEdit++;  //Y estas acciones mucho menos necesarias, de hecho al parecer vienen del comportamiento del btn anadir/sgt no de este aceptar del formulario
                    //LoadPlayerInfo(TheTeam[idxPlyEdit]);
                }
                else
                { 
                    MessageBox.Show("Ya existe un jugador con el nombre de \"" + miPlayer.Name + "\" en este equipo.", "ELIJA OTRO NOMBRE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Restablezco el nombre anterior:
                    PlyNameTB.Text = TheTeam[idxPlyEdit].Name;
                    return;
                }

            }
            TheTeam.TeamName = TeamNameTB.Text;
            this.DialogResult = DialogResult.OK;
            //Dejando el foco en el text box de nombre del jugador:
            PlyNameTB.Focus();  //Lo puse en ...Closing... tambien,tal vez es mejor ponerlo en LoadPlayerInfo()...
        }

        private void EditGameFrm_Activated(object sender, EventArgs e)
        {
            //PlyNameTB.Focus();
        }

        //Con esto se logró suprimir el sonidito de la campana ding!!! al apretar enter en el text box que recoge el nombre del jugador
        private void PlyNameTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void DelPlyBttn_Click(object sender, EventArgs e)
        {
           int plysel = PlayersList.SelectedIndices[0];  //Lanza una excpecion, si no hay elementos seleccionados, claro que no deberia haber llegado aqui, el btn tenia que haber estado deshabilitado
            if (TheTeam.Qty > 1 || !modifying) //Si hay mas de un jugador o si se esta abriendo un nuevo juego entra aqui
            {
                DialogResult result = MessageBox.Show("¿Está seguro de querer borrar a '" + TheTeam[plysel].Name + "'?", "Borrar a jugador...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    TheTeam.DeletePlayer(plysel);
                    PlayersList.Items[plysel].Remove();
                    if (TheTeam.Qty > 0)
                    {
                        if (plysel == TheTeam.Qty)
                            plysel--;

                        LoadPlayerInfo(TheTeam[plysel]);
                        idxPlyEdit = plysel;
                    }
                    else
                    {
                        //idxPlyEdit = -1;  //Aunq seria bueno tomar este convenio, no se venia usando y trae confusion
                        idxPlyEdit = 0;
                        ClearPlayerInfo();
                        BttnRulesA();
                    }
                    BttnRulesB();
                }
            }
            else        //Cuando se esta modificando y no abriendo nuevo juego no se deja borrar a todos los jugadores  //Pdria haber agregado una nueva regla en BtttnsRulesB() para que si se diera este caso el btn de borrar no se habilitara
                MessageBox.Show("No es posible quitar todos los jugadores, mejor use la opción de 'Cancelar Juego'.","Opción Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CorrectScoreBttn_Click(object sender, EventArgs e)
        {
            int plysel = PlayersList.SelectedIndices[0];  //Lanza una excepción, si no hay elementos seleccionados, claro que no deberia haber llegado aqui, el btn tenia que haber estado deshabilitado
            //correctScoreFrm.LoadPlayerInfo(TheTeam[plysel]);
            correctScoreFrm.LoadPlayerInfo(TheTeam, plysel);
            DialogResult diagR = correctScoreFrm.ShowDialog();
            /*
            if(diagR == DialogResult.OK)
            {

            }
            */

        }



    }
}
