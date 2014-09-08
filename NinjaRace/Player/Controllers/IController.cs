﻿using VitPro;
using System;
using VitPro.Engine;

interface IController
{
    bool NeedJump();
    Vec2 NeedVel();
    bool NeedAbility();
    void KeyDown(Key key);
    void KeyUp(Key key);
}