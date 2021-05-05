using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace snek
{
    class grid
    {

        

        static int screen_width = 1920;
        static int screen_height = 1080;

        //Gridgenerator
        float[,] invigrid = new float[screen_width, screen_height];


        //protected void LoadContent()
        //{
        //    darkgreysquareImg = Content.Load<Texture2D>("darkgreysquareimg");
        //    lightgreysquareImg = Content.Load<Texture2D>("lightgreysquareimg");

    }
        //}
        public class gridbackg : Object
    {
        public gridbackg(Texture2D img, Vector2 pos, Vector2 dir, float speed, float rotation, float scale, float time) :
           base(img, pos, dir, speed, rotation, scale, time)
        { 
        }


    }




}
