﻿using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class World : IUpdateable
{
    public Group<Effect> EffectsTop = new Group<Effect>();
    public Group<Effect> EffectsMid = new Group<Effect>();
    public Group<Effect> EffectsBot = new Group<Effect>();
    public Group<Effect> EffectsScreen = new Group<Effect>();
    public Player player1, player2;
    public Level level;
    Camera cam1 = new Camera(240), cam2 = new Camera(240), cam = new Camera(360), screenCam = new Camera(120);
    Vec2 camOffset = new Vec2(0, 120);
    public double Time = 0;
    public Texture background;

    private void Init()
    {
        List<StartTile> starts = level.tiles.GetStartTiles();
        Vec2 pos1 = starts[0].Position, pos2 = starts[starts.Count - 1].Position;

        player1 = new Player(pos1.X < pos2.X ? pos1 : pos2,
            Color.White).SetControls(new ControllerPlayer1());
        player2 = new Player(pos1.X > pos2.X ? pos1 : pos2,
            new Color(0.5, 0.7, 0.7)).SetControls(new ControllerPlayer2());
        player2.Dir = -1;
        cam1.Position = player1.Position;
        cam2.Position = player2.Position;
    }

    public World(string level)
    {
        Program.World = this;
        this.level = DBUtils.GetLevel(level);
        Init();
    }

    public bool RenderScreenEffects = true;

    public void Render(Player player = null)
    {
        EffectsBot.Render();
        if (player != null)
            level.RenderArea(player.Position, new Vec2(360, 140));
        else level.Render();
        EffectsMid.Render();
        player2.Render();
        player1.Render();
        EffectsTop.Render();
    }

    public void RenderSingle()
    {
        cam.FOV = Math.Max(360, Math.Abs(player1.Position.X - player2.Position.X) + 15);
        cam.Position = (player2.Position - player1.Position) / 2 + player1.Position;
        cam.Apply();
        Render();
		RenderState.Push();
        screenCam.Apply();
        if(RenderScreenEffects)
            EffectsScreen.Render();
		RenderState.Pop();
    }

    public void RenderSplit()
    {
        RenderState.Push();
        RenderState.BeginArea(new Vec2i(0, RenderState.Height / 2), 
            new Vec2i(RenderState.Width, RenderState.Height / 2));
        Draw.Clear(Color.Black);
        RenderState.Translate(Vec2.OrtY * 0.5);
        Camera camt = new Camera(cam1.FOV);
        camt.Position = cam1.Position + camOffset;
        camt.Apply();
        RenderState.Translate(Vec2.OrtY * cam1.FOV / 2);
        DrawBackground(player1);
        Render(player1);
        RenderState.EndArea();
        RenderState.Pop();

        RenderState.Push();
        RenderState.BeginArea(new Vec2i(0, 0),
            new Vec2i(RenderState.Width, RenderState.Height / 2));
        Draw.Clear(Color.Black);
        RenderState.Translate(Vec2.OrtY * 0.5);
        camt = new Camera(cam1.FOV);
        camt.Position = cam2.Position + camOffset;
        camt.Apply();
        RenderState.Translate(Vec2.OrtY * cam2.FOV / 2);
        DrawBackground(player2);
        Render(player2);
        RenderState.EndArea();
        RenderState.Pop();

		RenderState.Push();
        screenCam.Apply();
        if(RenderScreenEffects)
            EffectsScreen.Render();
		RenderState.Pop();
    }

    private void UpdateForPlayer(double dt, Player player)
    {
        Time += dt;
        player.CalculateCollisions();
        player.Update(dt);
        Camera cam = (player == player1) ? cam1 : cam2;
        cam.Position += (player.Position - cam.Position) * dt * 10;
        player.Position += player.Velocity * dt;
        level.Update(dt);
    }

    public void Update(double dt)
    {
        UpdateForPlayer(dt, player1);
        UpdateForPlayer(dt, player2);
        EffectsTop.Update(dt);
        EffectsTop.Refresh();
        EffectsMid.Update(dt);
        EffectsMid.Refresh();
        EffectsBot.Update(dt);
        EffectsBot.Refresh();
        EffectsScreen.Update(dt);
        EffectsScreen.Refresh();
    }
    public void KeyDown(Key key)
    {
        player1.Controller.KeyDown(key);
        player2.Controller.KeyDown(key);
    }
    public void KeyUp(Key key)
    {
        player1.Controller.KeyUp(key);
        player2.Controller.KeyUp(key);
    }

    private void DrawBackground(Player player)
    {
        if (background == null)
            return;
		RenderState.Push();
        new Camera(360).Apply();
        Vec2 offset = new Vec2(player.Position.X / 3, 0);
        offset = new Vec2(offset.X - App.Width * Math.Floor(offset.X / App.Width), 0);
		RenderState.Translate(-offset);
		RenderState.Scale(App.Width / 2, App.Height / 3);
		RenderState.Scale(2);
		RenderState.Origin(0.5, 0.5);
        background.Render();
		RenderState.Translate(new Vec2(1, 0));
        background.Render();
		RenderState.Pop();
    }
}