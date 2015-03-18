﻿using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

[Serializable]
class Tiles : IRenderable, IUpdateable
{
    private Tile[,] tiles;
    private Group<Tile> movingTiles;
    public static Vec2i MaxSize = new Vec2i(500, 150);

    public String level { get; set; }

    public Tiles(int sizex, int sizey)
    {
        if (sizex > MaxSize.X || sizey > MaxSize.Y)
            throw new Exception("Size of level is too big");
        tiles = new Tile[sizex, sizey];
        movingTiles = new Group<Tile>();
    }

    public StartTile GetStartTile()
    {
        foreach (var a in tiles)
            if (a is StartTile)
                return (StartTile)a;
        return null;
    }

    public void AddTile(Vec2i v, Tile t) { AddTile(v.X, v.Y, t); }

    public void AddTile(int x, int y, Tile tile)
    {
        if (x <= 0 || y <= 0)
            return;
        if (y >= tiles.GetLength(0) || x >= tiles.GetLength(1))
            return;
        if (tile == null)
        {
            tiles[y, x] = null;
            return;
        }
        if (tile is StartTile && GetStartTile() != null)
            return;
        int link = tile.Link;
        tile = (Tile)tile.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
        tile.Position = new Vec2(Tile.Size.X * x * 2, Tile.Size.Y * y * 2);
        tile.ID = GetID(x, y);
        tile.Link = link;
        tiles[y, x] = tile;
        if (tile.IsMoving)
        {
            movingTiles.Add(tile);
            movingTiles.Refresh();
        }
    }

    public Tile GetTile(int x, int y)
    {
        if (y >= tiles.GetLength(0) || x >= tiles.GetLength(1) || x < 0 || y < 0)
            return null;
        return tiles[y, x];
    }

    public int GetLength(int d)
    {
        return tiles.GetLength(d);
    }

    public void Render()
    {
        foreach (var a in tiles)
            if (a != null && !(a is Saw))
                a.Render();
        foreach (var a in tiles)
            if (a is Saw)
                a.Render();
    }

    public void Update(double dt)
    {
        foreach (var a in tiles)
            if (a != null)
                a.Update(dt);
    }

    public Group<Tile> GetCustomTiles()
    {
        return movingTiles;
    }

    public static int GetID(int x, int y)
    {
        return (y - 1) * MaxSize.X + x;
    }

    public static Vec2i GetCoords(int ID)
    {
        return new Vec2i(ID % MaxSize.X, ID / MaxSize.X + 1);
    }

    public Tile GetByID(int ID)
    {
        return GetTile(GetCoords(ID).X, GetCoords(ID).Y);
    }

    public static Vec2 GetPosition(Vec2i Coords)
    {
        return new Vec2(Coords.X * Tile.Size.X * 2, Coords.Y * Tile.Size.Y * 2);
    }
}