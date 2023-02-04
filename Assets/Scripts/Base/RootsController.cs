using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

public class RootsController : MonoBehaviour
{
    [SerializeField] private float rootCoroutineTime;
    [SerializeField] private int maxRoots;

    public static RootsController Instance => instance;

    private static RootsController instance;

    private List<Root> roots = new();

    private Queue<(Root, IActivable)> rootPairs = new();

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
        {
            instance = this;
        }
        
        roots = GetComponentsInChildren<Root>().ToList();

        foreach (var root in roots)
        {
            root.Reset();
        }
    }
    
    public void AttachRoot(Root root, IActivable attachPoint)
    {
        root.Show(rootCoroutineTime);
        rootPairs.Enqueue((root, attachPoint));
        if (rootPairs.Count > maxRoots)
        {
            (Root root, IActivable activable) pair = rootPairs.Dequeue();
            DetachRoot(pair.root);
            pair.activable.ChangeActivationState(false);
        }
    }

    public void DetachRoot(Root root)
    {
        root.Hide(rootCoroutineTime);
    }
}