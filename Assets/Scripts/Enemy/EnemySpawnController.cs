using UnityEngine;

public class EnemySpawnController : MonoBehaviour, ISpawner
{
    [SerializeField] private EnemyAsset asset;
    
    public Vector2 Position => transform.position;

    private void Start()
    {
        Enemy.Spawn(this, asset);
    }
}