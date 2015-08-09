﻿using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class Ground : Tile
{
    public Ground()
    {
        Colorable = true;
    }
    protected override void LoadTexture()
    {
        tex = new AnimatedTexture(new Texture("./Data/img/tiles/ground.png"));
    }
}