﻿using VitPro;
using VitPro.Engine;
using System;

partial class Player
{
    double Delay = 0.5, Timer = 0;
    bool NeedSwing = false;

    public Player GetOpponent()
    {
        return (Program.World.player1 == this) ? Program.World.player2 : Program.World.player1;
    }

    void DoSwing()
    {
        if (new CollisionBox(Position + new Vec2(Size.X * 1.5, 0) * Dir, new Vec2(Size.X * 3, Size.Y / 2))
            .Collide(GetOpponent().Box) != Side.None)
            GetOpponent().States.SetDead();
    }

    public void UpdateSword(double dt)
    {
        if (Controller.NeedSwing())
            NeedSwing = true;
        if (NeedSwing)
            Timer += dt;
        else return;
        if (Timer >= Delay)
        {
            Timer = 0;
            NeedSwing = false;
            DoSwing();
        }
    }
}