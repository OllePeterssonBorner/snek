using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace snek
{
    [System.Runtime.InteropServices.Guid("D024C003-7B5E-4B5C-9FF6-D1235315B79B")]
    public class Game1 : Game

    {

        private SpriteFont font;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public List<Object> _Objects;
        public List<Object> _Snek_Objects;
        public List<Object> _Misc_Objects;
        public List<gridbackg> _backg_Objects;
        public List<Object> _Apple_Objects;
        public List<Loadingscreens> _Loadingscreens;
        public List<Object> _Banana_Objects;
        public int[] _TurnX = new int[21];
        public int[] _TurnY = new int[21];



        public static int screen_width = 1000;
        public static int screen_height = 1000;

        public static int arbitraryNumX = 65;
        public static int arbitraryNumY = 185;

        private float odnumvert = (screen_width - 50) / 50;
        private float odnumhoriz = (screen_height - 50) / 50;

        public int arbitraryEquationX = screen_width / 2;
        public int arbitraryEquationY = screen_height / 2;

        public bool dieded = false;
        public bool hepressbutton = false;

        public float speed = 1f;
        public float rotation = 0.0f;
        public float imgscale = 1.0f;
        public float snekscale = 1.0f;
        public float time;

        public double oldtime;
        public double newtime;
        public double snaketimeold;
        public double snaketimenew;
        public double bananaremover;

        private int applecounter = 0;
        public int bananasurvivalcounter = 0;
        public int bananahydracounter = 0;
        public int turner;
        

        public Texture2D SnekBodImg;
        public Texture2D SnekHeadImgRight;
        public Texture2D SnekHeadImgLeft;
        public Texture2D SnekHeadImgUp;
        public Texture2D SnekHeadImgDown;

        public Texture2D AppleImg;
        public Texture2D darkgreysquareImg;
        public Texture2D lightgreysquareImg;
        public Texture2D deathsceneImg;
        public Texture2D bananakillImg;

      



        //int gameState = 1; //0 => meny | 1 => spela snake | 2 => death meny

        GameStates PrimaryCurrentState;
        Gamemodes secondaryCurrentSate;
        enum GameStates
        {
            menu = 0,
            play,
            death,


        }


        enum Gamemodes
        {
            normal = 0,
            enemies,
            hardcore,
        }




        public Game1()
        {


            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            _Banana_Objects = new List<Object>();
            _Objects = new List<Object>();
            _Snek_Objects = new List<Object>();
            _Misc_Objects = new List<Object>();
            _backg_Objects = new List<gridbackg>();
            _Apple_Objects = new List<Object>();
            _Loadingscreens = new List<Loadingscreens>();
            PrimaryCurrentState = GameStates.play;

            //TurnX/Y lista


            for (int i = 0; i < screen_width / 50; i++)
            {
                _TurnX[i] = (50 * i) - 15;
                _TurnY[i] = (50 * i);
            }
            _TurnY[0] = 25;


            _graphics.PreferredBackBufferWidth = screen_width;
            _graphics.PreferredBackBufferHeight = screen_height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Random r = new Random();
            turner = r.Next(0, 2);

            font = Content.Load<SpriteFont>("applecounter");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bananakillImg = Content.Load<Texture2D>("funibananmanlaugh");
            deathsceneImg = Content.Load<Texture2D>("deathscene");
            SnekBodImg = Content.Load<Texture2D>("detkropp");
            SnekHeadImgLeft = Content.Load<Texture2D>("dethuvudVÄNSTER");
            SnekHeadImgRight = Content.Load<Texture2D>("dethuvudHÖGER");
            SnekHeadImgUp = Content.Load<Texture2D>("dethuvudUPPÅT");
            SnekHeadImgDown = Content.Load<Texture2D>("dethuvudNEDÅT");
            AppleImg = Content.Load<Texture2D>("äppelfotografi");
            darkgreysquareImg = Content.Load<Texture2D>("darkgreysquareimg");
            lightgreysquareImg = Content.Load<Texture2D>("lightgreysquareimg");

            int initialr = r.Next(0, 2);

            int xdir = 0;
            int ydir = 0;

            if (initialr == 0) //X-dir
            {
                xdir = r.Next(0, 2);
                if (xdir == 0)
                {
                    xdir = -1;
                }
                
            }
            else if (initialr == 1) //Y-dir
            {
                ydir = r.Next(0, 2);
                if (ydir == 0)
                {
                    ydir = -1;
                }
            }

            
            

            if (xdir == 1)
            {
                _Snek_Objects.Add(new Snek(SnekHeadImgRight, new Vector2(screen_width / 2, (screen_height / 2)), new Vector2(xdir, ydir), speed, rotation, snekscale, time));
                for (int i = 0; i < 1; i++)
                {
                    _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(arbitraryEquationX + (50 * i) - 50, arbitraryEquationY), new Vector2(xdir, ydir), speed, rotation, snekscale, time));
                }

            }
            else if (xdir == -1)
            {
                _Snek_Objects.Add(new Snek(SnekHeadImgLeft, new Vector2(screen_width / 2, (screen_height / 2)), new Vector2(xdir, ydir), speed, rotation, snekscale, time));
                for (int i = 0; i < 1; i++)
                {
                    _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(arbitraryEquationX + (50 * i) + 50, arbitraryEquationY), new Vector2(xdir, ydir), speed, rotation, snekscale, time));
                }
            }
            else if (ydir == 1)
            {
                _Snek_Objects.Add(new Snek(SnekHeadImgDown, new Vector2(screen_width / 2, (screen_height / 2)), new Vector2(xdir, ydir), speed, rotation, snekscale, time));
                for (int i = 0; i < 1; i++)
                {
                    _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(arbitraryEquationX, arbitraryEquationY + (50 * i) - 50), new Vector2(xdir, ydir), speed, rotation, snekscale, time));
                }
            }

            else if (ydir == -1)
            {
                _Snek_Objects.Add(new Snek(SnekHeadImgUp, new Vector2(screen_width / 2, (screen_height / 2)), new Vector2(xdir, ydir), speed, rotation, snekscale, time));
                for (int i = 0; i < 1; i++)
                {
                    _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(arbitraryEquationX, arbitraryEquationY + (50 * i) + 50), new Vector2(xdir, ydir), speed, rotation, snekscale, time));
                }
            }





            for (int i = 0; i < odnumvert + 1; i++)
            {
                for (int t = 0; t < odnumhoriz + 1; t++)
                {
                    if (i % 2 != 0)
                    {
                        if ((t % 2) != 0)
                        {
                            _backg_Objects.Add(new gridbackg(lightgreysquareImg, new Vector2(50 * t, 50 * i), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                        }
                        else
                        {
                            _backg_Objects.Add(new gridbackg(darkgreysquareImg, new Vector2(50 * t, 50 * i), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                        }
                    }
                    else
                    {
                        if ((t % 2) == 0)
                        {
                            _backg_Objects.Add(new gridbackg(lightgreysquareImg, new Vector2(50 * t, 50 * i), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                        }
                        else
                        {
                            _backg_Objects.Add(new gridbackg(darkgreysquareImg, new Vector2(50 * t, 50 * i), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                        }
                    }


                }
            }




        }

        private void Speedometer()
        {


            if (applecounter <= 4)
            {
                for (int i = 0; i < _Snek_Objects.Count; i++)
                {
                    _Snek_Objects[i]._speed = 1.25f;
                    speed = 1.25f;
                }
            }
            else if (applecounter > 9 && applecounter <= 19)
            {
                for (int i = 0; i < _Snek_Objects.Count; i++)
                {
                    _Snek_Objects[i]._speed = 2.5f;
                    speed = 2.5f;
                }
            }
            else if (applecounter > 19 && applecounter <= 29)
            {
                for (int i = 0; i < _Snek_Objects.Count; i++)
                {
                    _Snek_Objects[i]._speed = 5.0f;
                    speed = 5f;
                }
            }
            else if (applecounter > 29 && applecounter <= 39)
            {
                for (int i = 0; i < _Snek_Objects.Count; i++)
                {
                    _Snek_Objects[i]._speed = 10.0f;
                    speed = 10f;

                }
            }
        }

        private void Death()
        {
            for (int i = 0; i < _backg_Objects.Count; i++)
            {
                _backg_Objects.RemoveAt(i);
            }
            for (int i = 0; i < _Snek_Objects.Count; i++)
            {
                _Snek_Objects.RemoveAt(i);
            }

            _Loadingscreens.Add(new Loadingscreens(deathsceneImg, new Vector2(0, 0), new Vector2(0, 0), 0.0f, 1.0f));

            PrimaryCurrentState = GameStates.death;

        }



        protected override void Update(GameTime gameTime)
        {
            Random r = new Random();

            int appleposX = r.Next(0, _TurnX.Length);
            int appleposY = r.Next(0, _TurnY.Length);

            

            int slumping = r.Next(0, 100);

            int knugen = r.Next(0, 100);



            if (slumping == knugen && _Banana_Objects.Count == 0)
            {
                if (bananahydracounter > 0)
                {
                    _Banana_Objects.Add(new Object(bananakillImg, new Vector2(_Snek_Objects[0]._pos.X, _Snek_Objects[0]._pos.Y - 250), new Vector2(0, 1), speed, 0.0f, 1.0f, 0.0f));
                    _Banana_Objects.Add(new Object(bananakillImg, new Vector2(_Snek_Objects[0]._pos.X - 250, _Snek_Objects[0]._pos.Y), new Vector2(1, 0), speed, 0.0f, 1.0f, 0.0f));
                    _Banana_Objects.Add(new Object(bananakillImg, new Vector2(_Snek_Objects[0]._pos.X, _Snek_Objects[0]._pos.Y + 250), new Vector2(0, -1), speed, 0.0f, 1.0f, 0.0f));
                    _Banana_Objects.Add(new Object(bananakillImg, new Vector2(_Snek_Objects[0]._pos.X + 250, _Snek_Objects[0]._pos.Y), new Vector2(-1, 0), speed, 0.0f, 1.0f, 0.0f));

                    bananahydracounter = 0;
                }

                else if (_Snek_Objects[0]._dir.X == 1)
                {
                    _Banana_Objects.Add(new Object(bananakillImg, new Vector2(_Snek_Objects[0]._pos.X + 250, _Snek_Objects[0]._pos.Y), new Vector2(-1, 0), speed, 0.0f, 1.0f, 0.0f));
                }

                else if (_Snek_Objects[0]._dir.X == -1)
                {
                    _Banana_Objects.Add(new Object(bananakillImg, new Vector2(_Snek_Objects[0]._pos.X - 250, _Snek_Objects[0]._pos.Y), new Vector2(1, 0), speed, 0.0f, 1.0f, 0.0f));
                }

                else if (_Snek_Objects[0]._dir.Y == 1)
                {
                    _Banana_Objects.Add(new Object(bananakillImg, new Vector2(_Snek_Objects[0]._pos.X, _Snek_Objects[0]._pos.Y + 250), new Vector2(0, -1), speed, 0.0f, 1.0f, 0.0f));
                }

                else if (_Snek_Objects[0]._dir.Y == -1)
                {
                    _Banana_Objects.Add(new Object(bananakillImg, new Vector2(_Snek_Objects[0]._pos.X, _Snek_Objects[0]._pos.Y - 250), new Vector2(0, 1), speed, 0.0f, 1.0f, 0.0f));
                }


            }

            for (int i = 0; i < _Banana_Objects.Count; i++)
            {
                if (_Banana_Objects[i]._pos.X <= -1 || _Banana_Objects[i]._pos.X >= (screen_width) || _Banana_Objects[i]._pos.Y <= -1 || _Banana_Objects[i]._pos.Y >= (screen_height))
                {
                    _Banana_Objects.RemoveAt(i);
                    bananasurvivalcounter++;
                }
                else if (_Snek_Objects[0]._dir == _Banana_Objects[0]._dir && (_Snek_Objects[0]._pos.Y == _Banana_Objects[0]._pos.Y && (_Snek_Objects[0]._pos.X + 50 == _Banana_Objects[0]._pos.X || _Snek_Objects[0]._pos.X - 50 == _Banana_Objects[0]._pos.X) || (_Snek_Objects[0]._pos.X == _Banana_Objects[0]._pos.X && (  _Snek_Objects[0]._pos.Y + 50 == _Banana_Objects[0]._pos.Y || _Snek_Objects[0]._pos.Y - 50 == _Banana_Objects[0]._pos.Y))))
                {
                    _Banana_Objects.RemoveAt(i);
                    bananasurvivalcounter++;
                    bananahydracounter++;
                }
            }
          



            snaketimenew = gameTime.TotalGameTime.TotalSeconds;








            if (_Snek_Objects[0]._pos.X <= -1 || _Snek_Objects[0]._pos.X >= (screen_width) || _Snek_Objects[0]._pos.Y <= -1 || _Snek_Objects[0]._pos.Y >= (screen_height))
            {
                dieded = true;
            }

           

            


            if (PrimaryCurrentState == GameStates.death && dieded == true && KeyMouseReader.keyState.IsKeyDown(Keys.Enter))
            {
                PrimaryCurrentState = GameStates.play;
                
                
            }

            if (PrimaryCurrentState == GameStates.play)
            {
                dieded = false;
                _Loadingscreens.Clear();

                for (int i = 1; i < _Snek_Objects.Count; i++)
                {
                    if (_Snek_Objects[0]._bb.Intersects(_Snek_Objects[i]._bb) && i != 1)
                    {
                        oldtime = gameTime.TotalGameTime.TotalSeconds;
                        dieded = true;
                    }

                    for (int ä = 0; ä < _Banana_Objects.Count; ä++)
                    {
                        if (_Snek_Objects[0].Hit(_Banana_Objects[ä]._bb))
                        {
                            oldtime = gameTime.TotalGameTime.TotalSeconds;
                            dieded = true;



                        }

                    }
                    

                }



                if (dieded == true)
                {
                    Death();
                }










                if (_Apple_Objects.Count == 0)
                {

                    _Apple_Objects.Add(new Object(AppleImg, new Vector2(_TurnX[appleposX] + 15, _TurnY[appleposY] - 10), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                    snaketimeold = gameTime.TotalGameTime.TotalSeconds;
                    applecounter++;

                    if (_Snek_Objects[_Snek_Objects.Count - 1]._dir.X == -1)
                    {
                        _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(_Snek_Objects[_Snek_Objects.Count - 1]._pos.X + 50.0f, _Snek_Objects[_Snek_Objects.Count - 1]._pos.Y), _Snek_Objects[_Snek_Objects.Count - 1]._dir, speed, rotation, snekscale, time));

                    }
                    else if (_Snek_Objects[_Snek_Objects.Count - 1]._dir.X == 1)
                    {
                        _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(_Snek_Objects[_Snek_Objects.Count - 1]._pos.X - 50.0f, _Snek_Objects[_Snek_Objects.Count - 1]._pos.Y), _Snek_Objects[_Snek_Objects.Count - 1]._dir, speed, rotation, snekscale, time));

                    }
                    if (_Snek_Objects[_Snek_Objects.Count - 1]._dir.Y == -1)
                    {
                        _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(_Snek_Objects[_Snek_Objects.Count - 1]._pos.X, _Snek_Objects[_Snek_Objects.Count - 1]._pos.Y + 50.0f), _Snek_Objects[_Snek_Objects.Count - 1]._dir, speed, rotation, snekscale, time));

                    }
                    else if (_Snek_Objects[_Snek_Objects.Count - 1]._dir.Y == 1)
                    {
                        _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(_Snek_Objects[_Snek_Objects.Count - 1]._pos.X, _Snek_Objects[_Snek_Objects.Count - 1]._pos.Y - 50.0f), _Snek_Objects[_Snek_Objects.Count - 1]._dir, speed, rotation, snekscale, time));

                    }
                }



                KeyMouseReader.Update();

                if (KeyMouseReader.KeyPressed(Keys.F))
                {
                    _Apple_Objects.RemoveAt(0);

                }


                else if (_Apple_Objects.Count > 0 && _Snek_Objects[0].Hit(_Apple_Objects[0]._bb))
                {
                    _Apple_Objects.RemoveAt(0);

                }



                if ((KeyMouseReader.keyState.IsKeyDown(Keys.Down) || KeyMouseReader.keyState.IsKeyDown(Keys.End) || KeyMouseReader.keyState.IsKeyDown(Keys.N)) && _Snek_Objects[0]._dir.Y != 1)
                {
                   
                    for (int a = 0; a < _TurnX.Length; a++)
                    {
                        if (_Snek_Objects[0]._pos.X == _TurnX[a])
                        {
                            _Snek_Objects[0]._dir = new Vector2(0, 1);
                        }
                    }
                }

                if ((KeyMouseReader.keyState.IsKeyDown(Keys.Up) || KeyMouseReader.keyState.IsKeyDown(Keys.Home) || KeyMouseReader.keyState.IsKeyDown(Keys.U)) && _Snek_Objects[0]._dir.Y != -1)
                {
                    
                    for (int b = 0; b < _TurnX.Length; b++)
                    {
                        if (_Snek_Objects[0]._pos.X == _TurnX[b])
                        {
                            _Snek_Objects[0]._dir = new Vector2(0, -1);




                        }
                    }
                }
                if ((KeyMouseReader.keyState.IsKeyDown(Keys.Right) || KeyMouseReader.keyState.IsKeyDown(Keys.PageDown) || KeyMouseReader.keyState.IsKeyDown(Keys.H)) && _Snek_Objects[0]._dir.X != -1)
                {
                    for (int c = 0; c < _TurnY.Length; c++)
                    {
                        if (_Snek_Objects[0]._pos.Y == _TurnY[c])
                        {
                            _Snek_Objects[0]._dir = new Vector2(1, 0);

                        }
                    }
                }
                if ((KeyMouseReader.keyState.IsKeyDown(Keys.Left) || KeyMouseReader.keyState.IsKeyDown(Keys.Delete) || KeyMouseReader.keyState.IsKeyDown(Keys.V)) && _Snek_Objects[0]._dir.X != 1)
                {
                    for (int d = 0; d < _TurnY.Length; d++)
                    {
                        if (_Snek_Objects[0]._pos.Y == _TurnY[d])
                        {
                            _Snek_Objects[0]._dir = new Vector2(-1, 0);

                        }
                    }
                }


                if (_Snek_Objects[0]._dir.X == -1)
                {
                    _Snek_Objects[0]._img = SnekHeadImgLeft;
                }
                else if (_Snek_Objects[0]._dir.X == 1)
                {
                    _Snek_Objects[0]._img = SnekHeadImgRight;
                }
                else if (_Snek_Objects[0]._dir.Y == -1)
                {
                    _Snek_Objects[0]._img = SnekHeadImgUp;
                }
                else if (_Snek_Objects[0]._dir.Y == 1)
                {
                    _Snek_Objects[0]._img = SnekHeadImgDown;
                }








                if (KeyMouseReader.keyState.IsKeyDown(Keys.Tab))
                {
                    

                    if (_Snek_Objects[0]._dir.Y != 1 || _Snek_Objects[0]._dir.Y != -1)
                    {
                        for (int f = 0; f < _TurnY.Length; f++)
                        {
                            if (_Snek_Objects[0]._pos.Y == _TurnY[f] && turner == 0)
                            {
                                _Snek_Objects[0]._dir = new Vector2(-1, 0);

                            }
                            else if (_Snek_Objects[0]._pos.Y == _TurnY[f] && turner == 1)
                            {
                                _Snek_Objects[0]._dir = new Vector2(1, 0);

                            }
                        }
                    }
                    else if (_Snek_Objects[0]._dir.X != 1 || _Snek_Objects[0]._dir.X != -1)
                    {
                        for (int q = 0; q < _TurnX.Length; q++)
                        {
                            if (_Snek_Objects[0]._pos.X == _TurnX[q] && turner == 0)
                            {
                                _Snek_Objects[0]._dir = new Vector2(0, 1);

                            }
                            else if (_Snek_Objects[0]._pos.X == _TurnX[q] && turner == 1)
                            {
                                _Snek_Objects[0]._dir = new Vector2(0, -1);

                            }
                        }
                    }
                }




                for (int i = 1; i < _Snek_Objects.Count; i++)
                {
                    if (_Snek_Objects[i - 1]._dir.Y == 0)
                    {

                        if (_Snek_Objects[i]._dir.Y == 1 && (_Snek_Objects[i]._pos.Y >= _Snek_Objects[i - 1]._pos.Y))
                        {
                            _Snek_Objects[i]._dir = _Snek_Objects[i - 1]._dir;
                        }
                        else if (_Snek_Objects[i]._dir.Y == -1 && (_Snek_Objects[i]._pos.Y <= _Snek_Objects[i - 1]._pos.Y))
                        {
                            _Snek_Objects[i]._dir = _Snek_Objects[i - 1]._dir;
                        }

                    }
                    else if (_Snek_Objects[i - 1]._dir.X == 0)
                    {

                        if (_Snek_Objects[i]._dir.X == 1 && (_Snek_Objects[i]._pos.X >= _Snek_Objects[i - 1]._pos.X))
                        {
                            _Snek_Objects[i]._dir = _Snek_Objects[i - 1]._dir;
                        }
                        else if (_Snek_Objects[i]._dir.X == -1 && (_Snek_Objects[i]._pos.X <= _Snek_Objects[i - 1]._pos.X))
                        {
                            _Snek_Objects[i]._dir = _Snek_Objects[i - 1]._dir;
                        }

                    }


                }





                Speedometer();




                float speedmodifyer = speed;











                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                foreach (Snek o in _Snek_Objects)
                {
                    o.Update(screen_width, screen_height);
                }

                foreach (gridbackg o in _backg_Objects)
                {
                    o.Update(screen_width, screen_height);
                }
                foreach (Object o in _Apple_Objects)
                {
                    o.Update(screen_width, screen_height);
                }
                foreach (Object o in _Banana_Objects)
                {
                    o.Update(screen_width, screen_height);
                }

            }

            base.Update(gameTime);


        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            if (PrimaryCurrentState == GameStates.play)
            {
                

                foreach (gridbackg o in _backg_Objects)
                {
                    o.Draw(_spriteBatch);
                }
                foreach (Object o in _Banana_Objects)
                {
                    o.Draw(_spriteBatch);
                }

                foreach (Snek o in _Snek_Objects)
                {
                    o.Draw(_spriteBatch);
                }

                foreach (Object o in _Apple_Objects)
                {
                    o.Draw(_spriteBatch);
                }


                _spriteBatch.DrawString(font, "Score:" + (applecounter - 1), new Vector2(125, 100), Color.DarkViolet);

                _spriteBatch.DrawString(font, "Sped: " + speed, new Vector2(260, 100), Color.DarkRed);

                _spriteBatch.DrawString(font, "Survived bananas: " + bananasurvivalcounter, new Vector2(450, 100), Color.Yellow);
            }
            else if (PrimaryCurrentState == GameStates.death)
            {
                foreach (Loadingscreens o in _Loadingscreens)
                {
                    o.Draw(_spriteBatch);
                }
                _spriteBatch.DrawString(font, " Your score this round:" + (applecounter - 1), new Vector2(450, 500), Color.Red);

            }





            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
