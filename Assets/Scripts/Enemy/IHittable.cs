public interface IHittable
{
    float Health { get; }
    void TryHit(float damage, Origin origin, out bool hit);
    void Kill();
}