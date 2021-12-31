using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAdmin
{
    
    class TClock
    {
        Thread PickTime;
        bool TimerActivated;
        DateTime now;
        Label DateLabel;    
        Form targetDateForm;  //El formulario donde se encontrarán las etiquetas a actualizar con la fecha y la hora
        bool showDate;
        Label TimeLabel;
        Form targetTimeForm;
        bool showTime;

        int Year;
        int Month;
        int MonthDay;
        //int WeekDay;
        DayOfWeek WeekDay;
        int Seconds;
        int Minutes;
        int Hours;

        int deltaTime;

        string DateToShow;
        string TimeToShow;
        
        public TClock()
        {
            PickTime = new Thread(OneSecondTrigger);
            TimerActivated = false;
            showDate = false;
            //
            MonthDay = -1;
            Seconds = -1;
            Minutes = -1;
            Hours = -1;
            Year = -1;
            //WeekDay = -1;
            //WeekDay = DayOfWeek.
            Month = -1;
            //
            deltaTime = 1000;
        }
        private void OneSecondTrigger()
        {
            while (TimerActivated)
            {
                //Console.Write("Current Date and Time is : ");
                now = DateTime.Now;
                //int x = now.;
                //Console.WriteLine(now);
                if (showDate)
                    ProcessDate();

                if (showTime)
                    ProcessTime();

                //Dentro de Process Time se calculó el lapso de tiempo: deltatime, que debe transcurrir para sinc este hilo
                //con cada cambio de segundo del reloj del sistema. No se tienen en cuenta las demoras en la ejec de ProcessDate() y ProcessTime()
                Thread.Sleep(deltaTime);
            }
        }
        public void  Start()
        {
            if (!TimerActivated)
            {
                TimerActivated = true;
                PickTime.Start();
            }
        }
        public void Stop()
        {
            TimerActivated = false;
            if (PickTime != null)  
                if (PickTime.IsAlive)
                    PickTime.Abort();
        }

        public void AttachDateLabel(Label targetLabel, Form targetForm)
        {
            DateLabel = targetLabel;
            targetDateForm = targetForm;
            //
            showDate = true;
        }

        public void AttachTimeLabel(Label targetLabel, Form targetForm)
        {
            TimeLabel = targetLabel;
            targetTimeForm = targetForm;
            //
            showTime = true;
        }

        private void  ProcessDate()
        {
            if(MonthDay != now.Day)
            {
                MonthDay = now.Day;
                WeekDay = now.DayOfWeek;
                Month = now.Month;
                Year = now.Year;
                BuildDateString();
                ShowDate();
            }
        }

        private void ProcessTime()
        {
            if(Seconds != now.Second || Minutes != now.Minute)
            {
                //Seconds = now.Second;
                //Minutes = now.Minute;
                //Hours = now.Hour;
                deltaTime = 1000 - now.Millisecond;
                if (deltaTime < 250)
                    deltaTime += 1000; //deltaTime: 250 - 1000 ms

                if(Minutes != now.Minute)
                {
                    Seconds = now.Second;
                    Minutes = now.Minute;
                    Hours = now.Hour;
                    BuildTimeString();
                    ShowTime();
                }
                else
                {
                    Seconds = now.Second;
                    Minutes = now.Minute;
                    Hours = now.Hour;
                }  
            }
        }//ProcessTime()

        private void BuildDateString()
        {
            DateToShow = "";
            switch(WeekDay)
            {
                case DayOfWeek.Sunday:
                    DateToShow = "Domingo, ";
                    break;
                case DayOfWeek.Monday:
                    DateToShow = "Lunes, ";
                    break;
                case DayOfWeek.Tuesday:
                    DateToShow = "Martes, ";
                    break;
                case DayOfWeek.Wednesday:
                    DateToShow = "Miércoles, ";
                    break;
                case DayOfWeek.Thursday:
                    DateToShow = "Jueves, ";
                    break;
                case DayOfWeek.Friday:
                    DateToShow = "Viernes, ";
                    break;
                case DayOfWeek.Saturday:
                    DateToShow = "Sábado, ";
                    break;
            }
            DateToShow += MonthDay + " de ";
            switch (Month)
            {
                case 1:
                    DateToShow += "enero del ";
                    break;
                case 2:
                    DateToShow += "febrero del ";
                    break;
                case 3:
                    DateToShow += "marzo del ";
                    break;
                case 4:
                    DateToShow += "abril del ";
                    break;
                case 5:
                    DateToShow += "mayo del ";
                    break;
                case 6:
                    DateToShow += "junio del ";
                    break;
                case 7:
                    DateToShow += "julio del ";
                    break;
                case 8:
                    DateToShow += "agosto del ";
                    break;
                case 9:
                    DateToShow += "septiembre del ";
                    break;
                case 10:
                    DateToShow += "octubre del ";
                    break;
                case 11:
                    DateToShow += "noviembre del ";
                    break;
                case 12:
                    DateToShow += "diciembre del ";
                    break;
            }
            DateToShow += Year;
        }//private void BuildDateString()

        void ShowDate()
        {
            if (targetDateForm.InvokeRequired)
                targetDateForm.Invoke(new MethodInvoker(ShowDate));
            else
                DateLabel.Text = DateToShow;
        }

        private void BuildTimeString()
        {
            int hrs = Hours;
            int mins = Minutes;
            bool am = true;

            if (hrs >= 12)
            {  
                am = false;
                if (hrs >= 13)
                    hrs -= 12;
            }

            if (hrs == 0)
                hrs = 12;

            if (mins < 10)
                TimeToShow = hrs + ":0" + mins;
            else
                TimeToShow = hrs + ":" + mins;

            if (am)
            {
                //if(hrs == 12 && mins == 0)
                if (Hours == 12 && mins == 0) //Hours y no hrs para que no ponga "M" a las doce de la noche en punto
                    TimeToShow += " M"; //No es AM es M
                else
                    TimeToShow += " AM";

            }
            else
                TimeToShow += " PM";
        }

        private void ShowTime()
        {
            if (targetTimeForm.InvokeRequired)
                targetTimeForm.Invoke(new MethodInvoker(ShowTime));
            else
                TimeLabel.Text = TimeToShow;
        }

        public DateTime getDateTime()
        {
            return now;
        }

    }//end of class TClock
}
