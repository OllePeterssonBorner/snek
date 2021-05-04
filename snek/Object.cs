using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace snek
{
    public class Object
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
        protected Rectangle _bb;
        public bool hit;


        public Object(Texture2D img, Vector2 pos, Vector2 dir, float speed, float rotation, float scale, double time)
        {

            Random r = new Random();

            this._img = img;
            this._pos = pos;
            this._dir = dir;
            this._speed = speed;
            this._time = time;
            this._origin = new Vector2(_img.Width / 2, _img.Height / 2);
            this._scale = scale;
            this._bb = new Rectangle((int)pos.X, (int)pos.Y, img.Width, img.Height);
            this._rotation = rotation;
            this._color = Color.White;
        }
        

        public virtual void Update(int screen_width, int screen_height)
        {
            //_pos += _dir * _speed;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(_img, _pos, null, _color, _rotation, _origin, _scale, SpriteEffects.None, 0.0f);
        }
    }




}

