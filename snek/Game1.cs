﻿using System;
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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public List<Object> _Objects;
        public List<Object> _Snek_Objects;
        public List<Object> _Misc_Objects;
        public List<gridbackg> _backg_Objects;


        public static int screen_width = 1920;
        public static int screen_height = 1080;

        public static int arbitraryNumX = 65;
        public static int arbitraryNumY = 185;

        private float odnumvert = 1080 / 50;
        private float odnumhoriz = 1920 / 50;

        public int arbitraryEquationX = screen_width / 2 + arbitraryNumY;
        public int arbitraryEquationY = screen_height / 2 + arbitraryNumX;

        public bool rightarrow = false;
        public bool leftarrow = false;
        public bool uparrow = false;
        public bool downarrow = false;

        public float speed = 5.0f;
        public float rotation = 0.0f;
        public float imgscale = 1.0f;
        public float snekscale = 1.0f;
        public float time;

        public Texture2D SnekBodImg;
        public Texture2D SnekHeadImg;
        public Texture2D AppleImg;
        public Texture2D darkgreysquareImg;
        public Texture2D lightgreysquareImg;

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

        _graphics.PreferredBackBufferWidth = screen_width;
            _graphics.PreferredBackBufferHeight = screen_height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Random r = new Random();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

           SnekBodImg = Content.Load<Texture2D>("snekbodyman");
           SnekHeadImg = Content.Load<Texture2D>("snekheadyman");
           AppleImg = Content.Load<Texture2D>("appleimg");
           darkgreysquareImg = Content.Load<Texture2D>("darkgreysquareimg");
           lightgreysquareImg = Content.Load<Texture2D>("lightgreysquareimg");



            _Snek_Objects.Add(new Snek(SnekHeadImg, new Vector2(screen_width / 2, (screen_height / 2) - 15), new Vector2(-1, 0), speed, rotation, snekscale, time));

            for (int i = 0; i < 4; i++)
            {
                _Snek_Objects.Add(new Snek(SnekBodImg, new Vector2(arbitraryEquationX + (arbitraryNumX * i), arbitraryEquationY), new Vector2(-1, 0), speed, rotation, snekscale, time));
            }

            for (int i = 0; i < odnumvert + 1; i++)
            {
                for (int t = 0; t < odnumhoriz + 1; t++)
                {
                    if (i % 2 != 0)
                    {
                        if ((t % 2) != 0)
                        {
                            _backg_Objects.Add(new gridbackg(lightgreysquareImg, new Vector2(50 * t, 50 * i + 25), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                        }
                        else
                        {
                            _backg_Objects.Add(new gridbackg(darkgreysquareImg, new Vector2(50 * t, 50 * i + 25), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                        }
                    }
                    else
                    {
                        if ((t % 2) == 0)
                        {
                            _backg_Objects.Add(new gridbackg(lightgreysquareImg, new Vector2(50 * t, 50 * i + 25), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                        }
                        else
                        {
                            _backg_Objects.Add(new gridbackg(darkgreysquareImg, new Vector2(50 * t, 50 * i + 25), new Vector2(1, 1), 0, 0.0f, 1.0f, 0.0f));
                        }
                    }

                    
                }
            }



        }

        //public bool DoesInputKey(Keys keyData)
        //{
        //    KeyMouseReader.Update();

        //    switch (keyData)
              
        //    {
        //        case Keys.Right:
        //            {
        //                rightarrow = true;
        //                return rightarrow;
        //            }
        //        case Keys.Left:
        //            {
        //                leftarrow = true;
        //                return leftarrow;
        //            }
        //        case Keys.Up:
        //            {
        //                uparrow = true;
        //                return uparrow;
        //            }
        //        case Keys.Down:
        //            {
        //                downarrow = true;
        //                return downarrow;
        //            }
                  
        //    }
        //    KeyMouseReader.keyState.IsKeyDown(Keys.Down
        //}

        protected override void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();
            
                
                        if (KeyMouseReader.keyState.IsKeyDown(Keys.Down))
                        {
                                for (int a = 0; a < _backg_Objects.Count; a++)
                                {
                                    if (_Snek_Objects[0]._pos.X > (_backg_Objects[a]._pos.X) || _Snek_Objects[0]._pos.X < (_backg_Objects[a]._pos.X))
                                    {
                                        _Snek_Objects[0]._dir = new Vector2(0, 1);
                                    }

                                }
                        }
                        if (KeyMouseReader.keyState.IsKeyDown(Keys.Up))
                         {
                              for (int b = 0; b < _backg_Objects.Count; b++)
                              {
                                    if (_Snek_Objects[0]._pos.X > (_backg_Objects[b]._pos.X) || _Snek_Objects[0]._pos.X < (_backg_Objects[b]._pos.X))
                                    {
                                        _Snek_Objects[0]._dir = new Vector2(0, -1);
                                    }

                                     

                              }
                        }
                        if (KeyMouseReader.keyState.IsKeyDown(Keys.Right))
                        {
                                for (int c = 0; c < _backg_Objects.Count; c++)
                                {
                                    if (_Snek_Objects[0]._pos.Y > (_backg_Objects[c]._pos.Y) || _Snek_Objects[0]._pos.Y < (_backg_Objects[c]._pos.Y))
                                    {
                                        _Snek_Objects[0]._dir = new Vector2(1, 0);
                                    }
                                }
                                        
                        }
                        if (KeyMouseReader.keyState.IsKeyDown(Keys.Left))
                        {
                               for (int d = 0; d < _backg_Objects.Count; d++)
                               {
                                      if (_Snek_Objects[0]._pos.Y > (_backg_Objects[d]._pos.Y) || _Snek_Objects[0]._pos.Y < (_backg_Objects[d]._pos.Y))
                                      {
                                         _Snek_Objects[0]._dir = new Vector2(-1, 0);
                                      }
                               }
                        }





                        if (KeyMouseReader.keyState.IsKeyDown(Keys.Space))
                        {
                            _Snek_Objects[0]._speed = 0.0f;


                        }
                        if (KeyMouseReader.keyState.IsKeyUp(Keys.Space))
                        {
                            _Snek_Objects[0]._speed = 1.0f;
                        }

                        for (int i = 1; i < _Snek_Objects.Count; i++)
                        {
                            _Snek_Objects[i]._pos = new Vector2(_Snek_Objects[(i - 1)]._pos.X + 65.0f, _Snek_Objects[(i - 1)]._pos.Y);
                        }



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

                        base.Update(gameTime);


                    }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (gridbackg o in _backg_Objects)
            {
                o.Draw(_spriteBatch);
            }

            foreach (Snek o in _Snek_Objects)
            {
                o.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime); 
        }
    }
}
