﻿using System;
using VitPro;
using VitPro.Engine;

class Level : IRenderable, IUpdateable
{
    public Tiles tiles;
    public string name;

    public Level(Tiles tiles, string name)
    {
        this.tiles = tiles;
        this.name = name;
    }

    public void Render() 
    {
        tiles.Render();
    }
    public void RenderArea (Vec2 pos, Vec2 size)
    {
        tiles.RenderArea(pos, size);
    }
    public void Update(double dt) 
    {
        tiles.Update(dt);
    }
}