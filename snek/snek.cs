using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
    
namespace snek
{
    public class Snek : Object
    {
        Random r = new Random();
        public Snek(Texture2D img, Vector2 pos, Vector2 dir, float speed, float rotation, float scale, double time) :
            base(img, pos, dir, speed, rotation, scale, time)
        {

        }

        public override void Update(int screen_width, int screen_height)
        {
            base.Update(screen_width, screen_height);
            _rotation += 0.03f;
        }

        public override void Draw(SpriteBatch sb)
        {
            //sb.Draw(_img, _pos, _color);
            sb.Draw(_img, _pos, null, _color, _rotation, _origin, _scale, SpriteEffects.None, 0.0f);
        }
    }
}
