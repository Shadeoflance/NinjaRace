﻿using VitPro;
using VitPro.Engine;
using System;

class Dead : PlayerState
{
    double DeathTime = 1;
    AnimatedTexture tex;
    public Dead(Player player) : base(player)
    {
        this.player = player;
        Animated = true;
        tex = new AnimatedTexture(new Texture("./Data/img/faces/states/dead/dead.png"));
    }
    public override AnimatedTexture GetTexture()
    {
        return tex;
    }
    public override void Update(double dt)
    {
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
        bool touch = false;
        if (GetTime() > DeathTime)
        {
            player.Respawn();
        }
        foreach (var a in player.collisions.Values)
            foreach (var b in a)
                if (b is Spikes)
                    return;
                else touch = true;
        if (touch)
            player.Velocity = Vec2.Zero;
    }

    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.Gray);
    }

    public override void Jump()
    {
    }

    public override void TileJump() { }

    public override void Die(Vec2 position) { }
}