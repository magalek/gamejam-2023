using UnityEngine;

public class RangedTargetProvider : ITargetProvider
{
    public Vector2 GetTarget(Vector2 position)
    {
        var basePosition = BaseStructure.Instance.transform.position;
        var directionFromBase =((Vector3)position -  basePosition).normalized;
        return basePosition + (directionFromBase * 20);
    }
}