using UnityEngine;

public interface IProjectileShooter
{
    Vector2 ShotPoint { get; }
    Vector2 ShotDirection { get; }

    Origin ShotOrigin { get; }
}