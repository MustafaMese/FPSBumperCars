﻿using UnityEngine;

public abstract class IController : MonoBehaviour
{
    public abstract void Receive(RamDirection ramDir);
}