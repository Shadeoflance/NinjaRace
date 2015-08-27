﻿using VitPro;
using VitPro.Engine;
using System;
using System.Timers;

class FreezeBonus : Bonus
{
    public void Get(Player player)
    {
        Effect e = new BonusOnScreen(new Texture("./Data/img/bonuses/freeze.png"), player);
        foreach (var a in Program.World.EffectsScreen)
            if (a.GetType() == typeof(BonusOnScreen) && ((BonusOnScreen)a).player == player)
                a.Dispose();
        Program.World.EffectsScreen.Add(e);
        player.Bonus = () => 
        {
            Player op = player.GetOpponent();
            if (op.States.IsDead)
            {
                e.Dispose();
                return;
            }
            op.States.Set(new Frozen(op));
            Timer t = new Timer(2, () => { if(!op.States.IsDead) op.States.SetFalling(); });
            player.Bonus = () => { };
            e.Dispose();
        };
    }
}

class Frozen : PlayerState
{
    int Dir;
    public Frozen(Player player)
        : base(player)
    {
        player.Velocity = Vec2.Zero;
        Dir = player.Dir;
    }

    AnimatedTexture tex;

    public override AnimatedTexture GetTexture()
    {
        if (tex == null)
            tex = new AnimatedTexture(new Texture("./Data/img/player/idle/player1.png"));
        return tex;
    }

    public override void Update(double dt) { }

    public override void Jump() { }

    public override void Render()
    {
		RenderState.Push();
		RenderState.Color = new Color(0.5, 0.7, 1);
        RenderState.Translate(player.Position - player.Size);
        RenderState.Scale(player.Size * 2);
        if (Dir == -1)
            RenderState.SetOrts(-Vec2.OrtX, Vec2.OrtY, new Vec2(1, 0));
        GetTexture().Render();
		RenderState.Pop();
    }
}