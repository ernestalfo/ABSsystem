using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAdmin
{
    public delegate void SetProgressCBack(int i);

    public partial class Presentation : Form
    {
        MainViewFrm ppalWnd;
        Thread launchTask;
        SetProgressCBack delegateSetProgress;
        SetProgressCBack delegateSetRelativeProgress;
        bool loading = true;

        public Presentation()
        {
            InitializeComponent();
            delegateSetProgress = SetProgress;
            delegateSetRelativeProgress = SetRelativeProgress;
            launchTask = new Thread(Loading);
            launchTask.Start();
        }

        void Loading()
        {
            Thread.Sleep(1000);  //Dando tiempo a que se cargue la forma
            SetProgress(5);
            /*
            //Thread.Sleep(5000); //Enjoy de View
            Thread.Sleep(1000);
            SetProgress(10);
            Thread.Sleep(1000);
            SetProgress(15);
            Thread.Sleep(1000);
            SetProgress(20);
            Thread.Sleep(1000);
            SetProgress(25);
            */
            ppalWnd = new MainViewFrm();
            SetProgress(25);
            ppalWnd.AssignInitialProgressBar(SetRelativeProgress);
            ppalWnd.PopulateBD();
            SetProgress(100);
            Thread.Sleep(1000); //Dando tiempo a que se vea que se llegó al 100 % de la carga
            SetProgress(101);
            //ppalWnd.Show();
            ppalWnd.ShowDialog();
            loading = false;
            SetProgress(102); //Esto ocurriría al terminar el programa
        }

        void SetProgress(int p)
        {
            if (p == 102 || this.Visible)  //No tiene sentido que se ejecute sino está visible, al menos que sea para cerrar la ventana (p = 102)
            {
                if (this.InvokeRequired)
                    //this.Invoke(new MethodInvoker(SetProgress()));
                    this.Invoke(delegateSetProgress, new Object[] { p });
                else
                {
                    if (p == 101)
                    {
                        this.TopMost = false;
                        this.Hide();

                    }
                    else if (p == 102)
                        this.Close();
                    else
                    {
                        if (p > 100)
                            p = 100;

                        loadingLabel.Text = p + " %";
                        LoadingProgressBar.Value = p;
                    }
                }
            }
        } //void SetProgress(int p)

        void SetRelativeProgress(int p)
        {
            if (this.Visible)  //Se va a ejecutar siempre que la forma este visible, sino ya no tiene sentido
            {
                if (this.InvokeRequired)
                    //this.Invoke(new MethodInvoker(SetProgress()));
                    this.Invoke(delegateSetRelativeProgress, new Object[] { p });
                else
                {

                    p = LoadingProgressBar.Value + p;
                    if (p > 100)
                        p = 100;
                    LoadingProgressBar.Value = p;
                    loadingLabel.Text = LoadingProgressBar.Value + " %";

                }
            }
        } //void SetRelativeProgress(int p)

        private void Presentation_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = (e.CloseReason == CloseReason.UserClosing);
            // disable user closing the form, but no one else
            e.Cancel = loading;
        }
    }
}
