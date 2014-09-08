﻿using VitPro;
using VitPro.Engine;
using System;

class RocketJumpState : Flying
{
    public RocketJumpState(Player player) : base(player) { }

    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.Orange);
    }
    public override void Update(double dt)
    {
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
    }
}