﻿using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class LiftTile : Tile
{
    public LiftTile()
    {
        Moving = true;
        Colorable = true;
    }
    AnimatedTexture idle, moving;

    protected override void LoadTexture()
    {
        Shader s = new Shader(NinjaRace.Shaders.Glow);
        Texture t = new Texture("./Data/img/tiles/lift.png");
        idle = new AnimatedTexture(t.Copy());
        RenderState.Push();
        RenderState.Set("texture", t);
        RenderState.Set("doX", 1);
        RenderState.Set("size", new Vec2(150, 150));
        t.ApplyShader(s);
        RenderState.Set("doX", 0);
        t.ApplyShader(s);
        RenderState.Pop();
        moving = new AnimatedTexture(t);
        tex = idle;
    }

    double speed = 100;
    bool lifting = false;
    Player player;
    List<Tile> near;

    public override void Effect(Player player, Side side)
    {
        if (side == Side.Down)
        {
            lifting = true;
            tex = moving;
            this.player = player;
            if (near == null)
            {
                near = new List<Tile>();
                Vec2i coords = Tiles.GetCoords(ID);
                int x = coords.X + 1;
                Tiles tiles = Program.World.level.Tiles;
                while (tiles.GetTile(x, coords.Y) != null &&
                    tiles.GetTile(x, coords.Y).GetType() == typeof(LiftTile))
                {
                    near.Add(tiles.GetTile(x, coords.Y));
                    x++;
                }
                x = coords.X - 1;
                while (tiles.GetTile(x, coords.Y) != null &&
                    tiles.GetTile(x, coords.Y).GetType() == typeof(LiftTile))
                {
                    near.Add(tiles.GetTile(x, coords.Y));
                    x--;
                }
            }
        }
    }

    public override void Update(double dt)
    {
        if (!lifting)
            return;
        if ((player.States.IsFlying || player.States.IsDead) && lifting)
        {
            lifting = false;
            tex = idle;
            return;
        }
        if (player.collisions[Side.Up].Count > 0)
            player.States.current.Die(Position);
        Vec2 v = new Vec2(0, dt * speed);
        Position += v;
        foreach (var a in near)
        {
            if (player.collisions[Side.Down].Contains(a))
                continue;
            a.Position = new Vec2(a.Position.X, Position.Y);
        }
        player.Position = new Vec2(player.Position.X, Position.Y + Size.Y + player.Size.Y);
        lifting = player.collisions[Side.Down].Contains(this);
    }
}