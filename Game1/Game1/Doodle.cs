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
    class Doodle
    {
        Model model;
        public Vector3 position;
        bool is_Jumping;
        TimeSpan start;
        TimeSpan lastjump;

        public Doodle(Vector3 pos)
        {
            position = pos;
            is_Jumping = false;

           // model = Content.Load<Model>("Doodle/doodle");
        }

        public void Update(GameTime gameTime, bool collision)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.X += 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.Z += 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.X -= 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.Z -= 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !is_Jumping && (gameTime.TotalGameTime.Subtract(lastjump).Milliseconds > 300 || lastjump == null))
            {
                start = gameTime.TotalGameTime;
                position.Y += 1f;
                is_Jumping = true;
            }

            if (is_Jumping)
            {
                position.Y += 1f;

                if (gameTime.TotalGameTime.Subtract(start).TotalMilliseconds > 800)
                {
                    is_Jumping = false;
                    lastjump = gameTime.TotalGameTime;
                }

            }

            if (!collision && !is_Jumping)
            {
                position.Y -= 1f;
            }
        }

    }
}
