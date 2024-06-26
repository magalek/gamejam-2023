﻿using UnityEngine;

public interface IHittable
{
    float Health { get; }
    void TryHit(float damage, Origin origin, AttackType type, out bool hit);
    void Kill();
}