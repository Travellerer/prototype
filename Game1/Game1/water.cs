using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class water
    {

        Model model;
        public Vector3 position;



        public water(Vector3 pos)
        {
            position = pos;
        }

        public void Initialize(ContentManager Content)
        {
            model = Content.Load<Model>("Water/water");

        }

        public void Update(GameTime gametime)
        {
            position.Y += 30f * gametime.ElapsedGameTime.Milliseconds;
        }


        public void draw(Camera cam)
        {
            VertexLoader waterMesh = new VertexLoader(model, cam, new Vector3(0, 0, 0));
            waterMesh.draw(position);
        }
    }
}
