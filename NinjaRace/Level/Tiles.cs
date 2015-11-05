﻿using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

[Serializable]
class Tiles : IRenderable, IUpdateable
{
    private Tile[,] tiles;
    private PosGroup<Tile> PosTiles;
    private Group<Tile> movingTiles;
    public static Vec2i MaxSize = new Vec2i(500, 150);
    private Color Color { get; set; }
    public bool Dirty = true;

    public Tiles(int sizex, int sizey)
    {
        sizex += 2;
        sizey += 2;
        if (sizex > MaxSize.X || sizey > MaxSize.Y)
            throw new Exception("Size of level is too big");
        tiles = new Tile[sizey, sizex];
        PosTiles = new PosGroup<Tile>(0, 0, sizex * Tile.Size.X * 2, sizey * Tile.Size.Y * 2, Tile.Size.X * 2, Tile.Size.Y * 2);

        movingTiles = new Group<Tile>();
        Color = Color.White;
        BorderTile t = new BorderTile();
        for (int i = 1; i < sizex - 1; i++)
        {
            AddTileInternal(i, 0, t);
            AddTileInternal(i, sizey - 1, t);
        }
        for (int i = 0; i < sizey; i++)
        {
            AddTileInternal(0, i, t);
            AddTileInternal(sizex - 1, i, t);
        }
    }

    public List<StartTile> GetStartTiles()
    {
        List<StartTile> l = new List<StartTile>();
        foreach (var a in tiles)
            if (a != null && a.GetType() == typeof(StartTile))
            {
                StartTile t = (StartTile)a;
                l.Add(t);
            }
            return l;
    }

    void AddTileInternal(int x, int y, Tile tile)
    {
        Dirty = true;
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
        else
        {
            PosTiles.Add(tile, tile.Position);
        }
    }

    public void AddTile(Vec2i v, Tile t) { AddTile(v.X, v.Y, t); }

    public void AddTile(int x, int y, Tile tile)
    {
        Dirty = true;
        if (x <= 0 || y <= 0)
            return;
        if (y >= tiles.GetLength(0) - 1 || x >= tiles.GetLength(1) - 1)
            return;
        if (tile == null)
        {
            tiles[y, x] = null;
            return;
        }
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
        else
        {
            PosTiles.Add(tile, tile.Position);
        }
    }

    public Tile GetTile(int x, int y)
    {
        if (y >= tiles.GetLength(0) || x >= tiles.GetLength(1) || x < 0 || y < 0)
        {
            return null;
        }
        return tiles[y, x];
    }

    public Tile GetTile(Vec2i v)
    {
        return GetTile(v.X, v.Y);
    }

    public int GetLength(int d)
    {
        return tiles.GetLength(d) - 1;
    }

    public Vec2i GetSize()
    {
        return new Vec2i((GetLength(1)) * (int)Tile.Size.X * 2, (GetLength(0)) * (int)Tile.Size.Y * 2);
    }

    public void RenderBackground()
    {
        Draw.Rect(Tile.Size, new Vec2(GetLength(1) * Tile.Size.X * 2, GetLength(0) * Tile.Size.Y * 2) - Tile.Size, new Color(0.1, 0.1, 0.1));
    }

    public void Render()
    {
        foreach (var a in tiles)
            if (a != null && !(a.IsMoving))
            {
                if (a.Colorable)
                {
                    RenderState.Push();
                    RenderState.Color = Color;
                    a.Render();
                    RenderState.Pop();
                }
                else
                    a.Render();
            }
        foreach (var a in movingTiles)
            if (a.Colorable)
            {
                RenderState.Push();
                RenderState.Color = Color;
                a.Render();
                RenderState.Pop();
            }
            else
                a.Render();
    }

    public void RenderArea(Vec2 pos, Vec2 size)
    {
        IEnumerable<Tile> t = PosTiles.Query(pos - size, pos + size);
        foreach (var a in t)
            if (a.Colorable)
            {
                RenderState.Push();
                RenderState.Color = Color;
                a.Render();
                RenderState.Pop();
            }
            else
                a.Render();
        foreach (var a in movingTiles)
            if (a.Colorable)
            {
                RenderState.Push();
                RenderState.Color = Color;
                a.Render();
                RenderState.Pop();
            }
            else
                a.Render();
    }

    public void Update(double dt)
    {
        foreach (var a in tiles)
            if (a != null)
                a.Update(dt);
        Dirty = false;
    }

    public void Clear()
    {
        tiles = new Tile[tiles.GetLength(0), tiles.GetLength(1)];
        BorderTile t = new BorderTile();
        for (int i = 1; i < tiles.GetLength(1) - 1; i++)
        {
            AddTileInternal(i, 0, t);
            AddTileInternal(i, tiles.GetLength(0) - 1, t);
        }
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            AddTileInternal(0, i, t);
            AddTileInternal(tiles.GetLength(1) - 1, i, t);
        }
        PosTiles = new PosGroup<Tile>(0, 0, GetLength(1) * Tile.Size.X * 2, GetLength(0) * 
            Tile.Size.Y * 2, Tile.Size.X * 2, Tile.Size.Y * 2);

        movingTiles = new Group<Tile>();
    }

    public Group<Tile> GetMovingTiles()
    {
        return movingTiles;
    }

    public static int GetID(int x, int y)
    {
        return y * MaxSize.X + x;
    }

    public static Vec2i GetCoords(int id)
    {
        return new Vec2i(id % MaxSize.X, id / MaxSize.X);
    }

    public Tile GetByID(int id)
    {
        Vec2i coords = GetCoords(id);
        return GetTile(coords.X, coords.Y);
    }

    public static Vec2 GetPosition(Vec2i Coords)
    {
        return new Vec2(Coords.X * Tile.Size.X * 2, Coords.Y * Tile.Size.Y * 2);
    }

    public static Vec2 GetPosition(int id)
    {
        return GetPosition(GetCoords(id));
    }

    public void DeleteTile(int id)
    {
        Vec2i coords = GetCoords(id);
        if (coords.X <= 0 || coords.Y <= 0)
            return;
        if (coords.Y >= tiles.GetLength(0) - 1 || coords.X >= tiles.GetLength(1) - 1)
            return;
        Tile t = tiles[coords.Y, coords.X];
        if (t == null)
            return;
        Dirty = true;
        PosTiles.Remove(t);
        tiles[coords.Y, coords.X] = null;
        movingTiles.Remove(t);
        movingTiles.Refresh();
    }
}