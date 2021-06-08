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
        public Texture2D _img;
        public Vector2 _pos;
        public Vector2 _dir;
        public float _speed;
        protected Color _color;
        protected double _time;
        protected Vector2 _origin = Vector2.Zero;
        protected float _scale;
        public float _rotation;
        public Rectangle _bb;
        public bool hit;


        public Object(Texture2D img, Vector2 pos, Vector2 dir, float speed, float rotation, float scale, double time)
        {

            Random r = new Random();

            this._img = img;
            this._pos = pos;
            this._dir = dir;
            this._speed = speed;
            this._time = time;
            this._origin = Vector2.Zero;
            this._scale = scale;
            this._bb = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
            this._rotation = rotation;
            this._color = Color.White;
        }
        

        public virtual void Update(int screen_width, int screen_height)
        {
            _pos += _dir * _speed;
            _bb = new Rectangle(_pos.ToPoint(), new Point(50, 50));
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(_img, _pos, null, _color, _rotation, _origin, _scale, SpriteEffects.None, 0.0f);
        }

        public bool Hit(Rectangle otherbb)
        {


            if (_bb.Intersects(otherbb))
            {

                return true;


            }
            return false;






        }
    }




}

