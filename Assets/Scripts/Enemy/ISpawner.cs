using UnityEngine;

public interface ISpawner
{
    Vector2 Position { get; }

    Vector2 GetAreaPosition();
}