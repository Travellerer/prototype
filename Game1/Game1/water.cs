﻿using Microsoft.Xna.Framework;
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
        Vector3 position;

        public water(Vector3 pos)
        {
            position = pos;
            // model = Content.Load<Model>("Water/water");
        }
    }
}
