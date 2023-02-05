using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum Spawner
{
    RightTop,
    RightBottom,
    LeftTop,
    LeftBottom
}

[Serializable]
public class SpawnInfo
{
    public List<EnemyAsset> enemies = new List<EnemyAsset>();
    public Spawner spawner;
}

public class EnemySpawnController : MonoBehaviour, ISpawner
{
    [SerializeField] private EnemyAsset asset;
    [SerializeField, Range(1,10)] private float area;
    
    public Vector2 Position => transform.position;
    public Vector2 GetAreaPosition() => Position + Random.insideUnitCircle * area;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, area);
    }
}