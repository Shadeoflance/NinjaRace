﻿using VitPro;
using VitPro.Engine;
using System;
using System.IO;

class Program
{
    public static Random Random = new Random();
    public static MyManager Manager;
    public static Font font = new Font("./Data/font.TTF", 30, FontStyle.Bold);
    public static bool VSync = false;

    static void Main()
    {
        font.Smooth = false;
        Manager = new MyManager(new MainMenu());
        App.Fullscreen = false;
        App.VSync = VSync;
        App.Run(Manager);
    }

    public static Vec2 MousePosition()
    {
        Vec2 pos = Mouse.Position;
        pos += new Vec2(0, -240);
        pos -= new Vec2(320, 0);
        pos /= 2;
        return pos;
    }
}