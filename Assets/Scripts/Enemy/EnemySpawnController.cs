using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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

public class SpawnerDispatcher : MonoBehaviour
{
    public float waveDelay;
    
    [SerializeField] private ISpawner RightTop;
    [SerializeField] private ISpawner RightBottom;
    [SerializeField] private ISpawner LeftTop;
    [SerializeField] private ISpawner LeftBottom;
    
    public ISpawner ParseSpawnerEnum(Spawner spawnerEnum)
    {
        switch (spawnerEnum)
        {
            case Spawner.RightTop: return RightTop;
            case Spawner.RightBottom: return RightBottom;
            case Spawner.LeftTop: return LeftTop;
            case Spawner.LeftBottom: return LeftBottom;
            default: return RightTop;
        }
    }
}

public class EnemySpawnController : MonoBehaviour, ISpawner
{
    [SerializeField] private EnemyAsset asset;
    [SerializeField, Range(1,10)] private float area;
    
    public Vector2 Position => transform.position;

    private void Start()
    {
        Enemy.Spawn(this, asset);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, area);
    }
}