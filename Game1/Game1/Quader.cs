using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Quader
    {

        VertexPositionColor[] QuaderVertices;
        VertexBuffer vertexBuffer;

        public Quader(Vector3[] a, Color col, GraphicsDevice GraphicsDevice)
        {
            QuaderVertices = new VertexPositionColor[8];
            //ToDo: Abfangen bei Arrays <8 Plätze
            QuaderVertices[0] = new VertexPositionColor(a[0], col);
            QuaderVertices[1] = new VertexPositionColor(a[1], col);
            QuaderVertices[2] = new VertexPositionColor(a[2], col);
            QuaderVertices[2] = new VertexPositionColor(a[3], col);
            QuaderVertices[2] = new VertexPositionColor(a[4], col);
            QuaderVertices[2] = new VertexPositionColor(a[5], col);
            QuaderVertices[2] = new VertexPositionColor(a[6], col);
            QuaderVertices[2] = new VertexPositionColor(a[7], col);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(
   VertexPositionColor), 8, BufferUsage.
   WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(QuaderVertices);
        }
        public Quader(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, Vector3 f, Vector3 g, Vector3 h, Color col, GraphicsDevice GraphicsDevice)
        {
            QuaderVertices = new VertexPositionColor[8];

            QuaderVertices[0] = new VertexPositionColor(a, col);
            QuaderVertices[1] = new VertexPositionColor(b, col);
            QuaderVertices[2] = new VertexPositionColor(c, col);
            QuaderVertices[2] = new VertexPositionColor(d, col);
            QuaderVertices[2] = new VertexPositionColor(e, col);
            QuaderVertices[2] = new VertexPositionColor(f, col);
            QuaderVertices[2] = new VertexPositionColor(g, col);
            QuaderVertices[2] = new VertexPositionColor(h, col);

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(
               VertexPositionColor), 8, BufferUsage.
               WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(QuaderVertices);
        }
    }
}
