﻿using VitPro;
using VitPro.Engine;
using System;

class Game : State
{
    World World;

    public Game()
    {
        World = new World();
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        World.RenderSplit();
    }
    public override void Update(double dt)
    {
        dt = Math.Min(dt, 1.0 / 60);
        if (World.player1.States.current is Win || World.player2.States.current is Win)
        {
            Program.Manager.PushState(new Showdown());
            Close();
        }
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