﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSFrame.ECS
{
    public interface IInitSystem : ISystem
    {
        void Init();
    }
}