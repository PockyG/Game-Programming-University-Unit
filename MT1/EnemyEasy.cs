﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT1
{
    class EnemyEasy : Sprite3
    {

        public EnemyEasy(bool visibleZ, Texture2D texZ, float x, float y) : base( visibleZ,  texZ,  x,  y)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            setPosX(getPosX() - 3);
        }

    }
}
