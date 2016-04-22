using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace Game1
{
    class Doodle
    {
        Model doodle;
        public Camera cam;

        public Vector3 position;
        public Vector3 camtg;
        bool is_Jumping;
        bool startsupjump;
        TimeSpan start;
        TimeSpan lastjump;

        float jump = 0.5f;
        float moveSpeed = 0.5f;

        public BoundingSphere Boundingsphere
        {
            get
            {
                var sphere = doodle.Meshes[0].BoundingSphere;
                sphere.Center += position;
                return sphere;
            }
        }

        public Doodle(Vector3 pos, Camera cam)
        {
            position = pos;
            camtg = pos;
            is_Jumping = false;
            this.cam = cam;
            startsupjump = false;

        }

        public void Initialize(ContentManager Content)
        {
            doodle = Content.Load<Model>("Doodle/doodle");

        }

        public void Update(GameTime gameTime, bool collision)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Z += moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X += moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Z -= moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X -= moveSpeed;
            }



            if (!is_Jumping)
            {

              

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    position.Y += jump + 0.3f;
                    is_Jumping = true;
                }
            }

            if (gameTime.TotalGameTime.Milliseconds % 1000 <= 499)
            {
                position.Y += jump;
                is_Jumping = true;
                if(!startsupjump)
                {
                    camtg = position;
                    startsupjump = true;
                }
            }

            if (gameTime.TotalGameTime.Milliseconds % 1000 >= 500)
            {
                position.Y -= jump;
                is_Jumping = false;
                startsupjump = false;
            }


            //if (is_Jumping)
            //{
            //    position.Y += jump;
            //    if (gameTime.TotalGameTime.Subtract(start).TotalMilliseconds > 800)
            //    {
            //        is_Jumping = false;
            //        lastjump = gameTime.TotalGameTime;
            //    }

            //}

            //if (!collision && !is_Jumping)
            //{
            //    position.Y -= 0.3f;
            //}

        }

        public void draw()
        {


            VertexLoader doodleMesh = new VertexLoader(doodle, cam, new Vector3(0, 0, 0));
            doodleMesh.draw(position);
        }

    }
}
