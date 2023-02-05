using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour, IMovement
{
    public event Action EndedRoute;
    
    [SerializeField] private TargetingType targetingType;
    [SerializeField] private AudioSource roachwalkSoundEffect;
    
    private int divisionCount = 10;

    private Vector2 destination;
    private ITargetProvider targetProvider;
    private Enemy enemy;
    
    private Queue<Vector2> routeNodes = new();

    private Vector2 currentNode;

    private bool canMove;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        targetProvider = targetingType == TargetingType.Melee ? new MeleeTargetProvider() : new RangedTargetProvider();
    }

    public void Initialize(Vector2 _destination)
    {
        destination = targetProvider.GetTarget(transform.position);
        Debug.DrawLine(transform.position, destination, Color.yellow, 15);

        CalculateRoute(destination, transform.position, routeNodes);
        
        currentNode = routeNodes.Dequeue();
        canMove = currentNode != null;
    }
    
    public void Move()
    {
        if (!canMove) return;
        
        Vector3 direction = currentNode - (Vector2)transform.position;
        direction.Normalize();

        var distance = Vector2.Distance(transform.position, currentNode);
        if (distance > 0.1f) 
        {
            var add = direction * Time.deltaTime * enemy.EnemyAsset.movementSpeed;
            transform.position += add;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            //transform.up = direction;
        }
        else
        {
            if (routeNodes.Count == 0)
            {
                canMove = false;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, (BaseStructure.Instance.transform.position - transform.position).normalized);
                EndedRoute?.Invoke();
                return;
            }
            currentNode = routeNodes.Dequeue();
        }
    }

    private void CalculateRoute(Vector2 a, Vector2 b, Queue<Vector2> queueToPopulate)
    {
        Vector2 path = a - b;
        Vector2 direction = path.normalized;

        float pathPartLength = path.magnitude / divisionCount;

        var headPoint = b;
        
        for (int i = 0; i < divisionCount; i++)
        {
            headPoint += (direction * pathPartLength);

            var point = CalculateNode(headPoint, direction, pathPartLength);
            queueToPopulate.Enqueue(point);
            //yield return new WaitForSeconds(1);
        }
        
    }

    private Vector2 CalculateNode(Vector2 point, Vector2 direction, float maxLength)
    {
        bool rightSide = Random.value > 0.5f;

        float length = Random.Range(0, maxLength);
        Vector2 rightSideDirection = (Vector2.Perpendicular(direction) * length);
        
        return point + (rightSide ? -rightSideDirection : rightSideDirection);
    }
}