using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class HatPosition
    {
        private static Random gen = new Random();
        private Vector2 hatPosition;
        private Vector2 hatVelocity;
        private int RandomNumer;
        private Color randomColor;
        private bool isHit;
      
        private Texture2D hatPicture;

           

        public HatPosition(Texture2D picture , int rn) 
        {
            
            hatPosition = new Vector2((float)gen.Next(175, 900), (float)gen.Next(175,700));
            hatVelocity = new Vector2((float)(Math.Cos(2*Math.PI*gen.NextDouble())), (float)(Math.Sin(2*Math.PI*gen.NextDouble())));
            isHit = false;
            hatPicture = picture;
               
            RandomNumer = rn % 4;
            

            randomColor = new Color(gen.Next(7, 250), gen.Next(5, 254), gen.Next(17, 240));

        }

        //constructor which takes in texture of each hat, sets it's initial positioon(random), and gives a random velocity. sets isHit to false, so it gets torn before being removed. 
        //randomNumer stores a quadNumber for where the hat is right now. 
        //randomColor gives each hat a randomColor. 
        public HatPosition(Vector2 pos, Vector2 vel)
        {
            hatPosition = pos;  
            hatVelocity = vel;
        
        }


        public void move()
        {
            hatPosition += hatVelocity;

        }
        //increase position using velocity. 

        public Vector2 HatVelocity
        {
            get
            {
                return hatVelocity;
            }

            set
            {
                hatVelocity = value;
            }
        }

        //property for velocity
        public Vector2 HatPositionVector
        {
            get
            {
                return hatPosition;
            }

            set
            {
                hatPosition = value;
            }
        }

      //property for position. 

        public int RandomNumber
        {
            get
            {
                return RandomNumer;
            }

            set
            {
                RandomNumer = value;
            }
        }

        //property for quadrant number. 


        public Color Clr
        {
            get
            {
                return randomColor;
            }

            set
            {
                randomColor = value;
            }
        }

        //property for color of hat. 
        public bool IsHit
        {
            get
            {
                return isHit;
            }

            set
            {
                isHit = value;
            }
        }
        //property for boolean to check if hat is torn. 

        public Texture2D HatPicture
        {
            get
            {
                return hatPicture;
            }

            set
            {
                hatPicture = value;
            }
        }

        //property for current picture..either original or exploded/torn hat. 
    }
}
