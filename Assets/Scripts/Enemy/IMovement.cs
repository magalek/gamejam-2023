using System;
using UnityEngine;

public interface IMovement
{
    event Action EndedRoute;
    void Initialize(Vector2 _destination);
    void Move();
}