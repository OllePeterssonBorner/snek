using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
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

        private int screen_width = 1600;
        private int screen_height = 900;
        public float imgscale = 1.0f;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

           SnekBodImg = Content.Load<Texture2D>("snekbod");
           SnekHeadImg = Content.Load<Texture2D>("snekhead");
           AppleImg = Content.Load<Texture2D>("appleimg");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime); 
        }
    }
}
