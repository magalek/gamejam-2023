public interface IHittable
{
    float Health { get; }
    void TryHit(float damage, Origin origin);
    void Kill();
}