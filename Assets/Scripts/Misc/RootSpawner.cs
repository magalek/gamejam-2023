using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> roots;

    private void Awake()
    {
        foreach (var root in roots)
        {
            root.SetActive(false);
        }

        roots.Reverse();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnCoroutine());
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        foreach (var root in roots)
        {
            root.SetActive(false);
        }
        foreach (var root in roots)
        {
            root.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void FindRoute()
    {
        
    }
}