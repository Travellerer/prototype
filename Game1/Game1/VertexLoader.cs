using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game1
{
    class VertexLoader
    {

        Model model;
        Camera cam;
        Vector3 ambientLightColor;

        public VertexLoader(Model model, Camera cam, Vector3 ambientLightColor)
        {
            this.model = model;
            this.cam = cam;
            this.ambientLightColor = ambientLightColor;
        }

        public void draw(Vector3 pos)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    if (ambientLightColor != (new Vector3(0,0,0)))
                    {
                        effect.AmbientLightColor = ambientLightColor;
                    }
                    
                    effect.View = cam.viewMatrix;
                    effect.World = Matrix.CreateTranslation(pos);
                    effect.Projection = cam.projectionMatrix;
                }
                mesh.Draw();
            }
        }

    }
}
