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

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PetitPrince : Microsoft.Xna.Framework.Game
    {

        #region Vars
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        enum boardPos { cover, asteriod, planets, mountains, trees, instructions, gameStyle, end };

        boardPos currentLocation;

        KeyboardState oldKeyState;
        KeyboardState oldKeyState_1;
        GamePadState oldDpadState;
        //keystates that allow detection of single keypress

        bool isClassic;
        //used to check if user is playing in endless or classic mode. 

        Texture2D cover;
        Texture2D asteriod;
        Texture2D planets;
        Texture2D mountains;
        Texture2D trees;
        Texture2D instruction1;
        Texture2D gameStyle;
        Texture2D end;
        //the 8 different backgrounds. only 4 are actual locations. 

        Texture2D spaceShip;
        Texture2D circleShip;
        Texture2D Circle;
        Texture2D Prince;
        Texture2D spaceShipHighlight;
        Texture2D verticalLine;
        //first 5 are different kinds of spaceships. the last one is the straight line that makes up the circle. 

        int textureValue = 0;
        //denotes which spaceship is active right now. 

        Texture2D[] SpaceShips = new Texture2D[4];
        //contains the differnet spaceships. 

        Texture2D hat;
        Texture2D hatExplosion;
        // the two hats..2nd one is the exploded version . 
        Texture2D arrow;

        List<HatPosition> HatList;
        //contains the hat. HatPosition is the class they get their values from .

        //List<ScoreCircle> ScoreGraph;
        List<float> ScoreGraph = new List<float>();
        //contains the numerous lines that make up the circle. 

        List<Arrows> ArrowList;
        int arrowCount = 1;



        Vector2 hatVector2;
        Vector2 avatarVector;
        int hatX, hatY;
        //denotes the positions of the hats. 

        float rotation = 0f;
        double vibrationTimer;
        //rotation is rotation of avatar, and of arrows. vibrationTimer is used to turn of the vibrating moters after a time. 

        Vector2 centerVector = new Vector2(50, 700);
        //the position where the circle is located. 


        public static Random mainGen = new Random();



        int randomBoardNumber;
        int quadNumber;
        //quadNumber will later denote which enumerated location is active. 
        SpriteFont ComicSansMS;
        //font used to write highscore and score. 

        Song song0, song1, song2, song3, song4, song5, song6, song7;
        SoundEffect smack;
        const float pi = (float)(Math.PI);
        //the music and sound. music freely obtained online (source cited in PDF). Sound obtained form Richard Gesick. 
        //pi holds the value of pi, so that the Math class is not called over and over again. 
        #endregion



        #region level Arrays

        int[] countofHats = { 10, 15, 20 };
        int[] lineRemovalFactor = { 7, 9, 12 };
        int[] lineAdditionFactor = { 32, 30, 22 };
        float[] rotationFactor = { 0.13f, 0.10f, 0.07f };
        //denote the multiiple levels. the first value in each array is for easy, 2nd is for medium, 3rd is for hard.                 
        int value;
        #endregion


        #region class Object Declarations

        PlayerPosition p = new PlayerPosition();


        HighScore hs = new HighScore();
        #endregion



        public PetitPrince()
        {
            #region interface settings
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            
            graphics.PreferredBackBufferWidth = 1056;
            graphics.PreferredBackBufferHeight = 792;

             //graphics.IsFullScreen = true;
             //graphics.ApplyChanges();
            //these can be uncommented to make the game full-screen. 
            IsMouseVisible = true;
            #endregion

        }

        #region properties
        public Vector2 AvatarVector
        {
            get
            {
                return avatarVector;
            }

            set
            {
                avatarVector = value;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        #endregion


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            #region List Declarations, and controller State Variables ]
            currentLocation = boardPos.cover;


            HatList = new List<HatPosition>();
            ArrowList = new List<Arrows>(2);



            oldKeyState = Keyboard.GetState();
            oldKeyState_1 = Keyboard.GetState();
            oldDpadState = GamePad.GetState(PlayerIndex.One);
            #endregion
            avatarVector = p.PlayerVector;
            //holds the current location of the avatar. 
            #region Add Score, more object declarations




            for (double i = 0; i <= pi; i += ((pi) / 500))
            {

                ScoreGraph.Add((float)i);

            }
            //creats the graph from 0 to pi. 

            HatPosition h = new HatPosition(hat, 1);
            //initiates the hatlist class. 
            #endregion

            //    MediaPlayer.IsMuted = true;
            //can be uncommented to mute the game. 

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            #region Content Loads
            spaceShip = Content.Load<Texture2D>("Smaller Petit Prince Space Ship");
            circleShip = Content.Load<Texture2D>("Petit Prince Circle Ship");
            Circle = Content.Load<Texture2D>("Petit Prince Circle");
            Prince = Content.Load<Texture2D>("Petit Prince");
            spaceShipHighlight = Content.Load<Texture2D>("Petit Prince Space Ship Highlight");
            //the spaceships. 

            hat = Content.Load<Texture2D>("Boa Constrictor and Elephant");
            arrow = Content.Load<Texture2D>("arrow");
            hatExplosion = Content.Load<Texture2D>("Boa Constrictor and Elephant Explosion2");
            //hats and arrows. 

            verticalLine = Content.Load<Texture2D>("Vertical Line");
            //the circle-graph lines. 

            cover = Content.Load<Texture2D>("Petit Prince New Screen");
            asteriod = Content.Load<Texture2D>("B-612");
            planets = Content.Load<Texture2D>("planets");
            mountains = Content.Load<Texture2D>("Mountains");
            trees = Content.Load<Texture2D>("Trees");
            instruction1 = Content.Load<Texture2D>("InstructionSet ONe");
            gameStyle = Content.Load<Texture2D>("GameStyle");
            end = Content.Load<Texture2D>("end");
            //the enumerated textures. 

            ComicSansMS = Content.Load<SpriteFont>("ComicSansMS");
            //font

            song0 = Content.Load<Song>("S01 Salvation");
            song1 = Content.Load<Song>("S05 Low Bentstrumental");
            song2 = Content.Load<Song>("S02 Ice");
            song3 = Content.Load<Song>("S03 Snoisses");
            song4 = Content.Load<Song>("S04 Danger Zone");
            song5 = Content.Load<Song>("S05 Crunch Time");
            song6 = Content.Load<Song>("S06 UpDown");
            song7 = Content.Load<Song>("S07 Standing");
            smack = Content.Load<SoundEffect>("smack (2)");
            //music and sound. 

            SpaceShips[0] = spaceShip;
            SpaceShips[1] = circleShip;
            SpaceShips[2] = Circle;
            SpaceShips[3] = Prince;
            //giving values to the array of spaceships. 
            #endregion

            #region creating Hats

            MediaPlayer.Play(song0);
            //starts the first song. 

            for (int i = 0; i < countofHats[value]; i++)
            {
                HatList.Add(new HatPosition(hat, i));
            }
            //creating the hats. 
            #endregion



            spriteBatch = new SpriteBatch(GraphicsDevice);


        }


        protected override void UnloadContent()
        {

        }

        public Vector2 boaPosition()
        {

            Vector2 boaVector = new Vector2(hatX, hatY);
            //giving each hat it's position. 
            return (boaVector);

        }




        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            #region Vibrations
            vibrationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (vibrationTimer > 250)
            {
                GamePad.SetVibration(PlayerIndex.One, 0, 0);

                vibrationTimer = 0;
            }

            //stopping the vibration motors if time is greater than .250 secs
            #endregion



            #region Controller States
            KeyboardState currentKeyboard = Keyboard.GetState();


            GamePadState currentGamepad = GamePad.GetState(PlayerIndex.One);

            KeyboardState newKeyState = Keyboard.GetState();
            GamePadState newDpadState = GamePad.GetState(PlayerIndex.One);
            #endregion

            #region switch (currentLocation) for movement across screens

            switch (currentLocation)
            {

                case boardPos.cover:
                    #region Level Chooser
                    if ((newKeyState.IsKeyDown(Keys.E) && (oldKeyState.IsKeyUp(Keys.E))) || (newDpadState.IsButtonDown(Buttons.X) && (oldDpadState.IsButtonUp(Buttons.X))))
                    {
                        value = 0;
                        currentLocation = boardPos.gameStyle;
                        MediaPlayer.Play(song6);

                    }

                    if ((newKeyState.IsKeyDown(Keys.M) && (oldKeyState.IsKeyUp(Keys.M))) || (newDpadState.IsButtonDown(Buttons.Y) && (oldDpadState.IsButtonUp(Buttons.Y))))
                    {
                        value = 1;
                        currentLocation = boardPos.gameStyle;
                        MediaPlayer.Play(song6);
                    }
                    if (currentKeyboard.IsKeyDown(Keys.H) || currentGamepad.IsButtonDown(Buttons.B))
                    {
                        value = 2;
                        currentLocation = boardPos.gameStyle;
                        MediaPlayer.Play(song6);
                    }
                    //takes in which level the user wants to play in. 

                    if (currentKeyboard.IsKeyDown(Keys.T) || currentGamepad.IsButtonDown(Buttons.LeftShoulder))
                    {
                        currentLocation = boardPos.instructions;
                        MediaPlayer.Play(song7);
                    }
                    //opens up the instructions. 


                    rotation = rotationFactor[value];
                    //sets rotation according to level



                    break;
                    #endregion

                case boardPos.asteriod:
                    #region Quandrant 1 controls
                    if (avatarVector.X > graphics.PreferredBackBufferWidth - 35)
                        avatarVector.X = graphics.PreferredBackBufferWidth - 35;


                    if (avatarVector.X < 0)
                    {
                        currentLocation = boardPos.planets;
                        MediaPlayer.Play(song2);
                        avatarVector.X = graphics.PreferredBackBufferWidth - 105;
                    }

                    if (avatarVector.Y < 33)
                        avatarVector.Y = 33;

                    if (avatarVector.Y > graphics.PreferredBackBufferHeight)
                    {
                        currentLocation = boardPos.trees;
                        MediaPlayer.Play(song4);
                        avatarVector.Y = 0;
                    }

                    foreach (HatPosition h in HatList)
                    {
                        if (h.HatPositionVector.X > (graphics.PreferredBackBufferWidth - 22))
                            h.HatVelocity = new Vector2(-h.HatVelocity.X, h.HatVelocity.Y);

                        if (h.HatPositionVector.X < 0)
                        {
                            h.HatPositionVector = new Vector2(graphics.PreferredBackBufferWidth - 22, h.HatPositionVector.Y);
                            h.RandomNumber = 2;
                        }

                        if (h.HatPositionVector.Y < 22)
                            h.HatVelocity = new Vector2(h.HatVelocity.X, -h.HatVelocity.Y);

                        if (h.HatPositionVector.Y > graphics.PreferredBackBufferHeight)
                        {
                            h.HatPositionVector = new Vector2(h.HatPositionVector.X, 0);
                            h.RandomNumber = 4;
                        }
                    }




                    break;
                    #endregion

                case boardPos.planets:
                    #region Quadrant 2 controls
                    if (avatarVector.X > graphics.PreferredBackBufferWidth - 105)
                    {
                        avatarVector.X = 0;
                        currentLocation = boardPos.asteriod;
                        MediaPlayer.Play(song1);
                    }

                    if (avatarVector.X < 35)
                    {
                        avatarVector.X = 35;
                    }

                    if (avatarVector.Y > graphics.PreferredBackBufferHeight)
                    {
                        avatarVector.Y = 0;
                        currentLocation = boardPos.mountains;
                        MediaPlayer.Play(song3);
                    }

                    if (avatarVector.Y < 33)
                        avatarVector.Y = 33;

                    foreach (HatPosition h in HatList)
                    {
                        if (h.HatPositionVector.X > (graphics.PreferredBackBufferWidth))
                        {
                            h.HatPositionVector = new Vector2(0, h.HatPositionVector.Y);
                            h.RandomNumber = 1;
                        }

                        if (h.HatPositionVector.X < 22)
                            h.HatVelocity = new Vector2(-h.HatVelocity.X, h.HatVelocity.Y);


                        if (h.HatPositionVector.Y < 22)
                            h.HatVelocity = new Vector2(h.HatVelocity.X, -h.HatVelocity.Y);

                        if (h.HatPositionVector.Y > graphics.PreferredBackBufferHeight - 22)
                        {
                            h.HatPositionVector = new Vector2(h.HatPositionVector.X, 0);
                            h.RandomNumber = 3;
                        }
                    }
                    break;
                    #endregion

                case boardPos.mountains:
                    #region Quadrant 3 controls
                    if (avatarVector.X < 35)
                        avatarVector.X = 35;

                    if (avatarVector.X > graphics.PreferredBackBufferWidth - 35)
                    {
                        avatarVector.X = graphics.PreferredBackBufferWidth - 33;

                    }

                    if (avatarVector.Y < 0)
                    {
                        avatarVector.Y = graphics.PreferredBackBufferHeight;
                        currentLocation = boardPos.planets;
                        MediaPlayer.Play(song2);
                    }

                    if (avatarVector.Y > graphics.PreferredBackBufferHeight - 33)
                    {
                        avatarVector.Y = graphics.PreferredBackBufferHeight - 33;
                    }

                    foreach (HatPosition h in HatList)
                    {
                        if (h.HatPositionVector.X > (graphics.PreferredBackBufferWidth - 22))
                            h.HatVelocity = new Vector2(-h.HatVelocity.X, h.HatVelocity.Y);

                        if (h.HatPositionVector.X < 22)
                            h.HatVelocity = new Vector2(-h.HatVelocity.X, h.HatVelocity.Y);


                        if (h.HatPositionVector.Y < 0)
                        {
                            h.HatPositionVector = new Vector2(h.HatPositionVector.X, graphics.PreferredBackBufferHeight - 22);
                            h.RandomNumber = 2;
                        }

                        if (h.HatPositionVector.Y > graphics.PreferredBackBufferHeight - 15)
                            h.HatVelocity = new Vector2(h.HatVelocity.X, -h.HatVelocity.Y);


                    }

                    break;

                    #endregion

                case boardPos.trees:
                    #region Quadrant 4 controls
                    if (avatarVector.X < 35)
                    {
                        avatarVector.X = 35;


                    }

                    if (avatarVector.X > graphics.PreferredBackBufferWidth - 35)
                        avatarVector.X = graphics.PreferredBackBufferWidth - 35;

                    if (avatarVector.Y > graphics.PreferredBackBufferHeight - 33)
                        avatarVector.Y = graphics.PreferredBackBufferHeight - 33;

                    if (avatarVector.Y < 0)
                    {
                        avatarVector.Y = graphics.PreferredBackBufferHeight;
                        currentLocation = boardPos.asteriod;
                        MediaPlayer.Play(song1);
                    }

                    foreach (HatPosition h in HatList)
                    {
                        if (h.HatPositionVector.X > (graphics.PreferredBackBufferWidth - 22))
                            h.HatVelocity = new Vector2(-h.HatVelocity.X, h.HatVelocity.Y);

                        if (h.HatPositionVector.X < 22)
                            h.HatVelocity = new Vector2(-h.HatVelocity.X, h.HatVelocity.Y);

                        if (h.HatPositionVector.Y < 0)
                        {
                            h.HatPositionVector = new Vector2(h.HatPositionVector.X, graphics.PreferredBackBufferHeight - 55);
                            h.RandomNumber = 1;
                        }

                        if (h.HatPositionVector.Y > graphics.PreferredBackBufferHeight - 22)
                            h.HatVelocity = new Vector2(h.HatVelocity.X, -h.HatVelocity.Y);



                    }



                    break;

                    #endregion
                //each of these control the positon of the avatar and hat around the screen, and across the boundaries of each texture. 

                #region checking instructions, choosing gamestyle, and checing highscore
                case boardPos.instructions:

                    if (currentKeyboard.IsKeyDown(Keys.B) || currentGamepad.IsButtonDown(Buttons.RightShoulder))
                    {
                        currentLocation = boardPos.cover;
                        MediaPlayer.Play(song0);
                    }
                    //brings back to start menu. 
                    break;
                case boardPos.gameStyle:
                    if ((newKeyState.IsKeyDown(Keys.E) && (oldKeyState.IsKeyUp(Keys.E))) || (newDpadState.IsButtonDown(Buttons.X) && (oldDpadState.IsButtonUp(Buttons.X))))
                    {
                        currentLocation = boardPos.trees;
                        MediaPlayer.Play(song4);
                        isClassic = false;
                    }
                    //choose endless level. 
                    if ((newKeyState.IsKeyDown(Keys.C) && (oldKeyState.IsKeyUp(Keys.C))) || (newDpadState.IsButtonDown(Buttons.Y) && (oldDpadState.IsButtonUp(Buttons.Y))))
                    {
                        currentLocation = boardPos.trees;
                        MediaPlayer.Play(song4);
                        isClassic = true;
                    }
                    break;
                //choose classic level. 
                case boardPos.end:

                    if ((newKeyState.IsKeyDown(Keys.B) && (oldKeyState.IsKeyUp(Keys.B))) || (newDpadState.IsButtonDown(Buttons.A) && oldDpadState.IsButtonUp(Buttons.A)))
                    {
                        currentLocation = boardPos.cover;
                        MediaPlayer.Play(song0);
                    }
                    break;
                //go back to start when at final end page. 
                #endregion
            }
            #endregion

            #region Avatar Movement

            if (currentKeyboard.IsKeyDown(Keys.Down)
                || currentGamepad.IsButtonDown(Buttons.DPadDown))
            {

                avatarVector.X -= 15 * ((float)Math.Cos(rotation)); avatarVector.Y -= 10 * ((float)Math.Sin(rotation));


            }

            if (currentKeyboard.IsKeyDown(Keys.Up)
               || currentGamepad.IsButtonDown(Buttons.DPadUp))
            {

                avatarVector.X += 15 * ((float)Math.Cos(rotation)); avatarVector.Y += 10 * ((float)Math.Sin(rotation));

            }


            if (currentKeyboard.IsKeyDown(Keys.Left)
                || currentGamepad.ThumbSticks.Right.X < 0)
            {


                rotation -= 0.07f;

            }

            if (currentKeyboard.IsKeyDown(Keys.Right)
                || currentGamepad.ThumbSticks.Right.X > 0)
            {


                rotation += 0.07f;
            }


            #endregion
            //movement and rotation of avatar. 

            #region assigining quad-number to quadrants'
            switch (currentLocation)
            {
                case boardPos.asteriod:
                    quadNumber = 1;
                    break;
                case boardPos.planets:
                    quadNumber = 2;
                    break;
                case boardPos.mountains:
                    quadNumber = 3;
                    break;
                case boardPos.trees:
                    quadNumber = 4;
                    break;
            }
            #endregion
            //assings a number to currentLocation. 

            #region moving objects
            foreach (HatPosition h in HatList)
            {
                h.move();
            }




            foreach (Arrows a in ArrowList)
                a.aMove();

            #endregion
            //moves the hats and arrows from their classes. 
            checkCollision();
            //call method to check collisions. 
            #region changing Avatar

            if ((newKeyState.IsKeyDown(Keys.S) && (oldKeyState.IsKeyUp(Keys.S))) || (newDpadState.IsButtonDown(Buttons.RightShoulder) && (oldDpadState.IsButtonUp(Buttons.RightShoulder))))
            {
                if (textureValue >= (SpaceShips.Count() - 1))
                    textureValue = 0;
                else
                    textureValue++;
            }
            #endregion
            //change the avatar..on how you wannt it. 


            #region schooting arrows
            if ((newKeyState.IsKeyDown(Keys.Space) && (oldKeyState.IsKeyUp(Keys.Space))) || newDpadState.IsButtonDown(Buttons.A) && (oldDpadState.IsButtonUp(Buttons.A)))
            {

                for (int i = 0; i < arrowCount; i++)
                    ArrowList.Add(new Arrows(avatarVector, rotation));

                int count = ScoreGraph.Count;
                for (int k = count; k > (count - lineRemovalFactor[value]); k--)
                {
                    if (ScoreGraph.Count != 1)
                        ScoreGraph.RemoveAt(k - 1);

                }
            }

            #endregion
            //shoot an arrow, lose life. 

            #region hat movement-properties

            foreach (HatPosition h in HatList)

                if ((h.RandomNumber == quadNumber) && (Vector2.DistanceSquared(h.HatPositionVector, avatarVector) < 300))
                {

                    int count = ScoreGraph.Count;
                    for (int k = count; k > (count - lineRemovalFactor[value]); k--)
                    {
                        if (ScoreGraph.Count != 1)
                            ScoreGraph.RemoveAt(k - 1);
                    }
                }
            #endregion
            //get close to hats, lose life. 

            #region End Screen
            if ((newKeyState.IsKeyDown(Keys.R) && (oldKeyState.IsKeyUp(Keys.R))) || (newDpadState.IsButtonDown(Buttons.Start) && oldDpadState.IsButtonUp(Buttons.Start)) ||
             (isClassic == true && (ScoreGraph.Count >= 1000)))
            {

                if (isClassic == false)
                {
                    //hs.updateHighScore(1);
                    hs.readHighScore();
                    hs.updateHighScore(ScoreGraph.Count);
                    

                }
                //update highscore if in endless mode. 

                ScoreGraph.Clear();
                for (double i = 0; i <= pi; i += ((pi) / 500))
                {

                    ScoreGraph.Add((float)i);

                }
                HatList.Clear();


                for (int i = 0; i < countofHats[value]; i++)
                {
                    HatList.Add(new HatPosition(hat, i));
                }

                currentLocation = boardPos.end;
                MediaPlayer.Play(song5);

                //reinitializes the basic parts of the game. 

            #endregion


            }

            oldKeyState = newKeyState;
            oldDpadState = newDpadState;
            //reset values of keypad states. 



            base.Update(gameTime);


        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            #region Draw Backgrounds and HighScore
            switch (currentLocation)
            {

                case boardPos.cover:
                    spriteBatch.Draw(cover, new Vector2(0, 0), Color.White);
                    break;


                case boardPos.asteriod:
                    spriteBatch.Draw(asteriod, new Vector2(0, 0), Color.White);


                    break;

                case boardPos.planets:
                    spriteBatch.Draw(planets, new Vector2(0, 0), Color.White);

                    break;

                case boardPos.mountains:
                    spriteBatch.Draw(mountains, new Vector2(0, 0), Color.White);

                    break;

                case boardPos.trees:
                    spriteBatch.Draw(trees, new Vector2(0, 0), Color.White);

                    break;

                case boardPos.instructions:
                    spriteBatch.Draw(instruction1, new Vector2(0, 0), Color.White);
                    break;

                case boardPos.gameStyle:
                    spriteBatch.Draw(gameStyle, new Vector2(0, 0), Color.White);
                    break;

                case boardPos.end:
                    spriteBatch.Draw(end, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(ComicSansMS, "HighScores: ", new Vector2(200, 200), Color.Red);


                    int[] temp = hs.readHighScore();
                    for (int i = 0; i < 5; i++)
                        spriteBatch.DrawString(ComicSansMS, "1. " + temp[0], new Vector2(200, 250), Color.Blue);
                    spriteBatch.DrawString(ComicSansMS, "2. " + temp[1], new Vector2(200, 300), Color.Black);
                    spriteBatch.DrawString(ComicSansMS, "3. " + temp[2], new Vector2(200, 350), Color.Black);
                    spriteBatch.DrawString(ComicSansMS, "4. " + temp[3], new Vector2(200, 400), Color.Black);
                    spriteBatch.DrawString(ComicSansMS, "5. " + temp[4], new Vector2(200, 450), Color.Black);

                    break;
            #endregion
                //draw background textures, and highscore if in end-texture. 
            }


            #region Draw Avatar, Hats, Arrows, ScoreGraph
            if (currentLocation != boardPos.cover && currentLocation != boardPos.instructions && currentLocation != boardPos.gameStyle && currentLocation != boardPos.end)
            {

                if ((textureValue == 0) && (Keyboard.GetState().IsKeyDown(Keys.RightShift) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftShoulder)))
                {
                    spriteBatch.Draw(spaceShipHighlight, avatarVector, new Rectangle(0, 0, 1100, 1300), Color.Tomato, rotation, new Vector2(49, 36.5f), 1, SpriteEffects.None, 0);
                    int count = ScoreGraph.Count;
                    for (int k = count; k > (count - 1); k--)
                    {
                        if (ScoreGraph.Count != 1)
                            ScoreGraph.RemoveAt(k - 1);
                    }

                }
                else

                    spriteBatch.Draw(SpaceShips[textureValue], avatarVector, new Rectangle(0, 0, 1100, 1300), Color.White, rotation, new Vector2(SpaceShips[textureValue].Width / 2, SpaceShips[textureValue].Height / 2), 1, SpriteEffects.None, 0);

                foreach (HatPosition h in HatList)
                {
                    if (h.RandomNumber == quadNumber)
                    {

                        spriteBatch.Draw(h.HatPicture, h.HatPositionVector, null, h.Clr, 0.0f, new Vector2(h.HatPicture.Width / 2, h.HatPicture.Height / 2), 1.0f, SpriteEffects.None, 1.0f);
                    }
                }





                foreach (Arrows a in ArrowList)
                {
                    spriteBatch.Draw(arrow, a.ArrowPosition, null, Color.White, a.ArrowRotation, new Vector2(arrow.Width / 2, arrow.Height / 2), 1.0f, SpriteEffects.None, 1.0f);
                    //spriteBatch.Draw(arrow, new Vector2((float)a.ArrowPosition.X - 89.5f, (float)a.ArrowPosition.Y - 89.5f), Color.White);



                    //spriteBatch.Draw(arrow, new Vector2(200, 200), Color.Green);
                }



                //foreach (ScoreCircle sc in ScoreGraph)
                foreach (float sc in ScoreGraph)
                {
                    spriteBatch.Draw(verticalLine, centerVector, new Rectangle(0, 0, 200, 200), Color.White, sc, new Vector2(1, 50), 1, SpriteEffects.None, 0);
                }








                if (Keyboard.GetState().IsKeyDown(Keys.T))
                    rotation = 0;


                //spriteBatch.DrawString(ComicSansMS, "Current Position\n ( " +
                //                                                   (int)avatarVector.X +
                //                                                    " , " +
                //                                                    (int)avatarVector.Y +
                //                                                    " )", new Vector2(100, 115), Color.Red);
            }

            #endregion
            //draw avatar, hats, arrows, and ScoreGraph..only if the current location is one of the current backgrounds. 
            if ((ScoreGraph.Count >= 1000) && (isClassic == false))
                spriteBatch.DrawString(ComicSansMS, "Score : " + ScoreGraph.Count, new Vector2(100, 760), Color.White);
            //displays the score if player goes beyond 1000..before 1000, the circle represents the score anyways, so it is not required. 

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        #region checkCollisions
        public void checkCollision()
        {
            for (int i = HatList.Count - 1; i >= 0; i--)
            {
                for (int j = ArrowList.Count - 1; j >= 0; j--)
                {
                    if ((HatList[i].RandomNumber == quadNumber) && (Vector2.DistanceSquared(HatList[i].HatPositionVector, ArrowList[j].ArrowPosition) < 300))
                    {

                        ArrowList.RemoveAt(j);
                        if (HatList[i].IsHit == true)
                        {
                            smack.Play();

                            HatList.RemoveAt(i);
                            //if hat is torn, and arrow hits hat, it gets removed. 
                        }

                        else
                        {
                            smack.Play();
                            HatList[i].HatPicture = hatExplosion;

                            HatList[i].IsHit = true;
                            //if hat is being hit for the first time, it gets torn. 
                        }



                        float endLine = (ScoreGraph.ElementAt(ScoreGraph.Count - 1));
                        for (double k = endLine; k < (endLine + lineAdditionFactor[value] * ((pi) / 500)); k += ((pi) / 500))
                            //ScoreGraph.Add(new ScoreCircle( (float)k));
                            ScoreGraph.Add((float)k);
                        //increases score if arrow hits hat. 

                        HatList.Add(new HatPosition(hat, i+1));
                        //creates a new hat to replace an existiing torn or removed hat. 

                        GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.5f);
                        //starts vibration, and timer to check on vibration. 
                        vibrationTimer = 0;


                        break;
                    }



                    //else GamePad.SetVibration(PlayerIndex.One, 0, 0);
                }
            }

        #endregion



        }







    }
}

