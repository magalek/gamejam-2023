using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerDispatcher : MonoBehaviour
{
    [SerializeField] private GameObject RightTop;
    [SerializeField] private GameObject RightBottom;
    [SerializeField] private GameObject LeftTop;
    [SerializeField] private GameObject LeftBottom;

    [SerializeField] private EnemyAsset lightEnemy;
    [SerializeField] private EnemyAsset heavyEnemy;

    [SerializeField, Range(0.1f, 100f)] private float lightDelay;
    [SerializeField, Range(0.1f, 100f)] private float heavyDelay;
    
    [SerializeField, Range(0.001f, 5f)] private float lightDelayIncrement;
    [SerializeField, Range(0.001f, 5f)] private float heavyDelayIncrement;

    private float currentLightDelay;
    private float currentHeavyDelay;
    
    public ISpawner ParseSpawnerEnum(Spawner spawnerEnum)
    {
        switch (spawnerEnum)
        {
            case Spawner.RightTop: return RightTop.GetComponent<ISpawner>();
            case Spawner.RightBottom: return RightBottom.GetComponent<ISpawner>();
            case Spawner.LeftTop: return LeftTop.GetComponent<ISpawner>();
            case Spawner.LeftBottom: return LeftBottom.GetComponent<ISpawner>();
            default: return RightTop.GetComponent<ISpawner>();
        }
    }

    private ISpawner GetRandomSpawner()
    {
        int random = Random.Range(0, 4);
        return ParseSpawnerEnum((Spawner)random);
    }

    private void Awake()
    {
        currentLightDelay = lightDelay;
        currentHeavyDelay = heavyDelay;
        
        StartCoroutine(LightSpawningCoroutine());
        StartCoroutine(HeavySpawningCoroutine());
    }

    private IEnumerator LightSpawningCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Mathf.Clamp(currentLightDelay, 0, int.MaxValue));
            Enemy.Spawn(GetRandomSpawner(), lightEnemy);
            currentLightDelay -= lightDelayIncrement;
        }
    }
    
    private IEnumerator HeavySpawningCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Mathf.Clamp(currentHeavyDelay, 0, int.MaxValue));
            Enemy.Spawn(GetRandomSpawner(), heavyEnemy);
            currentHeavyDelay -= heavyDelayIncrement;
        }
    }
}