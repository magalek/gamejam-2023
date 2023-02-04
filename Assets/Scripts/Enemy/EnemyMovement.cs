using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour, IMovement
{
    private int divisionCount = 10;

    private Vector2 destination;
    private ISpawner spawner;
    
    private Queue<Vector2> routeNodesPartial = new();
    private Queue<Vector2> routeNodes = new();

    private Vector2 currentNode;

    private bool canMove;

    public void Initialize(Vector2 _destination, ISpawner _spawner)
    {
        destination = _destination;
        spawner = _spawner;

        CalculateRoute(destination, spawner.Position, routeNodes);

        currentNode = routeNodes.Dequeue();
        canMove = currentNode != null;
    }
    
    public void Move()
    {
        if (!canMove) return;
        
        Vector3 direction = currentNode - (Vector2)transform.position;
        direction.Normalize();

        var distance = Vector2.Distance(transform.position, currentNode);
        if (distance > 0.01f) 
        {
            var add = direction * Time.deltaTime;
            transform.position += add;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            //transform.up = direction;
        }
        else
        {
            if (routeNodes.Count == 0)
            {
                canMove = false;
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
            
            queueToPopulate.Enqueue(CalculateNode(headPoint, direction, pathPartLength));
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