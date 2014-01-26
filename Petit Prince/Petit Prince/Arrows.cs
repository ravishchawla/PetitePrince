using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    class Arrows
    {
        private static Random gen = new Random();
        private Vector2 arrowPosition;
        private Vector2 arrowVelocity;
        private Vector2 arrowCenter;
        
        private float arotation;

        PetitPrince tp = new PetitPrince();
       
        PlayerPosition p = new PlayerPosition();

        public Arrows(Vector2 ArrowVector)
        {
            //arrowPosition = new Vector2((float)p.PlayerVector.X, (float)p.PlayerVector.Y);
            arrowPosition = ArrowVector;
          //  avatarVector.X += 10 * ((float)Math.Cos(rotation)); avatarVector.Y += 10 * ((float)Math.Sin(rotation));
            //arrowVelocity = new Vector2((float)(10 * (Math.Cos(tp.Rotation))), (float)(10 * Math.Sin(tp.Rotation)));

            
            arrowVelocity.X = ((float)(10 * (Math.Cos(arotation))));
            arrowVelocity.Y = ((float)(10 * (Math.Sin(arotation))));

            arrowCenter = new Vector2((float)Math.Cos(2 * Math.PI )+ (arrowPosition.X + 45f), (float)Math.Sin(2 * Math.PI) + (arrowPosition.X + 45f));



       //     arrowCenter = new Vector2((arrowPosition.X + 45f), (arrowPosition.Y + 7.5f));

        }

        public Arrows(Vector2 pos, float r)
        {
            arrowPosition = pos;
            arotation = r;
            //arrowVelocity = vel;
            arrowVelocity = new Vector2((float)(10 * (Math.Cos(arotation))), (float)(10 * Math.Sin(arotation)));
                 arrowCenter = new Vector2((float)Math.Cos(2 * Math.PI * (arrowPosition.X + 45f)), (float)Math.Sin(2 * Math.PI * (arrowPosition.X + 45f)));
        }

        public void aMove()
        {
            
            //arrowVelocity.X = ((float)(20 * (Math.Cos(arotation))));
            //arrowVelocity.Y = ((float)(20 * (Math.Sin(arotation))));

            arrowPosition += (2 * arrowVelocity);
            arrowCenter += (2*arrowVelocity);

                      
        }

        public void Pause()
        {
            arrowVelocity = new Vector2(0,0);
        }
        

        public Vector2 ArrowVelocity
        {
            get
            {
                return arrowVelocity;
            }

            set
            {
                arrowVelocity = value;
            }
        }
        
        public Vector2 ArrowPosition
        {
            get
            {
                return arrowPosition;
            }

            set
            {
                arrowPosition = value;
            }
        }

        public float ArrowRotation
        {
            get
            {
                return arotation;
            }

            set
            {
                arotation = value;
            }
        }
        




    }


}
