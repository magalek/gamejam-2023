using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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