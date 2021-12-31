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
    public partial class ProgressDialog : Form
    {
        SetProgressCBack delegateSetProgress;
        SetProgressCBack delegateSetRelativeProgress;

        public ProgressDialog()
        {
            InitializeComponent();
            delegateSetProgress = SetProgress_async;
            delegateSetRelativeProgress = SetRelativeProgress_async;
        }

        //Como esto es algo que se va a hacer antes de mostrar la barra de progreso (el formulario mas bien, pero bueno...)
        //aprovecho e inicializo alores
        public void SetInfo(string textToShow)
        {
            TaskInfoLabel.Text = textToShow;
            progBar.Value = progBar.Minimum;
        }


        //Creo que debo fijarme como se hicieron las cosas en presentation, e imitarlas un poco aquí
        public void SetProgress(int p)
        {
            progBar.Value = p;
        }


        //Como es asincronica, ocurrira cuando se pueda, cuando pueda alterar el formulario o la barra de progreso
        //Y se pueda dar el caso de que esto suceda (en algunos escenarios con bajos recursos, procesamiento lento, etc)
        //despues de que se haya enviado a cerrar la forma, y aqui como cuando se cierra la forma(o formulario)
        //se  lleva el Value de la barra de progreso (el control) a 100, y tiene un maximo de 100 fijado si luego
        //se quiere aumentar 25 mas por ejemplo lanzaria una excpecion, claro que esto ya se validó
        //...
        public void SetProgress_async(int p)
        {
            if (this.Visible)  //Si ya cerraron la forma, no tiene sentido hacerlo
            {
                if (this.InvokeRequired)
                    //this.Invoke(new MethodInvoker(SetProgress()));
                    this.Invoke(delegateSetProgress, new Object[] { p });
                else
                {
                    if (p > 100)
                        p = 100;
                    progBar.Value = p;
                }
            }
        } //void SetProgress(int p)

        public void SetRelativeProgress_async(int p)
        {
            if (this.Visible)  //Si ya cerraron la forma, no tiene sentido hacerlo
            {
                if (this.InvokeRequired)
                    //this.Invoke(new MethodInvoker(SetProgress()));
                    this.Invoke(delegateSetRelativeProgress, new Object[] { p });
                else
                {
                    p = progBar.Value + p;
                    if (p > 100)
                        p = 100;
                    progBar.Value = p;
                }
            }
        } //void SetRelativeProgress(int p)
    }
}
