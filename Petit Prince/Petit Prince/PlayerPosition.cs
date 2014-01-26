using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//Copyright (C) Ravish Chawla 2011
namespace Petit_Prince
{
    class PlayerPosition
    {
        private Vector2 playerVector;

        

        public PlayerPosition()
        {
             playerVector = new Vector2(200, 200);

        }
        


        public Vector2 PlayerVector
        {
            get
            {
                return playerVector;
            }

            set
            {
                playerVector = value;
            }
        }

        
        //Avatar's position vector2's components are stored in this class, and are returned likewise. 

    }



}
