﻿using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;
using System.Collections.Generic;

class Showdown : UI.State
{
    World World;
    bool finished = false;
    public Showdown(string level, bool first)
    {
        World = new World(level);

        if (first)
            World.player2.Lives = 2;
        else World.player1.Lives = 2;
        World.EffectsScreen.Add(new Hearts(World.player1));
        World.EffectsScreen.Add(new Hearts(World.player2));

        World.player1.Respawn = World.player1.ChangeSpawn + World.player1.Respawn;
        World.player2.Respawn = World.player2.ChangeSpawn + World.player2.Respawn;

        Program.Manager.PushState(this);
        Program.Manager.PushState(new PreShowdown(World));
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        RenderState.Push();
        World.RenderSingle();
        RenderState.Pop();
        base.Render();
    }
    double T = 0, FinishTimeout = 3;
    public override void Update(double dt)
    {
        if (finished)
        {
            T += dt;
            if (T > FinishTimeout)
                Finish();
            dt /= 7;
        }
        dt = Math.Min(dt, 1.0 / 60);
        base.Update(dt);
        World.Update(dt);
        if ((World.player1.Lives < 1 || World.player2.Lives < 1) && !finished)
        {
            finished = true;
            World.RenderScreenEffects = false;
        }
    }
    private void Finish()
    {
        string s = "PLAYER" + (World.player1.Lives < 1 ? "2" : "1");

        Texture tex = new Texture(RenderState.Width, RenderState.Height);
        RenderState.BeginTexture(tex);
        Render();
        RenderState.EndTexture();
        Program.Manager.NextState = new GameOver(s, tex);
        TimerContainer.Clear();
    }
    public override void KeyDown(Key key)
    {
        World.KeyDown(key);
        if (key == Key.Escape)
        {
            TimerContainer.Clear();
            Close();
        }
    }

    public override void KeyUp(Key key)
    {
        World.KeyUp(key);
    }

}