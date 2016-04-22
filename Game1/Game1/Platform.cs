using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class Platform
    {

        Model model;
        Vector3 position;

        public BoundingSphere Boundingsphere
        {
            get
            {
                var sphere = model.Meshes[0].BoundingSphere;
                sphere.Center += position;
                return sphere;
            }
        }

        public Platform(Vector3 pos)
        {
            position = pos;
        }

        public void Initialize(ContentManager Content)
        {
            model = Content.Load<Model>("Platform/platform");

        }

        public void draw(Camera cam)
        {
            VertexLoader platformMesh = new VertexLoader(model, cam, new Vector3(0, 0, 0));
            platformMesh.draw(position);
        }
    }
}
