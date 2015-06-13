﻿using VitPro;
using VitPro.Engine;
using System;

class Showdown : State
{
    World World;

    public Showdown()
    {
        World = new World("showdown");
        World.player1.EnableSword();
        World.player2.EnableSword();
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        World.RenderSingle();
    }
    public override void Update(double dt)
    {
        dt = Math.Min(dt, 1.0 / 60);
        World.Update(dt);
        TimerContainer.Update(dt);
    }
    public override void KeyDown(Key key)
    {
        World.KeyDown(key);
        if (key == Key.Escape)
            Close();
    }

    public override void KeyUp(Key key)
    {
        World.KeyUp(key);
    }

}