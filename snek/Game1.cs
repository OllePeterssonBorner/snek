using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace snek
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public List<Object> _Objects;
        public List<Snek> _Snek_Objects;
        public List<Object> _Misc_Objects;

        private int screen_width = 1920;
        private int screen_height = 1080;
       

        public float speed = 0.0f;
        public float rotation = 0.0f;
        public float imgscale = 1.0f;
        public float snekscale = 1.0f;
        public float time;

        public Texture2D SnekBodImg;
        public Texture2D SnekHeadImg;
        public Texture2D AppleImg;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _Objects = new List<Object>();
            _Snek_Objects = new List<Snek>();
            _Misc_Objects = new List<Object>();

            _graphics.PreferredBackBufferWidth = screen_width;
            _graphics.PreferredBackBufferHeight = screen_height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Random r = new Random();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

           SnekBodImg = Content.Load<Texture2D>("snekbod");
           SnekHeadImg = Content.Load<Texture2D>("snekhead");
           AppleImg = Content.Load<Texture2D>("appleimg");

           
            
           _Snek_Objects.Add(new Snek(SnekHeadImg, new Vector2(screen_width / 2, screen_height / 2), new Vector2(1, 1), speed, rotation, snekscale, time));
            
           

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Snek o in _Snek_Objects)
            {
                o.Update(screen_width, screen_height);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

          
            foreach (Snek o in _Snek_Objects)
            {
                o.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime); 
        }
    }
}
