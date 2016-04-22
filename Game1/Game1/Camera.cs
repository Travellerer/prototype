using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Camera
    {
        Vector3 camTarget;
        Vector3 camPosition;
        Vector3 camplayerdirection;

        public Matrix projectionMatrix { get; private set; }
        public Matrix viewMatrix { get; private set; }
        public Matrix worldMatrix { get; private set; }

        //Orbit
        bool orbit = false;

        public Camera(GraphicsDevice GraphicsDevice)
        {
            camTarget = new Vector3(0f, 0f, 0f);
            camPosition = new Vector3(0f, 0f, -50);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f),GraphicsDevice.DisplayMode.AspectRatio,1f, 1000f);
            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget,new Vector3(0f, 1f, 0f));// Y up
            worldMatrix = Matrix.CreateWorld(camTarget, Vector3.Forward, Vector3.Up);
        }

        public void Update(Vector3 pos)
        {
            //camTarget.X = pos.X;
            //camTarget.Z = pos.Z;

            //camPosition.X = pos.X;
            camPosition.Y = pos.Y +10;
            //camTarget.Z = pos.Z -50;

            camTarget.X = pos.X;
            camTarget.Y = pos.Y;
            camTarget.Z = pos.Z;


            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(-1f));
                camPosition = Vector3.Transform(camPosition, rotationMatrix);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
                camPosition = Vector3.Transform(camPosition, rotationMatrix);
            }
            

            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus) && Math.Sqrt((double)Math.Pow(camPosition.X, 2) + (double)Math.Pow(camPosition.Z + 1f, 2)) < 100)
            {
                camPosition.Z += 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus) && Math.Sqrt((double)Math.Pow(camPosition.X, 2) + (double)Math.Pow(camPosition.Z-1f, 2)) < 100)
            {
                camPosition.Z -= 1f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                orbit = !orbit;
            }

            if (orbit)
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
                camPosition = Vector3.Transform(camPosition, rotationMatrix);
            }

            camplayerdirection.X = pos.X - camPosition.X;
            camplayerdirection.Z = pos.Z - camPosition.Z;

            if (camplayerdirection.Length() > 20)
            {
                camplayerdirection.Normalize();
                camPosition.X = pos.X - (camplayerdirection.X * 20);
                camPosition.Z = pos.Z - (camplayerdirection.Z * 20);
            }

            viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
        }
    }
}
