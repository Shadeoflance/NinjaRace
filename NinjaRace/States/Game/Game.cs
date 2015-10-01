﻿using VitPro;
using VitPro.Engine;
using System;

class Game : State
{
    World World;

    public Game(string level)
    {
        Program.Statistics = new Statistics();
        World = new World(level);
        Program.Manager.PushState(this);
        Program.Manager.PushState(new PreGame(World));
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        World.RenderSplit();
    }
    public override void Update(double dt)
    {
        dt = Math.Min(dt, 1.0 / 60);
        World.Update(dt);
    }
    public void Finish(Player player)
    {
        new Showdown(World.Level.Name.Trim() + "_S", World.Player1 == player ? true : false);
        TimerContainer.Clear();
        Close();
    }
    public override void KeyDown(Key key)
    {
        World.KeyDown(key);
        if (key == Key.Escape)
        {
            Close();
            TimerContainer.Clear();
        }
    }

    public override void KeyUp(Key key)
    {
        World.KeyUp(key);
    }
}