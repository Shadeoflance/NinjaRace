﻿using VitPro;
using VitPro.Engine;
using System;
using System.Diagnostics;

class MyManager : State.Manager
{
    public MyManager(params State[] states)
        : base(states)
    {
        sw.Start();
    }
    double t = 1;

    public override void Update(double dt)
    {
        if (sw.Elapsed.Milliseconds > 500)
        {
            fps = 1 / dt;
            sw.Restart();
        }
        if (t == 0)
            base.Update(dt);
        t -= 3 * dt;
        if (t < 0)
            t = 0;
    }

    public override void StateChanged()
    {
        base.StateChanged();
        Previous = Current;
        Current = CurrentState;
        t = 2;
        if (tex != null)
        {
            back = tex.Copy();
        }
    }

    Texture tex = null, back = null;

    State Previous, Current;

    void DefaultRender()
    {
		RenderState.Push();
		RenderState.Push();
		RenderState.Scale(2);
		RenderState.Origin(0.5, 0.5);
        var a = Math.Max(0, 1 - t);
        RenderState.Color = new Color(a, a, a, a);
        tex.Render();
		RenderState.Pop();
		RenderState.Scale(2);
		RenderState.Origin(0.5, 0.5);
        if (back != null)
        {
            a = Math.Max(0, t - 1);
			RenderState.Color = new Color(a, a, a, a);
            back.Render();
        }
		RenderState.Pop();
    }

    void NoAnimationRender()
    {
        RenderState.Push();
        RenderState.Scale(2);
        RenderState.Origin(0.5, 0.5);
        tex.Render();
        RenderState.Pop();
    }

    double fps = 0;
    Stopwatch sw = new Stopwatch();

    public override void Render()
    {
		if (tex == null || tex.Width != RenderState.Width || tex.Height != RenderState.Height)
			tex = new Texture(RenderState.Width, RenderState.Height);
		RenderState.BeginTexture(tex);
        base.Render();
		RenderState.EndTexture();
        tex.RemoveAlpha();
        if (!((Previous != null && Previous.GetType() == typeof(KeyPress)) || (Current != null && Current.GetType() == typeof(KeyPress))))
            DefaultRender();
        else NoAnimationRender();
        RenderState.Push();
        Texture t = Program.font.MakeTexture(Math.Truncate(fps).ToString());
        RenderState.Color = (Color.Yellow);
        Draw.Texture(t, new Vec2(0.85, 0.85) - new Vec2(0.1 * t.Width / t.Height / 2, 0.1), new Vec2(0.85, 0.85) + new Vec2(0.1 * t.Width / t.Height / 2, 0.1));
        t.Dispose();
        RenderState.Pop();
    }
}