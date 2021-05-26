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

        public float speed = 0.0f;
        public float rotation = 0.0f;
        public float imgscale = 1.0f;
        public float snekscale = 1.0f;
        public float time;
        private int applecounter;

        public Texture2D SnekBodImg;
        public Texture2D SnekHeadImg;
        public Texture2D AppleImg;
        public Texture2D darkgreysquareImg;
        public Texture2D lightgreysquareImg;
        public Texture2D deathsceneImg;

        //int gameState = 1; //0 => meny | 1 => spela snake | 2 => death meny

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


            _Objects = new List<Object>();
            _Snek_Objects = new List<Object>();
            _Misc_Objects = new List<Object>();
            _backg_Objects = new List<gridbackg>();
            _Apple_Objects = new List<Object>();
            _Loadingscreens = new List<Loadingscreens>();
            currentState = GameStates.play;

            //TurnX/Y lista


            for (int i = 0; i < screen_width / 50; i++)
            {
                _TurnX[i] = (50 * i) - 15;
                _TurnY[i] = (50 * i);
            }


            _graphics.PreferredBackBufferWidth = screen_width;
            _graphics.PreferredBackBufferHeight = screen_height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Random r = new Random();

            font = Content.Load<SpriteFont>("applecounter");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            deathsceneImg = Content.Load<Texture2D>("deathscene");
            SnekBodImg = Content.Load<Texture2D>("detkropp");
            SnekHeadImg = Content.Load<Texture2D>("dethuvud");
            AppleImg = Content.Load<Texture2D>("äppelfotografi");
            darkgreysquareImg = Content.Load<Texture2D>("darkgreysquareimg");
            lightgreysquareImg = Content.Load<Texture2D>("lightgreysquareimg");

            _Snek_Objects.Add(new Snek(SnekHeadImg, new Vector2(screen_width / 2, (screen_height / 2)), new Vector2(-1, 0), speed, rotation, snekscale, time));

            for (int i = 0; i < 1; i++)
            {
                _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(arbitraryEquationX + (50 * i) + 50, arbitraryEquationY), new Vector2(-1, 0), speed, rotation, snekscale, time));
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

            currentState = GameStates.death;
                  
        }
      

        protected override void Update(GameTime gameTime)
        {
            if (_Snek_Objects[0]._pos.X <= 1 || _Snek_Objects[0]._pos.X >= (screen_width) || _Snek_Objects[0]._pos.Y <= 1 || _Snek_Objects[0]._pos.Y >= (screen_height))
            {
                Death();
            }

            if (KeyMouseReader.keyState.IsKeyDown(Keys.Enter))
            {
                hepressbutton = true;
            }

            if (currentState == GameStates.death && hepressbutton == true)
            {
                currentState = GameStates.play;
            }

            if (currentState == GameStates.play)
            {

                _Loadingscreens.Clear();

                for (int i = 1; i < _Snek_Objects.Count; i++)
                {
                    if (_Snek_Objects[0]._bb.Intersects(_Snek_Objects[i]._bb) && i != 1)
                    {
                        dieded = true;
                    }
                }

                if (dieded == true)
                {
                    Death();
                }
           

                for (int i = 0; i < _Snek_Objects.Count; i++)
                {
                    _Snek_Objects[i]._speed = 1.0f;
                }
            



                Random r = new Random();

                int appleposX = r.Next(0, _TurnX.Length);
                int appleposY = r.Next(0, _TurnY.Length);

                if (_Apple_Objects.Count == 0)
                {

                    _Apple_Objects.Add(new Object(AppleImg, new Vector2(_TurnX[appleposX] + 15, _TurnY[appleposY] -10), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));

                    applecounter++;

                    if (_Snek_Objects[_Snek_Objects.Count - 1]._dir == new Vector2(0, -1) || _Snek_Objects[_Snek_Objects.Count - 1]._dir == new Vector2(0, 1))
                    {
                        _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(_Snek_Objects[_Snek_Objects.Count - 1]._pos.X, _Snek_Objects[_Snek_Objects.Count - 1]._pos.Y + 50.0f), _Snek_Objects[_Snek_Objects.Count - 1]._dir, speed, rotation, snekscale, time));

                    }
                    else if (_Snek_Objects[_Snek_Objects.Count - 1]._dir == new Vector2(-1, 0) || _Snek_Objects[_Snek_Objects.Count - 1]._dir == new Vector2(1, 0))
                    {
                        _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(_Snek_Objects[_Snek_Objects.Count - 1]._pos.X + 50.0f, _Snek_Objects[_Snek_Objects.Count - 1]._pos.Y), _Snek_Objects[_Snek_Objects.Count - 1]._dir, speed, rotation, snekscale, time));

                    }
                }



                KeyMouseReader.Update();

                if (KeyMouseReader.KeyPressed(Keys.F))
                {
                    _Apple_Objects.RemoveAt(0);
                }


                else if (_Apple_Objects.Count > 0 &&  _Snek_Objects[0].Hit(_Apple_Objects[0]._bb))
                {
                    _Apple_Objects.RemoveAt(0);
                }



                if (KeyMouseReader.keyState.IsKeyDown(Keys.Down) && _Snek_Objects[0]._dir.Y != 1)
                {
                    for (int a = 0; a < _TurnX.Length; a++)
                    {
                        if (_Snek_Objects[0]._pos.X == _TurnX[a])
                        {
                            _Snek_Objects[0]._dir = new Vector2(0, 1);
                        }
                    }
                }

                if (KeyMouseReader.keyState.IsKeyDown(Keys.Up) && _Snek_Objects[0]._dir.Y != -1) 
                {
                    for (int b = 0; b < _TurnX.Length; b++)
                    {
                        if (_Snek_Objects[0]._pos.X == _TurnX[b])
                        {
                            _Snek_Objects[0]._dir = new Vector2(0, -1);
                       



                        }
                    }
                }
                if (KeyMouseReader.keyState.IsKeyDown(Keys.Right) && _Snek_Objects[0]._dir.X != -1) 
                {
                    for (int c = 0; c < _TurnY.Length; c++)
                    {
                        if (_Snek_Objects[0]._pos.Y == _TurnY[c])
                        {
                            _Snek_Objects[0]._dir = new Vector2(1, 0);
                        
                        }
                    }
                }
                if (KeyMouseReader.keyState.IsKeyDown(Keys.Left) && _Snek_Objects[0]._dir.X != 1)
                {
                    for (int d = 0; d < _TurnY.Length; d++)
                    {
                        if (_Snek_Objects[0]._pos.Y == _TurnY[d])
                        {
                            _Snek_Objects[0]._dir = new Vector2(-1, 0);
                        
                        }
                    }
                }

        

                for (int i = 1; i < _Snek_Objects.Count; i++)
                {
                    if (_Snek_Objects[i - 1]._dir.Y == 0)
                    {

                        if (_Snek_Objects[i]._dir.Y == 1)
                        {
                            if (_Snek_Objects[i]._pos.Y >= _Snek_Objects[i - 1]._pos.Y)
                            {
                                _Snek_Objects[i]._dir = _Snek_Objects[i - 1]._dir;
                            }

                        }
                        else if (_Snek_Objects[i]._dir.Y == -1)
                        {
                            if (_Snek_Objects[i]._pos.Y <= _Snek_Objects[i - 1]._pos.Y)
                            {
                                _Snek_Objects[i]._dir = _Snek_Objects[i - 1]._dir;
                            }
                        }

                    }
                    else if (_Snek_Objects[i - 1]._dir.X == 0)
                    {

                        if (_Snek_Objects[i]._dir.X == 1)
                        {
                            if (_Snek_Objects[i]._pos.X >= _Snek_Objects[i - 1]._pos.X)
                            {
                                _Snek_Objects[i]._dir = _Snek_Objects[i - 1]._dir;
                            }
                        }
                        else if (_Snek_Objects[i]._dir.X == -1)
                        {
                            if (_Snek_Objects[i]._pos.X <= _Snek_Objects[i - 1]._pos.X)
                            {
                                _Snek_Objects[i]._dir = _Snek_Objects[i - 1]._dir;
                            }
                        }

                    }


                }



                for (int i = 1; i < _Snek_Objects.Count; i++)
                {
                    
                    _Snek_Objects[i]._pos += _Snek_Objects[i - 1]._dir * speed;

                }

            
            


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
            }

            base.Update(gameTime);


        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            if (currentState == GameStates.play)
            {

                foreach (gridbackg o in _backg_Objects)
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

                //if (dieded != true)
                //{
                    _spriteBatch.DrawString(font, "Score:" + applecounter, new Vector2(125, 100), Color.Black);
                //}
            }
            else if (currentState == GameStates.death)
            {
                foreach (Loadingscreens o in _Loadingscreens)
                {
                    o.Draw(_spriteBatch);
                }
                _spriteBatch.DrawString(font, " Your score this round:" + applecounter, new Vector2(450, 500), Color.Red);

            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
