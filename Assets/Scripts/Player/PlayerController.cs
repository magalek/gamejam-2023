using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance => instance;

    private static PlayerController instance;
    
    public PlayerMovement Movement;

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        
    }
}
