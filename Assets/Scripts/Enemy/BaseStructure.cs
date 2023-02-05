using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseStructure : MonoBehaviour, IHittable
{
    public event Action<float> HealthChanged;
    public event Action Destroyed;
    
    [SerializeField] private float maxHealth;
    
    public static BaseStructure Instance => instance;
    private static BaseStructure instance;
    
    public float Health { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }

        Health = maxHealth;
    }

    public void TryHit(float damage, Origin origin, AttackType type, out bool hit)
    {
        hit = false;
        if (origin == Origin.Player) return;

        hit = true;
        Health -= damage;
        HealthChanged?.Invoke(Health/maxHealth);
        if (Health <= 0) Kill();
    }

    public void Heal(float hp)
    {
        Health = Mathf.Clamp(Health + hp, 0, maxHealth);
        HealthChanged?.Invoke(Health/maxHealth);
    }

    public void Kill()
    {
        Destroyed?.Invoke();
        var sceneBuildIndex = 2;
        SceneManager.LoadScene(sceneBuildIndex);
        Destroy(transform.parent.gameObject);
    }
}