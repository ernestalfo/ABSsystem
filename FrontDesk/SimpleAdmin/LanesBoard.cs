using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdmin
{
    class LanesBoard
    {
        LaneCard[] Lanes;

        int conectadas;
        int libres;
        int desactivadas;
        int ocupadas;
        int totalPistas;

        //public LanesBoard(System.Windows.Forms.Control parentCtrl, System.EventHandler Clickcback, int board_width, int board_height)
        public LanesBoard(System.Windows.Forms.Control parentCtrl, System.EventHandler Clickcback, int cols, int boxes)
        {
            //Lanes = new LaneCard[board_width * board_height];
            Lanes = new LaneCard[boxes];

            //totalPistas = board_width * board_height;
            totalPistas = boxes;

            int LanesIDX = 0;
            int Xi = 14;
            //int Yi = 33;
            int Yi = 14;
            //for (int filas = 0; filas < board_height; filas++)
            for (int box = 0;  box < boxes;)
            {
                Xi = 14;
                //for (int cols = 0; cols < board_width; cols++)
                for (int col = 0; col < cols && box < boxes; col++)
                {
                    Lanes[LanesIDX] = new LaneCard(parentCtrl);
                    Lanes[LanesIDX].PlaceIn(new System.Drawing.Point(Xi, Yi));
                    //Lanes[LanesIDX].setLaneID(LanesIDX + 1);
                    Lanes[LanesIDX].LaneID = LanesIDX + 1;
                    Lanes[LanesIDX].setOnClick(Clickcback);
                    //Lanes[LanesIDX].LaneTeam.TeamName = "Equipo " + (filas * board_width + cols + 1);
                    Lanes[LanesIDX].LaneTeam.TeamName = "Equipo " + (box + 1);
                    //Lanes[LanesIDX].setState(LaneStates.Free);
                    Lanes[LanesIDX].LaneState = LaneStates.Free;
                    //Lanes[LanesIDX].LaneState = LaneStates.ByTime;
                    Xi += LaneCard.sizeCard_X + 17;
                    LanesIDX++;
                    box++;
                }
                Yi += LaneCard.sizeCard_Y + 17;
            }
        } //LanesBoard(...)

        public LaneCard this[int index]
        {
            get
            {
                return Lanes[index];
            }
            set
            {
                Lanes[index] = value;
            }
        }

        public String [] GetLanesAvailablesList()
        {
            //String[] listLanes = new string[totalPistas];
            String[] listLanes = null;
            int[] ids = new int[totalPistas];
            int j = 0;
            for(int i = 0; i < totalPistas; i++)
            {
                if(Lanes[i].Connected && Lanes[i].LaneState == LaneStates.Free)
                {
                    //listLanes[j++] = "Pista_" + Lanes[i].LaneID;
                    ids[j++] = i;
                }
            }
            
            if(j > 0)
            {
                listLanes = new string[j];
                for (int i = 0; i < j; i++)
                    listLanes[i] = "Pista_" + Lanes[ids[i]].LaneID;
            }

            return listLanes;

        }
    }//end of definition of class LanesBoard
}
