using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Platform
    {

        Model model;
        Vector3 position;

        public Platform(Vector3 pos)
        {
            position = pos;
        }
    }
}
