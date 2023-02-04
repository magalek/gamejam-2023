﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Create Enemy", fileName = "Enemy", order = 0)]
public class EnemyAsset : ScriptableObject
{
    public Sprite sprite;
    public Enemy prefab;
    public float health;
}