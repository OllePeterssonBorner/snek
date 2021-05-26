using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace snek
{
    public class Loadingscreens
    {
        protected Texture2D _img;
        public Vector2 _pos;
        public Vector2 _dir;
        public float _speed;
        protected Color _color;
        protected double _time;
        protected Vector2 _origin;
        protected float _scale;
        protected float _rotation;
        public Rectangle _bb;
        public bool hit;


        public Loadingscreens(Texture2D img, Vector2 pos, Vector2 dir, float speed, float scale)
        {

            Random r = new Random();

            this._img = img;
            this._pos = pos;
            this._dir = dir;
            this._speed = speed;
            this._origin = Vector2.Zero;
            this._scale = scale; this._bb = new Rectangle((int)pos.X, (int)pos.Y, img.Width, img.Height);
            this._color = Color.White;
        }


        public virtual void Update(int screen_width, int screen_height)
        {

        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(_img, _pos, null, _color, _rotation, _origin, _scale, SpriteEffects.None, 0.0f);
        }

        
    }




}

