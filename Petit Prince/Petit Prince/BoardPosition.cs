using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petit_Prince
{
    class BoardPosition
    {
        PrincePosition p = new PrincePosition();


        enum boardPos { asteriod, planets, mountains, trees };
        boardPos currentLocation;

        
        
        

        public BoardPosition()
        {
            currentLocation = boardPos.asteriod;
        }


   
        




    }







}
