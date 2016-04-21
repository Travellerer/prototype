using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Doodle
    {
        Model doodle;
        Camera cam;

        public Vector3 position;
        bool is_Jumping;
        TimeSpan start;
        TimeSpan lastjump;

        float jump = 0.5f;
        float moveSpeed = 10f;

        public Doodle(Vector3 pos, Camera cam)
        {
            position = pos;
            is_Jumping = false;
            this.cam = cam;

        }

        public void Initialize(ContentManager Content)
        {
            doodle = Content.Load<Model>("Doodle/doodle");

        }

        public void Update(GameTime gameTime, bool collision)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.X += moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.Z += moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.X -= moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.Z -= moveSpeed;
            }

            if (gameTime.TotalGameTime.Milliseconds % 1000 <= 499)
            {
                position.Y += jump;
                is_Jumping = true;
            }

            if (gameTime.TotalGameTime.Milliseconds % 1000 >= 500)
            {
                position.Y -= jump;
                is_Jumping = false;
            }


            if(is_Jumping == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    position.Y += jump + 0.3f;
                    is_Jumping = true;
                }
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
            //    position.Y -= ySpeed;
            //}

        }

        public void draw()
        {
            VertexLoader doodleMesh = new VertexLoader(doodle, cam, new Vector3(0, 0, 0));
            doodleMesh.draw();
        }

    }
}
