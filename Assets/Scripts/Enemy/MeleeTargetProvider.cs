using UnityEngine;

public class MeleeTargetProvider : ITargetProvider
{
    public Vector2 GetTarget(Vector2 position) => BaseStructure.Instance.transform.position;
}