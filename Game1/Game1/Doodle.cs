using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Game1
{
    class Doodle
    {
        Model doodle;
        public Camera cam;

        bool isColliding;

        public Vector3 position;
        Vector3 predictedPosition;
        public Vector3 camtg;
        bool is_Jumping;
        bool startsupjump;

        float jump = 0.5f;
        float moveSpeed = 0.5f;

        public BoundingSphere Boundingsphere
        {
            get
            {
                var sphere = doodle.Meshes[0].BoundingSphere;
                sphere.Center += predictedPosition;
                return sphere;
            }
        }

        public Doodle(Vector3 pos, Camera cam)
        {
            position = pos;
            predictedPosition = pos;
            camtg = pos;
            is_Jumping = false;
            this.cam = cam;
            startsupjump = false;
            isColliding = false;
        }

        public void Initialize(ContentManager Content)
        {
            doodle = Content.Load<Model>("Doodle/doodle");

        }

        public void Update(GameTime gameTime, List<Platform> plList)
        {
            isColliding = false;
            predictedPosition = position;


            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                predictedPosition.Z += moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                predictedPosition.X += moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                predictedPosition.Z -= moveSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                predictedPosition.X -= moveSpeed;
            }

            if (!is_Jumping)
            {



                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    predictedPosition.Y += jump + 0.3f;
                    is_Jumping = true;
                }
            }




            for (int i = 0; i < plList.Count; ++i)
                if (plList[i].Boundingsphere.Intersects(this.Boundingsphere))
                {
                    isColliding = true;
                }


            if (gameTime.TotalGameTime.Milliseconds % 1000 <= 499)
            {
                if(!isColliding)
                {
                    position.Y = predictedPosition.Y;
                }
                position.Y += jump;
                is_Jumping = true;
                // if (!startsupjump)
                //  {
                //     camtg = predictedPosition;
                //  startsupjump = true;
                //  }
            }

            if (gameTime.TotalGameTime.Milliseconds % 1000 >= 500)
            {

                if (!isColliding)
                {
                    position.Y = predictedPosition.Y;
                }
                position.Y -= jump;
                is_Jumping = false;
                //   startsupjump = false;
            }


            if (!isColliding)
            {


            

                if (Math.Sqrt((double)Math.Pow(predictedPosition.X, 2) + (double) Math.Pow(predictedPosition.Z, 2)) < 100)
                {
                    position.X = predictedPosition.X;
                    position.Z = predictedPosition.Z;
                }
                camtg = position;
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
