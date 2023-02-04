using System;
using UnityEngine;

public class BaseStructure : MonoBehaviour, IHittable
{
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

    public void TryHit(float damage, Origin origin)
    {
        if (origin == Origin.Player) return;

        Health -= damage;
        if (Health <= 0) Kill();
    }

    public void Kill()
    {
        Destroyed?.Invoke();
        Destroy(gameObject);
    }
}